using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class PostAppointment
    {
       /// <summary>
       /// 商家id
       /// </summary>
        public int business_id { get; set; }

        /// <summary>
        /// 衣品id
        /// </summary>
        public string value_ids { get; set; }

        /// <summary>
        /// 模块id
        /// </summary>
        public int module { get; set; }

        /// <summary>
        /// 预约到店时间
        /// </summary>
        public DateTime appointment_time { get; set; }

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



    }
}