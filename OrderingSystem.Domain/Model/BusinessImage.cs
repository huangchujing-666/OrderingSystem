using System;
namespace OrderingSystem.Domain.Model
{
	public class BusinessImage: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int BusinessImageId { get; set; }
        /// <summary>
        /// 商家Id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 系统图片表Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 图片类型（1轮播2VR实景）
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 图片排序
        /// </summary>
        public virtual int SortNo { get; set; }

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

        public virtual BusinessInfo BusinessInfo { get; set; } 

    }
}

