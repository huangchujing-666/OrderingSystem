using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Business
{
    public class ProductImageBusiness:IProductImageBusiness
    {
        private IRepository<ProductImage> _repoProductImage;

        public ProductImageBusiness(
          IRepository<ProductImage> repoProductImage
          )
        {
            _repoProductImage = repoProductImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductImage GetById(int id)
        {
            return this._repoProductImage.GetById(id);
        }

        public ProductImage Insert(ProductImage model)
        {
            return this._repoProductImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ProductImage model)
        {
            this._repoProductImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ProductImage model)
        {
            this._repoProductImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<ProductImage> GetManagerList(int productId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ProductImage>();
            if (productId > 0)
            {
                where = where.And(p => p.ProductId == productId);
            }

            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoProductImage.Table.Where(where).Count();
            return this._repoProductImage.Table.Where(where).OrderBy(p => p.ProductImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

      
    }
}


