
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
    /// 文章控制器
    /// </summary>
    public class JourneyArticleController : BaseController
    {
        // GET: JourneyArticle
        private readonly IJourneyArticleService _JourneyArticleService;
        private readonly IBaseAreaService _baseAreaService;
        private readonly IBusinessInfoService _businessInfoService;

        public JourneyArticleController(IJourneyArticleService JourneyArticleService, IBaseAreaService baseAreaService, 
            IBusinessInfoService businessInfoService)
        {
            _JourneyArticleService = JourneyArticleService;
            _baseAreaService = baseAreaService;
            _businessInfoService = businessInfoService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="_dictionariesVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(JourneyArticleVM vm, int pn = 1)
        {
            int totalCount,
                    pageIndex = pn,
                    pageSize = PagingConfig.PAGE_SIZE;
            var list = _JourneyArticleService.GetManagerList(vm.QueryName,vm.QueryBusinessName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<JourneyArticle>()
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
        public ActionResult Edit(JourneyArticleVM vm)
        {
            vm.JourneyArticle = _JourneyArticleService.GetById(vm.Id) ?? new JourneyArticle();
            vm.ImgInfo = vm.JourneyArticle.BaseImage ?? new BaseImage();
            //获取景点列表
            int tcount = 0;
            vm.BusinessList = _businessInfoService.GetListByType((int)BusinessTypeEnum.景点, 1, 1000, out tcount);
            return View(vm);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(JourneyArticle model)
        {
            try
            { 
                if (model.JourneyArticleId > 0)
                {
                    var entity = _JourneyArticleService.GetById(model.JourneyArticleId);

                    //修改  
                    entity.BaseImageId = model.BaseImageId; 
                    entity.BusinessInfoId = model.BusinessInfoId; 
                    entity.Name = model.Name; 
                    entity.Content = model.Content;
                    entity.EditTime = DateTime.Now;

                    _JourneyArticleService.Update(model);
                }
                else
                { 
                    //添加
                    model.Status = (int)OrderingSystem.Domain.EnumHelp.EnabledEnum.有效;
                    model.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;

                    _JourneyArticleService.Insert(model);
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
                var entity = _JourneyArticleService.GetById(id);
                entity.IsDelete = (int)OrderingSystem.Domain.EnumHelp.IsDeleteEnum.已删除;
                entity.EditTime = DateTime.Now;
                _JourneyArticleService.Update(entity);
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
                var entity = _JourneyArticleService.GetById(id);
                entity.Status = isEnabled;
                entity.EditTime = DateTime.Now;
                _JourneyArticleService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }
    }
}