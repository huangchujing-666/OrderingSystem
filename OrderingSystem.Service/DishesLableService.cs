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
    public class DishesLableService: IDishesLableService
    {
        /// <summary>
        /// The DishesLable biz
        /// </summary>
        private IDishesLableBusiness _DishesLableBiz;

        public DishesLableService(IDishesLableBusiness DishesLableBiz)
        {
            _DishesLableBiz = DishesLableBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesLable GetById(int id)
        {
            return this._DishesLableBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesLable Insert(DishesLable model)
        {
            return this._DishesLableBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesLable model)
        {
            this._DishesLableBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesLable model)
        {
            this._DishesLableBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesLableBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<DishesLable> GetAll()
        {
            return this._DishesLableBiz.GetAll();
        }
    }
}
