using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class LevelBusiness : ILevelBusiness
    {
        private IRepository<Level> _repoLevel;

        public LevelBusiness(
          IRepository<Level> repoLevel
          )
        {
            _repoLevel = repoLevel;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Level GetById(int id)
        {
            return this._repoLevel.GetById(id);
        }

        public Level Insert(Level model)
        {
            return this._repoLevel.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Level model)
        {
            this._repoLevel.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Level model)
        {
            this._repoLevel.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Level> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Level>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoLevel.Table.Where(where).Count();
            return this._repoLevel.Table.Where(where).OrderBy(p => p.LevelId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取等级列表
        /// </summary>
        /// <returns></returns>
        public List<Level> GetList()
        {
            return this._repoLevel.Table.OrderBy(c => c.LevelId).ToList();
        }
        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoLevel.Table.Any(p => p.Name == name);
        }

    }
}


