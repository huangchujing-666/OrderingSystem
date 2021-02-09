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
    public class GoodsImageController : BaseController
    {
        IUserService _userService;
        IGoodsImageService _GoodsImageService; 

        public GoodsImageController(IUserService userService,
            IGoodsImageService GoodsImageService)
        {
            _userService = userService;
            _GoodsImageService = GoodsImageService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(GoodsImageVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _GoodsImageService.GetManagerList(vm.Id, pageIndex, pageSize, out totalCount);
            var paging = new Paging<GoodsImage>()
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
        public ActionResult Edit(GoodsImageVM vm)
        {

            vm.GoodsImage = _GoodsImageService.GetById(vm.Id) ?? new GoodsImage();
            vm.ImgInfo = vm.GoodsImage.BaseImage ?? new BaseImage();
            if (vm.GoodsImage.GoodsId == 0 )
            {
                vm.GoodsImage.GoodsId = vm.GoodsId;
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
        public JsonResult Edit(GoodsImage model)
        {
            try
            {
                if (model.GoodsImageId > 0)
                {
                    var entity = _GoodsImageService.GetById(model.GoodsImageId);

                    //修改
                    entity.BaseImageId = model.BaseImageId; 
                    entity.Type = model.Type; 

                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    _GoodsImageService.Update(model);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if (_GoodsImageService.CheckBusinessName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加 
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreatePersonId = Loginer.AccountId;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now; 
                    model = _GoodsImageService.Insert(model);
                    if (model.GoodsImageId > 0)
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
                var entity = _GoodsImageService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _GoodsImageService.Update(entity);
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
                var entity = _GoodsImageService.GetById(id);
                entity.Status = (int)isEnabled;

                _GoodsImageService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}