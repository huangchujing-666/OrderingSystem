using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBusinessCommentService
    {
        BusinessComment GetById(int id);

        BusinessComment Insert(BusinessComment model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BusinessComment model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BusinessComment model);

        /// <summary>
        /// 判断用户是否已经对订单评论了
        /// </summary>
        /// <param name="businesssId"></param>
        /// <param name="userInfoId"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        bool IsExist(int businesssId, int userInfoId, string orderNo);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessComment> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);


        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="levelId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<BusinessComment> GetPageList(int businessId, int levelId, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="levelId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<BusinessComment> GetPageListByUserId(int userId,int module, int levelId, int pageNum, int pageSize, out int totalCount);
    }
}
