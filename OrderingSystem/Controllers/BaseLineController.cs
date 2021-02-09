
using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LOT.Admin.Controllers
{
    /// <summary>
    /// 线路控制器
    /// </summary>
    public class BaseLineController : BaseController
    {
        // GET: BaseLine
        private readonly IBaseLineService _baseLineService;
        private readonly IBaseAreaService _baseAreaService;

        public BaseLineController(IBaseLineService baseLineService, IBaseAreaService baseAreaService)
        {
            _baseLineService = baseLineService;
            _baseAreaService = baseAreaService; 
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="_dictionariesVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BaseLineVM vm, int pn = 1)
        {
            int totalCount,
                    pageIndex = pn,
                    pageSize = PagingConfig.PAGE_SIZE;
            var list = _baseLineService.GetManagerList(vm.QueryLineName, vm.QueryCityName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BaseLine>()
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
        /// <param name="_lineVM"></param>
        /// <returns></returns>
        public ActionResult Edit(BaseLineVM _lineVM)
        {
            _lineVM.Line = _baseLineService.GetById(_lineVM.Id) ?? new BaseLine();
            _lineVM.Provinces = _baseAreaService.GetAll().Where(p => p.Grade == 1).ToList();
            _lineVM.City = _baseAreaService.GetById(_lineVM.Line.BaseAreaId) ?? new BaseArea();
            _lineVM.Citys = _baseAreaService.GetAll().Where(p => p.Grade == 2).ToList();
            _lineVM.ImgInfo = _lineVM.Line.BaseImage ?? new BaseImage();
            return View(_lineVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(BaseLine model)
        {
            try
            { 
                if (model.BaseLineId > 0)
                {
                    var entity = _baseLineService.GetById(model.BaseLineId);

                    //修改 
                    entity.BaseAreaId = model.BaseAreaId;
                    entity.BaseImageId = model.BaseImageId;
                    entity.LineName = model.LineName;
                    entity.LineNumber= model.LineNumber; 
                    entity.EditTime = DateTime.Now;

                    _baseLineService.Update(model);
                }
                else
                {
                    //if (_baseAreaService.CheckName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加
                    model.Status = (int)OrderingSystem.Domain.EnumHelp.EnabledEnum.有效;
                    model.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;

                    _baseLineService.Insert(model);
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
                var entity = _baseLineService.GetById(id);
                entity.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.已删除;
                entity.EditTime = DateTime.Now;
                _baseLineService.Update(entity);
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
                var entity = _baseLineService.GetById(id);
                entity.Status = isEnabled;
                entity.EditTime = DateTime.Now;
                _baseLineService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }
    }
}