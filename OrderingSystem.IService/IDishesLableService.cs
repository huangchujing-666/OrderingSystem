using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IDishesLableService
    {
        DishesLable GetById(int id);

        DishesLable Insert(DishesLable model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(DishesLable model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(DishesLable model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<DishesLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        List<DishesLable> GetAll();
    }
}
