
using System;
namespace OrderingSystem.Domain.Model
{
    /// <summary>
    /// 订单关联用户信息表
    /// </summary>
	public class OrderCustomer: IAggregateRoot
    { 
		public virtual int OrderCustomerId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public virtual int OrderId { get; set; }

        /// <summary>
        /// 关联订单信息
        /// </summary>
        public virtual Order Order { get; set; }

        public virtual int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

    }
}

