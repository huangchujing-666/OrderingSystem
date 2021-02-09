﻿using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IOrderBusiness
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

        List<Order> GetListBySearch(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int pageIndex, int pageSize, out int totalCount, out decimal totalAmount, int businessId);


        List<Order> OrderExpert(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int businessId);
        /// <summary>
        /// 根据用户Id获取商家列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<Order> GetOrderListByUserId(int UserId, int Module, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据商家id获取新付款订单信息
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        List<Order> GetNewOrdersByBusinessId(int businessInfoId);

        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        Order GetDetailByOrderNo(string order_no);
        /// <summary>
        /// 检查订单
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        bool CheckOrder(string out_trade_no);

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="orderNo">订单号/param>
        /// <param name="payAccount">openid或者支付宝帐户</param>
        /// <param name="transaction_id">商户号</param>
        /// <param name="payWay">支付方式(1支付宝 2 微信 3银联)</param>
        void PaymentCallback(string orderNo, string payAccount, string transaction_id, int payWay);
        List<Order> GetOrderListByOderStatusId(int orderStatusId, int Module, int UserId, int pageNum, int pageSize, out int totalCount);
        List<Order> GetOrderListByBueinessId(int bussiness_Id);
        List<Order> GetBusinessClientOrderList(int business_Id, DateTime start_Time, DateTime end_Time, out int totalCount);
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
        /// 根据用户id商家id获取订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        List<Order> GetOrderNoByUserIdAndBusinessId(int userId, int businessInfoId);
    }
}
