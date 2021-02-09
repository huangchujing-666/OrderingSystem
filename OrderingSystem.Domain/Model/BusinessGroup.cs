using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
    /// <summary>
    /// 商家分组表
    /// </summary>
	public class BusinessGroup : IAggregateRoot
    {
        /// <summary>
        /// 商家分组id
        /// </summary>
        public virtual int BusinessGroupId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 系统图片表Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 业务模块类型（食衣乐，酒店，景点）
        /// </summary>
        public virtual int BusinessTypeId { get; set; }
        public string BusinessTypeName
        {
            get
            {
                if (this.BusinessTypeId == 1)
                {
                    return "食模块";
                }
                else if (this.BusinessTypeId == 2)
                {
                    return "衣模块";
                }
                else if (this.BusinessTypeId == 3)
                {
                    return "乐模块";
                }
                else if (this.BusinessTypeId == 4)
                {
                    return "酒店";
                }
                else if (this.BusinessTypeId == 5)
                {
                    return "景点";
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

        public virtual List<BusinessGroupImage> BusinessGroupImageList { get; set; }
     
    }
}

