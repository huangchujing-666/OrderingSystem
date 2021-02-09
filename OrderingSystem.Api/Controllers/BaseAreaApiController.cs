using OrderingSystem.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    public class BaseAreaApiController : ApiController
    {
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<string>> GetDistrictList()
        {
            var result = new ResponseModel<List<string>>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            var data = new List<string> { "福田区", "罗湖区", "南山区", "盐田区", "宝安区", "龙岗区", "龙华新区", "坪山新区", "光明新区", "大鹏新区" };
            result.data = data;
            return result;
        }
    }
}
