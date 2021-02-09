using Exam.Common;
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingSystem.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderStatusLogService _orderStatusLogService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IPayDetailService _payDetailService;
        private readonly IBusinessPayConfigService _payConfigService;

        public OrderController(IOrderService orderService, IOrderStatusLogService orderStatusLogService,
            IOrderDetailService orderDetailService, IPayDetailService payDetailService, IBusinessPayConfigService payConfigService)
        {
            _orderService = orderService;
            _orderStatusLogService = orderStatusLogService;
            _orderDetailService = orderDetailService;
            _payDetailService = payDetailService;
            _payConfigService = payConfigService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(OrderVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            decimal totalAmount;
            var list = _orderService.GetListBySearch(vm.QueryBusinessName, vm.QueryUserName, vm.QueryOrderNo, vm.QueryOrderStatusId, vm.QueryStartTime, vm.QueryEndTime, pageIndex, pageSize, out totalCount, out totalAmount, int.Parse(Loginer.BusinessId));
            var paging = new Paging<Order>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.TotalAmount = totalAmount;
            vm.Paging = paging;
            vm.OrderDetailList = null;
            return View(vm);
        }


        /// <summary>
        /// 订单导出
        /// </summary>
        /// <param name="businessName">商家名称</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="userAccount">下单人</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ActionResult OrderExport(string QueryBusinessName, string QueryOrderNo, string QueryUserName, int QueryOrderStatusId, string QueryStartTime, string QueryEndTime)
        {
            try
            {
                var list = _orderService.OrderExpert(QueryBusinessName, QueryOrderNo, QueryUserName, QueryOrderStatusId, QueryStartTime, QueryEndTime, int.Parse(Loginer.BusinessId));
                var result = OrderToOrderExpert(list);
                var dt = EntityConverter<OrderExpert>.CreateDataTableByAnyListEntity(result);
                if (dt == null || dt.Rows.Count <= 0)
                    return Content("<script type='text/javascript'>alert('无查询结果，请检查。'); window.location.href = '/Order/List'</script>");
                string[] expColNames = { "OrderId", "OrderNo", "RealAmount", "NickName", "PhoneNo", "BusinessName", "StatusName", "OrderTime" };
                Dictionary<string, string> expColAsNames = new Dictionary<string, string>() {
                    { "OrderId","序号"},
                    { "OrderNo","订单编号"},
                    { "RealAmount","订单金额"},
                    { "NickName","下单人"},
                    { "PhoneNo","下单人电话"},
                    { "BusinessName","商家名称"},
                    { "StatusName","订单状态"},
                    { "OrderTime","创建时间"}
                };
                var ms = ExcelHelp.ToExcel(dt, null, expColNames, expColAsNames, null, 0);
                return File(ms, "application/vnd.ms-excel", ExcelHelp.CreateFileName("D"));
            }
            catch (Exception ex)
            {
                //Logger.Error("OrdersController--->OrderExport：" + ex.ToString());
            }
            return Content("<script type='text/javascript'>alert('Execl生成失败，请检查。'); window.location.href = '/Order/List'</script>");
        }

        public List<OrderExpert> OrderToOrderExpert(List<Order> list)
        {
            var result = new List<OrderExpert>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    result.Add(new OrderExpert()
                    {
                        BusinessName = item.BusinessInfo == null ? "" : item.BusinessInfo.Name,
                        NickName = item.User == null ? "" : item.User.NickName,
                        PhoneNo = item.User == null ? "" : item.User.PhoneNo,
                        OrderId = item.OrderId,
                        OrderNo = item.OrderNo,
                        OrderTime = item.OrderTime,
                        RealAmount = item.RealAmount,
                        StatusName = item.OrderStatus == null ? "" : item.OrderStatus.Name
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(OrderVM _orderVM)
        {
            _orderVM.Order = _orderService.GetDetailByOrderNo(_orderVM.QueryOrderNo) ?? new Order();
            _orderVM.OrderDetailList = _orderDetailService.GetListByOrderNo(_orderVM.QueryOrderNo);
            return View(_orderVM);
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int id = 0, int status = 0, string statusName = "")
        {
            if (status <= 0)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var oderModel = _orderService.GetById(id);
                oderModel.OrderStatusId = status;

                _orderService.Update(oderModel);

                var model = new OrderStatusLog()
                {
                    OrderId = id,
                    Status = status,
                    StatusName = statusName,
                    CreateTime = DateTime.Now
                };
                _orderStatusLogService.Insert(model);

                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 编辑支付信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PayEdit(OrderVM vm)
        {
            var oderModel = _orderService.GetById(vm.Id);
            int totalCount = 0;
            vm.BusinessPayConfigList = _payConfigService.GetManagerList(int.Parse(Loginer.BusinessId), 1, 10, out totalCount);

            return View(vm);
        }

        /// <summary>
        /// 写入支付信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payConfigId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public JsonResult WritePayInfo(int id, int payConfigId, string amount)
        {
            try
            {
                //获取订单对象
                var oderModel = _orderService.GetById(id);
                oderModel.OrderStatusId = (int)EnumHelp.OrderStatus.已付款;

                PayDetail pd = _payDetailService.GetDetailByOrderNo(oderModel.OrderNo);
                 
                pd.Remark = "";
                pd.PayType = (int)EnumHelp.PayType.商家付款;
                pd.PayTime = DateTime.Now;
                pd.PayStatus = (int)EnumHelp.PayStatus.已支付; 
                pd.Amount = decimal.Parse(amount);
                //写入支付信息
                _payDetailService.Update(pd);
                //更新订单状态
                _orderService.Update(oderModel);
                //记录订单状态变更信息
                var model = new OrderStatusLog()
                {
                    OrderId = id,
                    Status = (int)EnumHelp.OrderStatus.已付款,
                    StatusName = EnumHelp.OrderStatus.已付款.ToString(),
                    CreateTime = DateTime.Now
                };
                _orderStatusLogService.Insert(model);

                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}