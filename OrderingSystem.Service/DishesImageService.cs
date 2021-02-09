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
    public class DishesImageService: IDishesImageService
    {
        /// <summary>
        /// The DishesImage biz
        /// </summary>
        private IDishesImageBusiness _DishesImageBiz;

        public DishesImageService(IDishesImageBusiness DishesImageBiz)
        {
            _DishesImageBiz = DishesImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesImage GetById(int id)
        {
            return this._DishesImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DishesImage Insert(DishesImage model)
        {
            return this._DishesImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesImage model)
        {
            this._DishesImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesImage model)
        {
            this._DishesImageBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DishesImage> GetManagerList(int dishesId, int pageNum, int pageSize, out int totalCount)
        {
            return this._DishesImageBiz.GetManagerList(dishesId, pageNum, pageSize, out totalCount);
        }
    }
}
