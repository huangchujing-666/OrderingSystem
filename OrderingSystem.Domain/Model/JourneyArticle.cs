using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{

    /// <summary>
    /// 游玩攻略表
    /// </summary>
    public class JourneyArticle : IAggregateRoot
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public virtual int JourneyArticleId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
       
        /// <summary>
        /// 产品图片
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public virtual int Reads { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public virtual int Likes { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

         
        public virtual int Status { get; set; }
        public virtual int IsDelete { get; set; }
         

        public virtual BusinessInfo BusinessInfo { get; set; } 

        public virtual User User { get; set; }

        public virtual BaseImage BaseImage { get; set; }

        public virtual int Module { get; set; }

    }
}

