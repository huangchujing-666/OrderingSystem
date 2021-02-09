using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessLableBusiness:IBusinessLableBusiness
    {
        private IRepository<BusinessLable> _repoBusinessLable;

        public BusinessLableBusiness(
          IRepository<BusinessLable> repoBusinessLable
          )
        {
            _repoBusinessLable = repoBusinessLable;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessLable GetById(int id)
        {
            return this._repoBusinessLable.GetById(id);
        }

        public BusinessLable Insert(BusinessLable model)
        {
            return this._repoBusinessLable.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessLable model)
        {
            this._repoBusinessLable.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessLable model)
        {
            this._repoBusinessLable.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessLable> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessLable>();

            // businessId
            if (businessId ==0 )
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }

            totalCount = this._repoBusinessLable.Table.Where(where).Count();
            return this._repoBusinessLable.Table.Where(where).OrderBy(p => p.BusinessLableId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoBusinessLable.Table.Any(p => p.Name == name);
        }
        public List<BusinessLable> GetAll()
        {
            return this._repoBusinessLable.Table.ToList();
        }
    }
}


