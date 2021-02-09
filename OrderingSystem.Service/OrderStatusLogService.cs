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
    public class OrderStatusLogService: IOrderStatusLogService
    {
        /// <summary>
        /// The OrderStatusLog biz
        /// </summary>
        private IOrderStatusLogBusiness _OrderStatusLogBiz;

        public OrderStatusLogService(IOrderStatusLogBusiness OrderStatusLogBiz)
        {
            _OrderStatusLogBiz = OrderStatusLogBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderStatusLog GetById(int id)
        {
            return this._OrderStatusLogBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OrderStatusLog Insert(OrderStatusLog model)
        {
            return this._OrderStatusLogBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderStatusLog model)
        {
            this._OrderStatusLogBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderStatusLog model)
        {
            this._OrderStatusLogBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<OrderStatusLog> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderStatusLogBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
    }
}
