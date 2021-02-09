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
    public class DishesImageController : BaseController
    {
        IUserService _userService;
        IDishesImageService _dishesImageService;
        IBaseAreaService _baseAreaService;
        IBaseLineService _baseLineService;
        IBaseStationService _baseStationService;

        public DishesImageController(IUserService userService,
            IDishesImageService dishesImageService,
            IBaseAreaService baseAreaService,
              IBaseLineService baseLineService,
              IBaseStationService baseStationService)
        {
            _userService = userService;
            _dishesImageService = dishesImageService;
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
        public ActionResult List(DishesImageVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _dishesImageService.GetManagerList(vm.Id, pageIndex, pageSize, out totalCount);
            var paging = new Paging<DishesImage>()
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
        public ActionResult Edit(DishesImageVM vm)
        {

            vm.DishesImage = _dishesImageService.GetById(vm.Id) ?? new DishesImage();
            vm.ImgInfo = vm.DishesImage.BaseImage ?? new BaseImage();
            if (vm.DishesImage.DishesId == 0 )
            {
                vm.DishesImage.DishesId = vm.DishesId;
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
        public JsonResult Edit(DishesImage model)
        {
            try
            {
                if (model.DishesImageId > 0)
                {
                    var entity = _dishesImageService.GetById(model.DishesImageId);

                    //修改
                    entity.BaseImageId = model.BaseImageId; 
                    entity.Type = model.Type; 

                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    _dishesImageService.Update(model);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if (_dishesImageService.CheckBusinessName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加 
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreatePersonId = Loginer.AccountId;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now; 
                    model = _dishesImageService.Insert(model);
                    if (model.DishesImageId > 0)
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
                var entity = _dishesImageService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _dishesImageService.Update(entity);
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
                var entity = _dishesImageService.GetById(id);
                entity.Status = (int)isEnabled;

                _dishesImageService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}