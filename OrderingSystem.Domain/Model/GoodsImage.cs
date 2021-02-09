using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class GoodsImage:IAggregateRoot
    {
        /// <summary>
        /// 图片id
        /// </summary>
        public virtual int GoodsImageId { get; set; }

        /// <summary>
        /// 衣品
        /// </summary>
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// 衣品问题
        /// </summary>
        public virtual int  GoodsId { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        public virtual int BaseImageId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

        /// <summary>
        /// 编辑人id
        /// </summary>
        public virtual int EditPersonId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        public virtual int CreatePersonId { get; set; }
    }
}
