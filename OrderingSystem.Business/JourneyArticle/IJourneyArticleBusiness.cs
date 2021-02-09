using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IJourneyArticleBusiness
    {


        JourneyArticle GetById(int id);

        JourneyArticle Insert(JourneyArticle model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(JourneyArticle model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(JourneyArticle model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<JourneyArticle> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExistName(string name);

        List<JourneyArticle> GetAll();
        List<JourneyArticle> GetByBusinessId(int business_Id, int page_Index, int page_Size, out int total_count);
    }
}
