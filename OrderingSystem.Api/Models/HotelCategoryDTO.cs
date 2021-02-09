using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 酒店分类
    /// </summary>
    public class HotelCategoryDTO
    {
        public int HotelCategoryId { get; set; }

        public string Name { get; set; }
    }
}