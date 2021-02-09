
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 票务VM
    /// </summary>
    public class TicketRelateRoomVM : BaseImgInfoVM
    {
        public int RefreshFlag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        public int TicketId { get; set; }

        public List<BusinessInfo> BusinessInfoList { get; set; }
        public List<Room> RoomList { get; set; }

        /// <summary>
        /// 菜品信息
        /// </summary>
        public TicketRelateRoom TicketRelateRoom { get; set; }
          
        /// <summary>
        /// 菜品分页
        /// </summary>
        public Paging<TicketRelateRoom> Paging { get; set; }
           
    }
}