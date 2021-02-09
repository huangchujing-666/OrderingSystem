using System;
namespace OrderingSystem.Domain.Model
{
	public class RecommendDishes: IAggregateRoot
    {
		/// <summary>
		/// 主键
		/// </summary>
		public virtual int RecommendDishesId { get; set; }
        /// <summary>
        /// 商家评论表Id
        /// </summary>
        public virtual int BusinessCommentId { get; set; }
        /// <summary>
        /// 菜品Id
        /// </summary>
        public virtual int DishesId { get; set; }

    }
}

