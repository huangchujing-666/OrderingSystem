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
    public class GoodsImageService: IGoodsImageService
    {
        /// <summary>
        /// The GoodsImage biz
        /// </summary>
        private IGoodsImageBusiness _GoodsImageBiz;

        public GoodsImageService(IGoodsImageBusiness GoodsImageBiz)
        {
            _GoodsImageBiz = GoodsImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsImage GetById(int id)
        {
            return this._GoodsImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public GoodsImage Insert(GoodsImage model)
        {
            return this._GoodsImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(GoodsImage model)
        {
            this._GoodsImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(GoodsImage model)
        {
            this._GoodsImageBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<GoodsImage> GetManagerList(int dishesId, int pageNum, int pageSize, out int totalCount)
        {
            return this._GoodsImageBiz.GetManagerList(dishesId, pageNum, pageSize, out totalCount);
        }
    }
}
