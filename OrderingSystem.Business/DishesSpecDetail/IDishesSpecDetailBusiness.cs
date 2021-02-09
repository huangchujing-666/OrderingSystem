using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IDishesSpecDetailBusiness
    {


        DishesSpecDetail GetById(int id);

        DishesSpecDetail Insert(DishesSpecDetail model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(DishesSpecDetail model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(DishesSpecDetail model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<DishesSpecDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);


        /// <summary>
        /// 根据id字符串查询多条数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<DishesSpecDetail> GetListByIds(string ids);
    }
}
