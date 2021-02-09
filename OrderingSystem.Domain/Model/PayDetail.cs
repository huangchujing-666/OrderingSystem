using System;
namespace OrderingSystem.Domain.Model
{
	public class PayDetail: IAggregateRoot
    {
		/// <summary>
		/// 主键
		/// </summary>
		public virtual int PayDetailId { get; set; }
        /// <summary>
        /// 订单表号
        /// </summary>
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 支付方式 1微信 2支付宝
        /// </summary>
        public virtual int PayType { get; set; }
        /// <summary>
        /// 支付返回序列号
        /// </summary>
        public virtual string PaySerialNo { get; set; }
        /// <summary>
        /// 支付状态  未支付1,已支付2
        /// </summary>
        public virtual int PayStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime OrderTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual DateTime PayTime { get; set; }

        public virtual User User { get; set; }

    }
}

