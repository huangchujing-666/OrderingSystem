using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IDishesCategoryService
    {
        DishesCategory GetById(int id);

        DishesCategory Insert(DishesCategory model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(DishesCategory model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(DishesCategory model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<DishesCategory> GetManagerList(string name,int businessInfoId, int pageNum, int pageSize, out int totalCount);

        List<DishesCategory> GetAll();
    }
}
