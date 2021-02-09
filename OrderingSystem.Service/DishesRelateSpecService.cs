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
    public class DishesRelateSpecService: IDishesRelateSpecService
    {
        /// <summary>
        /// The DishesRelateSpec biz
        /// </summary>
        private IDishesRelateSpecBusiness _DishesRelateSpecBiz;

        public DishesRelateSpecService(IDishesRelateSpecBusiness DishesRelateSpecBiz)
        {
            _DishesRelateSpecBiz = DishesRelateSpecBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesRelateSpec GetById(int id)
        {
            return this._DishesRelateSpecBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesRelateSpec Insert(DishesRelateSpec model)
        {
            return this._DishesRelateSpecBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesRelateSpec model)
        {
            this._DishesRelateSpecBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesRelateSpec model)
        {
            this._DishesRelateSpecBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesRelateSpec> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesRelateSpecBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<DishesRelateSpec> GetAll()
        {
            return this._DishesRelateSpecBiz.GetAll();
        }
    }
}
