using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class TicketDTO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int ticket_id { get; set; }

        public string name { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public string orign_price { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public string real_price { get; set; }

        /// <summary>
        /// 规则
        /// </summary>
        public string rules { get; set; }

        /// <summary>
        /// 产品特色
        /// </summary>
        public string special { get; set; }
        /// <summary>
        /// 产品备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 取消说明
        /// </summary>
        public string notice { get; set; }

        /// <summary>
        /// 出行人数
        /// </summary>
        public int use_count { get; set; }

        /// <summary>
        /// 是否绑定身份证
        /// </summary>
        public int bind_card { get; set; }
        /// <summary>
        /// 酒店Id
        /// </summary>
        public int hotel_id { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string hotel_name { get; set; }

        /// <summary>
        /// 酒店评分
        /// </summary>
        public string hotel_rank { get; set; }

        /// <summary>
        /// 酒店距离
        /// </summary>
        public string hotel_distance { get; set; }

        /// <summary>
        /// 酒店图片
        /// </summary>
        public string hotel_img_path { get; set; }
        ///// <summary>
        ///// 房间Id
        ///// </summary>
        //public int room_id { get; set; }
        ///// <summary>
        ///// 房间名称
        ///// </summary>
        //public string room_name { get; set; }

        /// <summary>
        /// 顾客列表
        /// </summary>
        public List<CustomerDTO> customer_list { get; set; }

        public List<RelateRoomDTO> relate_room_list { get; set; }

        public List<RelateTicketDTO> relate_ticket_list { get; set; }

    }

    public class RelateRoomDTO
    {
        public int room_id { get; set; }

        public int count { get; set; }

        public string name { get; set; }

        public string orign_price { get; set; }

        public string real_price { get; set; }

        /// <summary>
        /// 是否有窗
        /// </summary>
        public string room_window { get; set; }

        /// <summary>
        /// 是否含早餐
        /// </summary>
        public string room_breakfast { get; set; }

        /// <summary>
        /// 房间面积
        /// </summary>
        public string room_area { get; set; }

        /// <summary>
        /// 窗
        /// </summary>
        public string room_bed { get; set; }

        public string room_bed_type { get; set; }
    }

    public class RelateTicketDTO
    {
        public int ticket_id { get; set; }

        public int count { get; set; }

        public string name { get; set; }

        public string orign_price { get; set; }

        public string real_price { get; set; }
    }
}