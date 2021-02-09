using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class CommentImageBusiness : ICommentImageBusiness
    {
        private IRepository<CommentImage> _repoCommentImage;

        public CommentImageBusiness(
          IRepository<CommentImage> repoCommentImage
          )
        {
            _repoCommentImage = repoCommentImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentImage GetById(int id)
        {
            return this._repoCommentImage.GetById(id);
        }

        public CommentImage Insert(CommentImage model)
        {
            return this._repoCommentImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(CommentImage model)
        {
            this._repoCommentImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(CommentImage model)
        {
            this._repoCommentImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<CommentImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<CommentImage>();


            totalCount = this._repoCommentImage.Table.Where(where).Count();
            return this._repoCommentImage.Table.Where(where).OrderBy(p => p.CommentImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据评论id获取评论图片集合
        /// </summary>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        public List<CommentImage> GetByBusinessCommentId(int comment_id)
        {
            var result = new List<CommentImage>();
            if (comment_id>0)
            {
                var where = PredicateBuilder.True<CommentImage>();
                result= this._repoCommentImage.Table.Where(c => c.BusinessCommentId == comment_id).OrderBy(c => c.CommentImageId).ToList();
            }
            return result;
        }

    }
}


