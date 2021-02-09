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
    public class BusinessPayConfigService: IBusinessPayConfigService
    {
        /// <summary>
        /// The BusinessPayConfig biz
        /// </summary>
        private IBusinessPayConfigBusiness _BusinessPayConfigBiz;

        public BusinessPayConfigService(IBusinessPayConfigBusiness BusinessPayConfigBiz)
        {
            _BusinessPayConfigBiz = BusinessPayConfigBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessPayConfig GetById(int id)
        {
            return this._BusinessPayConfigBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessPayConfig Insert(BusinessPayConfig model)
        {
            return this._BusinessPayConfigBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessPayConfig model)
        {
            this._BusinessPayConfigBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessPayConfig model)
        {
            this._BusinessPayConfigBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessPayConfig> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessPayConfigBiz.GetManagerList(businessId, pageNum, pageSize, out totalCount);
        }

        
    }
}
