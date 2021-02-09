using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IOrderDetailService
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
        /// 根据商家编号获取订单详情
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        List<OrderDetail> GetListByOrderNo(string order_no);
    }
}
