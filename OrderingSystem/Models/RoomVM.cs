
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 房间VM
    /// </summary>
    public class RoomVM : BaseImgInfoVM
    {
        public int RefreshFlag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 模型
        /// </summary>
        public Room Room { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<Room> Paging { get; set; }
         
         

        /// <summary>
        /// 商家列表
        /// </summary>
        public List<BusinessInfo> BusinessList { get; set; }
         
        public string QueryName { get; set; } 
        public string QueryBusinessmanName { get; set; }
    }
}