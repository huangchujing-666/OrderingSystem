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
    public class BusinessImageController : BaseController
    {
        IUserService _userService;
        IBusinessImageService _businessImageService;
        IBaseAreaService _baseAreaService;
        IBaseLineService _baseLineService;
        IBaseStationService _baseStationService;

        public BusinessImageController(IUserService userService,
            IBusinessImageService businessImageService,
            IBaseAreaService baseAreaService,
              IBaseLineService baseLineService,
              IBaseStationService baseStationService)
        {
            _userService = userService;
            _businessImageService = businessImageService;
            _baseAreaService = baseAreaService;
            _baseLineService = baseLineService;
            _baseStationService = baseStationService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BusinessImageVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //获取当前用户角色
            vm.RoleId = Loginer.RoleId;
            if (vm.RoleId == (int)RoleTypeEnum.商家)
            {
                vm.Id = int.Parse(Loginer.BusinessId);
            }
            if (vm.BusinessId>0)
            {
                vm.Id = vm.BusinessId;
            }
            var list = _businessImageService.GetManagerList(vm.Id, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BusinessImage>()
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
        public ActionResult Edit(BusinessImageVM vm)
        {
             
            vm.BusinessImage = _businessImageService.GetById(vm.Id) ?? new BusinessImage();
            vm.ImgInfo = vm.BusinessImage.BaseImage ?? new BaseImage();
            if (vm.BusinessImage.BusinessInfoId == 0 )
            {
                vm.BusinessImage.BusinessInfoId = vm.BusinessId;
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
        public JsonResult Edit(BusinessImage model)
        {
            try
            {
                if (model.BusinessImageId > 0)
                {
                    var entity = _businessImageService.GetById(model.BusinessImageId);
                    //修改
                    entity.BaseImageId = model.BaseImageId;
                    entity.SortNo = model.SortNo;
                    entity.Type = model.Type; 

                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    _businessImageService.Update(model);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if (_businessImageService.CheckBusinessName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加 
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreatePersonId = Loginer.AccountId;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now; 
                    model = _businessImageService.Insert(model);
                    if (model.BusinessImageId > 0)
                    {
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    } 
                }
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
                var entity = _businessImageService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _businessImageService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
        public JsonResult UpdateStatus(int id = 0, OrderingSystem.Domain.EnumHelp.EnabledEnum isEnabled = EnumHelp.EnabledEnum.有效)
        {
            try
            {
                var entity = _businessImageService.GetById(id);
                entity.Status = (int)isEnabled;

                _businessImageService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}