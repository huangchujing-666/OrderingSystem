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
    public class BusinessBannerImageService: IBusinessBannerImageService
    {
        /// <summary>
        /// The BusinessBannerImage biz
        /// </summary>
        private IBusinessBannerImageBusiness _BusinessBannerImageBiz;

        public BusinessBannerImageService(IBusinessBannerImageBusiness BusinessBannerImageBiz)
        {
            _BusinessBannerImageBiz = BusinessBannerImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessBannerImage GetById(int id)
        {
            return this._BusinessBannerImageBiz.GetById(id);
        }

        public List<BusinessBannerImage> GetList()
        {
            return this._BusinessBannerImageBiz.GetList();
        }

        public  List<BusinessBannerImage> GetListByModule(int module){
            return this._BusinessBannerImageBiz.GetListByModule(module);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessBannerImage Insert(BusinessBannerImage model)
        {
            return this._BusinessBannerImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessBannerImage model)
        {
            this._BusinessBannerImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessBannerImage model)
        {
            this._BusinessBannerImageBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessBannerImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessBannerImageBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
    }
}
