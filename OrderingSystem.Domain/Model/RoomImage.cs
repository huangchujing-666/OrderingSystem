using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class RoomImage: IAggregateRoot
    {
        /// <summary>
		/// 
		/// </summary>
		public virtual int RoomImageId { get; set; }
        /// <summary>
        /// 房间Id
        /// </summary>
        public virtual int RoomId { get; set; }
        /// <summary>
        /// 系统图片表Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 图片类型（1大图）
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 状态（0为无效 1为有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 是否删除（0为否 1为是）
        /// </summary>
        public virtual int IsDelete { get; set; }

        public virtual int CreatePersonId { get; set; }
        public virtual int EditPersonId { get; set; }
        public virtual DateTime EditTime { get; set; }
        public virtual DateTime CreateTime { get; set; }

        public virtual BaseImage BaseImage { get; set; }

        public virtual Room Room { get; set; }
    }
}
