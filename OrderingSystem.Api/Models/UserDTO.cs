using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class UserDTO
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 用户图片id
        /// </summary>
        public string user_img_id { get; set; }
        /// <summary>
        /// 用户图片路径
        /// </summary>
        public string user_img_path { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; }
    }
}