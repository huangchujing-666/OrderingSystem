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
    public class BusinessLableService: IBusinessLableService
    {
        /// <summary>
        /// The BusinessLable biz
        /// </summary>
        private IBusinessLableBusiness _BusinessLableBiz;

        public BusinessLableService(IBusinessLableBusiness BusinessLableBiz)
        {
            _BusinessLableBiz = BusinessLableBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessLable GetById(int id)
        {
            return this._BusinessLableBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessLable Insert(BusinessLable model)
        {
            return this._BusinessLableBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessLable model)
        {
            this._BusinessLableBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessLable model)
        {
            this._BusinessLableBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessLable> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessLableBiz.GetManagerList(businessId, pageNum, pageSize, out totalCount);
        }

        public List<BusinessLable> GetAll()
        {
            return this._BusinessLableBiz.GetAll();
        }
    }
}
