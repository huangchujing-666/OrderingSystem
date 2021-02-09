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
    public class ActivityMinusService : IActivityMinusService
    {
        /// <summary>
        /// The ActivityMinus biz
        /// </summary>
        private IActivityMinusBusiness _ActivityMinusBiz;

        public ActivityMinusService(IActivityMinusBusiness ActivityMinusBiz)
        {
            _ActivityMinusBiz = ActivityMinusBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityMinus GetById(int id)
        {
            return this._ActivityMinusBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActivityMinus Insert(ActivityMinus model)
        {
            return this._ActivityMinusBiz.Insert(model);
        }
        public ActivityMinus InsertVerifyRepeat(int BusinessInfoId, decimal AchiveAmount) {
            return this._ActivityMinusBiz.InsertVerifyRepeat(BusinessInfoId,AchiveAmount);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ActivityMinus model)
        {
            this._ActivityMinusBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ActivityMinus model)
        {
            this._ActivityMinusBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ActivityMinus> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._ActivityMinusBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
        /// <summary>
        /// 根据商家Id获取满减信息
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>

        public List<ActivityMinus> GetListByBusinessId(int businessId) {
            return this._ActivityMinusBiz.GetListByBusinessId(businessId);
        }
    }
}
