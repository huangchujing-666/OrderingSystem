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
    public class ActivityDiscountBusiness : IActivityDiscountBusiness
    {
        private IRepository<ActivityDiscount> _repoActivityDiscount;

        public ActivityDiscountBusiness(
          IRepository<ActivityDiscount> repoActivityDiscount
          )
        {
            _repoActivityDiscount = repoActivityDiscount;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityDiscount GetById(int id)
        {
            return this._repoActivityDiscount.GetById(id);
        }

        public ActivityDiscount Insert(ActivityDiscount model)
        {
            return this._repoActivityDiscount.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(ActivityDiscount model)
        {
            this._repoActivityDiscount.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(ActivityDiscount model)
        {
            this._repoActivityDiscount.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<ActivityDiscount> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ActivityDiscount>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
               // where = where.And(m => m.BusinessInfo.Name.Contains(name));
            }

            totalCount = this._repoActivityDiscount.Table.Where(where).Count();
            return this._repoActivityDiscount.Table.Where(where).OrderByDescending(p => p.Discount).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }


        /// <summary>
        /// 根据商家Id获取折扣信息
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        //public ActivityDiscount GetByBusinessId(int businessId)
        //{
        //    return this._repoActivityDiscount.Table.Where(c => c.BusinessInfoId == businessId && c.Status == (int)EnumHelp.EnabledEnum.有效 && c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效).FirstOrDefault();
        //}
    }
}


