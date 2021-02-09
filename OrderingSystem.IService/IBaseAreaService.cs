using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBaseAreaService
    {
        BaseArea GetById(int id);

        BaseArea Insert(BaseArea model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BaseArea model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BaseArea model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BaseArea> GetManagerList(string name, int pid, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<BaseArea> GetAll();

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        List<BaseArea> GetCityList(int pid);
    }
}
