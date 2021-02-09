using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class JourneyArticleLikeDTO
    {
        /// <summary>
        /// 攻略文章id
        /// </summary>
        public int JourneyArticle_Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int User_Id { get; set; }
    }
}