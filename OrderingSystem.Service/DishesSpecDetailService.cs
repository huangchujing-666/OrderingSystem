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
    public class DishesSpecDetailService: IDishesSpecDetailService
    {
        /// <summary>
        /// The DishesSpecDetail biz
        /// </summary>
        private IDishesSpecDetailBusiness _DishesSpecDetailBiz;

        public DishesSpecDetailService(IDishesSpecDetailBusiness DishesSpecDetailBiz)
        {
            _DishesSpecDetailBiz = DishesSpecDetailBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesSpecDetail GetById(int id)
        {
            return this._DishesSpecDetailBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesSpecDetail Insert(DishesSpecDetail model)
        {
            return this._DishesSpecDetailBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesSpecDetail model)
        {
            this._DishesSpecDetailBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesSpecDetail model)
        {
            this._DishesSpecDetailBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesSpecDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesSpecDetailBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<DishesSpecDetail> GetListByIds(string ids)
        {
            return this._DishesSpecDetailBiz.GetListByIds(ids);
        }

    }
}
