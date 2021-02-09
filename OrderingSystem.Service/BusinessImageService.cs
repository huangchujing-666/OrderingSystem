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
    public class BusinessImageService: IBusinessImageService
    {
        /// <summary>
        /// The BusinessImage biz
        /// </summary>
        private IBusinessImageBusiness _BusinessImageBiz;

        public BusinessImageService(IBusinessImageBusiness BusinessImageBiz)
        {
            _BusinessImageBiz = BusinessImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessImage GetById(int id)
        {
            return this._BusinessImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessImage Insert(BusinessImage model)
        {
            return this._BusinessImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessImage model)
        {
            this._BusinessImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessImage model)
        {
            this._BusinessImageBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessImage> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessImageBiz.GetManagerList(businessId, pageNum, pageSize, out totalCount);
        }

        public List<BusinessImage> GetByBusinessInfoId(int businessInfoId,int type) {
            return this._BusinessImageBiz.GetByBusinessInfoId(businessInfoId,type);
        }
    }
}
