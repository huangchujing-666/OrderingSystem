
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 票务VM
    /// </summary>
    public class TicketRelateTicketVM : BaseImgInfoVM
    {
        public int RefreshFlag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        public int TicketId { get; set; }

        public List<Ticket> TicketList { get; set; }

        /// <summary>
        /// 菜品信息
        /// </summary>
        public TicketRelateTicket TicketRelateTicket { get; set; }
          
        /// <summary>
        /// 菜品分页
        /// </summary>
        public Paging<TicketRelateTicket> Paging { get; set; }
           
    }
}