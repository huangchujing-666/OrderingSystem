
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
    public class ProductLabelController : BaseController
    {
        // GET: ProductLabel
        private readonly IProductService _ProductService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;
        private readonly IProductLableService _ProductLabelService;
        private readonly IProductRelateLableService _ProductRelateLabelService;


        public ProductLabelController(IProductService ProductService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService,
            IProductLableService ProductLabelService,IProductRelateLableService ProductRelateLabelService)
        {
            _ProductService = ProductService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
            _ProductLabelService = ProductLabelService;
            _ProductRelateLabelService = ProductRelateLabelService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(ProductLabelVM vm, int pn = 1)
        {
            //查询
            var data = _ProductService.GetById(vm.Id);

            vm.BusinessInfoId = data.BusinessInfoId;

            vm.ProductLabelList = data.ProductLableList;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(ProductLabelVM vm)
        {
            vm.ProductLabel = _ProductLabelService.GetById(vm.Id) ?? new ProductLable();

            if (vm.Id==0)
            {
                var data = _ProductService.GetById(vm.ProductId);

                vm.BusinessInfoId = data.BusinessInfoId;
            }
            else
            {
                //vm.ProductId = vm.ProductLabel.ProductId;
                vm.BusinessInfoId = vm.ProductLabel.BusinessInfoId;
            }
             
            //获取默认可选标签列表
            vm.ProductLabelList = _ProductLabelService.GetAll().Where(p => p.BusinessInfoId == 0 || p.BusinessInfoId == vm.BusinessInfoId).Distinct().ToList();

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(ProductLable model)
        {
            try
            {
                if (model.ProductLableId > 0)
                {
                    var entity = _ProductLabelService.GetById(model.ProductLableId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _ProductLabelService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Name = model.Name;

                    _ProductLabelService.Insert(model);
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
                var entity = _ProductLabelService.GetById(id);
                
                _ProductLabelService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}