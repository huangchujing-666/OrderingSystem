
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
    public class ProductController : BaseController
    {
        // GET: Product
        private readonly IProductService _ProductService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;


        public ProductController(IProductService ProductService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService)
        {
            _ProductService = ProductService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(ProductVM vm, int pn = 1)
        {

            int total = 0,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //分页查询
            var list = _ProductService.GetManagerList(vm.QueryName, vm.QueryBusinessmanName, pageIndex, pageSize, out total, int.Parse(Loginer.BusinessId));
            var paging = new Paging<Product>()
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
        public ActionResult Edit(ProductVM vm)
        {
            vm.Product = _ProductService.GetById(vm.Id) ?? new Product();
            //获取餐厅列表
            int tcount = 0;
            vm.BusinessList = _businessService.GetListByType((int)BusinessTypeEnum.乐, 1, 1000, out tcount);

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(Product model, string Business_Ids)
        {
            try
            {
                //如果有使用期限
                if (model.UseDateLimit == 1)
                { 
                    if (model.StartDate.Ticks == 0 || model.EndDate.Ticks == 0)
                    {
                        return Json(new { Status = Successed.Error, Info = "请选择开始日期和结束日期" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    model.StartDate = DateTime.MaxValue;
                    model.EndDate = DateTime.MaxValue;
                }


                if (model.ProductId > 0)
                {
                    var entity = _ProductService.GetById(model.ProductId);
                    //修改
                    entity.Name = model.Name;
                    entity.OrignPrice = model.OrignPrice;
                    entity.RealPrice = model.RealPrice;
                    entity.Descript = model.Descript;
                    entity.BusinessInfoId = model.BusinessInfoId;
                    entity.Content = model.Content;
                    entity.Remark = model.Remark;
                    entity.StartDate = model.StartDate;
                    entity.EndDate = model.EndDate;
                    entity.UseDateLimit = model.UseDateLimit;
                    entity.Notice = model.Notice;
                    entity.Rules = model.Rules;

                    entity.EditPersonId = Loginer.AccountId;
                    entity.EditTime = DateTime.Now;

                    //执行更新
                    _ProductService.Update(entity);

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
                    _ProductService.Insert(model);

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
                var entity = _ProductService.GetById(id);
                entity.Status = (int)isEnabled;

                _ProductService.Update(entity);
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
                var entity = _ProductService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _ProductService.Update(entity);
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
        //public JsonResult GetProductByBusinessId(int businessId = 0)
        //{
        //    var list = _ProductService.GetRecommandProduct(businessId);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
    }
}