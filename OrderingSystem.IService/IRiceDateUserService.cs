using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IRiceDateUserService
    {
        RiceDateUser GetById(int id);

        RiceDateUser Insert(RiceDateUser model);

        List<RiceDateUser> GetList();
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(RiceDateUser model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(RiceDateUser model);

        /// <summary>
        /// 管理后台列表
        /// </summary> 
        /// <returns></returns>
        List<RiceDateUser> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);
        /// <summary>
        /// 根据用户Id获取约饭成功集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<RiceDateUser> GetByUserId(int userId);

        /// <summary>
        /// 根据用户Id获取报名约饭集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<RiceDateUser> GetByUserId(int UserId, int Page_Index, int Page_Size, out int totalCount);
        /// <summary>
        /// 获取用户是否已经报名
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        RiceDateUser GetByUserIdAndRiceDateId(int userId, int riceDateId);

        /// <summary>
        /// 根据约饭ID获取报名人数
        /// </summary>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        List<RiceDateUser> GetByRiceDateId(int riceDateId);

        /// <summary>
        /// 获取报名我约饭的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<RiceDateUser> GetJoinMyRiceDate(int userId, int page_Index, int page_Size, out int totalCount);
    }
}
