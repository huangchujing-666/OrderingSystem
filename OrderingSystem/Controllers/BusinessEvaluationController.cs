
using Exam.Common;
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Admin.Controllers
{
    public class BusinessEvaluationController : BaseController
    {
        // GET: BusinessEvaluation
        private readonly IBusinessEvaluationService _BusinessEvaluationService;
        private readonly IBusinessInfoService _businessInfoService;


        public BusinessEvaluationController(IBusinessEvaluationService BusinessEvaluationService,
            IBusinessInfoService businessInfoService)
        {
            _BusinessEvaluationService = BusinessEvaluationService;
            _businessInfoService = businessInfoService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(BusinessEvaluationVM vm, int pn = 1)
        {
            int totalCount,
                  pageIndex = pn,
                  pageSize = PagingConfig.PAGE_SIZE;
            var list = _businessInfoService.GetManagerList(vm.QueryName,(int)BusinessTypeEnum.乐, pageIndex, pageSize, out totalCount);
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
        /// <returns></returns>
        public ActionResult Edit(BusinessEvaluationVM vm)
        {
            var _business = _businessInfoService.GetById(vm.Id) ?? new BusinessInfo();
             
            var pageModel = new BusinessEvaluationModel();

            if (_business.BusinessEvaluation==null)
            {
                _business.BusinessEvaluation = new BusinessEvaluation(); 
            }
            pageModel.BusinessEvaluationId = _business.BusinessEvaluation.BusinessEvaluationId;
            pageModel.Grade = _business.Grade;
            pageModel.BusinessInfoId = _business.BusinessInfoId;
            pageModel.Environment = _business.BusinessEvaluation.Environment;
            pageModel.Service = _business.BusinessEvaluation.Service;
            pageModel.Facilities = _business.BusinessEvaluation.Facilities;

            vm.BusinessEvaluationModel = pageModel;

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(BusinessEvaluationModel model)
        {
            try
            {
                //判断数据
                if (model.Grade > 5 || model.Grade < 0)
                {
                    return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
                }
                if (model.Environment > 5 || model.Environment < 0)
                {
                    return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
                }
                if (model.Service > 5 || model.Service < 0)
                {
                    return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
                }
                if (model.Facilities > 5 || model.Facilities < 0)
                {
                    return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
                } 

                //修改
                var busi = _businessInfoService.GetById(model.BusinessInfoId);
                if (busi == null)
                {
                    return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
                }
                busi.Grade = Math.Round((model.Environment + model.Service + model.Facilities) / 3, 1);
             

                if (model.BusinessEvaluationId > 0)
                {
                    var be = _BusinessEvaluationService.GetById(model.BusinessEvaluationId);
                    //修改
                    be.Environment = model.Environment;
                    be.Service = model.Service;
                    be.Facilities = model.Facilities;
                    be.EditTime = DateTime.Now;

                    //执行更新
                    _BusinessEvaluationService.Update(be);
                }
                else
                { 
                    //新增  
                    BusinessEvaluation be = new BusinessEvaluation();
                    
                    be.Environment = model.Environment;
                    be.Service = model.Service;
                    be.Facilities = model.Facilities;
                    be.CreateTime = DateTime.Now;
                    be.EditTime = DateTime.Now;

                    be = _BusinessEvaluationService.Insert(be);

                    busi.BusinessEvaluationId = be.BusinessEvaluationId; 
                }  

                _businessInfoService.Update(busi); 
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
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
                var entity = _BusinessEvaluationService.GetById(id);

                _BusinessEvaluationService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}