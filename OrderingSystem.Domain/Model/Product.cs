using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
    
    /// <summary>
    /// 乐模块产品表
    /// </summary>
    public class Product : IAggregateRoot
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public virtual int ProductId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public virtual string Descript { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal OrignPrice { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public virtual decimal RealPrice { get; set; }
        /// <summary>
        /// 产品详情
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 产品备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 有效期开始
        /// </summary>
        public virtual DateTime StartDate { get; set; }
        /// <summary>
        /// 有效期结束
        /// </summary>
        public virtual DateTime EndDate { get; set; }
        /// <summary>
        /// 购买须知（有效期+）
        /// </summary>
        public virtual string Notice { get; set; }
        /// <summary>
        /// 使用规则
        /// </summary>
        public virtual string Rules { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }


        public virtual int UseDateLimit { get; set; }
        public virtual int Status { get; set; }
        public virtual int IsDelete { get; set; }

        public virtual int CreatePersonId { get; set; }
        public virtual int EditPersonId { get; set; } 

        public virtual BusinessInfo BusinessInfo { get; set; }
          

        public virtual List<ProductImage> ProductImageList { get; set; }

        public virtual List<ProductLable> ProductLableList { get; set; } 

        public virtual List<ProductRelateLable> ProductRelateLableList { get; set; }
    }
}

