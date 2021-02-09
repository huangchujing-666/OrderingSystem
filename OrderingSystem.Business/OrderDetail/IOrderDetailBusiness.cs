using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IOrderDetailBusiness
    {


        OrderDetail GetById(int id);

        OrderDetail Insert(OrderDetail model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(OrderDetail model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(OrderDetail model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<OrderDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据订单编号获取子订单详情
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        List<OrderDetail> GetListByOrderNo(string order_no);


    }
}
