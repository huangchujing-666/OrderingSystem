using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IDishesCategoryBusiness
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
        List<DishesCategory> GetManagerList(string name, int businessInfoId,int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExistName(string name);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<DishesCategory> GetAll();
    }
}
