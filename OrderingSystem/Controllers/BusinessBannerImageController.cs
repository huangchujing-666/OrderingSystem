using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;

namespace OrderingSystem.Controllers
{
    public class BusinessBannerImageController : Controller
    {
        private readonly IBusinessBannerImageService _businessBannerImageService;
        private readonly IBusinessInfoService _businessService;
        public BusinessBannerImageController(IBusinessBannerImageService businessBannerImageService, IBusinessInfoService businessService)
        {
            _businessBannerImageService = businessBannerImageService;
            _businessService = businessService;
        }
        
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_userVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BusinessBannerImageVM _businessBannerImageVM, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _businessBannerImageService.GetManagerList(_businessBannerImageVM.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BusinessBannerImage>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            _businessBannerImageVM.Paging = paging;
            return View(_businessBannerImageVM);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="_userVM"></param>
        /// <returns></returns>
        public ActionResult Edit(BusinessBannerImageVM vm)
        {
            vm.BusinessBannerImage = _businessBannerImageService.GetById(vm.Id) ?? new BusinessBannerImage();
            vm.BusinessBannerImage.BusinessInfo= vm.BusinessBannerImage.BusinessInfo ?? new BusinessInfo();
            vm.BusinessBannerImage.BaseImage = vm.BusinessBannerImage.BaseImage ?? new BaseImage();
            vm.ImgInfo = vm.BusinessBannerImage.BaseImage ?? new BaseImage();
            int tcount = 0;
            vm.BusinessList= _businessService.GetListByType(vm.BusinessBannerImage.Module, 1, 100000, out tcount);
            return View(vm);
        }

        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(BusinessBannerImage model)
        {
            try
            {
                if (model.BusinessBannerImageId > 0)
                {
                    var entity = _businessBannerImageService.GetById(model.BusinessBannerImageId);
                    //修改  
                    entity.BaseImageId = model.BaseImageId;
                    entity.BusinessInfoId = model.BusinessInfoId;
                    entity.SortNo = model.SortNo;
                    entity.Descript = model.Descript;
                    _businessBannerImageService.Update(entity);
                }
                else
                {
                    //添加
                    model.CreateTime = DateTime.Now;

                    _businessBannerImageService.Insert(model);
                }
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 根据模块类型获取商家列表
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBusinessList(int module = 0)
        {
            int tcount = 0;
            var list = _businessService.GetListByType(module, 1, 100000, out tcount);
            list = (from p in list
                    select new BusinessInfo
                    {
                        BusinessInfoId = p.BusinessInfoId,
                        Name = p.Name
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}