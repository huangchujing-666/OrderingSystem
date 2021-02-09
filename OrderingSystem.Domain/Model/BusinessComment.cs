using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
	public class BusinessComment: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int BusinessCommentId { get; set; }
        /// <summary>
        /// 商家Id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }

        /// <summary>
        /// 商家信息
        /// </summary>
        public virtual BusinessInfo BusinessInfo { get; set; }

        /// <summary>
        /// 关联订单Id
        /// </summary>
        public virtual int OrderId { get; set; }

        /// <summary>
        /// 关联订单信息
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 用户评价
        /// </summary>
        public virtual string Contents { get; set; }
        /// <summary>
        /// 评价等级
        /// </summary>
        public virtual int LevelId { get; set; }
        /// <summary>
        /// 是否匿名（0否1是）
        /// </summary>
        public virtual int IsAnonymous { get; set; }
        /// <summary>
        /// 评价时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public virtual int Status { get; set; }
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 菜品名称字符串
        /// </summary>
        public virtual string RecommendDishes { get; set; }

        public virtual Level Level { get; set; }
        public virtual User User { get; set; }

        public virtual List<CommentImage> CommentImageList { get; set; }
    }
}

