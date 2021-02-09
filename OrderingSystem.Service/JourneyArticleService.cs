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
    public class JourneyArticleService: IJourneyArticleService
    {
        /// <summary>
        /// The JourneyArticle biz
        /// </summary>
        private IJourneyArticleBusiness _JourneyArticleBiz;

        public JourneyArticleService(IJourneyArticleBusiness JourneyArticleBiz)
        {
            _JourneyArticleBiz = JourneyArticleBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JourneyArticle GetById(int id)
        {
            return this._JourneyArticleBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JourneyArticle Insert(JourneyArticle model)
        {
            return this._JourneyArticleBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(JourneyArticle model)
        {
            this._JourneyArticleBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(JourneyArticle model)
        {
            this._JourneyArticleBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<JourneyArticle> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount)
        {
            return this._JourneyArticleBiz.GetManagerList(name, businessName, pageNum, pageSize, out totalCount);
        }

        public List<JourneyArticle> GetAll()
        {
            return this._JourneyArticleBiz.GetAll();
        }
        public List<JourneyArticle> GetByBusinessId(int business_Id, int page_Index, int page_Size, out int total_count)
        {
            return this._JourneyArticleBiz.GetByBusinessId(business_Id, page_Index, page_Size,out total_count);
        }
    }
}
