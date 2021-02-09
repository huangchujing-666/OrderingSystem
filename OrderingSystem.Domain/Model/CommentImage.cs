using System;
namespace OrderingSystem.Domain.Model
{
	public class CommentImage: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int CommentImageId { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        public virtual int BusinessCommentId { get; set; }
        /// <summary>
        /// 评论图片Id
        /// </summary>
        public virtual int BaseImageId { get; set; }


        public virtual BusinessComment BusinessComment { get; set; }

        public virtual BaseImage BaseImage { get; set; }

    }
}

