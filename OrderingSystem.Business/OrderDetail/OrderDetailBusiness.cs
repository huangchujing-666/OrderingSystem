using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private IRepository<OrderDetail> _repoOrderDetail;

        public OrderDetailBusiness(
          IRepository<OrderDetail> repoOrderDetail
          )
        {
            _repoOrderDetail = repoOrderDetail;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetail GetById(int id)
        {
            return this._repoOrderDetail.GetById(id);
        }

        public OrderDetail Insert(OrderDetail model)
        {
            return this._repoOrderDetail.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderDetail model)
        {
            this._repoOrderDetail.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderDetail model)
        {
            this._repoOrderDetail.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<OrderDetail>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.OrderNo.Contains(name));
            }

            totalCount = this._repoOrderDetail.Table.Where(where).Count();
            return this._repoOrderDetail.Table.Where(where).OrderBy(p => p.OrderDetailId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<OrderDetail> GetListByOrderNo(string order_no)
        {
            return this._repoOrderDetail.Table.Where(c => c.OrderNo == order_no).ToList();
        }
    }
}


