using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessTypeBusiness:IBusinessTypeBusiness
    {
        private IRepository<BusinessType> _repoBusinessType;

        public BusinessTypeBusiness(
          IRepository<BusinessType> repoBusinessType
          )
        {
            _repoBusinessType = repoBusinessType;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessType GetById(int id)
        {
            return this._repoBusinessType.GetById(id);
        }

        public BusinessType Insert(BusinessType model)
        {
            return this._repoBusinessType.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessType model)
        {
            this._repoBusinessType.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessType model)
        {
            this._repoBusinessType.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessType> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessType>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.TypeName.Contains(name));
            }

            totalCount = this._repoBusinessType.Table.Where(where).Count();
            return this._repoBusinessType.Table.Where(where).OrderBy(p => p.BusinessTypeId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoBusinessType.Table.Any(p => p.TypeName == name);
        }
         

    }
}


