using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface ICommentImageService
    {
        CommentImage GetById(int id);

        CommentImage Insert(CommentImage model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(CommentImage model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(CommentImage model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<CommentImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据评论id获取评论图片集合
        /// </summary>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        List<CommentImage> GetByBusinessCommentId(int comment_id);
    }
}
