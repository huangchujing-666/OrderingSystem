using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class CommentImageService : ICommentImageService
    {
        /// <summary>
        /// The CommentImage biz
        /// </summary>
        private ICommentImageBusiness _CommentImageBiz;

        public CommentImageService(ICommentImageBusiness CommentImageBiz)
        {
            _CommentImageBiz = CommentImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentImage GetById(int id)
        {
            return this._CommentImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CommentImage Insert(CommentImage model)
        {
            return this._CommentImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(CommentImage model)
        {
            this._CommentImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(CommentImage model)
        {
            this._CommentImageBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<CommentImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._CommentImageBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据评论id获取评论图片集合
        /// </summary>
        /// <param name="comment_id"></param>
        /// <returns></returns>
        public List<CommentImage> GetByBusinessCommentId(int comment_id)
        {
            return this._CommentImageBiz.GetByBusinessCommentId(comment_id);
        }

    }
}
