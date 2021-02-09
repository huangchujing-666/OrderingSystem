using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Business
{
    public interface IUserLikesBusiness
    {
        /// <summary>
        /// 根据用户id  攻略id获取点赞记录
        /// </summary>
        /// <param name="journeyArticle_Id"></param>
        /// <param name="user_Id"></param>
        UserLikes GetByIds(int journeyArticle_Id, int user_Id);
        UserLikes Insert(UserLikes userLikes);
    }
}
