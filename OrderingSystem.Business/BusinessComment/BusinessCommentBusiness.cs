using OrderingSystem.Core.Data;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessCommentBusiness : IBusinessCommentBusiness
    {
        private IRepository<BusinessComment> _repoBusinessComment;

        public BusinessCommentBusiness(
          IRepository<BusinessComment> repoBusinessComment
          )
        {
            _repoBusinessComment = repoBusinessComment;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessComment GetById(int id)
        {
            return this._repoBusinessComment.Table.Where(c => c.BusinessCommentId == id).FirstOrDefault();
        }

        public BusinessComment Insert(BusinessComment model)
        {
            return this._repoBusinessComment.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessComment model)
        {
            this._repoBusinessComment.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessComment model)
        {
            this._repoBusinessComment.Delete(model);
        }

        /// <summary>
        /// 判断用户是否已经对订单评论了
        /// </summary>
        /// <param name="businesssId"></param>
        /// <param name="userInfoId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool IsExist(int businesssId, int userInfoId, string orderNo)
        {
            return this._repoBusinessComment.Table.Any(p => p.BusinessInfoId == businesssId && p.UserId == userInfoId && p.Order.OrderNo == orderNo);
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessComment> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessComment>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(m => m.Contents.Contains(name));
            }
            where = where.And(m => m.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);
            totalCount = this._repoBusinessComment.Table.Where(where).Count();
            return this._repoBusinessComment.Table.Where(where).OrderBy(p => p.BusinessInfoId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="levelId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        public List<BusinessComment> GetPageList(int businessId, int levelId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessComment>();

            if (businessId != 0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }
            if (levelId != 0)
            {
                where = where.And(m => m.LevelId == levelId);
            }
            where = where.And(m => m.Status == 1 && m.IsDelete == 0);
            totalCount = this._repoBusinessComment.Table.Where(where).Count();
            return this._repoBusinessComment.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据用户Id获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="levelId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BusinessComment> GetPageListByUserId(int userId, int module, int levelId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessComment>();

            if (userId != 0)
            {
                where = where.And(m => m.UserId == userId);
            }
            if (levelId != 0)
            {
                where = where.And(m => m.LevelId == levelId);
            }
            if (module != 0)
            {
                if (module == (int)EnumHelp.BusinessTypeEnum.娱乐模块)
                {
                    where = where.And(m => (new int[] { 3, 4, 5 }).Contains(m.BusinessInfo.BusinessTypeId));
                }
                else
                {
                    where = where.And(m => m.BusinessInfo.BusinessTypeId == module);
                }
            }
            where = where.And(m => m.Status == 1 && m.IsDelete == 0);
            totalCount = this._repoBusinessComment.Table.Where(where).Count();
            return this._repoBusinessComment.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}


