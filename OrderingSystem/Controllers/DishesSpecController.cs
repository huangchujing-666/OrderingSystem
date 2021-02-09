
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
    public class DishesSpecController : BaseController
    {
        // GET: DishesSpec
        private readonly IDishesService _dishesService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;
        private readonly IDishesSpecService _DishesSpecService;
        private readonly IDishesSpecDetailService _DishesSpecDetailService;
        private readonly IDishesRelateLableService _dishesRelateLabelService;


        public DishesSpecController(IDishesService dishesService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService,
            IDishesSpecService DishesSpecService, IDishesSpecDetailService DishesSpecDetailService,
            IDishesRelateLableService dishesRelateLabelService)
        {
            _dishesService = dishesService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
            _DishesSpecService = DishesSpecService;
            _DishesSpecDetailService = DishesSpecDetailService;
            _dishesRelateLabelService = dishesRelateLabelService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(DishesSpecVM vm, int pn = 1)
        {
            //查询
            var data = _dishesService.GetById(vm.Id);

            vm.BusinessInfoId = data.BusinessInfoId;

            vm.DishesSpecList = data.DishesSpecList;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(DishesSpecVM vm)
        {
            vm.DishesSpec = _DishesSpecService.GetById(vm.Id) ?? new DishesSpec();

            if (vm.Id==0)
            {
                var data = _dishesService.GetById(vm.DishesId);

                vm.BusinessInfoId = data.BusinessInfoId;
            }
            else
            {
                vm.DishesId = vm.DishesSpec.DishesId;
                vm.BusinessInfoId = vm.DishesSpec.BusinessInfoId;
            }
              
            return View(vm);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDetail(DishesSpecVM vm)
        {
            vm.DishesSpecDetail = _DishesSpecDetailService.GetById(vm.Id) ?? new DishesSpecDetail();

            if (vm.Id>0)
            {
                vm.DishesSpecId = vm.DishesSpecDetail.DishesSpecId;
            }
            //if (vm.Id == 0)
            //{
            //    var data = _DishesSpecDetailService.GetById(vm.DishesSpecId);

            //    vm.BusinessInfoId = data.BusinessInfoId;
            //}
            //else
            //{
            //    vm.DishesId = vm.DishesSpec.DishesId;
            //    vm.BusinessInfoId = vm.DishesSpec.BusinessInfoId;
            //}

            return View(vm);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailList(DishesSpecVM vm)
        {
            vm.DishesSpec = _DishesSpecService.GetById(vm.Id) ?? new DishesSpec();

            //if (vm.Id == 0)
            //{
            //    var data = _dishesService.GetById(vm.DishesId);

            //    vm.BusinessInfoId = data.BusinessInfoId;
            //}
            //else
            //{
            //    vm.DishesId = vm.DishesSpec.DishesId;
            //    vm.BusinessInfoId = vm.DishesSpec.BusinessInfoId;
            //}
             
            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(DishesSpec model)
        {
            try
            {
                if (model.DishesSpecId > 0)
                {
                    var entity = _DishesSpecService.GetById(model.DishesSpecId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _DishesSpecService.Update(entity);

                }
                else
                {
                    //新增   
                    _DishesSpecService.Insert(model);
                }
            }
            catch (Exception)
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
                var entity = _DishesSpecService.GetById(id);
                
                _DishesSpecService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult EditDetail(DishesSpecDetail model)
        {
            try
            {
                if (model.DishesSpecDetailId > 0)
                {
                    var entity = _DishesSpecDetailService.GetById(model.DishesSpecDetailId);
                    //修改
                    entity.Descript = model.Descript;
                    entity.OrignPrice = model.OrignPrice;
                    entity.RealPrice = model.OrignPrice;
                    //entity.RealPrice = model.RealPrice;

                    //执行更新
                    _DishesSpecDetailService.Update(entity);

                }
                else
                {
                    //新增   
                    _DishesSpecDetailService.Insert(model);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 删除详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteDetail(int id = 0)
        {
            try
            {
                var entity = _DishesSpecDetailService.GetById(id);

                _DishesSpecDetailService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}