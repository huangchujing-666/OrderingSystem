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
    public class SmsLogService: ISmsLogService
    {
        /// <summary>
        /// The SmsLog biz
        /// </summary>
        private ISmsLogBusiness _SmsLogBiz;

        public SmsLogService(ISmsLogBusiness SmsLogBiz)
        {
            _SmsLogBiz = SmsLogBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SmsLog GetById(int id)
        {
            return this._SmsLogBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SmsLog Insert(SmsLog model)
        {
            return this._SmsLogBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SmsLog model)
        {
            this._SmsLogBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SmsLog model)
        {
            this._SmsLogBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SmsLog> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._SmsLogBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public SmsLog GetByPhoneNo(string Phone) {
            return this._SmsLogBiz.GetByPhoneNo(Phone);
        }
    }
}
