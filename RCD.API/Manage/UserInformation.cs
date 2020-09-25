using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCD.API.Manage
{
    public class UserInformation
    {
        public string Ip { get; set; }

        public string Hostname { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string Loc { get; set; }

        public string Org { get; set; }

        public string Postal { get; set; }
        public string MacAddress { get; set; }
    }
}
