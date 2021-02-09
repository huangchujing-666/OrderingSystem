using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IActivityMinusService
    {
        ActivityMinus GetById(int id);

        ActivityMinus Insert(ActivityMinus model);

        /// <summary>
        /// 插入前验证重复
        /// </summary>
        /// <param name="BusinessInfoId">商家Id</param>
        /// <param name="AchiveAmount">到达金额</param>
        /// <returns></returns>
        ActivityMinus InsertVerifyRepeat(int BusinessInfoId, decimal AchiveAmount);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(ActivityMinus model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(ActivityMinus model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<ActivityMinus> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据商家Id获取满减信息
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        List<ActivityMinus> GetListByBusinessId(int businessId);
    }
}
