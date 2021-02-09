using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class DishesRelateSpecBusiness:IDishesRelateSpecBusiness
    {
        private IRepository<DishesRelateSpec> _repoDishesRelateSpec;

        public DishesRelateSpecBusiness(
          IRepository<DishesRelateSpec> repoDishesRelateSpec
          )
        {
            _repoDishesRelateSpec = repoDishesRelateSpec;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesRelateSpec GetById(int id)
        {
            return this._repoDishesRelateSpec.GetById(id);
        }

        public DishesRelateSpec Insert(DishesRelateSpec model)
        {
            return this._repoDishesRelateSpec.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesRelateSpec model)
        {
            this._repoDishesRelateSpec.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesRelateSpec model)
        {
            this._repoDishesRelateSpec.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesRelateSpec> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesRelateSpec>();
            

            totalCount = this._repoDishesRelateSpec.Table.Where(where).Count();
            return this._repoDishesRelateSpec.Table.Where(where).OrderBy(p => p.DishesRelateSpecId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<DishesRelateSpec> GetAll()
        {
            return this._repoDishesRelateSpec.Table.ToList();
        }
    }
}


