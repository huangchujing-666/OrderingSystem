using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IBaseLineBusiness
    {


        BaseLine GetById(int id);

        BaseLine Insert(BaseLine model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BaseLine model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BaseLine model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BaseLine> GetManagerList(string lineName, string areaName, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        List<BaseLine> GetAll();
    }
}
