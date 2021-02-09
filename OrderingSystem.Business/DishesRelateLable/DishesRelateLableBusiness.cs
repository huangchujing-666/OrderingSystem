using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class DishesRelateLableBusiness:IDishesRelateLableBusiness
    {
        private IRepository<DishesRelateLable> _repoDishesRelateLable;

        public DishesRelateLableBusiness(
          IRepository<DishesRelateLable> repoDishesRelateLable
          )
        {
            _repoDishesRelateLable = repoDishesRelateLable;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesRelateLable GetById(int id)
        {
            return this._repoDishesRelateLable.GetById(id);
        }

        public DishesRelateLable Insert(DishesRelateLable model)
        {
            return this._repoDishesRelateLable.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesRelateLable model)
        {
            this._repoDishesRelateLable.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesRelateLable model)
        {
            this._repoDishesRelateLable.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesRelateLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesRelateLable>();
              
             
            totalCount = this._repoDishesRelateLable.Table.Where(where).Count();
            return this._repoDishesRelateLable.Table.Where(where).OrderBy(p => p.DishesRelateLableId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<DishesRelateLable> GetAll()
        { 
            return this._repoDishesRelateLable.Table.ToList();
        }

    }
}


