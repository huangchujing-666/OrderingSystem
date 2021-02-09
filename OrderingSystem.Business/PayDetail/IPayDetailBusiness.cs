using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IPayDetailBusiness
    {


        PayDetail GetById(int id);

        PayDetail Insert(PayDetail model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(PayDetail model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(PayDetail model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<PayDetail> GetManagerList(string orderNo, string userName, int payStatus, string startTime, string endTime, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        List<PayDetail> PayDetailExpert(string orderNo, string userName, int payStatus, string startTime, string endTime);


        PayDetail GetDetailByOrderNo(string order_No);
    }
}
