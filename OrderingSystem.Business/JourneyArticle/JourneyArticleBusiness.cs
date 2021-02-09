using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Business
{
    public class JourneyArticleBusiness : IJourneyArticleBusiness
    {
        private IRepository<JourneyArticle> _repoJourneyArticle;

        public JourneyArticleBusiness(
          IRepository<JourneyArticle> repoJourneyArticle
          )
        {
            _repoJourneyArticle = repoJourneyArticle;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JourneyArticle GetById(int id)
        {
            return this._repoJourneyArticle.GetById(id);
        }

        public JourneyArticle Insert(JourneyArticle model)
        {
            return this._repoJourneyArticle.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(JourneyArticle model)
        {
            this._repoJourneyArticle.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(JourneyArticle model)
        {
            this._repoJourneyArticle.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<JourneyArticle> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<JourneyArticle>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                where = where.And(m => m.BusinessInfo.Name.Contains(businessName));
            }
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoJourneyArticle.Table.Where(where).Count();
            return this._repoJourneyArticle.Table.Where(where).OrderBy(p => p.JourneyArticleId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoJourneyArticle.Table.Any(p => p.Name == name);
        }
        public List<JourneyArticle> GetAll()
        {
            return this._repoJourneyArticle.Table.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效).ToList();
        }

        public List<JourneyArticle> GetByBusinessId(int business_Id, int page_Index, int page_Size, out int total_count)
        {
            var where = PredicateBuilder.True<JourneyArticle>();

            if (business_Id>0)
            {
                where = where.And(m => m.BusinessInfoId== business_Id);
            }
            total_count = this._repoJourneyArticle.Table.Where(where).Count();
            return this._repoJourneyArticle.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((page_Index - 1) * page_Size).Take(page_Size).ToList();
        }
    }

}


