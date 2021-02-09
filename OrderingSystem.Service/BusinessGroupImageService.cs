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
    public class BusinessGroupImageService: IBusinessGroupImageService
    {
        /// <summary>
        /// The BusinessGroupImage biz
        /// </summary>
        private IBusinessGroupImageBusiness _BusinessGroupImageBiz;

        public BusinessGroupImageService(IBusinessGroupImageBusiness BusinessGroupImageBiz)
        {
            _BusinessGroupImageBiz = BusinessGroupImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessGroupImage GetById(int id)
        {
            return this._BusinessGroupImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessGroupImage Insert(BusinessGroupImage model)
        {
            return this._BusinessGroupImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessGroupImage model)
        {
            this._BusinessGroupImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessGroupImage model)
        {
            this._BusinessGroupImageBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessGroupImage> GetManagerList(int groupId, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessGroupImageBiz.GetManagerList(groupId, pageNum, pageSize, out totalCount);
        }
         
    }
}
