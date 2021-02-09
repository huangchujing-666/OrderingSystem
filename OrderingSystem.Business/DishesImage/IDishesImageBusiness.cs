using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IDishesImageBusiness
    {


        DishesImage GetById(int id);

        DishesImage Insert(DishesImage model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(DishesImage model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(DishesImage model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<DishesImage> GetManagerList(int dishesId, int pageNum, int pageSize, out int totalCount);
         
    }
}
