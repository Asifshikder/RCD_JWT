using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using RCD.DATA.Entity;
using RCD.DATA.Models;
using RCD.SERVICE.Interface;

namespace RCD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private IWalletService walletService;
        private readonly UserManager<ApplicationUser> userManager;

        public WalletController(IWalletService walletService,UserManager<ApplicationUser> userManager)
        {
            this.walletService = walletService;
            this.userManager = userManager;
        }


        [HttpGet("UserWallet")]
        public IActionResult Index() 
        {
            var usr = userManager.GetUserAsync(HttpContext.User).Result;
            int walletCount = walletService.GetWallets().Where(s => s.UserID == usr.Id).Count();
            if (walletCount == 0)
            {
                WalletInfoResponse wresponse = new WalletInfoResponse();
                wresponse.WalletID = 0;
                wresponse.UserID = usr.Id;
                wresponse.WalletAddress = null;
                wresponse.Balance = null;
                wresponse.Message = "User has no wallet yet";
                return Ok(wresponse);
            }
            else
            {
                var wl = walletService.GetWallets().Where(s => s.UserID == usr.Id).FirstOrDefault();
                WalletInfoResponse wresponse = new WalletInfoResponse();
                wresponse.WalletID = wl.Id;
                wresponse.UserID = wl.UserID;
                wresponse.WalletAddress = wl.Address;
                wresponse.Balance = wl.Balance;
                wresponse.Status = wl.Status;
                return Ok(wresponse);
            }
        }    
        [HttpPost("CreateWallet")]
        public IActionResult Create()
        {
            try
            {
                var usr = userManager.GetUserAsync(HttpContext.User).Result;
                string random = usr.Id.Take(24).ToString();
                string wa = "ree" + random + "bux";
                Wallet wallet = new Wallet();
                wallet.AddDate = DateTime.Now;
                wallet.Address = wa;
                wallet.Balance = 0;
                wallet.UserID = usr.Id;
                walletService.InsertWallet(wallet);
                return Ok(new WalletResponse { Message = "Successfully wallet created", IsSuccess = true });
            }
            catch(Exception ex)
            {
                return Ok(new WalletResponse { Message = "Failed to create wallet", IsSuccess = false });
            }
          
        }
        [HttpPost("DeleteWallet")]

        public IActionResult Delete()
        {
            try
            {
                var usr = userManager.GetUserAsync(HttpContext.User).Result;
                var wallet = walletService.GetWallets().Where(s => s.UserID == usr.Id).FirstOrDefault();
                walletService.DeleteWallet(wallet.Id);
                return Ok(new WalletResponse { Message = "Successfully deleted", IsSuccess = true });
            }
            catch (Exception ex)
            {
                return Ok(new WalletResponse { Message = "Failed to delete", IsSuccess = false });
            }

        }
    }
}
