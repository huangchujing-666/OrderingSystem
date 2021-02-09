using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
    
    /// <summary>
    /// 经典票务表
    /// </summary>
    public class Ticket : IAggregateRoot
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public virtual int TicketId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        ///// <summary>
        ///// 关联景点门票
        ///// </summary>
        //public virtual int RelatedTicketId { get; set; }
        ///// <summary>
        ///// 关联入驻酒店
        ///// </summary>
        //public virtual int RelatedTicketHotelId { get; set; }
        ///// <summary>
        ///// 关联入驻房间
        ///// </summary>
        //public virtual int RelatedTicketRoomId { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal OrignPrice { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public virtual decimal RealPrice { get; set; }
        /// <summary>
        /// 产品特色
        /// </summary>
        public virtual string Special { get; set; }
        /// <summary>
        /// 产品备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 取消说明
        /// </summary>
        public virtual string Notice { get; set; }
        /// <summary>
        /// 使用说明
        /// </summary>
        public virtual string Rules { get; set; }

        /// <summary>
        /// 出行人数
        /// </summary>
        public virtual int UseCount { get; set; }

        /// <summary>
        /// 是否绑定身份证
        /// </summary>
        public virtual int BindCard { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

         
        public virtual int Status { get; set; }
        public virtual int IsDelete { get; set; }
         

        public virtual BusinessInfo BusinessInfo { get; set; } 

        public virtual List<TicketRelateRoom> TicketRelateRoom { get; set; }
        public virtual List<TicketRelateTicket> TicketRelateTicket { get; set; }
    }
}

