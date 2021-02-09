using System;
namespace OrderingSystem.Domain.Model
{
	public class OrderStatus: IAggregateRoot
    {
		/// 
		/// </summary>
		public virtual int OrderStatusId { get; set; }
        /// <summary>
        /// 订单状态(未付款1,已付款2,已取消3,退款中4,已退款5,退款失败6,已完结7,已结算8)
        /// </summary>
        public virtual string Name { get; set; }

    }
}

