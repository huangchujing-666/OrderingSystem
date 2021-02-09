using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IHotelCategoryService
    {
        HotelCategory GetById(int id);

        HotelCategory Insert(HotelCategory model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(HotelCategory model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(HotelCategory model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<HotelCategory> GetManagerList(string name,int businessInfoId, int pageNum, int pageSize, out int totalCount);

        List<HotelCategory> GetAll();
    }
}
