using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class JourneyArticleDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int journey_article_id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 阅读量
        /// </summary>
        public int reads { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int likes { get; set; }

        /// <summary>
        /// 文章时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 内容纯文本
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 内容富文本
        /// </summary>
        public string content_html { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string path { get; set; }


        public int user_id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 上一篇文章id
        /// </summary>
        public int last_article_id { get; set; }

        /// <summary>
        /// 上篇文章名称
        /// </summary>
        public string last_article_name { get; set; }

        /// <summary>
        /// 下一篇文章id
        /// </summary>
        public int next_article_id { get; set; }

        /// <summary>
        /// 下一篇文章名
        /// </summary>
        public string next_article_name { get; set; }

        public List<RecommandArticle> recommand_article_list;

        /// <summary>
        /// 是否点赞过
        /// </summary>
        public int is_like { get; set; }
    }

    public class RecommandArticle
    {
         public int journey_article_id { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 阅读量
        /// </summary>
        public int reads { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int likes { get; set; }

        /// <summary>
        /// 文章时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string path { get; set; }


        public int user_id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_name { get; set; }
    }
}