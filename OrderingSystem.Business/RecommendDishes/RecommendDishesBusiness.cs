using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class RecommendDishesBusiness:IRecommendDishesBusiness
    {
        private IRepository<RecommendDishes> _repoRecommendDishes;

        public RecommendDishesBusiness(
          IRepository<RecommendDishes> repoRecommendDishes
          )
        {
            _repoRecommendDishes = repoRecommendDishes;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RecommendDishes GetById(int id)
        {
            return this._repoRecommendDishes.GetById(id);
        }

        public RecommendDishes Insert(RecommendDishes model)
        {
            return this._repoRecommendDishes.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RecommendDishes model)
        {
            this._repoRecommendDishes.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RecommendDishes model)
        {
            this._repoRecommendDishes.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<RecommendDishes> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<RecommendDishes>();
               

            totalCount = this._repoRecommendDishes.Table.Where(where).Count();
            return this._repoRecommendDishes.Table.Where(where).OrderBy(p => p.RecommendDishesId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
         
    }
}


