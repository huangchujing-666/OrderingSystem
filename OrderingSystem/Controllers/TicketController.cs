
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
    public class TicketController : BaseController
    {
        // GET: Ticket
        private readonly ITicketService _TicketService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;


        public TicketController(ITicketService TicketService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService)
        {
            _TicketService = TicketService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(TicketVM vm, int pn = 1)
        {

            int total = 0,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //分页查询
            var list = _TicketService.GetManagerList(vm.QueryName, vm.QueryBusinessmanName, pageIndex, pageSize, out total, int.Parse(Loginer.BusinessId));
            var paging = new Paging<Ticket>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = total,
                Index = pn,
            };
            vm.Paging = paging;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(TicketVM vm)
        {
            vm.Ticket = _TicketService.GetById(vm.Id) ?? new Ticket();
            //获取餐厅列表
            int tcount = 0;
            vm.BusinessList = _businessService.GetListByType((int)BusinessTypeEnum.景点, 1, 1000, out tcount);

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(Ticket model, string Business_Ids)
        {
            try
            { 
                 
                if (model.TicketId > 0)
                {
                    var entity = _TicketService.GetById(model.TicketId);
                    //修改
                    entity.Name = model.Name;
                    entity.OrignPrice = model.OrignPrice;
                    entity.RealPrice = model.RealPrice; 
                    entity.BusinessInfoId = model.BusinessInfoId; 
                    entity.Remark = model.Remark; 
                    entity.Notice = model.Notice;
                    entity.Rules = model.Rules;
                    entity.BindCard = model.BindCard;
                    entity.UseCount = model.UseCount;
                    //entity.RelatedTicketHotelId = model.RelatedTicketHotelId;
                    //entity.RelatedTicketId = model.RelatedTicketId;
                    //entity.RelatedTicketRoomId = model.RelatedTicketRoomId;
                    entity.Rules = model.Special; 

                    entity.EditTime = DateTime.Now;

                    //执行更新
                    _TicketService.Update(entity);

                }
                else
                {
                    //新增 
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效; 
                    model.CreateTime = DateTime.Now; 
                    model.EditTime = DateTime.Now;
                    _TicketService.Insert(model);

                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int id = 0, OrderingSystem.Domain.EnumHelp.EnabledEnum isEnabled = EnumHelp.EnabledEnum.有效)
        {
            try
            {
                var entity = _TicketService.GetById(id);
                entity.Status = (int)isEnabled;

                _TicketService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
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
                var entity = _TicketService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _TicketService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }
         
    }
}