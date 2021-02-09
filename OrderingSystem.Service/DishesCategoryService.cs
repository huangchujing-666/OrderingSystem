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
    public class DishesCategoryService: IDishesCategoryService
    {
        /// <summary>
        /// The DishesCategory biz
        /// </summary>
        private IDishesCategoryBusiness _DishesCategoryBiz;

        public DishesCategoryService(IDishesCategoryBusiness DishesCategoryBiz)
        {
            _DishesCategoryBiz = DishesCategoryBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesCategory GetById(int id)
        {
            return this._DishesCategoryBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesCategory Insert(DishesCategory model)
        {
            return this._DishesCategoryBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesCategory model)
        {
            this._DishesCategoryBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesCategory model)
        {
            this._DishesCategoryBiz.Delete(model);
        }
        
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesCategory> GetManagerList(string name,int businessInfoId, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesCategoryBiz.GetManagerList(name, businessInfoId, pageNum, pageSize, out totalCount);
        }

        public List<DishesCategory> GetAll()
        {
            return this._DishesCategoryBiz.GetAll();
        }
    }
}
