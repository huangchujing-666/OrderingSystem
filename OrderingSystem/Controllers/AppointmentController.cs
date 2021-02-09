
using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LOT.Admin.Controllers
{
    /// <summary>
    /// 线路控制器
    /// </summary>
    public class AppointmentController : BaseController
    {
        // GET: Appointment
        private readonly IAppointmentService _AppointmentService;
        private readonly IGoodsService _goodsService;

        public AppointmentController(IAppointmentService AppointmentService, IGoodsService goodsService)
        {
            _AppointmentService = AppointmentService;
            _goodsService = goodsService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="_dictionariesVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(AppointmentVM vm, int pn = 1)
        {
            int totalCount,
                    pageIndex = pn,
                    pageSize = PagingConfig.PAGE_SIZE;
            var list = _AppointmentService.GetManagerList(vm.QueryUserName, vm.QueryPhone, pageIndex, pageSize, out totalCount);
            var paging = new Paging<Appointment>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;
            return View(vm);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult Edit(AppointmentVM vm)
        {
            vm.Appointment = _AppointmentService.GetById(vm.Id) ?? new Appointment();
            return View(vm);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult Detail(AppointmentVM vm)
        {
            vm.Appointment = _AppointmentService.GetById(vm.Id) ?? new Appointment();
            if (vm.Appointment != null)
            {
                var ids = Array.ConvertAll(vm.Appointment.ValueIds.Split(','), int.Parse);

                vm.GoodsList = _goodsService.GetGoodsByIds(ids.ToList());
                 
            }
            return View(vm);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Appointment model)
        {
            try
            {
                if (model.AppointmentId > 0)
                {
                    var entity = _AppointmentService.GetById(model.AppointmentId);

                    //修改  
                    entity.EditTime = DateTime.Now;

                    _AppointmentService.Update(model);
                }
                else
                {
                    //if (_baseAreaService.CheckName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加
                    model.Status = (int)OrderingSystem.Domain.EnumHelp.EnabledEnum.有效;
                    model.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;

                    _AppointmentService.Insert(model);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
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
                var entity = _AppointmentService.GetById(id);
                entity.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.已删除;
                entity.EditTime = DateTime.Now;
                _AppointmentService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int id = 0, int isEnabled = 0, string remark = "")
        {
            try
            {
                var entity = _AppointmentService.GetById(id);
                entity.Status = isEnabled;
                entity.DenyReason = remark;
                entity.EditTime = DateTime.Now;
                _AppointmentService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }
    }
}