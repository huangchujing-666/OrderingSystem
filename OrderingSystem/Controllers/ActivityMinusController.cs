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
    public class ActivityMinusController : BaseController
    {
        private readonly IActivityMinusService _activityMinusService;
        private readonly IBusinessInfoService _businessInfo;
        public ActivityMinusController(IActivityMinusService activityMinusService, IBusinessInfoService businessInfo)
        {
            _activityMinusService = activityMinusService;
            _businessInfo = businessInfo;
        }
        /// <summary>
        /// 商家满减列表
        /// </summary>
        /// <param name="_activityManjianVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(ActivityMinusVM _activityMinusVM, int pn = 1)
        {
            var list = _activityMinusService.GetListByBusinessId(_activityMinusVM.BusinessInfoId);
            var paging = new Paging<ActivityMinus>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = list.Count,
                Index = pn,
            };
            _activityMinusVM.Paging = paging;
            return View(_activityMinusVM);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ActivityDiscountVM"></param>
        /// <returns></returns>
        public ActionResult Edit(ActivityMinusVM vm)
        {
            vm.ActivityMinus = _activityMinusService.GetById(vm.ActivityMinusId) ?? new ActivityMinus();
            if (vm.ActivityMinusId == 0)
            {
                if (vm.BusinessInfoId > 0)
                {
                    vm.BusinessInfoId = vm.BusinessInfoId;
                }
                else
                {
                    vm.BusinessInfoId = int.Parse(Loginer.BusinessId);
                } 
            } 
            return View(vm);

        }

        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ActivityMinus model)
        {
            if (model.ActivityMinusId > 0)
            {
                var activityMinus = _activityMinusService.GetById(model.ActivityMinusId);
                //修改
                activityMinus.EditPersonId = Loginer.AccountId;
                activityMinus.EditTime = DateTime.Now;
                activityMinus.MinusAmount = model.MinusAmount;
                activityMinus.AchiveAmount = model.AchiveAmount;
                try
                {
                    _activityMinusService.Update(model);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (_activityMinusService.InsertVerifyRepeat(model.BusinessInfoId, model.AchiveAmount) != null)
                    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                //添加
                model.Status = (int)EnumHelp.EnabledEnum.有效;
                model.IsDelete = (int)EnumHelp.IsDeleteEnum.有效;
                model.CreateTime = DateTime.Now;
                model.CreatePersonId = Loginer.AccountId;
                model.EditPersonId = Loginer.AccountId;
                model.EditTime = DateTime.Now;
                if (_activityMinusService.Insert(model).ActivityMinusId > 0)
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int ActivityMinusId = 0)
        {
            var activityMinus = _activityMinusService.GetById(ActivityMinusId);
            if (activityMinus != null)
            {
                activityMinus.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _activityMinusService.Update(activityMinus);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            else
            { return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet); }
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int ActivityMinusId = 0, int Status = 0)
        {
            var activityMinus = _activityMinusService.GetById(ActivityMinusId);
            if (activityMinus != null)
            {
                activityMinus.Status = Status;
                _activityMinusService.Update(activityMinus);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            else
            { return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet); }
        }
    }
}