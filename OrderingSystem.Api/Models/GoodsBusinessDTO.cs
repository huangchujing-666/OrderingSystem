using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class GoodsBusinessDTO
    {
        public int business_group_id { get; set; }

        public string name { get; set;}

        public List<BusinessDTO> businesslist { get; set; }
    }
}