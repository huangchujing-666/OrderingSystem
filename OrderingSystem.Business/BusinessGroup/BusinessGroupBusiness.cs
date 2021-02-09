using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessGroupBusiness:IBusinessGroupBusiness
    {
        private IRepository<BusinessGroup> _repoBusinessGroup;

        public BusinessGroupBusiness(
          IRepository<BusinessGroup> repoBusinessGroup
          )
        {
            _repoBusinessGroup = repoBusinessGroup;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessGroup GetById(int id)
        {
            return this._repoBusinessGroup.GetById(id);
        }

        public BusinessGroup Insert(BusinessGroup model)
        {
            return this._repoBusinessGroup.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessGroup model)
        {
            this._repoBusinessGroup.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessGroup model)
        {
            this._repoBusinessGroup.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessGroup> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessGroup>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoBusinessGroup.Table.Where(where).Count();
            return this._repoBusinessGroup.Table.Where(where).OrderBy(p => p.BusinessGroupId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoBusinessGroup.Table.Any(p => p.Name == name);
        }
        public List<BusinessGroup> GetAll()
        {
            return this._repoBusinessGroup.Table.ToList();
        }


        /// <summary>
        /// 根据模块id获取分组
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public List<BusinessGroup> GetByBusinessTypeId(int module)
        {
            return this._repoBusinessGroup.Table.Where(c=>c.BusinessTypeId==module).ToList();
        }
    }
}


