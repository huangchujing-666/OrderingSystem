using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BCDishesDTO
    {
        public int order_count { get; set; }

        public string total_amount { get; set; }

        public List<BCTopDishesDTO> top_dishesDto_list { get; set; }
    }

    public class BCTopDishesDTO {
        public string name { get; set; }
        public int count { get; set; }

        public string path { get; set; }
    }
}