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
    public class PayDetailService: IPayDetailService
    {
        /// <summary>
        /// The PayDetail biz
        /// </summary>
        private IPayDetailBusiness _PayDetailBiz;

        public PayDetailService(IPayDetailBusiness PayDetailBiz)
        {
            _PayDetailBiz = PayDetailBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PayDetail GetById(int id)
        {
            return this._PayDetailBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PayDetail Insert(PayDetail model)
        {
            return this._PayDetailBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(PayDetail model)
        {
            this._PayDetailBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(PayDetail model)
        {
            this._PayDetailBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<PayDetail> GetManagerList(string orderNo, string userName, int payStatus, string startTime, string endTime, int pageNum, int pageSize, out int totalCount)
        {
            return this._PayDetailBiz.GetManagerList(orderNo, userName, payStatus, startTime, endTime, pageNum, pageSize, out totalCount);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public List<PayDetail> PayDetailExpert(string orderNo, string userName, int payStatus, string startTime, string endTime)
        {
            return this._PayDetailBiz.PayDetailExpert(orderNo, userName, payStatus, startTime, endTime);
        }

        public PayDetail GetDetailByOrderNo(string order_No)
        {
            return this._PayDetailBiz.GetDetailByOrderNo(order_No);
        }
    }
}
