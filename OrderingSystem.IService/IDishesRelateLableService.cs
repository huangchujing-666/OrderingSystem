using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IDishesRelateLableService
    {
        DishesRelateLable GetById(int id);

        DishesRelateLable Insert(DishesRelateLable model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(DishesRelateLable model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(DishesRelateLable model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<DishesRelateLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        List<DishesRelateLable> GetAll();
    }
}
