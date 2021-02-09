using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IRepository<Product> _repoProduct;

        public ProductBusiness(
          IRepository<Product> repoProduct
          )
        {
            _repoProduct = repoProduct;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            return this._repoProduct.GetById(id);
        }

        public Product Insert(Product model)
        {
            return this._repoProduct.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Product model)
        {
            this._repoProduct.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Product model)
        {
            this._repoProduct.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Product> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            var where = PredicateBuilder.True<Product>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                where = where.And(m => m.BusinessInfo.Name.Contains(businessName));
            }
            if (businessId != 0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }

            totalCount = this._repoProduct.Table.Where(where).Count();
            return this._repoProduct.Table.Where(where).OrderBy(p => p.ProductId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoProduct.Table.Any(p => p.Name == name);
        }
        public List<Product> GetAll()
        {
            return this._repoProduct.Table.ToList();
        }
    }
}


