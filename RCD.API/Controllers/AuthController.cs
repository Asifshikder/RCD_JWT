using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RCD.DATA.Entity;
using RCD.DATA.Models;
using RCD.DATA.Models.ACCVM;
using RCD.SERVICE.Interface;

namespace RCD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private IUserService userService;

        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IConfiguration configuration;

        public AuthController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("Login")]

        public async Task<IActionResult> LoginAsync([FromBody] LoginVM login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return Ok(new UserManagerResponse { Message = "Wrong Creadentials", IsSuccess = false });
            }
            var claims = new[] {
                new Claim("Email" ,login.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSetting:Key"]));
            var token = new JwtSecurityToken
            (
                issuer: configuration["AuthSetting:Audience"],
                audience: configuration["AuthSetting:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterVM model, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            if (model == null)

                throw new NullReferenceException("Model is null");


            if (model.Password != model.PasswordConfirm)

                return Ok(new UserManagerResponse
                {
                    Message = "Password doesn't match",
                    IsSuccess = false,
                });

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Client");

                var mailtoken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //var linkas = Url.PageLink("http://facebook.com", new { userID = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
                var url = $"{configuration["AppUrl"]}/api/auth/VerifyEmail?userId={user.Id}&token={mailtoken}";

                var title = "REEBUX.COM: Email Confimation.";

                var mailbody = "<html>" +
                "<head>" +
                "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">" +
                "</head>" +
                "<body>" +
                "<div class=\"container\">" +
                "<div class=\"row\">" +
                "<div class=\"col-md-12\" style=\"padding:30px;background-color:#d8e9ff\">" +
                "<h3 style=\"color:forestgreen\">Welcome to REEBUX.COM.</h3>" +
                "<div>" +
                    "<p>You have successfully created a account to REEUX.COM.Please confirm you email to continue. </p>" +
                "</div>" +
                "<div>" +
                    "<p>Please visit the link or click the button to confimr your email.</p>" +
                    "<p>" + url + "</p>" +
                "</div>" +
                "<div style=\"text-align:center\">" +
                    "<a href=" + url + " class=\"btn btn-success\">Confirm Email </a>" +
                 "</div>" +
                 "</div>" +
                 "</div>" +
              "</div>" +
              "</ body >" +
              "</html>";
                emailSender.Post(
                   subject: title,
                   body: mailbody,
                   recipients: user.Email,
                   sender: configuration["AdminContact"]);
                return Ok(new UserManagerResponse
                {
                    Message = "Successfully Created",
                    IsSuccess = true,
                });
            }
            return Ok(new UserManagerResponse
            {
                Message = "User is not Created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            });
        }

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new UserManagerResponse { Message = "Successfully Confirmed", IsSuccess = true });
            }
            return BadRequest();
        }


        [HttpPost("ResetPasswordEmail")]
        public async Task<IActionResult> ResetPasswordMailAsync([FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration, LoginVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Ok(new UserManagerResponse { Message = "There is no account assosiated with this email", IsSuccess = false });
            }
            else
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var link = $"{configuration["AppUrl"]}/api/auth/ResetPassword?userId={user.Id}&token={token}";
                //var link = Url.Action(nameof(ResetPassword), "Account", new { userID = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
                emailSender.Post(
                   subject: "REEBUX.COM: Reset Password",
                   body: $"<div><p>Please click on the link to reset your password.</p><br/><p>{link} </p><br/><p> or <button  class=\"btn btn-success\"><a href=\"{link}\">Click Here</a></button></p></div>",
                   recipients: user.Email,
                   sender: configuration["AdminContact"]);
                return Ok(new UserManagerResponse { Message = "Email Sent to the email address", IsSuccess = true });
            }
        }

        [HttpGet("ResetToken")]
        public IActionResult ResetPassword(string userID, string code)
        {
            PasswordVM recover = new PasswordVM();
            recover.UserID = userID;
            recover.BaseCode = code;
            return Ok(recover);
        }

        [HttpPost("PasswordConfirm")]
        public async Task<IActionResult> ResetPasswordConfirm(PasswordVM model)
        {
            var user = await userManager.FindByIdAsync(model.UserID);
            var result = await userManager.ResetPasswordAsync(user, model.BaseCode, model.Password);
            if (result.Succeeded)
            {
                return Ok(new UserManagerResponse { Message = "Successfuly reset password", IsSuccess = true });
            }
            return BadRequest("Somethin went wrong");
        }
        [HttpGet("UserProfile")]
        [Authorize]
        public async Task<IActionResult> Profiles()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return Ok(usr);
        }
        [HttpGet("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            PasswordVM model = new PasswordVM();
            var user = await userManager.GetUserAsync(HttpContext.User);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            model.UserID = user.Id;
            model.Email = user.Email;
            model.BaseCode = token;
            return Ok(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordConfirm(PasswordVM model)
        {
            var user = await userManager.FindByIdAsync(model.UserID);
            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            if (result.Succeeded)
            {
                return Ok(new UserManagerResponse { Message = "Succesfully Change",IsSuccess=true });
            }
            else
            {
                return Ok(new UserManagerResponse { Message = "Something went wrong",IsSuccess=false,Errors= result.Errors.Select(e => e.Description) });

            }
        }

        [HttpGet("ProfileUpdate")]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            ProfileVM model = new ProfileVM();
            model.FullName = usr.FullName;
            model.AvatarUrl = usr.Avatar;
            model.PhoneNumber = usr.PhoneNumber;
            return Ok(model);
        }
        [HttpPost("ProfileUpdateConfirm")]
        [Authorize]
        public async Task<IActionResult> EditProfileConfirm(ProfileVM model)
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            usr.FullName = model.FullName;
            usr.Avatar = model.AvatarUrl;
            usr.PhoneNumber = model.PhoneNumber;
            var result = await userManager.UpdateAsync(usr);
            if (result.Succeeded)
            {
                return Ok(new UserManagerResponse { Message = "Successfully updated", IsSuccess = true });
            }
            return Ok(new UserManagerResponse { Message = "Successfully updated", IsSuccess = false,Errors=result.Errors.Select(s=>s.Description) });
        }
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
            }
            return Ok(new UserManagerResponse { Message = "Logout successfully", IsSuccess = true });
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

    }

}