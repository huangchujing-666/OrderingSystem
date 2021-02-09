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
    public class ProductLableService: IProductLableService
    {
        /// <summary>
        /// The ProductLable biz
        /// </summary>
        private IProductLableBusiness _ProductLableBiz;

        public ProductLableService(IProductLableBusiness ProductLableBiz)
        {
            _ProductLableBiz = ProductLableBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductLable GetById(int id)
        {
            return this._ProductLableBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductLable Insert(ProductLable model)
        {
            return this._ProductLableBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ProductLable model)
        {
            this._ProductLableBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ProductLable model)
        {
            this._ProductLableBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ProductLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._ProductLableBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<ProductLable> GetAll()
        {
            return this._ProductLableBiz.GetAll();
        }
    }
}
