using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class DishesLableBusiness:IDishesLableBusiness
    {
        private IRepository<DishesLable> _repoDishesLable;

        public DishesLableBusiness(
          IRepository<DishesLable> repoDishesLable
          )
        {
            _repoDishesLable = repoDishesLable;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesLable GetById(int id)
        {
            return this._repoDishesLable.GetById(id);
        }

        public DishesLable Insert(DishesLable model)
        {
            return this._repoDishesLable.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesLable model)
        {
            this._repoDishesLable.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesLable model)
        {
            this._repoDishesLable.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesLable>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoDishesLable.Table.Where(where).Count();
            return this._repoDishesLable.Table.Where(where).OrderBy(p => p.DishesLableId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoDishesLable.Table.Any(p => p.Name == name);
        }
        public List<DishesLable> GetAll()
        {
            return this._repoDishesLable.Table.ToList();
        }
    }
}


