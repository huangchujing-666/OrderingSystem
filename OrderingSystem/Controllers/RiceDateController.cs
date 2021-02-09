
using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;
using static OrderingSystem.Domain.EnumHelp;

namespace LOT.Admin.Controllers
{
    /// <summary>
    /// 拼饭控制器
    /// </summary>
    public class RiceDateController : BaseController
    {
        // GET: RiceDate
        private readonly IRiceDateService _RiceDateService; 
        private readonly IRiceDateUserService _RiceDateUserService;
        private readonly IRiceDateFeedbackService _RiceDateFeedbackService;
        private readonly IBaseImageService _BaseImageService;

        public RiceDateController(IRiceDateService RiceDateService, 
            IRiceDateUserService RiceDateUserService,
            IRiceDateFeedbackService RiceDateFeedbackService,
            IBaseImageService BaseImageService)
        {
            _RiceDateService = RiceDateService;
            _RiceDateUserService = RiceDateUserService;
            _RiceDateFeedbackService = RiceDateFeedbackService;
            _BaseImageService = BaseImageService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="_dictionariesVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(RiceDateVM vm, int pn = 1)
        {
            int totalCount,
                    pageIndex = pn,
                    pageSize = PagingConfig.PAGE_SIZE;
            var list = _RiceDateService.GetManagerList(vm.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<RiceDate>()
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
        public ActionResult Edit(RiceDateVM vm)
        {
            //vm.RiceDate = _RiceDateService.GetById(vm.Id) ?? new RiceDate();
            //vm.ImgInfo = vm.RiceDate.BaseImage ?? new BaseImage();
            ////获取景点列表
            //int tcount = 0;
            //vm.BusinessList = _businessInfoService.GetListByType((int)BusinessTypeEnum.景点, 1, 1000, out tcount);
            return View(vm);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(RiceDate model)
        {
            try
            { 
                if (model.RiceDateId > 0)
                {
                    var entity = _RiceDateService.GetById(model.RiceDateId);

                    ////修改  
                    //entity.BaseImageId = model.BaseImageId; 
                    //entity.BusinessInfoId = model.BusinessInfoId; 
                    //entity.Name = model.Name; 
                    //entity.Content = model.Content;
                    //entity.EditTime = DateTime.Now;

                    _RiceDateService.Update(model);
                }
                else
                { 
                    //添加
                    model.Status = (int)OrderingSystem.Domain.EnumHelp.EnabledEnum.有效;
                    model.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;

                    _RiceDateService.Insert(model);
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
                var entity = _RiceDateService.GetById(id);
                entity.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.已删除;
                entity.EditTime = DateTime.Now;
                _RiceDateService.Update(entity);
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
                var entity = _RiceDateService.GetById(id);
                entity.Status = isEnabled;
                entity.EditTime = DateTime.Now;
                _RiceDateService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 城市列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult UserList(RiceDateUserVM vm)
        {
            var list = _RiceDateUserService.GetList().Where(p => p.RiceDateId == vm.Id).ToList();
          
            vm.Paging = list;

            return View(vm);
        }

        /// <summary>
        /// 反馈列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult FeedbackList(RiceDateFeedbackVM vm)
        {
            var list = _RiceDateFeedbackService.GetList().Where(p => p.RiceDateId == vm.Id).ToList();

            vm.Paging = list;

            return View(vm);
        }

        /// <summary>
        /// 获取详情
        /// </summary> 
        /// <returns></returns>
      
        public ActionResult Detail(RiceDateVM vm)
        {
            vm.RiceDate = _RiceDateService.GetById(vm.Id);
            if (vm.RiceDate!=null && vm.RiceDate.BaseImageIds!="")
            { 
                vm.BaseImageList = _BaseImageService.GetByIds(Array.ConvertAll(vm.RiceDate.BaseImageIds.Split(','), int.Parse));
            }
            return View(vm);
        }
    }
}