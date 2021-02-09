
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
    public class GoodsController : BaseController
    {
        // GET: Goods
        private readonly IGoodsService _GoodsService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService; 


        public GoodsController(IGoodsService GoodsService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService)
        {
            _GoodsService = GoodsService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService; 
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(GoodsVM vm, int pn = 1)
        {
            if (Session["QueryData"] != null && vm.RefreshFlag == 1)
            {
                vm = (GoodsVM)Session["QueryData"];
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
            var list = _GoodsService.GetManagerList(vm.QueryName, vm.QueryBusinessmanName, pageIndex, pageSize, out total, int.Parse(Loginer.BusinessId));
            var paging = new Paging<Goods>()
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
        public ActionResult Edit(GoodsVM vm)
        {
            vm.Goods = _GoodsService.GetById(vm.Id) ?? new Goods();
            vm.ImgInfo = vm.Goods.BaseImage ?? new BaseImage();
            ///vm.Goods.Describes = StringToolsHelper.RemoveHtmlTag(vm.Goods.Describes);
            //获取菜品分类
            //vm.GoodsCategoryList = _GoodsCategoryoService.GetAll().Where(p => p.BusinessInfoId == 0 || p.BusinessInfoId == vm.Goods.BusinessInfoId).ToList();
            //获取餐厅列表
            int tcount = 0;
            vm.BusinessList = _businessService.GetListByType((int)BusinessTypeEnum.衣, 1, 1000, out tcount);


            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(Goods model, string Business_Ids)
        {
            try
            {
                if (model.GoodsId > 0)
                {
                    var entity = _GoodsService.GetById(model.GoodsId);
                    //修改
                    entity.Name = model.Name;
                    entity.OrignPrice = model.OrignPrice;
                    entity.RealPrice = model.RealPrice; 
                    entity.Descript = model.Descript;
                    entity.BusinessInfoId = model.BusinessInfoId;
                    entity.BaseImageId = model.BaseImageId; 
                    entity.Size = model.Size;
                    entity.EditTime = DateTime.Now;

                    //执行更新
                    _GoodsService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效; 
                    model.CreateTime = DateTime.Now; 
                    model.EditTime = DateTime.Now;
                    _GoodsService.Insert(model);

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
                var entity = _GoodsService.GetById(id);
                entity.Status = (int)isEnabled;

                _GoodsService.Update(entity);
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
                var entity = _GoodsService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _GoodsService.Update(entity);
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
        //public JsonResult GetGoodsByBusinessId(int businessId = 0)
        //{
        //    var list = _GoodsService.GetRecommandGoods(businessId);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
    }
}