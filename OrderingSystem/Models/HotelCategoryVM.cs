
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 酒店分类VM
    /// </summary>
    public class HotelCategoryVM
    {

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        public int BusinessInfoId { get; set; }
        /// <summary>
        /// 酒店信息
        /// </summary>
        public HotelCategory HotelCategory { get; set; }

        public Paging<HotelCategory> Paging { get; set; }

         
        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }
 

    }
}