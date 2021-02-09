
using Exam.Common;
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Admin.Controllers
{
    public class TicketRelateTicketController : BaseController
    {
        // GET: TicketRelateTicket
        private readonly ITicketRelateTicketService _TicketRelateTicketService;
        private readonly IBusinessInfoService _businessService;
        private readonly ITicketService _ticketService;


        public TicketRelateTicketController(ITicketRelateTicketService TicketRelateTicketService,
            IBusinessInfoService businessService, ITicketService ticketService)
        {
            _TicketRelateTicketService = TicketRelateTicketService;
            _businessService = businessService;
            _ticketService = ticketService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(TicketRelateTicketVM vm, int pn = 1)
        {

            int total = 0,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //分页查询
            var list = _TicketRelateTicketService.GetManagerList(vm.Id, pageIndex, pageSize, out total);
            var paging = new Paging<TicketRelateTicket>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = total,
                Index = pn,
            };
            vm.Paging = paging;

            vm.TicketList = _ticketService.GetAll();

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(TicketRelateTicketVM vm)
        {
            vm.TicketRelateTicket = _TicketRelateTicketService.GetById(vm.Id) ?? new TicketRelateTicket();
            if (vm.TicketRelateTicket.TicketRelateTicketId==0)
            {
                vm.TicketRelateTicket.TicketId = vm.TicketId;
            }

            vm.TicketList = _ticketService.GetAll().Where(p => p.Status == 1 && p.IsDelete == 0).ToList();

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(TicketRelateTicket model, string Business_Ids)
        {
            try
            {

                if (model.TicketRelateTicketId > 0)
                {
                    var entity = _TicketRelateTicketService.GetById(model.TicketRelateTicketId);
                    //修改
                    entity.RelateTicketId = model.RelateTicketId;
                    entity.Count = model.Count;

                    //执行更新
                    _TicketRelateTicketService.Update(entity);

                }
                else
                {
                    //新增  
                    _TicketRelateTicketService.Insert(model);

                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            try
            {
                var entity = _TicketRelateTicketService.GetById(id);

                _TicketRelateTicketService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}