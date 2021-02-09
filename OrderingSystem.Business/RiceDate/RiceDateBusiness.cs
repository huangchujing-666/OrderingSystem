using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain;

namespace OrderingSystem.Business
{
    public class RiceDateBusiness : IRiceDateBusiness
    {
        private IRepository<RiceDate> _repoRiceDate;

        public RiceDateBusiness(
          IRepository<RiceDate> repoRiceDate
          )
        {
            _repoRiceDate = repoRiceDate;
        }
        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RiceDate GetById(int id)
        {
            return this._repoRiceDate.GetById(id);
        }

        public RiceDate Insert(RiceDate model)
        {
            return this._repoRiceDate.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RiceDate model)
        {
            this._repoRiceDate.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RiceDate model)
        {
            this._repoRiceDate.Delete(model);
        }
        /// <summary>
        /// 管理后台列表
        /// </summary> 
        /// <returns></returns>
        public List<RiceDate> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<RiceDate>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.BusinessName.Contains(name));
            }

            where = where.And(m => m.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);

            totalCount = this._repoRiceDate.Table.Where(where).Count();
            return this._repoRiceDate.Table.Where(where).OrderBy(p => p.RiceDateId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<RiceDate> GetList()
        {
            return this._repoRiceDate.Table.OrderBy(c => c.RiceDateId).ToList();
        }

        /// <summary>
        /// 根据发布人 约饭Id获取约饭信息
        /// </summary>
        /// <param name="riceDateId">约饭主键</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public RiceDate GetByUserIdAndRiceDateId(int riceDateId, int userId)
        {
            return this._repoRiceDate.Table.Where(c => c.RiceDateId == riceDateId && c.UserId == userId).FirstOrDefault();
        }


        /// <summary>
        /// 多条件查询拼饭内容
        /// </summary>
        /// <param name="date"></param>
        /// <param name="businessName"></param>
        /// <param name="district"></param>
        /// <returns></returns>
        public List<RiceDate> GetRiceDateByCondiction(DateTime? date, string businessName, string district, int Page_Index, int Page_Size, out int totalCount)
        {
            var where = PredicateBuilder.True<RiceDate>();

            // name过滤
            if (!string.IsNullOrWhiteSpace(businessName))
            {
                where = where.And(m => m.BusinessName.Contains(businessName));
            }
            if (!string.IsNullOrWhiteSpace(district))
            {
                where = where.And(m => m.Zone.Equals(district));
            }
            if (date != null)
            {
                var dateTime = DateTime.Parse(date.ToString());
                where = where.And(m => m.BeginDate.Year == dateTime.Year && m.BeginDate.Month == dateTime.Month && m.BeginDate.Day == dateTime.Day);
            }

            where = where.And(m => m.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && m.Status == (int)EnumHelp.EnabledEnum.有效);

            totalCount = this._repoRiceDate.Table.Where(where).Count();
            return this._repoRiceDate.Table.Where(where).OrderByDescending(p => p.BeginDate).Skip((Page_Index - 1) * Page_Size).Take(Page_Size).ToList();
        }

        /// <summary>
        /// 根据用户id获取发布的拼饭列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<RiceDate> GetByUserId(int userId, int page_Index, int page_Size, out int totalCount)
        {
            var where = PredicateBuilder.True<RiceDate>();

            where = where.And(m => m.UserId == userId && m.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && m.Status == (int)EnumHelp.EnabledEnum.有效);

            totalCount = this._repoRiceDate.Table.Where(where).Count();
            return this._repoRiceDate.Table.Where(where).OrderByDescending(p => p.BeginDate).Skip((page_Index - 1) * page_Size).Take(page_Size).ToList();
        }
    }
}


