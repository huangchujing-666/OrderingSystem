using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class DelBusinessCommentDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int User_Id { get; set; }
        //商家评论id
        public int Business_Comment_Id { get; set; }
    }
}