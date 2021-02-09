using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderingSystem.Domain.Model;
using OrderingSystem.Domain;

namespace OrderingSystem.Api.Controllers
{
    public class CustomerApiController : ApiController
    {
        private readonly ICustomerService _customerService = EngineContext.Current.Resolve<ICustomerService>();
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        /// <summary>
        /// 添加或编辑用户的身份证信息
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<CustomerDTO> AddCustomer(Customer Customer)
        {
            var result = new ResponseModel<CustomerDTO>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            EnumHelp.IdCardType type;
            if (Customer.UserId <= 0 || Customer.CardType <= 0 || string.IsNullOrWhiteSpace(Customer.Name) || string.IsNullOrWhiteSpace(Customer.Mobile) || string.IsNullOrWhiteSpace(Customer.CardNo))
            {
                result.error_code = Result.ERROR;
                result.message = "必须字段不能为0或空";
                return result;
            }
            else if (!CheckInputHelper.RegexPhone(Customer.Mobile))
            {
                result.error_code = Result.ERROR;
                result.message = "Mobile不合法";
                return result;
            }
            else if (!CheckInputHelper.RegexCardNo(Customer.CardNo))
            {
                result.error_code = Result.ERROR;
                result.message = "CardNo不合法";
                return result;
            }
            else if (!Enum.TryParse(Customer.CardType.ToString(), out type))
            {
                result.error_code = Result.ERROR;
                result.message = "CardType不合法";
                return result;
            }
            var user = _userService.GetById(Customer.UserId);
            if (user == null)
            {
                result.error_code = Result.ERROR;
                result.message = "用户不存在";
                return result;
            }
            if (user.IsDelete == (int)EnumHelp.IsDeleteEnum.已删除 || user.Status == (int)EnumHelp.EnabledEnum.无效)
            {
                result.error_code = Result.ERROR;
                result.message = "用户状态无效";
                return result;
            }
            CustomerDTO dto = new CustomerDTO();
            if (Customer.CustomerId > 0)//编辑
            {
                var customer = _customerService.GetById(Customer.CustomerId);
                if (customer != null)
                {
                    customer.EditTime = System.DateTime.Now;
                    customer.CardNo = Customer.CardNo;
                    customer.CardType = Customer.CardType;
                    customer.Mobile = Customer.Mobile;
                    customer.Name = Customer.Name;
                    _customerService.Update(customer);
                    dto.CustomerId = Customer.CustomerId;
                }
            }
            else//新增
            {
                var searchCustomer = _customerService.GetCustomerByCardNo(Customer.UserId, Customer.CardNo, Customer.CardType);
                if (searchCustomer != null)
                {
                    result.error_code = Result.ERROR;
                    result.message = "该身份证已存在";
                    return result;
                }
                Customer.CustomerId = 0;
                Customer.CreateTime = System.DateTime.Now;
                Customer.EditTime = System.DateTime.Now;
                var cus = _customerService.Insert(Customer);
                if (cus.CustomerId <= 0)
                {
                    result.error_code = Result.ERROR;
                    result.message = "插入身份证失败";
                    return result;
                }
                dto.CustomerId = cus.CustomerId;
            }
            result.data = dto;
            return result;
        }

        /// <summary>
        /// 根据用户Id获取身份证列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ResponseModel<List<CustomerDTO>> GetCustomerListByUserId(int UserId)
        {
            var result = new ResponseModel<List<CustomerDTO>>();
            var data = new List<CustomerDTO>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            if (UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                result.data = data;
                return result;
            }
            var customerList = _customerService.GetCustomerListByUserId(UserId);
            if (customerList != null && customerList.Count > 0)
            {
                foreach (var item in customerList)
                {
                    data.Add(new CustomerDTO()
                    {
                        CustomerId = item.CustomerId,
                        CardNo = item.CardNo,
                        CardType = item.CardType,
                        Mobile = item.Mobile,
                        Name = item.Name,
                        UserId = item.UserId
                    });
                }
            }
            result.data = data;
            return result;
        }

        /// <summary>
        /// 获取客户身份证信息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ResponseModel<CustomerDTO> GetCustomerById(int CustomerId, int UserId)
        {
            var result = new ResponseModel<CustomerDTO>();
            var data = new CustomerDTO();
            result.error_code = Result.SUCCESS;
            result.total_count = 1;
            if (UserId <= 0 || CustomerId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
            }
            else
            {
                var customer = _customerService.GetById(CustomerId);
                if (customer != null)
                {
                    if (customer.UserId == UserId)
                    {
                        data.UserId = customer.UserId;
                        data.Name = customer.Name;
                        data.CustomerId = customer.CustomerId;
                        data.CardType = customer.CardType;
                        data.CardNo = customer.CardNo;
                        data.Mobile = customer.Mobile;
                    }
                    else
                    {
                        result.total_count = 0;
                        result.error_code = Result.ERROR;
                        result.message = "UserId无效";
                    }
                }
                else
                {
                    result.total_count = 0;
                    result.error_code = Result.ERROR;
                    result.message = "CustomerId不存在";
                }
            }
            result.data = data;
            return result;
        }
    }
}
