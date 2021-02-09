using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class PayDetailBusiness : IPayDetailBusiness
    {
        private IRepository<PayDetail> _repoPayDetail;

        public PayDetailBusiness(
          IRepository<PayDetail> repoPayDetail
          )
        {
            _repoPayDetail = repoPayDetail;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PayDetail GetById(int id)
        {
            return this._repoPayDetail.GetById(id);
        }

        public PayDetail Insert(PayDetail model)
        {
            return this._repoPayDetail.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(PayDetail model)
        {
            this._repoPayDetail.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(PayDetail model)
        {
            this._repoPayDetail.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<PayDetail> GetManagerList(string orderNo, string userName, int payStatus, string startTime, string endTime, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<PayDetail>();

            // orderNo
            if (!string.IsNullOrEmpty(orderNo))
            {
                where = where.And(m => m.OrderNo.Contains(orderNo));
            }
            // userName
            if (!string.IsNullOrEmpty(userName))
            {
                where = where.And(m => m.User.NickName.Contains(userName));
            }
            // payStatus
            if (payStatus != 0)
            {
                where = where.And(m => m.PayStatus == payStatus);
            }
            // startTime
            if (!string.IsNullOrEmpty(startTime))
            {
                DateTime t1 = DateTime.Parse(startTime + " 00:00:00");
                where = where.And(m => m.OrderTime >= t1);
            }
            //endTime
            if (!string.IsNullOrEmpty(endTime))
            {
                DateTime t1 = DateTime.Parse(endTime + " 23:59:59");
                where = where.And(m => m.OrderTime <= t1);
            }

            totalCount = this._repoPayDetail.Table.Where(where).Count();
            return this._repoPayDetail.Table.Where(where).OrderBy(p => p.PayDetailId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="userName"></param>
        /// <param name="payStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PayDetail> PayDetailExpert(string orderNo, string userName, int payStatus, string startTime, string endTime)
        {
            var where = PredicateBuilder.True<PayDetail>();

            // orderNo
            if (!string.IsNullOrEmpty(orderNo))
            {
                where = where.And(m => m.OrderNo.Contains(orderNo));
            }
            // userName
            if (!string.IsNullOrEmpty(userName))
            {
                where = where.And(m => m.User.NickName.Contains(userName));
            }
            // payStatus
            if (payStatus != 0)
            {
                where = where.And(m => m.PayStatus == payStatus);
            }
            // startTime
            if (!string.IsNullOrEmpty(startTime))
            {
                DateTime t1 = DateTime.Parse(startTime + " 00:00:00");
                where = where.And(m => m.OrderTime >= t1);
            }
            //endTime
            if (!string.IsNullOrEmpty(endTime))
            {
                DateTime t1 = DateTime.Parse(endTime + " 23:59:59");
                where = where.And(m => m.OrderTime <= t1);
            }

            return this._repoPayDetail.Table.Where(where).OrderBy(p => p.PayDetailId).ToList();

        }
        public PayDetail GetDetailByOrderNo(string order_No)
        {
            var where = PredicateBuilder.True<PayDetail>();

            if (!string.IsNullOrWhiteSpace(order_No))
            {
                where = where.And(c => c.OrderNo == order_No);
            }
            return this._repoPayDetail.Table.Where(where).FirstOrDefault();
        }
    }
}


