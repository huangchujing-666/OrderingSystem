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
    public class OrderStatusService: IOrderStatusService
    {
        /// <summary>
        /// The OrderStatus biz
        /// </summary>
        private IOrderStatusBusiness _OrderStatusBiz;

        public OrderStatusService(IOrderStatusBusiness OrderStatusBiz)
        {
            _OrderStatusBiz = OrderStatusBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderStatus GetById(int id)
        {
            return this._OrderStatusBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OrderStatus Insert(OrderStatus model)
        {
            return this._OrderStatusBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderStatus model)
        {
            this._OrderStatusBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderStatus model)
        {
            this._OrderStatusBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<OrderStatus> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderStatusBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
    }
}
