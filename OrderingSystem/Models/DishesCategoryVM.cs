
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 菜品分类VM
    /// </summary>
    public class DishesCategoryVM
    {

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        public int BusinessInfoId { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public DishesCategory DishesCategory { get; set; }

        public Paging<DishesCategory> Paging { get; set; }

         
        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }
 

    }
}