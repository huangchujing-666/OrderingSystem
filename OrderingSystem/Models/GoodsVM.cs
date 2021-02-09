
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 衣品VM
    /// </summary>
    public class GoodsVM : BaseImgInfoVM
    {
        public int RefreshFlag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 衣品信息
        /// </summary>
        public Goods Goods { get; set; }
        /// <summary>
        /// 衣品分页
        /// </summary>
        public Paging<Goods> Paging { get; set; }
         

       ///// <summary>
       ///// 衣品标签
       ///// </summary>
       // public List<GoodsRelateLable> GoodsRelateLableList { get; set; }

        /// <summary>
        /// 商家列表
        /// </summary>
        public List<BusinessInfo> BusinessList { get; set; }
         
        public string QueryName { get; set; } 
        public string QueryBusinessmanName { get; set; }
    }
}