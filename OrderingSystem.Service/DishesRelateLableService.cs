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
    public class DishesRelateLableService: IDishesRelateLableService
    {
        /// <summary>
        /// The DishesRelateLable biz
        /// </summary>
        private IDishesRelateLableBusiness _DishesRelateLableBiz;

        public DishesRelateLableService(IDishesRelateLableBusiness DishesRelateLableBiz)
        {
            _DishesRelateLableBiz = DishesRelateLableBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesRelateLable GetById(int id)
        {
            return this._DishesRelateLableBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesRelateLable Insert(DishesRelateLable model)
        {
            return this._DishesRelateLableBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesRelateLable model)
        {
            this._DishesRelateLableBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesRelateLable model)
        {
            this._DishesRelateLableBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesRelateLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesRelateLableBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<DishesRelateLable> GetAll()
        {
            return this._DishesRelateLableBiz.GetAll();
        }
    }
}
