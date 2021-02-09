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
    public class GoodsService : IGoodsService
    {
        /// <summary>
        /// The Goods biz
        /// </summary>
        private IGoodsBusiness _GoodsBiz;

        public GoodsService(IGoodsBusiness GoodsBiz)
        {
            _GoodsBiz = GoodsBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Goods GetById(int id)
        {
            return this._GoodsBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Goods Insert(Goods model)
        {
            return this._GoodsBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Goods model)
        {
            this._GoodsBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Goods model)
        {
            this._GoodsBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Goods> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            return this._GoodsBiz.GetManagerList(name, businessName, pageNum, pageSize, out totalCount, businessId);
        }

        public List<Goods> GetGoodsByBusinessId(int BusinessInfoId)
        {
            return this._GoodsBiz.GetGoodsByBusinessId(BusinessInfoId);
        }

        /// <summary>
        /// 根据商家id衣品类别id获取衣品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsByBusinessIdAndCategoryId(int categoryId, int businessInfoId, int page_index, int page_size, out int totalCount)
        {
            return this._GoodsBiz.GetGoodsByBusinessIdAndCategoryId(categoryId, businessInfoId, page_index, page_size,out totalCount);
        }


        /// <summary>
        /// 根据ID列表获取商品信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsByIds(List<int> ids)
        {
            return this._GoodsBiz.GetGoodsByIds(ids);
        }
    }
}
