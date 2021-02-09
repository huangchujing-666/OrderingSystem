using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class ProductImage: IAggregateRoot
    {
        /// <summary>
        /// 产品图片Id
        /// </summary>
        public virtual int ProductImageId { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual int ProductId { get; set; }

        /// <summary>
        /// 系统图片表Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 菜品图片类型（1大图）
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 状态（0为无效 1为有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 是否删除（0为否 1为是）
        /// </summary>
        public virtual int IsDelete { get; set; }

        public virtual int CreatePersonId { get; set; }
        public virtual int EditPersonId { get; set; }
        public virtual DateTime EditTime { get; set; }
        public virtual DateTime CreateTime { get; set; }

        public virtual BaseImage BaseImage { get; set; }

        public virtual Product Product { get; set; }
    }
}
