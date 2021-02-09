
using System;
namespace OrderingSystem.Domain.Model
{
	public class BusinessType: IAggregateRoot
    {
		/// 
		/// </summary>
		public virtual int BusinessTypeId { get; set; }
        /// <summary>
        /// 商家类型（性质）
        /// </summary>
        public virtual string TypeName { get; set; }

    }
}

