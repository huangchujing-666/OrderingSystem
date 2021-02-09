using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class Goods : IAggregateRoot
    {
        /// <summary>
        /// 衣品主键
        /// </summary>
        public virtual int GoodsId { get; set; }

        /// <summary>
        /// 商家
        /// </summary>
        public virtual BusinessInfo BusinessInfo { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }

        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }

        public virtual int BaseImageId { get; set; }

        /// <summary>
        /// 衣品名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 衣服尺码
        /// </summary>
        public virtual string Size { get; set; }

        /// <summary>
        /// 衣服描述
        /// </summary>
        public virtual string Descript { get; set; }

        /// <summary>
        /// 衣品原价
        /// </summary>
        public virtual decimal OrignPrice { get; set; }

        /// <summary>
        /// 衣品实际价格
        /// </summary>
        public virtual decimal RealPrice { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

        /// <summary>
        /// 衣品图片
        /// </summary>
        public virtual List<GoodsImage> GoodsImageList { get; set; }

        /// <summary>
        /// 衣服种类
        /// </summary>
        public virtual int CategoryId { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl
        {
            get
            {
                if (BaseImage != null)
                {
                    return BaseImage.Source + BaseImage.Path;
                }
                return string.Empty;
            }
        }
    }
}
