using System;
namespace OrderingSystem.Domain.Model
{
	public class DishesCategory: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int DishesCategoryId { get; set; }
        /// <summary>
        /// 商家ID
        /// </summary>
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 菜品类别名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}

