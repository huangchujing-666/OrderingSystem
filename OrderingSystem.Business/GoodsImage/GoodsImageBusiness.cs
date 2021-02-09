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
    public class GoodsImageBusiness:IGoodsImageBusiness
    {
        private IRepository<GoodsImage> _repoGoodsImage;

        public GoodsImageBusiness(
          IRepository<GoodsImage> repoGoodsImage
          )
        {
            _repoGoodsImage = repoGoodsImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsImage GetById(int id)
        {
            return this._repoGoodsImage.GetById(id);
        }

        public GoodsImage Insert(GoodsImage model)
        {
            return this._repoGoodsImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(GoodsImage model)
        {
            this._repoGoodsImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(GoodsImage model)
        {
            this._repoGoodsImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<GoodsImage> GetManagerList(int dishesId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<GoodsImage>();
            if (dishesId > 0)
            {
                where = where.And(p => p.GoodsId == dishesId);
            }

            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoGoodsImage.Table.Where(where).Count();
            return this._repoGoodsImage.Table.Where(where).OrderBy(p => p.GoodsImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

      
    }
}


