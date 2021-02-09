using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
	public class Order: IAggregateRoot
    {
		/// <summary>
		/// 主键
		/// </summary>
		public virtual int OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 商家Id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 订单原金额
        /// </summary>
        public virtual decimal OrignAmount { get; set; }

        /// <summary>
        /// 实际价格减少金额
        /// </summary>
        public virtual decimal MinusAmount { get; set; }
        /// <summary>
        /// 订单优惠后金额
        /// </summary>
        public virtual decimal RealAmount { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 订单状态表Id
        /// </summary>
        public virtual int OrderStatusId { get; set; }
        /// <summary>
        /// i下单时间
        /// </summary>
        public virtual DateTime OrderTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual DateTime PayTime { get; set; }
        /// <summary>
        /// 折扣优惠备注
        /// </summary>
        public virtual string DiscountRemark { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public virtual int ActivityType { get; set; }

        /// <summary>
        /// 关联评论
        /// </summary>
        public virtual List<BusinessComment> BusinessCommentList { get; set; }
        /// <summary>
        /// 关联用户信息
        /// </summary>
        public virtual List<OrderCustomer> OrderCustomerList { get; set; }

        /// <summary>
        /// 商家信息
        /// </summary>
        public virtual BusinessInfo BusinessInfo { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 订单状态表
        /// </summary>
        public virtual OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 座位号
        /// </summary>
        public virtual int SeatNo { get; set; }

        /// <summary>
        /// 订单使用开始日期
        /// </summary>
        public virtual DateTime UseFromDate { get; set; }

        /// <summary>
        /// 订单使用结束日期
        /// </summary>
        public virtual DateTime UseEndDate { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public virtual int Count { get; set; }

        /// <summary>
        /// 子订单详情
        /// </summary>
        public virtual List<OrderDetail> OrderDetailList { get; set; }

    }
}

