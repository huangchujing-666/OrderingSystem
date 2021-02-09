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
    public class BaseStationBusiness:IBaseStationBusiness
    {
        private IRepository<BaseStation> _repoBaseStation;

        public BaseStationBusiness(
          IRepository<BaseStation> repoBaseStation
          )
        {
            _repoBaseStation = repoBaseStation;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation GetById(int id)
        {
            return this._repoBaseStation.GetById(id);
        }

        public BaseStation Insert(BaseStation model)
        {
            return this._repoBaseStation.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseStation model)
        {
            this._repoBaseStation.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseStation model)
        {
            this._repoBaseStation.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BaseStation> GetManagerList(string station,string line, string area, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BaseStation>();
            if (!string.IsNullOrWhiteSpace(station))
            {
                where = where.And(p => p.Name.Contains(station));
            }
            if (!string.IsNullOrWhiteSpace(line))
            {
                where = where.And(p => p.BaseLine.LineName.Contains(line));
            }
            if (!string.IsNullOrWhiteSpace(area))
            {
                where = where.And(p => p.BaseArea.Name.Contains(area));
            }
            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);


            totalCount = this._repoBaseStation.Table.Where(where).Count();
            return this._repoBaseStation.Table.Where(where).OrderBy(p => p.BaseStationId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        public List<BaseStation> GetAll()
        { 
            return this._repoBaseStation.Table.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效).OrderBy(p => p.BaseStationId).ToList();
        }
    }
}


