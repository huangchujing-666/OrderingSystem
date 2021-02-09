using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IRoomService
    {
        Room GetById(int id);

        Room Insert(Room model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Room model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Room model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Room> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId);

        List<Room> GetAll();

        /// <summary>
        /// 根据商家ID获取房间列表
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        List<Room> GetListByBusinessId(int businessId);
    }
}
