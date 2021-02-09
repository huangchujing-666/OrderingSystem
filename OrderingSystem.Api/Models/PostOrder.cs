using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class PostPay
    {
        public string OrderNo { get; set; }
        public string OpenId { get; set; }
    }
}