using System;
namespace OrderingSystem.Domain.Model
{
	public class DishesRelateSpec: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int DishesRelateSpecId { get; set; }
        /// <summary>
        /// 菜品Id
        /// </summary>
        public virtual int DishesId { get; set; }
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 菜品规格表Id
        /// </summary>
        public virtual int DishesSpecId { get; set; }

        public virtual DishesSpec DishesSpec { get; set; }
        public virtual Dishes Dishes { get; set; }
    }
}

