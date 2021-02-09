using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public int CardType { get; set; }


        public string CardNo { get; set; }

        public string Mobile { get; set; }
    }
}