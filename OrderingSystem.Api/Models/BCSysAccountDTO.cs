using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BCSysAccountDTO
    {

        public string token_str { get; set; }
        public int sys_business_account_id { get; set; }

        public string account { get; set; }

        public string phone_no { get; set; }

        public string nick_name { get; set; }

        public string path { get; set; }

        public string last_loin_time { get; set; }
    }
}