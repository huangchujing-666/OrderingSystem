using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class RecommendDishesService: IRecommendDishesService
    {
        /// <summary>
        /// The RecommendDishes biz
        /// </summary>
        private IRecommendDishesBusiness _RecommendDishesBiz;

        public RecommendDishesService(IRecommendDishesBusiness RecommendDishesBiz)
        {
            _RecommendDishesBiz = RecommendDishesBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RecommendDishes GetById(int id)
        {
            return this._RecommendDishesBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RecommendDishes Insert(RecommendDishes model)
        {
            return this._RecommendDishesBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RecommendDishes model)
        {
            this._RecommendDishesBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RecommendDishes model)
        {
            this._RecommendDishesBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RecommendDishes> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._RecommendDishesBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
    }
}
