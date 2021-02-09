using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class DishesCategoryBusiness : IDishesCategoryBusiness
    {
        private IRepository<DishesCategory> _repoDishesCategory;

        public DishesCategoryBusiness(
          IRepository<DishesCategory> repoDishesCategory
          )
        {
            _repoDishesCategory = repoDishesCategory;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesCategory GetById(int id)
        {
            return this._repoDishesCategory.GetById(id);
        }

        public DishesCategory Insert(DishesCategory model)
        {
            return this._repoDishesCategory.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesCategory model)
        {
            this._repoDishesCategory.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesCategory model)
        {
            this._repoDishesCategory.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesCategory> GetManagerList(string name, int businessInfoId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesCategory>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            //商家ID
            if (businessInfoId > 0)
            {
                where = where.And(m => m.BusinessInfoId == 0 || m.BusinessInfoId == businessInfoId);
            }

            totalCount = this._repoDishesCategory.Table.Where(where).Count();
            return this._repoDishesCategory.Table.Where(where).OrderBy(p => p.DishesCategoryId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoDishesCategory.Table.Any(p => p.Name == name);
        }

        public List<DishesCategory> GetAll()
        {
            return this._repoDishesCategory.Table.ToList();
        }

    }
}


