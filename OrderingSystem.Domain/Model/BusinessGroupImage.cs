using System;
namespace OrderingSystem.Domain.Model
{
	public class BusinessGroupImage: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int BusinessGroupImageId { get; set; }
        /// <summary>
        /// 商家分组Id
        /// </summary>
        public virtual int BusinessGroupId { get; set; }
        /// <summary>
        /// 系统图片表Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 图片类型（
        /// </summary>
        public virtual int Type { get; set; } 
        public virtual DateTime EditTime { get; set; }
        public virtual DateTime CreateTime { get; set; }

        public virtual BaseImage BaseImage { get; set; }

        public virtual BusinessGroup BusinessGroup { get; set; } 

    }
}

