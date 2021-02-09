using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OrderingSystem.Models;
using OrderingSystem.Admin.Infrastructure;

namespace OrderingSystem.Controllers
{
    [AccountAuthorize]
    public class BaseController : Controller
    {
        
    }
}