using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 商家评论视图类
    /// </summary>
    public class BusinessCommentVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int BusinessCommentId { get; set; }
        public int UserId { get; set; }

        public int BusinessInfoId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 评论信息
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelId { get; set; }

        public int IsAnonymous { get; set; }

        public List<Level> Levels { get; set; }

        public BusinessComment BusinessComment { get; set; }

        public Paging<BusinessComment> Paging { get; set; }

        public string QueryName { get; set; }
    }
}