using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BCConfirmOrderDTO
    {
        public int Business_Id { get; set; }
        public string Order_No { get; set; }
    }
}