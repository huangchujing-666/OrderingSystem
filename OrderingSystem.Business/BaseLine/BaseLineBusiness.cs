using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Business
{
    public class BaseLineBusiness:IBaseLineBusiness
    {
        private IRepository<BaseLine> _repoBaseLine;

        public BaseLineBusiness(
          IRepository<BaseLine> repoBaseLine
          )
        {
            _repoBaseLine = repoBaseLine;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseLine GetById(int id)
        {
            return this._repoBaseLine.GetById(id);
        }

        public BaseLine Insert(BaseLine model)
        {
            return this._repoBaseLine.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseLine model)
        {
            this._repoBaseLine.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseLine model)
        {
            this._repoBaseLine.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BaseLine> GetManagerList(string lineName, string areaName, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BaseLine>();

            if (!string.IsNullOrWhiteSpace(lineName))
            {
                where = where.And(p => p.LineName.Contains(lineName));
            }
            if (!string.IsNullOrWhiteSpace(areaName))
            {
                where = where.And(p => p.BaseArea.Name.Contains(areaName));
            }
            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoBaseLine.Table.Where(where).Count();
            return this._repoBaseLine.Table.Where(where).OrderBy(p => p.BaseLineId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        public List<BaseLine> GetAll()
        { 
            return this._repoBaseLine.Table.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效).OrderBy(p => p.LineNumber).ToList();
        }
    }
}


