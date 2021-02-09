using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BCUpdPwdDTO
    {
        public int account_id { get; set; }
        public string account { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}