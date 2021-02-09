using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IRiceDateBusiness
    {


        RiceDate GetById(int id);

        RiceDate Insert(RiceDate model);

        List<RiceDate> GetList();
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(RiceDate model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(RiceDate model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<RiceDate> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据发布人 约饭Id获取约饭信息
        /// </summary>
        /// <param name="riceDateId">约饭主键</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        RiceDate GetByUserIdAndRiceDateId(int riceDateId, int userId);

        /// <summary>
        /// 多条件查询拼饭内容
        /// </summary>
        /// <param name="date"></param>
        /// <param name="businessName"></param>
        /// <param name="district"></param>
        /// <returns></returns>
        List<RiceDate> GetRiceDateByCondiction(DateTime? date, string businessName, string district,int Page_Index, int Page_Size,out int totalCount);

        /// <summary>
        /// 根据用户id获取发布的拼饭列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<RiceDate> GetByUserId(int userId, int page_Index, int page_Size, out int totalCount);
    }
}
