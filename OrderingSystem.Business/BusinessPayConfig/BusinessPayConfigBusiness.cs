using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessPayConfigBusiness:IBusinessPayConfigBusiness
    {
        private IRepository<BusinessPayConfig> _repoBusinessPayConfig;

        public BusinessPayConfigBusiness(
          IRepository<BusinessPayConfig> repoBusinessPayConfig
          )
        {
            _repoBusinessPayConfig = repoBusinessPayConfig;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessPayConfig GetById(int id)
        {
            return this._repoBusinessPayConfig.GetById(id);
        }

        public BusinessPayConfig Insert(BusinessPayConfig model)
        {
            return this._repoBusinessPayConfig.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessPayConfig model)
        {
            this._repoBusinessPayConfig.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessPayConfig model)
        {
            this._repoBusinessPayConfig.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessPayConfig> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessPayConfig>();

            // businessId
            if (businessId!=0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }

            totalCount = this._repoBusinessPayConfig.Table.Where(where).Count();
            return this._repoBusinessPayConfig.Table.Where(where).OrderBy(p => p.BusinessPayConfigId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
         
    }
}


