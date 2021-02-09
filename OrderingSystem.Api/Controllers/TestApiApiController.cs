using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.IService;
using OrderingSystem.Api.Models;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Api.Controllers
{
    public class TestApiApiController : ApiController
    {
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        //IUserService _userService;
        //public TestApiController(IUserService userService)
        //{
        //    _userService = userService;
        //}
        public ResponseModel<User> GetList()
        {
            var result = new ResponseModel<User>();

            result.data= _userService.GetById(1000);

            return result;
        }
    }
}
