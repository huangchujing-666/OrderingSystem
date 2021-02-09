using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Controllers
{
    public class SelectBusinessInfoByManyCondDTO
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int Module { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public int[] Price { get; set; }

        /// <summary>
        /// 星级数组
        /// </summary>
        public int[] Grade { get; set; }

        /// <summary>
        /// 酒店标签
        /// </summary>
        public int[] HotelCategoryId { get; set; }

        public int Page_Index { get; set; }

        public int Page_Size { get; set; }
    }
}