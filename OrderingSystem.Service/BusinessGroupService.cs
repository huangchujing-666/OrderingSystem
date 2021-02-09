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
    public class BusinessGroupService: IBusinessGroupService
    {
        /// <summary>
        /// The BusinessGroup biz
        /// </summary>
        private IBusinessGroupBusiness _BusinessGroupBiz;

        public BusinessGroupService(IBusinessGroupBusiness BusinessGroupBiz)
        {
            _BusinessGroupBiz = BusinessGroupBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessGroup GetById(int id)
        {
            return this._BusinessGroupBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessGroup Insert(BusinessGroup model)
        {
            return this._BusinessGroupBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessGroup model)
        {
            this._BusinessGroupBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessGroup model)
        {
            this._BusinessGroupBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessGroup> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessGroupBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<BusinessGroup> GetAll()
        {
            return this._BusinessGroupBiz.GetAll();
        }

        /// <summary>
        /// 根据模块id获取分组
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public List<BusinessGroup> GetByBusinessTypeId(int module)
        {
            return this._BusinessGroupBiz.GetByBusinessTypeId(module);
        }
    }
}
