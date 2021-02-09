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
    public class ProductImageService: IProductImageService
    {
        /// <summary>
        /// The ProductImage biz
        /// </summary>
        private IProductImageBusiness _ProductImageBiz;

        public ProductImageService(IProductImageBusiness ProductImageBiz)
        {
            _ProductImageBiz = ProductImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductImage GetById(int id)
        {
            return this._ProductImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductImage Insert(ProductImage model)
        {
            return this._ProductImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ProductImage model)
        {
            this._ProductImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ProductImage model)
        {
            this._ProductImageBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ProductImage> GetManagerList(int productId, int pageNum, int pageSize, out int totalCount)
        {
            return this._ProductImageBiz.GetManagerList(productId, pageNum, pageSize, out totalCount);
        }
    }
}
