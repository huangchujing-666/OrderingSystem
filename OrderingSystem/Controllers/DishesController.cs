
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
    public class DishesController : BaseController
    {
        // GET: Dishes
        private readonly IDishesService _dishesService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;
        private readonly IDishesCategoryService _dishesCategoryoService;


        public DishesController(IDishesService dishesService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService,
            IDishesCategoryService dishesCategoryoService)
        {
            _dishesService = dishesService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
            _dishesCategoryoService = dishesCategoryoService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(DishesVM vm, int pn = 1)
        {
            if (Session["QueryData"] != null && vm.RefreshFlag == 1)
            {
                vm = (DishesVM)Session["QueryData"];
                vm.RefreshFlag = 0;
            }
            else
            {
                Session["QueryData"] = vm;
            }

            int total = 0,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //分页查询
            var list = _dishesService.GetManagerList(vm.QueryName, vm.QueryBusinessmanName, pageIndex, pageSize, out total, int.Parse(Loginer.BusinessId));
            var paging = new Paging<Dishes>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = total,
                Index = pn,
            };
            vm.Paging = paging;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(DishesVM vm)
        {
            vm.Dishes = _dishesService.GetById(vm.Id) ?? new Dishes();
            vm.ImgInfo = vm.Dishes.BaseImage ?? new BaseImage();
            ///vm.Dishes.Describes = StringToolsHelper.RemoveHtmlTag(vm.Dishes.Describes);
            //获取菜品分类
            vm.DishesCategoryList = _dishesCategoryoService.GetAll().Where(p => p.BusinessInfoId == 0 || p.BusinessInfoId == vm.Dishes.BusinessInfoId).ToList();
            //获取餐厅列表
            int tcount = 0;
            vm.BusinessList = _businessService.GetList(1, 1000, out tcount);


            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(Dishes model, string Business_Ids)
        {
            try
            {
                if (model.DishesId > 0)
                {
                    var entity = _dishesService.GetById(model.DishesId);
                    //修改
                    entity.Name = model.Name;
                    entity.OrignPrice = model.OrignPrice;
                    entity.RealPrice = model.RealPrice;
                    entity.DishesCategoryId = model.DishesCategoryId;
                    entity.SellCountPerMonth = model.SellCountPerMonth;
                    entity.Descript = model.Descript;
                    entity.BusinessInfoId = model.BusinessInfoId;
                    entity.BaseImageId = model.BaseImageId;

                    entity.EditPersonId = Loginer.AccountId;
                    entity.EditTime = DateTime.Now;

                    //执行更新
                    _dishesService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreatePersonId = Loginer.AccountId;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    _dishesService.Insert(model);

                }
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

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
                var entity = _dishesService.GetById(id);
                entity.Status = (int)isEnabled;

                _dishesService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
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
                var entity = _dishesService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _dishesService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

        ///// <summary>
        ///// 根据商家id获取菜品
        ///// </summary>
        ///// <param name="businessId">商家Id</param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult GetDishesByBusinessId(int businessId = 0)
        //{
        //    var list = _dishesService.GetRecommandDishes(businessId);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
    }
}