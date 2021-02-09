using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IBaseStationBusiness
    {


        BaseStation GetById(int id);

        BaseStation Insert(BaseStation model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BaseStation model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BaseStation model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BaseStation> GetManagerList(string station, string line, string area, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        List<BaseStation> GetAll();
    }
}
