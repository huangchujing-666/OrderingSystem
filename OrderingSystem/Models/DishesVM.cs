
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 菜品VM
    /// </summary>
    public class DishesVM : BaseImgInfoVM
    {
        public int RefreshFlag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public Dishes Dishes { get; set; }
        /// <summary>
        /// 菜品分页
        /// </summary>
        public Paging<Dishes> Paging { get; set; }

        public List<DishesCategory> DishesCategoryList { get; set; }

       /// <summary>
       /// 菜品标签
       /// </summary>
        public List<DishesRelateLable> DishesRelateLableList { get; set; }

        /// <summary>
        /// 商家列表
        /// </summary>
        public List<BusinessInfo> BusinessList { get; set; }
         
        public string QueryName { get; set; } 
        public string QueryBusinessmanName { get; set; }
    }
}