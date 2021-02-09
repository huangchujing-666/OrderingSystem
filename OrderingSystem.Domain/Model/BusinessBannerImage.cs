using System;
namespace OrderingSystem.Domain.Model
{
	public class BusinessBannerImage: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int BusinessBannerImageId { get; set; }
		/// <summary>
		/// 商家Id
		/// </summary>
		public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 轮播图片Id
        /// </summary>
        public virtual int BaseImageId{ get; set; }
        /// <summary>
        /// 描述文字
        /// </summary>
        public virtual string Descript { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 图片对象
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }

        /// <summary>
        /// 图片排序
        /// </summary>
        public virtual int SortNo { get; set; }

        /// <summary>
		/// 商家
		/// </summary>
		public virtual BusinessInfo BusinessInfo { get; set; }

        public virtual int Module { get; set; }

    }
}

