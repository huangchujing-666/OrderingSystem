using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// The Customer biz
        /// </summary>
        private ICustomerBusiness _CustomerBiz;

        public CustomerService(ICustomerBusiness CustomerBiz)
        {
            _CustomerBiz = CustomerBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetById(int id)
        {
            return this._CustomerBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Customer Insert(Customer model)
        {
            return this._CustomerBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Customer model)
        {
            this._CustomerBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Customer model)
        {
            this._CustomerBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Customer> GetManagerList(string name, int businessInfoId, int pageNum, int pageSize, out int totalCount)
        {
            return this._CustomerBiz.GetManagerList(name, businessInfoId, pageNum, pageSize, out totalCount);
        }

        public List<Customer> GetAll()
        {
            return this._CustomerBiz.GetAll();
        }

        public Customer GetCustomerByCardNo(int userId, string cardNo,int cardType)
        {
            return this._CustomerBiz.GetCustomerByCardNo(userId, cardNo, cardType);
        }

        public List<Customer> GetCustomerListByUserId(int userId)
        {
            return this._CustomerBiz.GetCustomerListByUserId(userId);
        }

        public bool CustomerIdIsAllExist(int[] output)
        {
            return this._CustomerBiz.CustomerIdIsAllExist( output);
        }
    }
}
