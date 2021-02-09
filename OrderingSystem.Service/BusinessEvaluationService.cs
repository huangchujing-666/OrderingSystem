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
    public class BusinessEvaluationService: IBusinessEvaluationService
    {
        /// <summary>
        /// The BusinessEvaluation biz
        /// </summary>
        private IBusinessEvaluationBusiness _BusinessEvaluationBiz;

        public BusinessEvaluationService(IBusinessEvaluationBusiness BusinessEvaluationBiz)
        {
            _BusinessEvaluationBiz = BusinessEvaluationBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessEvaluation GetById(int id)
        {
            return this._BusinessEvaluationBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessEvaluation Insert(BusinessEvaluation model)
        {
            return this._BusinessEvaluationBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessEvaluation model)
        {
            this._BusinessEvaluationBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessEvaluation model)
        {
            this._BusinessEvaluationBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessEvaluation> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessEvaluationBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<BusinessEvaluation> GetAll()
        {
            return this._BusinessEvaluationBiz.GetAll();
        }
    }
}
