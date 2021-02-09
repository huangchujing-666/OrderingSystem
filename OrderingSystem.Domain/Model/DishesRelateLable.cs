using System;
namespace OrderingSystem.Domain.Model
{
	public class DishesRelateLable: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int DishesRelateLableId { get; set; }
        /// <summary>
        /// 菜品Id
        /// </summary>
        public virtual int DishesId { get; set; }
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 菜品标签表Id
        /// </summary>
        public virtual int DishesLableId { get; set; }

        public virtual DishesLable DishesLable { get; set; }
        public virtual Dishes Dishes { get; set; }

    }
}

