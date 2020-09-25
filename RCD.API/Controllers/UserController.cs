using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RCD.API.Manage;
using Wangkanai.Detection;

namespace RCD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class UserController : ControllerBase
    {
        private IDetection detection;
        private IHttpContextAccessor contextAccessor;

        public UserController(IDetection detection, IHttpContextAccessor contextAccessor)
        {
            this.detection = detection;
            this.contextAccessor = contextAccessor;
        }
        [HttpGet("NetworkInfo")]
        public IActionResult GetInfo()
        {
            string browser_info = detection.Browser.Type.ToString() + detection.Browser.Version;
            var macadd = GetMACAddress();
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            string ip = Convert.ToString(hostEntry.AddressList.FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
            UserInformation information = GetUserInformation(ip);
            information.MacAddress = macadd;
            return Ok(information);
        }
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
           
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        public UserInformation GetUserInformation(string ip)
        {
            UserInformation ipInfo = new UserInformation();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<UserInformation>(info);
            }
            catch (Exception)
            {
                return new UserInformation { };
            }

            return ipInfo;
        }

    }
}
