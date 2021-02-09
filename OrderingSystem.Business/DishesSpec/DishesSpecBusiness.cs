using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class DishesSpecBusiness : IDishesSpecBusiness
    {
        private IRepository<DishesSpec> _repoDishesSpec;

        public DishesSpecBusiness(
          IRepository<DishesSpec> repoDishesSpec
          )
        {
            _repoDishesSpec = repoDishesSpec;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesSpec GetById(int id)
        {
            return this._repoDishesSpec.GetById(id);
        }

        public DishesSpec Insert(DishesSpec model)
        {
            return this._repoDishesSpec.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesSpec model)
        {
            this._repoDishesSpec.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesSpec model)
        {
            this._repoDishesSpec.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesSpec> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesSpec>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoDishesSpec.Table.Where(where).Count();
            return this._repoDishesSpec.Table.Where(where).OrderBy(p => p.DishesSpecId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoDishesSpec.Table.Any(p => p.Name == name);
        }

        public List<DishesSpec> GetAll()
        {
            return this._repoDishesSpec.Table.ToList();
        }
    }
}


