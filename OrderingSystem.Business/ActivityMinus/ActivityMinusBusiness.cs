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
    public class ActivityMinusBusiness:IActivityMinusBusiness
    {
        private IRepository<ActivityMinus> _repoActivityMinus;

        public ActivityMinusBusiness(
          IRepository<ActivityMinus> repoActivityMinus
          )
        {
            _repoActivityMinus = repoActivityMinus;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityMinus GetById(int id)
        {
            return this._repoActivityMinus.GetById(id);
        }

        public ActivityMinus Insert(ActivityMinus model)
        {
            return this._repoActivityMinus.Insert(model);
        }

        public ActivityMinus InsertVerifyRepeat(int BusinessInfoId, decimal AchiveAmount) {
            return this._repoActivityMinus.Table.Where(c => c.BusinessInfoId == BusinessInfoId && c.AchiveAmount == AchiveAmount).FirstOrDefault();
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ActivityMinus model)
        {
            this._repoActivityMinus.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ActivityMinus model)
        {
            this._repoActivityMinus.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<ActivityMinus> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ActivityMinus>();
               

            totalCount = this._repoActivityMinus.Table.Where(where).Count();
            return this._repoActivityMinus.Table.Where(where).OrderBy(p => p.ActivityMinusId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<ActivityMinus> GetListByBusinessId(int businessId)
        {
            return this._repoActivityMinus.Table.Where(c=>c.BusinessInfoId==businessId&&c.IsDelete==(int)EnumHelp.IsDeleteEnum.有效&&c.Status==(int)EnumHelp.EnabledEnum.有效).OrderByDescending(c=>c.AchiveAmount).ToList();
        }
    }
}


