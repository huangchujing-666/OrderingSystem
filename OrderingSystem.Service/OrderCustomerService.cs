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
    public class OrderCustomerService: IOrderCustomerService
    {
        /// <summary>
        /// The OrderCustomer biz
        /// </summary>
        private IOrderCustomerBusiness _OrderCustomerBiz;

        public OrderCustomerService(IOrderCustomerBusiness OrderCustomerBiz)
        {
            _OrderCustomerBiz = OrderCustomerBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderCustomer GetById(int id)
        {
            return this._OrderCustomerBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OrderCustomer Insert(OrderCustomer model)
        {
            return this._OrderCustomerBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderCustomer model)
        {
            this._OrderCustomerBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderCustomer model)
        {
            this._OrderCustomerBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<OrderCustomer> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderCustomerBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<OrderCustomer> GetAll()
        {
            return this._OrderCustomerBiz.GetAll();
        }
    }
}
