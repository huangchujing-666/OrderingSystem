using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class UserLikesBusiness : IUserLikesBusiness
    {
        private IRepository<UserLikes> _repoUserLikes;

        public UserLikesBusiness(
          IRepository<UserLikes> repoUserLikes
          )
        {
            _repoUserLikes = repoUserLikes;
        }

        /// <summary>
        /// 根据用户id  攻略id获取点赞记录
        /// </summary>
        /// <param name="journeyArticle_Id"></param>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        public UserLikes GetByIds(int journeyArticle_Id, int user_Id)
        {
            return this._repoUserLikes.Table.Where(c => c.JourneyArticleId == journeyArticle_Id && c.UserId == user_Id).FirstOrDefault();
        }

        public UserLikes Insert(UserLikes userLikes)
        {
            return this._repoUserLikes.Insert(userLikes);
        }
    }
}
