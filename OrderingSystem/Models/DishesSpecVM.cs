
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 菜品标签VM
    /// </summary>
    public class DishesSpecVM : BaseImgInfoVM
    {

        /// <summary>
        /// 商家id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int BusinessInfoId { get; set; }

        public int DishesId { get; set; }

        /// <summary>
        /// 菜品规格ID
        /// </summary>
        public int DishesSpecId { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public DishesSpec DishesSpec { get; set; }
        public DishesSpecDetail DishesSpecDetail { get; set; }

        
        public DishesRelateSpec DishesRelateSpec { get; set; }

        

        public List<DishesSpec> DishesSpecList { get; set; }

        public List<DishesSpecDetail> DishesSpecDetailList { get; set; }

        /// <summary>
        /// 菜品标签
        /// </summary>
        public List<DishesRelateSpec> DishesRelateSpecList { get; set; }

        
    }
}