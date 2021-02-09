using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class ProductLableBusiness:IProductLableBusiness
    {
        private IRepository<ProductLable> _repoProductLable;

        public ProductLableBusiness(
          IRepository<ProductLable> repoProductLable
          )
        {
            _repoProductLable = repoProductLable;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductLable GetById(int id)
        {
            return this._repoProductLable.GetById(id);
        }

        public ProductLable Insert(ProductLable model)
        {
            return this._repoProductLable.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ProductLable model)
        {
            this._repoProductLable.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ProductLable model)
        {
            this._repoProductLable.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<ProductLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ProductLable>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoProductLable.Table.Where(where).Count();
            return this._repoProductLable.Table.Where(where).OrderBy(p => p.ProductLableId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoProductLable.Table.Any(p => p.Name == name);
        }
        public List<ProductLable> GetAll()
        {
            return this._repoProductLable.Table.ToList();
        }
    }
}


