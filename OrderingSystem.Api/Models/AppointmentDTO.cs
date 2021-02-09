using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class AppointmentDTO
    {
        /// <summary>
        /// 预约主键
        /// </summary>
        public int appointment_id { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string business_name { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int business_id { get; set; }

        /// <summary>
        /// 模块id
        /// </summary>
        public int module { get; set; }

        /// <summary>
        /// 预约到店时间
        /// </summary>
        public string appointment_time { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string phone_no { get; set; }

        /// <summary>
        /// 尺寸备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string deny_reason { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public string apply_status { get; set; }
        /// <summary>
        /// 预约衣品
        /// </summary>
        public List<GoodsDTO> GoodsList { get; set; }
    }
}