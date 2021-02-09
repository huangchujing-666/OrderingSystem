
using OrderingSystem.Controllers;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System.Linq;
using System.Web.Mvc;

namespace OrderingSystem.Admin.Controllers
{
    public class AreaController : BaseController
    {
        // GET: Area
        private readonly IBaseAreaService _baseAreaService;
        private readonly IBaseLineService _baseLineService;
        private readonly IBaseStationService _baseStationService;
        public AreaController(IBaseAreaService baseAreaService, IBaseLineService baseLineService, IBaseStationService baseStationService)
        {
            _baseAreaService = baseAreaService;
            _baseLineService = baseLineService;
            _baseStationService = baseStationService;
        }

        /// <summary>
        /// 根据省Id获取城市集合
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProvinceIdByCitys(int provinceId = 0)
        {
            var list = _baseAreaService.GetAll().Where(p => p.Grade == 2 && p.FId == provinceId).ToList();
            list = (from p in list
                    select new BaseArea
                    {
                        BaseAreaId = p.BaseAreaId,
                        FId = p.FId,
                        Name = p.Name,
                        Grade = p.Grade
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据城市Id获取线路集合
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCityIdByLines(int cityId = 0)
        {
            var list = _baseLineService.GetAll().Where(p=> p.BaseAreaId == cityId);
            list = (from p in list
                   select new BaseLine
                   {
                       BaseLineId = p.BaseLineId,
                       LineName = p.LineName,
                       LineNumber = p.LineNumber
                   }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据线路ID获取站点集合
        /// </summary>
        /// <param name="lineId">线路Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLineIdByStations(int lineId = 0)
        {
            var list = _baseStationService.GetAll().Where(p => p.BaseLineId == lineId);
            list = (from p in list
                    select new BaseStation
                    {
                        BaseLineId = p.BaseLineId,
                        BaseAreaId = p.BaseAreaId,
                        Name = p.Name, 
                        BaseStationId = p.BaseStationId
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}