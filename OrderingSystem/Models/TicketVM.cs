
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 票务VM
    /// </summary>
    public class TicketVM : BaseImgInfoVM
    {
        public int RefreshFlag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public Ticket Ticket { get; set; }
         
        public TicketRelateRoom TicketRelateRoom { get; set; }
        public TicketRelateTicket TicketRelateTicket { get; set; }
        /// <summary>
        /// 菜品分页
        /// </summary>
        public Paging<Ticket> Paging { get; set; }
          
        /// <summary>
        /// 商家列表
        /// </summary>
        public List<BusinessInfo> BusinessList { get; set; }

        /// <summary>
        /// 关联的门票
        /// </summary>
        public List<Ticket> Tickets { get; set; }
        /// <summary>
        /// 关联的酒店房间
        /// </summary>
        public List<Room> Rooms { get; set; }


        public string QueryName { get; set; } 
        public string QueryBusinessmanName { get; set; }
    }
}