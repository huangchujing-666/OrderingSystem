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
    public class ActivityDiscountService: IActivityDiscountService
    {
        /// <summary>
        /// The ActivityDiscount biz
        /// </summary>
        private IActivityDiscountBusiness _ActivityDiscountBiz;

        public ActivityDiscountService(IActivityDiscountBusiness ActivityDiscountBiz)
        {
            _ActivityDiscountBiz = ActivityDiscountBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityDiscount GetById(int id)
        {
            return this._ActivityDiscountBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActivityDiscount Insert(ActivityDiscount model)
        {
            return this._ActivityDiscountBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ActivityDiscount model)
        {
            this._ActivityDiscountBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ActivityDiscount model)
        {
            this._ActivityDiscountBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ActivityDiscount> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._ActivityDiscountBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
        /// <summary>
        /// 根据商家ID获取折扣信息
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        //public ActivityDiscount GetByBusinessId(int businessId) {
        //    return this._ActivityDiscountBiz.GetByBusinessId(businessId);
        //}
    }
}
