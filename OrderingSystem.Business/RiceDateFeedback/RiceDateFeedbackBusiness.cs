using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class RiceDateFeedbackBusiness : IRiceDateFeedbackBusiness
    {
        private IRepository<RiceDateFeedback> _repoRiceDateFeedback;

        public RiceDateFeedbackBusiness(
          IRepository<RiceDateFeedback> repoRiceDateFeedback
          )
        {
            _repoRiceDateFeedback = repoRiceDateFeedback;
        }
        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RiceDateFeedback GetById(int id)
        {
            return this._repoRiceDateFeedback.GetById(id);
        }

        public RiceDateFeedback Insert(RiceDateFeedback model)
        {
            return this._repoRiceDateFeedback.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RiceDateFeedback model)
        {
            this._repoRiceDateFeedback.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RiceDateFeedback model)
        {
            this._repoRiceDateFeedback.Delete(model);
        }
        /// <summary>
        /// 管理后台列表
        /// </summary> 
        /// <returns></returns>
        public List<RiceDateFeedback> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<RiceDateFeedback>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Content.Contains(name));
            }

            totalCount = this._repoRiceDateFeedback.Table.Where(where).Count();
            return this._repoRiceDateFeedback.Table.Where(where).OrderBy(p => p.RiceDateFeedbackId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<RiceDateFeedback> GetList()
        {
            return this._repoRiceDateFeedback.Table.OrderBy(c => c.RiceDateFeedbackId).ToList();
        }

        /// <summary>
        /// 获取用户被投诉集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RiceDateFeedback> GetByUserId(int userId)
        {
            return this._repoRiceDateFeedback.Table.Where(c => c.RiceDate.UserId== userId).OrderByDescending(c=>c.CreateTime).ToList();
        }


        /// <summary>
        /// 根据用户id 约饭id获取投诉详情
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        public RiceDateFeedback GetByUserIdAndRiceDateId(int userId, int riceDateId)
        {
            return this._repoRiceDateFeedback.Table.Where(c => c.UserId==userId&&c.RiceDateId== riceDateId).FirstOrDefault();
        }
    }
}


