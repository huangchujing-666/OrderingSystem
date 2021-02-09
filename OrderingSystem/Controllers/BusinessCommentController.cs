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

namespace OrderingSystem.Controllers
{
    public class BusinessCommentController : BaseController
    {
        private readonly IBusinessCommentService _businessCommentService;
        private readonly ILevelService _levelService;
        public BusinessCommentController(IBusinessCommentService businessCommentService, ILevelService levelService)
        {
            _businessCommentService = businessCommentService;
            _levelService = levelService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BusinessCommentVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _businessCommentService.GetManagerList(vm.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BusinessComment>()
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
        public ActionResult Edit(BusinessCommentVM _businessCommentVM)
        {
            var businessComment = _businessCommentService.GetById(_businessCommentVM.BusinessCommentId);
            if (businessComment != null)
            {
                _businessCommentVM.BusinessCommentId = businessComment.BusinessCommentId;
                _businessCommentVM.BusinessName = businessComment.BusinessInfo == null ? "" : businessComment.BusinessInfo.Name;
                _businessCommentVM.Contents = businessComment.Contents;
                _businessCommentVM.NickName = businessComment.User == null ? "" : businessComment.User.NickName;
                _businessCommentVM.Paging = new Paging<BusinessComment>();
                _businessCommentVM.QueryName = "";
                _businessCommentVM.BusinessInfoId = businessComment.BusinessInfoId;
                _businessCommentVM.LevelId = businessComment.LevelId.ToString();
                _businessCommentVM.UserId = businessComment.UserId;
                _businessCommentVM.IsAnonymous = businessComment.IsAnonymous;
                _businessCommentVM.BusinessComment = businessComment;  
                _businessCommentVM.Levels = _levelService.GetList();
            }
            else
            {
                _businessCommentVM.Paging = new Paging<BusinessComment>();
                _businessCommentVM.BusinessComment = businessComment;
            }
            return View(_businessCommentVM);
        }


        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(BusinessComment model)
        {
            //更新
            if (model.BusinessCommentId > 0 && model.BusinessInfoId > 0&&model.UserId>0)
            {
                var businessComment = _businessCommentService.GetById(model.BusinessCommentId);
                businessComment.Contents = model.Contents;
                businessComment.LevelId = model.LevelId;
                businessComment.RecommendDishes = model.RecommendDishes;
                businessComment.IsAnonymous = model.IsAnonymous;
                try
                {
                    _businessCommentService.Update(businessComment);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }//插入
            else
            {
                return Json(new { Status =  Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int BusinessCommentId = 0)
        {
            if (BusinessCommentId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var businessComment = _businessCommentService.GetById(BusinessCommentId);
                businessComment.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _businessCommentService.Update(businessComment);
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
        public JsonResult UpdateStatus(int BusinessCommentId = 0, int Status = 0)
        {
            if (BusinessCommentId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var businessComment = _businessCommentService.GetById(BusinessCommentId);
                businessComment.Status = Status;
                _businessCommentService.Update(businessComment);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}