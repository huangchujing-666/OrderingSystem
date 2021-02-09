using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Business
{
    public class GoodsBusiness : IGoodsBusiness
    {
        private IRepository<Goods> _repoGoods;

        public GoodsBusiness(
          IRepository<Goods> repoGoods
          )
        {
            _repoGoods = repoGoods;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Goods GetById(int id)
        {
            return this._repoGoods.GetById(id);
        }

        public Goods Insert(Goods model)
        {
            return this._repoGoods.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Goods model)
        {
            this._repoGoods.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Goods model)
        {
            this._repoGoods.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Goods> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            var where = PredicateBuilder.True<Goods>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                where = where.And(m => m.BusinessInfo.Name.Contains(businessName));
            }

            if (businessId != 0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }
            where = where.And(m => m.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoGoods.Table.Where(where).Count();
            return this._repoGoods.Table.Where(where).OrderBy(p => p.GoodsId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据商家Id获取菜品信息
        /// </summary>
        /// <param name="BusinessInfoId"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsByBusinessId(int businessInfoId)
        {
            return this._repoGoods.Table.Where(c => c.BusinessInfoId == businessInfoId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效).ToList();
        }

        /// <summary>
        /// 根据ID列表获取商品信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsByIds(List<int> ids)
        {
            return this._repoGoods.Table.Where(c => ids.Contains(c.GoodsId) && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效).ToList();
        }
        
         /// <summary>
        /// 根据商家id 衣品类别id获取衣品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsByBusinessIdAndCategoryId(int categoryId, int businessInfoId,int page_index, int page_size, out int total_count)
        {
            total_count = this._repoGoods.Table.Where(c => c.BusinessInfoId == businessInfoId && c.CategoryId == categoryId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效).Count();
            return this._repoGoods.Table.Where(c => c.BusinessInfoId == businessInfoId && c.CategoryId == categoryId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效).OrderBy(c=>c.GoodsId).Skip(page_size*(page_index-1)).Take(page_size).ToList();
        }

    }
}


