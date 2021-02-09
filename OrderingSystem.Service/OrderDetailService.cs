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
    public class OrderDetailService : IOrderDetailService
    {
        /// <summary>
        /// The OrderDetail biz
        /// </summary>
        private IOrderDetailBusiness _OrderDetailBiz;

        public OrderDetailService(IOrderDetailBusiness OrderDetailBiz)
        {
            _OrderDetailBiz = OrderDetailBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetail GetById(int id)
        {
            return this._OrderDetailBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OrderDetail Insert(OrderDetail model)
        {
            return this._OrderDetailBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderDetail model)
        {
            this._OrderDetailBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderDetail model)
        {
            this._OrderDetailBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<OrderDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._OrderDetailBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据订单编号获取子订单
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public List<OrderDetail> GetListByOrderNo(string order_no)
        {
           return  this._OrderDetailBiz.GetListByOrderNo(order_no);
        }
    }
}
