using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IOrderService
    {
        Order GetById(int id);

        Order Insert(Order model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Order model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Order model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Order> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        List<Order> GetOrderListByUserId(int UserId,int Module, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据商家id获取新付款订单信息
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        List<Order> GetNewOrdersByBusinessId(int businessInfoId);

        /// <summary>
        /// 根据订单id获取订单列表
        /// </summary>
        /// <param name="orderStatusId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Order> GetOrderListByOderStatusId(int orderStatusId,int Module,int UserId, int pageNum, int pageSize, out int totalCount);
        /// <summary>
        /// 列表搜索
        /// </summary>
        /// <param name="queryBusinessName">商家名称</param>
        /// <param name="queryUserName">下单人</param>
        /// <param name="queryOrderNo">订单编号</param>
        /// <param name="queryOrderStatusId">订单状态</param>
        /// <param name="queryStartTime">开始时间</param>
        /// <param name="queryEndTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        List<Order> GetListBySearch(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int pageIndex, int pageSize, out int totalCount, out decimal totalAmount, int businessId);

        List<Order> OrderExpert(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int businessId);

        Order GetDetailByOrderNo(string order_no);
        bool CheckOrder(string order_no);
        void PaymentCallback(string out_trade_no, string openid, string transaction_id, int paytype);
        List<Order> GetOrderListByBueinessId(int bussiness_Id);
        List<Order> GetBusinessClientOrderList(int business_Id, DateTime start_Time, DateTime end_Time,  out int totalCount);
        int UpdateOrderDetailByOrdereNo(int orderId, string orderNo);

        /// <summary>
        /// 商家客户端确认订单
        /// </summary>
        /// <param name="order_No"></param>
        /// <param name="business_Id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool ConfirmOrder(string order_No, int business_Id, out string msg);

        /// <summary>
        /// 根据用户id，商家id获取订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        List<Order> GetOrderNoByUserIdAndBusinessId(int userId, int businessInfoId);
    }
}
