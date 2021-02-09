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
    public class BusinessTypeService: IBusinessTypeService
    {
        /// <summary>
        /// The BusinessType biz
        /// </summary>
        private IBusinessTypeBusiness _BusinessTypeBiz;

        public BusinessTypeService(IBusinessTypeBusiness BusinessTypeBiz)
        {
            _BusinessTypeBiz = BusinessTypeBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessType GetById(int id)
        {
            return this._BusinessTypeBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessType Insert(BusinessType model)
        {
            return this._BusinessTypeBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessType model)
        {
            this._BusinessTypeBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessType model)
        {
            this._BusinessTypeBiz.Delete(model);
        }
        
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessType> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessTypeBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
        
    }
}
