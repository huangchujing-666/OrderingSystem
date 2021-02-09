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
    public class RoomImageController : BaseController
    {
        IUserService _userService;
        IRoomImageService _RoomImageService;
        IBaseAreaService _baseAreaService;
        IBaseLineService _baseLineService;
        IBaseStationService _baseStationService;

        public RoomImageController(IUserService userService,
            IRoomImageService RoomImageService,
            IBaseAreaService baseAreaService,
              IBaseLineService baseLineService,
              IBaseStationService baseStationService)
        {
            _userService = userService;
            _RoomImageService = RoomImageService;
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
        public ActionResult List(RoomImageVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _RoomImageService.GetManagerList(vm.Id, pageIndex, pageSize, out totalCount);
            var paging = new Paging<RoomImage>()
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
        public ActionResult Edit(RoomImageVM vm)
        {

            vm.RoomImage = _RoomImageService.GetById(vm.Id) ?? new RoomImage();
            vm.ImgInfo = vm.RoomImage.BaseImage ?? new BaseImage();
            if (vm.RoomImage.RoomId == 0 )
            {
                vm.RoomImage.RoomId = vm.RoomId;
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
        public JsonResult Edit(RoomImage model)
        {
            try
            {
                if (model.RoomImageId > 0)
                {
                    var entity = _RoomImageService.GetById(model.RoomImageId);

                    //修改
                    entity.BaseImageId = model.BaseImageId; 
                    entity.Type = model.Type; 

                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    _RoomImageService.Update(model);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if (_RoomImageService.CheckBusinessName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加 
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreatePersonId = Loginer.AccountId;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now; 
                    model = _RoomImageService.Insert(model);
                    if (model.RoomImageId > 0)
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
                var entity = _RoomImageService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _RoomImageService.Update(entity);
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
                var entity = _RoomImageService.GetById(id);
                entity.Status = (int)isEnabled;

                _RoomImageService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}