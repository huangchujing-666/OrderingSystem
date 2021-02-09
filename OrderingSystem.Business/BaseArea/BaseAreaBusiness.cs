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
    public class BaseAreaBusiness:IBaseAreaBusiness
    {
        private IRepository<BaseArea> _repoBaseArea;

        public BaseAreaBusiness(
          IRepository<BaseArea> repoBaseArea
          )
        {
            _repoBaseArea = repoBaseArea;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseArea GetById(int id)
        {
            return this._repoBaseArea.GetById(id);
        }

        public BaseArea Insert(BaseArea model)
        {
            return this._repoBaseArea.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseArea model)
        {
            this._repoBaseArea.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseArea model)
        {
            this._repoBaseArea.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BaseArea> GetManagerList(string name,int pid, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BaseArea>();
         
            where = where.And(p => p.FId == pid);

            if(!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(p => p.Name.Contains(name));
            }
            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);
            totalCount = this._repoBaseArea.Table.Where(where).Count();
            return this._repoBaseArea.Table.Where(where).OrderBy(p => p.BaseAreaId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        public List<BaseArea> GetAll()
        { 
            return this._repoBaseArea.Table.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效).OrderBy(p => p.BaseAreaId).ToList();
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<BaseArea> GetCityList(int pid)
        {
            return this._repoBaseArea.Table.Where(p => p.FId == pid && p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效).OrderBy(p => p.BaseAreaId).ToList();
        }
    }
}


