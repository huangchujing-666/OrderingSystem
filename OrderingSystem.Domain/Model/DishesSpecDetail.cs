using System;
namespace OrderingSystem.Domain.Model
{
	public class DishesSpecDetail: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int DishesSpecDetailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int DishesSpecId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Descript { get; set; }
        /// <summary>
        /// 原金额
        /// </summary>
        public virtual decimal OrignPrice { get; set; }
        /// <summary>
        /// 现金额
        /// </summary>
        public virtual decimal RealPrice { get; set; }

        public virtual DishesSpec DishesSpec { get; set; }
    }
}

