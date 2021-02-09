using OrderingSystem.Admin.Common;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using Exam.Common;

namespace OrderingSystem.Controllers
{
    public class HomeController : BaseController
    {
        IUserService _userService;
        IOrderService _orderService;
        ISysMenuService _sysMenuService;
        ISysRoleMenuService _sysRoleMenuService;

        private List<int> orderlist = new List<int>();
        public HomeController(IUserService userService,
             IOrderService orderService,
            ISysMenuService sysMenuService,
            ISysRoleMenuService sysRoleMenuService)
        {
            _userService = userService;
            _orderService = orderService;
            _sysMenuService = sysMenuService;
            _sysRoleMenuService = sysRoleMenuService;
        }
        public ActionResult Index()
        {
            var mylist = from p in _sysRoleMenuService.GetAll().Where(p => p.SysRoleId == Loginer.RoleId && p.SysMenu.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && p.SysMenu.Status == (int)EnumHelp.EnabledEnum.有效).OrderBy(p => p.SysMenu.SortNo)
                         select new SysMenu
                         {
                             SysMenuId = p.SysMenu.SysMenuId,
                             Icon = p.SysMenu.Icon,
                             Fid = p.SysMenu.Fid,
                             Name = p.SysMenu.Name,
                             Url = p.SysMenu.Url
                         };
            //查询新订单
            var newPayOrders = _orderService.GetNewOrdersByBusinessId(int.Parse(Loginer.BusinessId));

            var result = from p in newPayOrders
                         select new
                         {
                             Id = p.OrderId
                         };
            foreach (var item in result)
            {
                orderlist.Add(item.Id);
            }

            Session["orderlist"] = orderlist;

            return View(mylist.ToList());
        }

        /// <summary>
        /// 获取新订单
        /// </summary>
        /// <returns></returns> 
        public JsonResult GetNewOrders()
        {
            //查询新订单
            var newPayOrders = _orderService.GetNewOrdersByBusinessId(int.Parse(Loginer.BusinessId));

            var result = from p in newPayOrders
                         select new
                         {
                             Id = p.OrderId
                         };

            int newCount = 0;

            orderlist = (List<int>)Session["orderlist"];

            foreach (var item in result)
            {
                var orderId = item.Id;
                //判断是否是新订单
                if (!orderlist.Contains(orderId))
                {
                    orderlist.Add(orderId);
                    newCount++;
                }
            }

            //如果有新订单 提醒通知 
            return Json(new { Status = Successed.Ok, Data = newCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Console()
        {

            return View("Console_User");
        }

    }
}