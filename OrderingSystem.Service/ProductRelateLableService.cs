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
    public class ProductRelateLableService: IProductRelateLableService
    {
        /// <summary>
        /// The ProductRelateLable biz
        /// </summary>
        private IProductRelateLableBusiness _ProductRelateLableBiz;

        public ProductRelateLableService(IProductRelateLableBusiness ProductRelateLableBiz)
        {
            _ProductRelateLableBiz = ProductRelateLableBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductRelateLable GetById(int id)
        {
            return this._ProductRelateLableBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductRelateLable Insert(ProductRelateLable model)
        {
            return this._ProductRelateLableBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ProductRelateLable model)
        {
            this._ProductRelateLableBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ProductRelateLable model)
        {
            this._ProductRelateLableBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ProductRelateLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._ProductRelateLableBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<ProductRelateLable> GetAll()
        {
            return this._ProductRelateLableBiz.GetAll();
        }
    }
}
