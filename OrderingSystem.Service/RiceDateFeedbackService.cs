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
    public class RiceDateFeedbackService: IRiceDateFeedbackService
    {
        /// <summary>
        /// The RiceDateFeedback biz
        /// </summary>
        private IRiceDateFeedbackBusiness _RiceDateFeedbackBiz;

        public RiceDateFeedbackService(IRiceDateFeedbackBusiness RiceDateFeedbackBiz)
        {
            _RiceDateFeedbackBiz = RiceDateFeedbackBiz;
        }


        public List<RiceDateFeedback> GetList()
        {
            return this._RiceDateFeedbackBiz.GetList();
        }
        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RiceDateFeedback GetById(int id)
        {
            return this._RiceDateFeedbackBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RiceDateFeedback Insert(RiceDateFeedback model)
        {
            return this._RiceDateFeedbackBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RiceDateFeedback model)
        {
            this._RiceDateFeedbackBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RiceDateFeedback model)
        {
            this._RiceDateFeedbackBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RiceDateFeedback> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._RiceDateFeedbackBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取用户被投诉集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RiceDateFeedback> GetByUserId(int userId)
        {
            return this._RiceDateFeedbackBiz.GetByUserId(userId);
        }

        /// <summary>
        /// 用户id  约饭id获取投诉信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        public RiceDateFeedback GetByUserIdAndRiceDateId(int userId, int riceDateId)
        {
            return this._RiceDateFeedbackBiz.GetByUserIdAndRiceDateId(userId, riceDateId);
        }
    }
}
