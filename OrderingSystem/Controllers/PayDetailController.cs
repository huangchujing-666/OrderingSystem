using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
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
    public class PayDetailController : BaseController
    {
        private readonly IPayDetailService _payDetailService; 

        public PayDetailController(IPayDetailService payDetailService)
        {
            _payDetailService = payDetailService; 
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(PayDetailVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _payDetailService.GetManagerList(vm.QueryOrderNo,vm.QueryUserName,vm.QueryPayStatus,vm.QueryStartTime,vm.QueryEndTime, pageIndex, pageSize, out totalCount);
            var paging = new Paging<PayDetail>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging; 
            return View(vm);
        }


        public ActionResult PayDetailExport(string QueryOrderNo, string QueryUserName, int QueryPayStatus, string QueryStartTime, string QueryEndTime)
        {
            try
            {
                var list = _payDetailService.PayDetailExpert(QueryOrderNo, QueryUserName, QueryPayStatus, QueryStartTime, QueryEndTime);
                var result = PayDetailToExpert(list);
                var dt = EntityConverter<PayDetailExpert>.CreateDataTableByAnyListEntity(result);
                if (dt == null || dt.Rows.Count <= 0)
                    return Content("<script type='text/javascript'>alert('无查询结果，请检查。'); window.location.href = '/Order/List'</script>");
                string[] expColNames = { "PayDetailId", "OrderNo", "RealAmount", "NickName", "PhoneNo", "Remark", "OrderTime", "PayTime", "PayType", "PaySerialNo", "PayStatus"};
                Dictionary<string, string> expColAsNames = new Dictionary<string, string>() {
                    { "PayDetailId","序号"},
                    { "OrderNo","订单编号"},
                    { "RealAmount","订单金额"},
                    { "NickName","下单人"},
                    { "PhoneNo","下单人电话"},
                    { "Remark","备注"},
                    { "OrderTime","订单时间"},
                    { "PayTime","支付时间"},
                    { "PayType","支付方式"},
                    { "PaySerialNo","支付流水号"},
                    { "PayStatus","支付状态"}
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
        public List<PayDetailExpert> PayDetailToExpert(List<PayDetail> list)
        {
            var result = new List<PayDetailExpert>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    result.Add(new PayDetailExpert()
                    {
                        PayDetailId = item.PayDetailId,
                        NickName = item.User == null ? "" : item.User.NickName,
                        PhoneNo = item.User == null ? "" : item.User.PhoneNo,
                        OrderNo = item.OrderNo,
                        OrderTime = item.OrderTime,
                        RealAmount = item.Amount,
                        PayStatus = item.PayStatus == 2 ? "已支付" : (item.PayStatus==1? "未支付": "支付超时"),
                        PaySerialNo = item.PaySerialNo,
                        PayTime = item.PayTime,
                        PayType = item.PayType == 1 ?  "微信": "支付宝",
                        Remark = item.Remark,
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(PayDetailVM vm)
        {
            //_orderVM.Order = _orderService.GetDetailByOrderNo(_orderVM.QueryOrderNo) ?? new Order();
            //_orderVM.OrderDetailList = _orderDetailService.GetListByOrderNo(_orderVM.QueryOrderNo);
            return View(vm);
        }
    }
}