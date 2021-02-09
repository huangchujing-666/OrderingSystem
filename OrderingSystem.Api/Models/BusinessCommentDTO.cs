using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BusinessCommentDTO
    {
        public List<CommentDTO> comment_list { get; set; }


    }

    public class CommentDTO
    {

        public int comment_id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// 用户name
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 用户图片id
        /// </summary>
        public int user_img_id { get; set; }
        /// <summary>
        /// 用户图片路径
        /// </summary>
        public string user_img_path { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 等级id
        /// </summary>
        public int level_id { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public string level_name { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public int is_anonymous { get; set; }
        /// <summary>
        /// 评论日期
        /// </summary>
        public string create_date { get; set; }
        /// <summary>
        /// 评论所属订单里面的菜品名称
        /// </summary>
        public string order_dishes_list { get; set; }
        /// <summary>
        /// 评论上传的图片列表
        /// </summary>
        public List<BaseImageDTO> imges_list { get; set; }



        #region 乐模块新增字段
        public int business_id { get; set; }
        public string business_name { get; set; }

        /// <summary>
        /// 商家图片路径
        /// </summary>
        public string business_image_path { get; set; }
        #endregion

    }
}