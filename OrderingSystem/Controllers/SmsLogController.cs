using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;

namespace OrderingSystem.Controllers
{
    public class SmsLogController : BaseController
    {
        private readonly ISmsLogService _smsLogService;
        public SmsLogController(ISmsLogService userService)
        {
            _smsLogService = userService;
        }
        
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_userVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(SmsLogVM _smsLogVM, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _smsLogService.GetManagerList(_smsLogVM.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<SmsLog>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            _smsLogVM.Paging = paging;
            return View(_smsLogVM);
        }

    }
}