﻿using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService.ResponseModel;

namespace OrderingSystem.Service
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// The Order biz
        /// </summary>
        private IOrderBusiness _OrderBiz;
       private IJpushLogBusiness _JpushLogBiz;

        public OrderService(IOrderBusiness OrderBiz, IJpushLogBusiness JpushLogBiz)//
        {
            _OrderBiz = OrderBiz;
            _JpushLogBiz = JpushLogBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetById(int id)
        {
            return this._OrderBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Order Insert(Order model)
        {
            return this._OrderBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Order model)
        {
            this._OrderBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Order model)
        {
            this._OrderBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Order> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据订单id获取订单列表
        /// </summary>
        /// <param name="orderStatusId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Order> GetOrderListByOderStatusId(int orderStatusId, int Module, int UserId, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderBiz.GetOrderListByOderStatusId(orderStatusId, Module, UserId, pageNum, pageSize, out totalCount);
        }
        public List<Order> GetListBySearch(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int pageIndex, int pageSize, out int totalCount, out decimal totalAmount, int businessId)
        {
            return this._OrderBiz.GetListBySearch(queryBusinessName, queryUserName, queryOrderNo, queryOrderStatusId, queryStartTime, queryEndTime, pageIndex, pageSize, out totalCount, out totalAmount, businessId);
        }

        public List<Order> OrderExpert(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int businessId)
        {
            return this._OrderBiz.OrderExpert(queryBusinessName, queryUserName, queryOrderNo, queryOrderStatusId, queryStartTime, queryEndTime, businessId);
        }

        /// <summary>
        /// 根据用户Id获取订单列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Order> GetOrderListByUserId(int UserId, int Module, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderBiz.GetOrderListByUserId(UserId, Module, pageNum, pageSize, out totalCount);
        }


        /// <summary>
        /// 根据商家id获取新付款订单信息
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public List<Order> GetNewOrdersByBusinessId(int businessInfoId)
        {
            return this._OrderBiz.GetNewOrdersByBusinessId(businessInfoId);
        }


        public Order GetDetailByOrderNo(string order_no)
        {
            return this._OrderBiz.GetDetailByOrderNo(order_no);
        }

        public bool CheckOrder(string order_no)
        {
            return this._OrderBiz.CheckOrder(order_no);
        }

        public void PaymentCallback(string out_trade_no, string openid, string transaction_id, int paytype)
        {
            this._OrderBiz.PaymentCallback(out_trade_no, openid, transaction_id, paytype);
        }

        public List<Order> GetOrderListByBueinessId(int bussiness_Id)
        {
            return this._OrderBiz.GetOrderListByBueinessId(bussiness_Id);
        }

        public List<Order> GetBusinessClientOrderList(int business_Id, DateTime start_Time, DateTime end_Time, out int totalCount)
        {
            return this._OrderBiz.GetBusinessClientOrderList(business_Id, start_Time, end_Time, out totalCount);
        }

        public int UpdateOrderDetailByOrdereNo(int orderId, string orderNo)
        {
            return this._OrderBiz.UpdateOrderDetailByOrdereNo(orderId, orderNo);
        }

        /// <summary>
        /// 商家客户端确认订单
        /// </summary>
        /// <param name="order_No"></param>
        /// <param name="business_Id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ConfirmOrder(string order_No, int business_Id, out string msg)
        {
            return this._OrderBiz.ConfirmOrder(order_No, business_Id,out msg);
        }


        /// <summary>
        /// 根据用户id，商家id获取订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        public List<Order> GetOrderNoByUserIdAndBusinessId(int userId, int businessInfoId)
        { return this._OrderBiz.GetOrderNoByUserIdAndBusinessId(userId, businessInfoId); }

    
}
}
