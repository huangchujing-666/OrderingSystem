using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class PostComment
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int business_id { get; set; }

        /// <summary>
        /// 评价id
        /// </summary>
        public int comment_id { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 评价图片
        /// </summary>
        public string imageId { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public int level_id { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public int is_anonymous { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public int order_id { get; set; }
    }
}