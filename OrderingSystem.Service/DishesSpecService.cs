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
    public class DishesSpecService: IDishesSpecService
    {
        /// <summary>
        /// The DishesSpec biz
        /// </summary>
        private IDishesSpecBusiness _DishesSpecBiz;

        public DishesSpecService(IDishesSpecBusiness DishesSpecBiz)
        {
            _DishesSpecBiz = DishesSpecBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesSpec GetById(int id)
        {
            return this._DishesSpecBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesSpec Insert(DishesSpec model)
        {
            return this._DishesSpecBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesSpec model)
        {
            this._DishesSpecBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesSpec model)
        {
            this._DishesSpecBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesSpec> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesSpecBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<DishesSpec> GetAll()
        {
            return this._DishesSpecBiz.GetAll();
        }
    }
}
