using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BaseDicBusiness : IBaseDicBusiness
    {
        private IRepository<BaseDic> _repoBaseDic;

        public BaseDicBusiness(
          IRepository<BaseDic> repoBaseDic
          )
        {
            _repoBaseDic = repoBaseDic;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseDic GetById(int id)
        {
            return this._repoBaseDic.GetById(id);
        }

        public BaseDic Insert(BaseDic model)
        {
            return this._repoBaseDic.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseDic model)
        {
            this._repoBaseDic.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseDic model)
        {
            this._repoBaseDic.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BaseDic> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BaseDic>();


            totalCount = this._repoBaseDic.Table.Where(where).Count();
            return this._repoBaseDic.Table.Where(where).OrderBy(p => p.BaseDicId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 查询所有
        /// </summary> 
        /// <returns></returns>
        public List<BaseDic> GetAll()
        {
            var where = PredicateBuilder.True<BaseDic>();

            return this._repoBaseDic.Table.ToList();
        }


        /// <summary>
        /// 根据type获取字典集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<BaseDic> GetBaseDiscListByType(string type)
        {
            var where = PredicateBuilder.True<BaseDic>();
            where = where.And(c => c.Type.Contains(type));
            return this._repoBaseDic.Table.Where(where).OrderBy(c=>c.SortNo).ToList();
        }
    }
}


