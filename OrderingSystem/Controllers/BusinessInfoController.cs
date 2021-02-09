using Exam.Common;
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Controllers
{
    public class BusinessInfoController : BaseController
    {
        IUserService _userService;
        IBusinessInfoService _businessInfoService;
        IBusinessGroupService _businessGroupService;
        IBaseAreaService _baseAreaService;
        IBaseLineService _baseLineService;
        IBaseStationService _baseStationService;
        ISysAccountService _sysAccountService;
        IHotelRelateCategoryService _hotelRelateCategoryService;
        IHotelCategoryService _hotelCategoryService;

        public BusinessInfoController(IUserService userService,
            IBusinessInfoService businessInfoService, IBusinessGroupService businessGroupService,
            IBaseAreaService baseAreaService,
              IBaseLineService baseLineService,
              IBaseStationService baseStationService,
                ISysAccountService sysAccountService,
                IHotelRelateCategoryService hotelRelateCategoryService,
                IHotelCategoryService hotelCategoryService)
        {
            _userService = userService;
            _businessInfoService = businessInfoService;
            _businessGroupService = businessGroupService;
            _baseAreaService = baseAreaService;
            _baseLineService = baseLineService;
            _baseStationService = baseStationService;
            _sysAccountService = sysAccountService;
            _hotelRelateCategoryService = hotelRelateCategoryService;
            _hotelCategoryService = hotelCategoryService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BusinessInfoVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _businessInfoService.GetManagerList(vm.QueryName, vm.QueryType, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BusinessInfo>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;

            //获取所有系统用户名称 
            vm.SysAccountList = _sysAccountService.GetAll();


            ////更新所有距离地铁站数据信息
            //var alllist = _businessInfoService.GetManagerList("", 1, 1000, out totalCount);

            //foreach (var item in alllist)
            //{
            //    SetLocationInfo(item);
            //}
            return View(vm);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult Edit(BusinessInfoVM vm)
        {

            //获取当前用户角色
            vm.RoleId = Loginer.RoleId;
            if (vm.RoleId == (int)RoleTypeEnum.商家)
            {
                vm.Id = int.Parse(Loginer.BusinessId);
            }

            var _areas = _baseAreaService.GetAll();

            vm.BusinessInfo = _businessInfoService.GetById(vm.Id) ?? new BusinessInfo();
            vm.Provinces = _areas.Where(p => p.Grade == 1).ToList();
            vm.City = _areas.Where(p => p.BaseAreaId == vm.BusinessInfo.BaseAreaId).FirstOrDefault() ?? new BaseArea();
            vm.Citys = _areas.Where(p => p.Grade == 2 && p.FId == vm.City.FId).ToList();
            vm.Lines = _baseLineService.GetAll();
            vm.Stations = vm.Id > 0 ? _baseStationService.GetAll().Where(p => p.BaseLineId == vm.BusinessInfo.BaseLineId).ToList() : new System.Collections.Generic.List<BaseStation>();
            vm.ImgInfo = vm.BusinessInfo.BaseImage ?? new BaseImage();

            if (vm.BusinessInfo.BusinessInfoId>0)
            {
                vm.BusinessGroupList = _businessGroupService.GetAll().Where(p => p.BusinessTypeId == vm.BusinessInfo.BusinessTypeId).ToList();
            }
            

            return View(vm);
        }

        [HttpPost]
        public JsonResult GetGroupByType(int typeid = 0)
        {
            var list = _businessGroupService.GetAll().Where(p => p.BusinessTypeId == typeid).ToList();
            list = (from p in list
                    select new BusinessGroup
                    {
                        BusinessGroupId = p.BusinessGroupId,
                        Name = p.Name,
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult CategoryEdit(BusinessInfoVM vm)
        {

            //获取当前用户角色
            vm.RoleId = Loginer.RoleId;
            if (vm.RoleId == (int)RoleTypeEnum.商家)
            {
                vm.Id = int.Parse(Loginer.BusinessId);
            }

            vm.BusinessInfo = _businessInfoService.GetById(vm.Id) ?? new BusinessInfo();


            vm.HotelCategoryList = _hotelCategoryService.GetAll().ToList();
            vm.HotelRelateCategoryList = _hotelRelateCategoryService.GetAll().Where(p => p.BusinessInfoId == vm.Id).ToList();

            return View(vm);
        }

        [HttpPost]
        public JsonResult CategorySave(int bussinessId, string Categories)
        {
            //当前商家id
            var bid = bussinessId;


            var diningtims = Categories.Split(',');

            //获取当前商家订座配置
            var bconfig = _hotelRelateCategoryService.GetAll().Where(p => p.BusinessInfoId == bid);

            //删除数据
            foreach (var item in bconfig)
            {
                _hotelRelateCategoryService.Delete(item);
            }

            //插入数据
            foreach (var item in diningtims)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                var addmodel = new HotelRelateCategory();
                addmodel.BusinessInfoId = bid;
                addmodel.HotelCategoryId = int.Parse(item);
                addmodel.CreateTime = DateTime.Now;

                _hotelRelateCategoryService.Insert(addmodel);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(BusinessInfo model)
        {
            try
            {
                if (model.BusinessInfoId > 0)
                {
                    var entity = _businessInfoService.GetById(model.BusinessInfoId);

                    //修改
                    entity.Address = model.Address;
                    entity.AveragePay = model.AveragePay;
                    entity.BaseAreaId = model.BaseAreaId;
                    entity.BaseImageId = model.BaseImageId;
                    entity.BaseLineId = model.BaseLineId;
                    entity.BaseStationId = model.BaseStationId;
                    entity.BusinessHour = model.BusinessHour;
                    entity.Grade = model.Grade;
                    entity.Introduction = model.Introduction;
                    entity.Services = model.Services;
                    entity.Latitude = model.Latitude;
                    entity.Longitude = model.Longitude;
                    entity.Mobile = model.Mobile;
                    entity.Name = model.Name;
                    entity.OpenDate = string.IsNullOrWhiteSpace(model.OpenDate) ? "" : model.OpenDate;
                    entity.RefreshDate = model.RefreshDate;
                    entity.TotalFloors = model.TotalFloors;
                    entity.TotalRooms = model.TotalRooms;
                    entity.BusinessTypeId = model.BusinessTypeId;
                    entity.BusinessGroupId = model.BusinessGroupId;
                    entity.Notic = model.Notic;
                    entity.OrderCountPerMonth = model.OrderCountPerMonth;
                    entity.SortNo = model.SortNo;

                    entity.EditPersonId = Loginer.AccountId;
                    entity.EditTime = DateTime.Now;
                    _businessInfoService.Update(entity);

                    //设置距离
                    SetLocationInfo(entity);

                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if (_businessInfoService.CheckBusinessName(model.Name) > 0)
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加
                    //model.BusinessTypeId = 1;
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreatePersonId = Loginer.AccountId;
                    model.CreateTime = DateTime.Now;
                    model.EditPersonId = Loginer.AccountId;
                    model.EditTime = DateTime.Now;
                    model.OpenDate = string.IsNullOrWhiteSpace(model.OpenDate) ? "" : model.OpenDate;
                    model = _businessInfoService.Insert(model);
                    if (model.BusinessInfoId > 0)
                    {
                        //设置距离
                        SetLocationInfo(model);

                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SetLocationInfo(BusinessInfo businessInfo)
        {
            Task.Run(() =>
            {
                //调用搞的地图接口 获取商家距离地铁站距离信息
                if (businessInfo != null && businessInfo.Latitude != 0 && businessInfo.Longitude != 0)
                {
                    string location = businessInfo.Longitude + "," + businessInfo.Latitude;

                    var url = "http://restapi.amap.com/v3/place/around?key=61ff9c2378fa22a4dc69d1489d703a75&location={0}&keywords=地铁&types=150500&radius=5000&offset=20&page=1&extensions=all";

                    url = string.Format(url, location);

                    //查询
                    string topicListStr = GetHtml(url, "").Replace("null", "0"); ;
                    //解析数据
                    var returnModel = JsonHelper.ParseFormJson<AroundVM>(topicListStr);

                    if (returnModel != null && returnModel.pois != null && returnModel.pois.Count > 0)
                    {
                        var firstModel = returnModel.pois[0];
                        var distanceStr = "距离" + firstModel.name + firstModel.distance + "m";
                        businessInfo.Distance = distanceStr;
                        _businessInfoService.Update(businessInfo);
                    }

                }

            });
        }

        /// <summary>
        /// 获取连接的返回值
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addCookie"></param>
        /// <returns></returns>
        public static string GetHtml(string url, string addCookie)
        {
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            Stream stream2 = response1.GetResponseStream();//获得回应的数据流
                                                           //将数据流转成 String
            return new StreamReader(stream2, System.Text.Encoding.UTF8).ReadToEnd();
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
                var entity = _businessInfoService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _businessInfoService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
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
                var entity = _businessInfoService.GetById(id);
                entity.Status = (int)isEnabled;

                _businessInfoService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 创建对应后台商家帐号
        /// </summary>
        /// <param name="id">商家Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateBusinessAccount(int id)
        {
            try
            {

                var businessModel = _businessInfoService.GetById(id);
                if (businessModel != null && businessModel.BusinessInfoId > 0)
                {
                    //创建商家帐号实体模型
                    var model = new SysAccount();

                    var _date = DateTime.Now;

                    model.Account = "yssgadmin" + businessModel.BusinessInfoId;
                    model.NickName = businessModel.Name;
                    model.PassWord = MD5Util.GetMD5_32("123456");
                    model.MobilePhone = businessModel.Mobile;
                    model.SysRoleId = (int)RoleTypeEnum.商家;
                    model.BusinessInfoId = businessModel.BusinessInfoId;
                    model.BaseImageId = businessModel.BaseImageId;
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.CreateTime = _date;
                    model.EditTime = _date;
                    model.LoginTime = _date;

                    //添加
                    _sysAccountService.Insert(model);
                }
                else
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
        }
    }
}