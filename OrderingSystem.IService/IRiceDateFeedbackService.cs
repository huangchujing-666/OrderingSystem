using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IRiceDateFeedbackService
    {
        RiceDateFeedback GetById(int id);

        RiceDateFeedback Insert(RiceDateFeedback model);

        List<RiceDateFeedback> GetList();
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(RiceDateFeedback model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(RiceDateFeedback model);

        /// <summary>
        /// 管理后台列表
        /// </summary> 
        /// <returns></returns>
        List<RiceDateFeedback> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);
        /// <summary>
        /// 获取用户被投诉集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<RiceDateFeedback> GetByUserId(int userId);

        /// <summary>
        /// 用户id  约饭id获取投诉信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        RiceDateFeedback GetByUserIdAndRiceDateId(int userId, int riceDateId);
    }
}
