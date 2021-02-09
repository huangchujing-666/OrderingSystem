
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 菜品标签VM
    /// </summary>
    public class DishesLabelVM : BaseImgInfoVM
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
        /// 菜品信息
        /// </summary>
        public DishesLable DishesLabel { get; set; }
        public DishesRelateLable DishesRelateLable { get; set; }

        

        public List<DishesLable> DishesLabelList { get; set; }

        /// <summary>
        /// 菜品标签
        /// </summary>
        public List<DishesRelateLable> DishesRelateLableList { get; set; }

        
    }
}