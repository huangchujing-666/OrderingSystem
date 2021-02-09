
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 产品标签VM
    /// </summary>
    public class ProductLabelVM : BaseImgInfoVM
    {

        /// <summary>
        /// 商家id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int BusinessInfoId { get; set; }
        public int ProductId { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public ProductLable ProductLabel { get; set; }
        public ProductRelateLable ProductRelateLable { get; set; }

        

        public List<ProductLable> ProductLabelList { get; set; }

        /// <summary>
        /// 菜品标签
        /// </summary>
        public List<ProductRelateLable> ProductRelateLableList { get; set; }

        
    }
}