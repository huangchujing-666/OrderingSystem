using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 房间实体对象
    /// </summary>
    public class RoomDTO
    {
        public int room_id { get; set; }
        public string name { get; set; }
        public string window { get; set; }

        public string breakfast { get; set; }

        public string area { get; set; }

        public string internet { get; set; }

        public string bed { get; set; }
        public string bathroom { get; set; }
        public string floor { get; set; }

        public string airConditioner { get; set; }

        public string notice { get; set; }

        public string rules { get; set; }

        public string remain { get; set; }

        public string orignPrice { get; set; }
        public string realPrice { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string phone_no { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string customer_name { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 床型
        /// </summary>
        public string bed_type { get; set; }

        public List<BaseImageDTO> room_image_list { get; set; }
    }
}