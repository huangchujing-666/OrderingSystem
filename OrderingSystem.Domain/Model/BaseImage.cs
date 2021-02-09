using System;
namespace OrderingSystem.Domain.Model
{
	public class BaseImage: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int BaseImageId { get; set; }
		/// <summary>
		/// 图片域名
		/// </summary>
		public virtual string Source { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public virtual string Path { get; set; }
        /// <summary>
        /// 图片标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}

