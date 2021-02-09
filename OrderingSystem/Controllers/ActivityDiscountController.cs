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
    public class ActivityDiscountController : BaseController
    {
        private readonly IActivityDiscountService _activityDiscountService;
        private readonly IBusinessInfoService _businessInfo;
        public ActivityDiscountController(IActivityDiscountService activityDiscountService, IBusinessInfoService businessInfo)
        {
            _activityDiscountService = activityDiscountService;
            _businessInfo = businessInfo;
        }

        /// <summary>
        /// 商家折扣列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(ActivityDiscountVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _businessInfo.GetManagerList(vm.QueryName,vm.QueryType, pageIndex, pageSize, out totalCount);
            foreach (var item in list)
            {
                if (item.ActivityMinusList != null)
                {
                    item.ActivityMinusList.OrderByDescending(c => c.AchiveAmount);
                }
            }
            var paging = new Paging<BusinessInfo>()
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
        /// <param name="ActivityDiscountVM"></param>
        /// <returns></returns>
        public ActionResult Edit(ActivityDiscountVM _activityDiscountVM)
        {
            var businessInfo = _businessInfo.GetById(_activityDiscountVM.BusinessInfoId);
            if (businessInfo != null)
            {
                if (businessInfo.ActivityDiscount!=null)
                {
                    _activityDiscountVM.Name = businessInfo.Name ?? "";
                    _activityDiscountVM.Discount = businessInfo.ActivityDiscount.Discount;
                    _activityDiscountVM.ActivityDiscountId = businessInfo.ActivityDiscount.ActivityDiscountId;
                }
                else
                {
                     _activityDiscountVM.Name = businessInfo.Name;
                    _activityDiscountVM.Discount = 1;
                    _activityDiscountVM.ActivityDiscountId = 0;
                }
                _activityDiscountVM.Paging = new Paging<BusinessInfo>();
                _activityDiscountVM.QueryName = "";
                _activityDiscountVM.ActivityDiscount = new ActivityDiscount();
            }
            return View(_activityDiscountVM);
        }


        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ActivityDiscount model,int businessInfoId)
        {
            var businessInfo = _businessInfo.GetById(businessInfoId);
            if (businessInfo==null) 
            {
                return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
            }
        
            //更新
            if (model.ActivityDiscountId > 0 )
            {
                var activityDiscount=   _activityDiscountService.GetById(model.ActivityDiscountId);
                activityDiscount.EditPersonId = Loginer.AccountId;
                activityDiscount.EditTime = DateTime.Now;
                activityDiscount.Discount = model.Discount;
                try
                {
                    _activityDiscountService.Update(activityDiscount);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }//插入
            else
            {
                //添加
                model.CreatePersonId = Loginer.AccountId;
                model.EditPersonId = Loginer.AccountId;
                model.Status = (int)EnumHelp.EnabledEnum.有效;
                model.IsDelete = (int)EnumHelp.IsDeleteEnum.有效;
                model.CreateTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                var activityDiscount = _activityDiscountService.Insert(model);

                businessInfo.ActivityDiscountId = activityDiscount.ActivityDiscountId;
                _businessInfo.Update(businessInfo);

                return Json(new { Status = activityDiscount.ActivityDiscountId>0?Successed.Ok:Successed.Error }, JsonRequestBehavior.AllowGet);
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
            if (id<= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var activityDiscount = _activityDiscountService.GetById(id);
                activityDiscount.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _activityDiscountService.Update(activityDiscount);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int ActivityDiscountId = 0, int Status = 0)
        {
            if (ActivityDiscountId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var activityDiscount = _activityDiscountService.GetById(ActivityDiscountId);
                activityDiscount.Status = Status;
                _activityDiscountService.Update(activityDiscount);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}