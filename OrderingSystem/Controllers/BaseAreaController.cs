
using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OrderingSystem.Admin.Controllers
{
    /// <summary>
    /// 地区控制器
    /// </summary>
    public class BaseAreaController : BaseController
    {
        // GET: BaseArea
        private readonly IBaseAreaService _baseAreaService;
        public BaseAreaController(IBaseAreaService baseAreaService)
        {
            _baseAreaService = baseAreaService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm">实体VM</param>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(BaseAreaVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = (pn - 1) * PagingConfig.PAGE_SIZE,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _baseAreaService.GetAll().Where(p => p.Grade == 1).ToList();
            var paging = new Paging<BaseArea>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = list.Count,
                Index = pn,
            };
            vm.Paging = paging;
            //vm.Provinces = _baseAreaService.Provinces();
            return View(vm);
        }
        /// <summary>
        /// 城市列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult CityList(BaseAreaVM vm, int pn = 1)
        {
            int totalCount,
                    pageIndex = pn,
                    pageSize = PagingConfig.PAGE_SIZE;
            var list = _baseAreaService.GetManagerList(vm.QueryName, vm.Id, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BaseArea>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = list.Count,
                Index = pn,
            };
            vm.Paging = paging;
            return View(vm);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="vm">实体VM</param>
        /// <returns></returns>
        public ActionResult Edit(BaseAreaVM vm)
        {
            vm.Area = _baseAreaService.GetById(vm.Id) ?? new BaseArea();
            vm.Provinces = _baseAreaService.GetAll().Where(p => p.Grade == 1).ToList();
            return View(vm);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(BaseArea model)
        {
            try
            {
                model.Grade = model.FId == 1 ? 1 : 2;
                model.FId = model.FId == 1 ? 1 : model.FId;
                if (model.BaseAreaId > 0)
                {
                    var entity = _baseAreaService.GetById(model.BaseAreaId);

                    //修改 
                    entity.Name = model.Name;
                    entity.FId = model.FId;
                    entity.Grade = model.Grade;
                    entity.EditTime = DateTime.Now;

                    _baseAreaService.Update(model);
                }
                else
                {
                    //if (_baseAreaService.CheckName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加
                    model.Status = (int)Domain.EnumHelp.EnabledEnum.有效;
                    model.IsDelete = (int)Domain.EnumHelp.IsDeleteEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;

                    _baseAreaService.Insert(model);
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
                var entity = _baseAreaService.GetById(id);
                entity.IsDelete = (int)Domain.EnumHelp.IsDeleteEnum.已删除;
                entity.EditTime = DateTime.Now;
                _baseAreaService.Update(entity);
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
        public JsonResult UpdateStatus(int id = 0, int isEnabled = 0)
        {
            try
            {
                var entity = _baseAreaService.GetById(id);
                entity.Status = isEnabled;
                entity.EditTime = DateTime.Now;
                _baseAreaService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
             
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
             
        }
    }
}