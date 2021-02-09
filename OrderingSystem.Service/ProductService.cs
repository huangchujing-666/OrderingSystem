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
    public class ProductService: IProductService
    {
        /// <summary>
        /// The Product biz
        /// </summary>
        private IProductBusiness _ProductBiz;

        public ProductService(IProductBusiness ProductBiz)
        {
            _ProductBiz = ProductBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            return this._ProductBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Product Insert(Product model)
        {
            return this._ProductBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Product model)
        {
            this._ProductBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Product model)
        {
            this._ProductBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Product> GetManagerList(string name,  string businessName,int pageNum, int pageSize, out int totalCount,int businessId)
        {
            return this._ProductBiz.GetManagerList(name, businessName, pageNum, pageSize, out totalCount, businessId);
        }

        public List<Product> GetAll()
        {
            return this._ProductBiz.GetAll();
        }
    }
}
