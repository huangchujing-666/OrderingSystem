
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
    /// 站点控制器
    /// </summary>
    public class BaseStationController : BaseController
    {
        // GET: BaseStation
        private readonly IBaseStationService _baseStationService;
        private readonly IBaseLineService _baseLineService;
        private readonly IBaseAreaService _baseAreaService;
        public BaseStationController(IBaseStationService baseStationService, IBaseLineService baseLineService, IBaseAreaService baseAreaService)
        {
            _baseStationService = baseStationService;
            _baseLineService = baseLineService;
            _baseAreaService = baseAreaService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_stationVM"></param>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(BaseStationVM _stationVM, int pn = 1)
        {
            int totalCount,
               pageIndex = pn,
               pageSize = PagingConfig.PAGE_SIZE;
            var list = _baseStationService.GetManagerList(_stationVM.QueryStationName, _stationVM.QueryLineName, _stationVM.QueryCityName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BaseStation>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            _stationVM.Paging = paging;
            return View(_stationVM);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="_stationVM"></param>
        /// <returns></returns>
        public ActionResult Edit(BaseStationVM _stationVM)
        {
            _stationVM.Station = _baseStationService.GetById(_stationVM.Id) ?? new BaseStation();
            _stationVM.Provinces = _baseAreaService.GetAll().Where(p => p.Grade == 1).ToList();
            _stationVM.City = _baseAreaService.GetById(_stationVM.Station.BaseAreaId) ?? new BaseArea();
            _stationVM.Citys = _baseAreaService.GetAll().Where(p => p.FId == _stationVM.Station.BaseArea.FId).ToList();
            _stationVM.Lines = _stationVM.Id > 0 ? _baseLineService.GetAll() : new System.Collections.Generic.List<BaseLine>();
            return View(_stationVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(BaseStation model)
        {
            try
            {
                if (model.BaseStationId > 0)
                {
                    var entity = _baseStationService.GetById(model.BaseStationId);

                    //修改 
                    entity.BaseAreaId = model.BaseAreaId; 
                    entity.BaseLineId = model.BaseLineId;
                    entity.Name = model.Name; 
                    entity.EditTime = DateTime.Now;

                    _baseStationService.Update(model);
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

                    _baseStationService.Insert(model);
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
                var entity = _baseStationService.GetById(id);
                entity.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.已删除;
                entity.EditTime = DateTime.Now;
                _baseStationService.Update(entity);
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
                var entity = _baseStationService.GetById(id);
                entity.Status = isEnabled;
                entity.EditTime = DateTime.Now;
                _baseStationService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }
    }
}