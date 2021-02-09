using OrderingSystem.Business;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Service
{
    public class UserLikesService: IUserLikesService
    {
        private IUserLikesBusiness _UserLikesBiz;

        public UserLikesService(IUserLikesBusiness UserLikesBi)
        {
            _UserLikesBiz = UserLikesBi;
        }

        /// <summary>
        /// 根据用户id  攻略id获取点赞记录
        /// </summary>
        /// <param name="journeyArticle_Id"></param>
        /// <param name="user_Id"></param>
        public UserLikes GetByIds(int journeyArticle_Id, int user_Id)
        {
            return this._UserLikesBiz.GetByIds(journeyArticle_Id, user_Id);
        }

        public UserLikes Insert(UserLikes userLikes)
        {
            return this._UserLikesBiz.Insert(userLikes);
        }
    }

}
