using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    /// <summary>
    /// 用户点赞表
    /// </summary>
    public class UserLikes : IAggregateRoot
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int UserLikesId { get; set; }

        /// <summary>
        /// 游玩攻略id
        /// </summary>
        public virtual int JourneyArticleId { get; set; }

        /// <summary>
        /// 游玩攻略
        /// </summary>
        public virtual JourneyArticle JourneyArticle { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}
