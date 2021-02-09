using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class ProductRelateLableBusiness:IProductRelateLableBusiness
    {
        private IRepository<ProductRelateLable> _repoProductRelateLable;

        public ProductRelateLableBusiness(
          IRepository<ProductRelateLable> repoProductRelateLable
          )
        {
            _repoProductRelateLable = repoProductRelateLable;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductRelateLable GetById(int id)
        {
            return this._repoProductRelateLable.GetById(id);
        }

        public ProductRelateLable Insert(ProductRelateLable model)
        {
            return this._repoProductRelateLable.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ProductRelateLable model)
        {
            this._repoProductRelateLable.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ProductRelateLable model)
        {
            this._repoProductRelateLable.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<ProductRelateLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ProductRelateLable>();
               

            totalCount = this._repoProductRelateLable.Table.Where(where).Count();
            return this._repoProductRelateLable.Table.Where(where).OrderBy(p => p.ProductRelateLableId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

   
        public List<ProductRelateLable> GetAll()
        {
            return this._repoProductRelateLable.Table.ToList();
        }
    }
}


