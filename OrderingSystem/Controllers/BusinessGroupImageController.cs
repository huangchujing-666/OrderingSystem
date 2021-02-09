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
    public class BusinessGroupImageController : BaseController
    {
        IUserService _userService;
        IBusinessGroupImageService _BusinessGroupImageService; 
        IBusinessGroupService _BusinessGroupService;

        public BusinessGroupImageController(IUserService userService,
            IBusinessGroupImageService BusinessGroupImageService,
            IBusinessGroupService BusinessGroupService)
        {
            _userService = userService;
            _BusinessGroupImageService = BusinessGroupImageService;
            _BusinessGroupService = BusinessGroupService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BusinessGroupImageVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE; 
            var list = _BusinessGroupImageService.GetManagerList(vm.Id, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BusinessGroupImage>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;
            vm.BusinessGroup = _BusinessGroupService.GetById(vm.Id);

            return View(vm);
        } 

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult Edit(BusinessGroupImageVM vm)
        {
             
            vm.BusinessGroupImage = _BusinessGroupImageService.GetById(vm.Id) ?? new BusinessGroupImage();
            vm.ImgInfo = vm.BusinessGroupImage.BaseImage ?? new BaseImage();
            if (vm.BusinessGroupImage.BusinessGroupImageId == 0)
            {
                vm.BusinessGroupImage.BusinessGroupId = vm.GroupId;
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
        public JsonResult Edit(BusinessGroupImage model)
        {
            try
            {
                if (model.BusinessGroupImageId > 0)
                {
                    var entity = _BusinessGroupImageService.GetById(model.BusinessGroupImageId);
                    //修改
                    entity.BaseImageId = model.BaseImageId; 
                    entity.Type = model.Type; 
                    entity.BusinessGroupId = model.BusinessGroupId;

                    model.EditTime = DateTime.Now;
                    _BusinessGroupImageService.Update(model);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if (_BusinessGroupImageService.CheckBusinessName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加  
                    model.CreateTime = DateTime.Now; 
                    model.EditTime = DateTime.Now; 
                    model = _BusinessGroupImageService.Insert(model);
                    if (model.BusinessGroupImageId > 0)
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
                var entity = _BusinessGroupImageService.GetById(id); 

                _BusinessGroupImageService.Delete(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
 
    }
}