using Exam.Common;
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Controllers
{
    public class BusinessController : BaseController
    {
        private readonly IActivityDiscountService _activityDiscountService;
        private readonly IActivityMinusService _activityManjianService;
        IUserService _userService;
        IBusinessInfoService _businessInfoService;
        IBaseAreaService _baseAreaService;
        IBaseLineService _baseLineService;
        IBaseStationService _baseStationService;
        ISysAccountService _sysAccountService;

        public BusinessController(IActivityDiscountService activityDiscountService,
            IActivityMinusService activityManjianService,
            IUserService userService,
            IBusinessInfoService businessInfoService,
            IBaseAreaService baseAreaService,
              IBaseLineService baseLineService,
              IBaseStationService baseStationService,
                ISysAccountService sysAccountService)
        {
            _activityDiscountService = activityDiscountService;
            _activityManjianService = activityManjianService;
            _userService = userService;
            _businessInfoService = businessInfoService;
            _baseAreaService = baseAreaService;
            _baseLineService = baseLineService;
            _baseStationService = baseStationService;
            _sysAccountService = sysAccountService;
        }


        /// <summary>
        /// 商家折扣列表
        /// </summary>
        /// <param name="_activityDiscountVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult Discount(ActivityDiscountVM _activityDiscountVM, int pn = 1)
        {
           // _activityDiscountVM.ActivityDiscount = _activityDiscountService.GetByBusinessId(int.Parse(Loginer.BusinessId)) ?? new ActivityDiscount();
            _activityDiscountVM.ActivityDiscount = _businessInfoService.GetById(int.Parse(Loginer.BusinessId)).ActivityDiscount ?? new ActivityDiscount();

            _activityDiscountVM.IsSupport = _activityDiscountVM.ActivityDiscount.ActivityDiscountId > 0 ? 1 : 0;

            var list = _activityManjianService.GetListByBusinessId(int.Parse(Loginer.BusinessId));

            _activityDiscountVM.ActivityMinusList = list;

            return View(_activityDiscountVM);
        }


        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ActivityDiscount model)
        {
            try
            {
                var businessInfo = _businessInfoService.GetById(int.Parse(Loginer.BusinessId));
                if (businessInfo == null)
                {
                    return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
                }


                if (model.ActivityDiscountId > 0)
                {
                    var entity = _activityDiscountService.GetById(model.ActivityDiscountId);
                    entity.Discount = model.Discount;

                    entity.EditPersonId = Loginer.AccountId;
                    entity.EditTime = DateTime.Now;
                    //修改
                    _activityDiscountService.Update(entity);

                }
                else
                {
                    //添加
                    //model.BusinessInfoId = int.Parse(Loginer.BusinessId);
                    model.CreatePersonId = Loginer.AccountId;
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    model.ActivityDiscountId = 0;
                    model =  _activityDiscountService.Insert(model);


                    businessInfo.ActivityDiscountId = model.ActivityDiscountId;
                    _businessInfoService.Update(businessInfo);
                }
            }
            catch (Exception)
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
                var entity = _activityDiscountService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _activityDiscountService.Update(entity); 
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int id = 0, int isEnabled = 0)
        {
            try
            {
                var entity = _activityDiscountService.GetById(id);
                entity.Status = isEnabled;

                _activityDiscountService.Update(entity);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }
    }
}