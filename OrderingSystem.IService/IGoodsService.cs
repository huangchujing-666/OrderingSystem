using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IGoodsService
    {
        Goods GetById(int id);

        Goods Insert(Goods model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Goods model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Goods model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Goods> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId);

        /// <summary>
        /// 根据商家ID获取菜品信息列表
        /// </summary>
        /// <param name="BusinessInfoId">商家ID</param>
        /// <returns></returns>
        List<Goods> GetGoodsByBusinessId(int BusinessInfoId);

        /// <summary>
        /// 根据ID列表获取商品信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<Goods> GetGoodsByIds(List<int> ids);
         /// <summary>
        /// 根据商家id衣品类别id获取衣品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        List<Goods> GetGoodsByBusinessIdAndCategoryId(int categoryId, int businessInfoId,int  page_index,int page_size, out int totalCount );

    }
}
