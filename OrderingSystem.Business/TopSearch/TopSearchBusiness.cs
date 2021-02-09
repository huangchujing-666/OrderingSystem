using OrderingSystem.Core.Data;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class TopSearchBusiness : ITopSearchBusiness
    {
        private IRepository<TopSearch> _repoTopSearch;

        public TopSearchBusiness(
          IRepository<TopSearch> repoTopSearch
          )
        {
            _repoTopSearch = repoTopSearch;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopSearch GetById(int id)
        {
            return this._repoTopSearch.GetById(id);
        }

        public TopSearch Insert(TopSearch model)
        {
            return this._repoTopSearch.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(TopSearch model)
        {
            this._repoTopSearch.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(TopSearch model)
        {
            this._repoTopSearch.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<TopSearch> GetManagerList(string name, int typeid, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<TopSearch>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Contents.Contains(name));
            }
            if (typeid > 0)
            {
                where = where.And(m => m.TypeId == typeid);
            }
            where=where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            totalCount = this._repoTopSearch.Table.Where(where).ToList().Count;
            return this._repoTopSearch.Table.Where(where).OrderBy(p => p.SortNo).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }


    }
}


