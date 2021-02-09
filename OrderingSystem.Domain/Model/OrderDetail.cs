
using System;
namespace OrderingSystem.Domain.Model
{
	public class OrderDetail: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int OrderDetailId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 菜品Id
        /// </summary>
        public virtual int DishesId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual int ProductId { get; set; }
        /// <summary>
        /// 原金额
        /// </summary>
        public virtual decimal OrignAmount { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public virtual decimal RealAmount { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime OrderTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Count { get; set; }

        /// <summary>
        /// 规格详情ID 用逗号分隔
        /// </summary>
        public virtual string DishesSpecDetailIds { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public virtual Dishes Dishes { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        public virtual int RoomId { get; set; }

        /// <summary>
        /// 房间
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }

        /// <summary>
        /// 景点票务ID
        /// </summary>
        public virtual int TicketId { get; set; }
        /// <summary>
        /// 景点票务
        /// </summary>
        public virtual Ticket Ticket { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual int OrderId { get; set; }
        /// <summary>
        /// 订单对象
        /// </summary>
        public virtual Order Order { get; set; }
    }
}

