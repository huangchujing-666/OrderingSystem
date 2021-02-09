using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IRecommendDishesBusiness
    {


        RecommendDishes GetById(int id);

        RecommendDishes Insert(RecommendDishes model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(RecommendDishes model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(RecommendDishes model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<RecommendDishes> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);
         
    }
}
