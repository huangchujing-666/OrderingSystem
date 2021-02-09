using OrderingSystem.Core.Data;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using OrderingSystem.IService.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private IRepository<Order> _repoOrder;
        private IRepository<OrderDetail> _orderDetail;
        private IRepository<PayDetail> _payDetail;
        private IRepository<User> _user;
        private IRepository<BusinessInfo> _businessInfo;

        private IJpushLogBusiness _JpushLogBusiness;
        public OrderBusiness(
          IRepository<Order> repoOrder,
          IRepository<PayDetail> payDetail,
           IRepository<OrderDetail> orderDetail,
          IJpushLogBusiness JpushLogBusiness
          )
        {
            _repoOrder = repoOrder;
            _payDetail = payDetail;
            _orderDetail = orderDetail;
            _JpushLogBusiness = JpushLogBusiness;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetById(int id)
        {
            return this._repoOrder.GetById(id);
        }

        public Order Insert(Order model)
        {
            return this._repoOrder.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Order model)
        {
            this._repoOrder.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Order model)
        {
            this._repoOrder.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Order> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Order>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.OrderNo.Contains(name));
            }

            totalCount = this._repoOrder.Table.Where(where).Count();
            return this._repoOrder.Table.Where(where).OrderBy(p => p.OrderId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Order> GetOrderListByOderStatusId(int orderStatusId, int Module, int UserId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Order>();
            if (UserId > 0)
            {
                where = where.And(m => m.UserId == UserId);
            }
            if (Module > 0)
            {
                if (Module < 5)
                {
                    where = where.And(m => m.BusinessInfo.BusinessTypeId == Module);
                }
                else if (Module == 6)
                {
                    where = where.And(m => (new int[] { 2, 3, 4, 5 }).Contains(m.BusinessInfo.BusinessTypeId));
                }
            }
            if (Module==(int)EnumHelp.BusinessTypeEnum.食&& orderStatusId>0)
            {
                where = where.And(c => c.OrderStatusId == orderStatusId);
            }
            else
            {
                if (orderStatusId == (int)EnumHelp.OrderStatus.未付款)
                {
                    DateTime time = System.DateTime.Now.AddMinutes(-15);
                    where = where.And(c => c.OrderStatusId == orderStatusId && c.OrderTime > time);//(System.DateTime.Now-m.OrderTime).TotalMinutes<15  m.OrderStatusId==orderStatusId&&  EntityFunctions.DiffMinutes(DateTime.Now,c.OrderTime)<15
                }
                if (orderStatusId == (int)EnumHelp.OrderStatus.已付款)
                {
                    DateTime time = System.DateTime.Now.AddMinutes(-15);
                    where = where.And(m => m.OrderStatusId == orderStatusId || m.OrderTime <= time);//(System.DateTime.Now - m.OrderTime).TotalMinutes >=15   EntityFunctions.DiffMinutes(DateTime.Now, m.OrderTime) >=15
                }
            }
            totalCount = this._repoOrder.Table.Where(where).Count();
            return this._repoOrder.Table.Where(where).OrderByDescending(p => p.OrderTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="queryBusinessName"></param>
        /// <param name="queryUserName"></param>
        /// <param name="queryOrderNo"></param>
        /// <param name="queryOrderStatusId"></param>
        /// <param name="queryStartTime"></param>
        /// <param name="queryEndTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Order> GetListBySearch(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int pageIndex, int pageSize, out int totalCount, out decimal totalAmount, int businessId)
        {

            var whereOrder = PredicateBuilder.True<Order>();
            if (businessId != 0)
            {
                whereOrder = whereOrder.And(c => c.BusinessInfoId == businessId);
            }

            if (!string.IsNullOrWhiteSpace(queryBusinessName))
            {
                whereOrder = whereOrder.And(c => c.BusinessInfo.Name.Contains(queryBusinessName));
            }
            if (!string.IsNullOrWhiteSpace(queryOrderNo))
            {
                whereOrder = whereOrder.And(c => c.OrderNo.Contains(queryOrderNo));
            }
            if (!string.IsNullOrWhiteSpace(queryUserName))
            {
                whereOrder = whereOrder.And(c => c.User.NickName.Contains(queryUserName));
            }
            if (queryOrderStatusId > 0)
            {
                whereOrder = whereOrder.And(c => c.OrderStatusId == queryOrderStatusId);
            }
            if (!string.IsNullOrWhiteSpace(queryStartTime))
            {
                DateTime startTime = Convert.ToDateTime(queryStartTime + " 00:00:00");
                whereOrder = whereOrder.And(c => c.OrderTime >= startTime);
            }
            if (!string.IsNullOrWhiteSpace(queryEndTime))
            {
                DateTime endTime = Convert.ToDateTime(queryEndTime + " 00:00:00");
                whereOrder = whereOrder.And(c => c.OrderTime <= endTime);
            }
            totalCount = this._repoOrder.Table.Where(whereOrder).ToList().Count;
            //获取订单总金额
            totalAmount = (this._repoOrder.Table.Where(whereOrder).Sum(p => ((decimal?)p.RealAmount))) ?? 0;
            return this._repoOrder.Table.Where(whereOrder).OrderByDescending(p => p.OrderTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<Order> OrderExpert(string queryBusinessName, string queryUserName, string queryOrderNo, int queryOrderStatusId, string queryStartTime, string queryEndTime, int businessId)
        {
            var whereOrder = PredicateBuilder.True<Order>();
            if (businessId != 0)
            {
                whereOrder = whereOrder.And(c => c.BusinessInfoId == businessId);
            }
            if (!string.IsNullOrWhiteSpace(queryBusinessName))
            {
                whereOrder = whereOrder.And(c => c.BusinessInfo.Name.Contains(queryBusinessName));
            }
            if (!string.IsNullOrWhiteSpace(queryOrderNo))
            {
                whereOrder = whereOrder.And(c => c.OrderNo.Contains(queryOrderNo));
            }
            if (!string.IsNullOrWhiteSpace(queryUserName))
            {
                whereOrder = whereOrder.And(c => c.User.NickName.Contains(queryUserName));
            }
            if (queryOrderStatusId > 0)
            {
                whereOrder = whereOrder.And(c => c.OrderStatusId == queryOrderStatusId);
            }
            if (!string.IsNullOrWhiteSpace(queryStartTime))
            {
                DateTime startTime = Convert.ToDateTime(queryStartTime + " 00:00:00");
                whereOrder = whereOrder.And(c => c.OrderTime >= startTime);
            }
            if (!string.IsNullOrWhiteSpace(queryEndTime))
            {
                DateTime endTime = Convert.ToDateTime(queryEndTime + " 00:00:00");
                whereOrder = whereOrder.And(c => c.OrderTime <= endTime);
            }
            //totalCount = this._repoOrder.Table.Where(whereOrder).ToList().Count;
            return this._repoOrder.Table.Where(whereOrder).OrderByDescending(p => p.OrderTime).ToList();

        }


        /// <summary>
        /// 根据用户Id获取订单列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<Order> GetOrderListByUserId(int UserId, int Module, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Order>();

            // name过滤
            if (UserId > 0)
            {
                where = where.And(m => m.UserId == UserId);
            }
            if (Module > 0)
            {
                if (Module < 5)
                {
                    where = where.And(m => m.BusinessInfo.BusinessTypeId == Module);
                }
                else if (Module == 5)
                {
                    where = where.And(m => (new int[] { 2, 3, 4, 5 }).Contains(m.BusinessInfo.BusinessTypeId));
                }
            }
            totalCount = this._repoOrder.Table.Where(where).OrderBy(p => p.OrderId).ToList().Count;
            return this._repoOrder.Table.Where(where).OrderBy(p => p.OrderId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据商家id获取新付款订单信息
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public List<Order> GetNewOrdersByBusinessId(int businessInfoId)
        {
            return this._repoOrder.Table.Where(p => p.BusinessInfoId == businessInfoId && p.OrderStatusId == (int)EnumHelp.OrderStatus.已付款).ToList();
        }

        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public Order GetDetailByOrderNo(string order_no)
        {
            return this._repoOrder.Table.FirstOrDefault(p => p.OrderNo == order_no);
        }

        /// <summary>
        /// 未支付返回true
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public bool CheckOrder(string order_no)
        {
            var orders = this._repoOrder.Table.Where(c => c.OrderNo == order_no).FirstOrDefault();
            var paydetail = this._payDetail.Table.Where(c => c.OrderNo == order_no).FirstOrDefault();
            if (orders.OrderStatusId == (int)EnumHelp.OrderStatus.未付款 && paydetail.PayStatus == (int)EnumHelp.PayStatus.未支付)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="orderNo">订单号/param>
        /// <param name="payAccount">openid或者支付宝帐户</param>
        /// <param name="transaction_id">商户号</param>
        /// <param name="payWay">支付方式</param>
        public void PaymentCallback(string order_no, string payAccount, string transaction_id, int payWay)
        {
            var orders = this._repoOrder.Table.Where(c => c.OrderNo == order_no).FirstOrDefault();
            var paydetail = this._payDetail.Table.Where(c => c.OrderNo == order_no).FirstOrDefault();
            if (orders.OrderStatusId == (int)EnumHelp.OrderStatus.未付款 && paydetail.PayStatus == (int)EnumHelp.PayStatus.未支付)
            {
                orders.OrderStatusId = (int)EnumHelp.OrderStatus.已付款;
                orders.PayTime = DateTime.Now;
                paydetail.PayStatus = (int)EnumHelp.PayStatus.已支付;
                paydetail.PayTime = DateTime.Now;
                paydetail.PaySerialNo = transaction_id;
                paydetail.PayType = payWay;
                _repoOrder.Update(orders);
                _payDetail.Update(paydetail);

                //极光推送商家客户端
                Task.Run(() =>
                {
                    var _request = new SnsRequest();
                    _request.UserId = orders.UserId;
                    _request.PushMsg = "您有新的订单啦！";
                    _request.PushUsers = orders.BusinessInfoId.ToString();
                    var dic = new Dictionary<string, string>();
                    dic.Add("orderNo", orders.OrderNo);
                    _request.PushExtras = dic;
                    var _response = _JpushLogBusiness.JpushSendToTag(_request);
                });
            }
        }

        public List<Order> GetOrderListByBueinessId(int bussiness_Id)
        {
            return this._repoOrder.Table.Where(c => c.BusinessInfoId == bussiness_Id).ToList();
        }


        public List<Order> GetBusinessClientOrderList(int business_Id, DateTime start_Time, DateTime end_Time, out int totalCount)
        {
            var where = PredicateBuilder.True<Order>();
            if (business_Id > 0)
            {
                where = where.And(m => m.BusinessInfoId == business_Id);
            }
            if (start_Time != null)
            {
                where = where.And(m => m.PayTime >= start_Time);
            }
            if (end_Time != null)
            {
                where = where.And(m => m.PayTime <= end_Time);
            }
            where = where.And(c => new int[] { 2, 7, 8 }.Contains(c.OrderStatusId));
            totalCount = this._repoOrder.Table.Where(where).Count();
            return this._repoOrder.Table.Where(where).ToList();
        }

        public int UpdateOrderDetailByOrdereNo(int orderId, string orderNo)
        {
            //定义sql
            string sqlStr = @"update OrderDetail set OrderId=" + orderId + " where OrderNo='" + orderNo + "'";
            return this._orderDetail.ExecuteSqlCommand(sqlStr, new String[] { });
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
            bool result = false;
            msg = "";
            if (string.IsNullOrWhiteSpace(order_No))
            {
                msg = "订单编号为空";
                return result;
            }
            var orders = _repoOrder.Table.Where(c => c.OrderNo == order_No).FirstOrDefault();
            if (orders != null && orders.OrderStatusId == (int)EnumHelp.OrderStatus.已使用)
            {
                msg = "订单已经确认，请勿重复确认";
                return result;
            }
            if (orders != null && orders.OrderStatusId == (int)EnumHelp.OrderStatus.已付款)
            {
                if (orders.BusinessInfoId == business_Id)
                {
                    orders.OrderStatusId = (int)EnumHelp.OrderStatus.已使用;
                    _repoOrder.Update(orders);
                    msg = "订单确认成功";
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据用户id商家id获取订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="businessInfoId"></param>
        /// <returns></returns>
        public List<Order> GetOrderNoByUserIdAndBusinessId(int userId, int businessInfoId)
        {
            return _repoOrder.Table.Where(c => c.UserId == userId && c.BusinessInfoId == businessInfoId).ToList();
        }
    }
}


