using System;
namespace OrderingSystem.Domain.Model
{
	public class DishesLable: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int DishesLableId { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public virtual string Name { get; set; }
        public virtual int DishesId { get; set; }
        public virtual int BusinessInfoId { get; set; }
         

    }
}

