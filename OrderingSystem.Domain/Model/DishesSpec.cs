using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
	public class DishesSpec: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int DishesSpecId { get; set; }
        /// <summary>
        /// 规格名称
        /// </summary>
        public virtual string Name { get; set; }


        public virtual int DishesId { get; set; }
        public virtual int BusinessInfoId { get; set; }

        public virtual List<DishesSpecDetail> DishesSpecDetailList { get; set; }
    }
}

