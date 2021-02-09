using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IActivityMinusBusiness
    {


        ActivityMinus GetById(int id);

        ActivityMinus Insert(ActivityMinus model);


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
        /// 重复验证
        /// </summary>
        /// <param name="BusinessInfoId"></param>
        /// <param name="AchiveAmount"></param>
        /// <returns></returns>
        ActivityMinus InsertVerifyRepeat(int BusinessInfoId, decimal AchiveAmount);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<ActivityMinus> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据商家Id获取满减列表
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        List<ActivityMinus> GetListByBusinessId(int businessId);

    }
}
