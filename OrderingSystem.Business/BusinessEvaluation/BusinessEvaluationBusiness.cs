using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessEvaluationBusiness:IBusinessEvaluationBusiness
    {
        private IRepository<BusinessEvaluation> _repoBusinessEvaluation;

        public BusinessEvaluationBusiness(
          IRepository<BusinessEvaluation> repoBusinessEvaluation
          )
        {
            _repoBusinessEvaluation = repoBusinessEvaluation;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessEvaluation GetById(int id)
        {
            return this._repoBusinessEvaluation.GetById(id);
        }

        public BusinessEvaluation Insert(BusinessEvaluation model)
        {
            return this._repoBusinessEvaluation.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessEvaluation model)
        {
            this._repoBusinessEvaluation.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessEvaluation model)
        {
            this._repoBusinessEvaluation.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessEvaluation> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessEvaluation>();
               

            totalCount = this._repoBusinessEvaluation.Table.Where(where).Count();
            return this._repoBusinessEvaluation.Table.Where(where).OrderBy(p => p.BusinessEvaluationId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
 
        public List<BusinessEvaluation> GetAll()
        {
            return this._repoBusinessEvaluation.Table.ToList();
        }
    }
}


