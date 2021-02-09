using OrderingSystem.Core.Data;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class RiceDateUserBusiness : IRiceDateUserBusiness
    {
        private IRepository<RiceDateUser> _repoRiceDateUser;

        public RiceDateUserBusiness(
          IRepository<RiceDateUser> repoRiceDateUser
          )
        {
            _repoRiceDateUser = repoRiceDateUser;
        }
        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RiceDateUser GetById(int id)
        {
            return this._repoRiceDateUser.GetById(id);
        }

        public RiceDateUser Insert(RiceDateUser model)
        {
            return this._repoRiceDateUser.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RiceDateUser model)
        {
            this._repoRiceDateUser.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RiceDateUser model)
        {
            this._repoRiceDateUser.Delete(model);
        }
        /// <summary>
        /// 管理后台列表
        /// </summary> 
        /// <returns></returns>
        public List<RiceDateUser> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<RiceDateUser>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.User.UserName.Contains(name));
            }

            totalCount = this._repoRiceDateUser.Table.Where(where).Count();
            return this._repoRiceDateUser.Table.Where(where).OrderBy(p => p.RiceDateUserId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<RiceDateUser> GetList()
        {
            return this._repoRiceDateUser.Table.OrderBy(c => c.RiceDateUserId).ToList();
        }


        /// <summary>
        /// 根据用户Id获取约饭成功集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetByUserId(int userId)
        {
            return this._repoRiceDateUser.Table.Where(c => c.RiceDate.UserId == userId && c.ApplyStatus ==(int)EnumHelp.RiceDateApplyStatus.申请通过).ToList();
        }

        /// <summary>
        /// 根据用户id约饭id获取报名信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        public RiceDateUser GetByUserIdAndRiceDateId(int userId, int riceDateId)
        {
            return this._repoRiceDateUser.Table.Where(c => c.RiceDateId== riceDateId && c.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// 根据约饭id获取报名集合列表
        /// </summary>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetByRiceDateId(int riceDateId)
        {
            return this._repoRiceDateUser.Table.Where(c => c.RiceDateId == riceDateId).ToList();
        }

        /// <summary>
        /// 根据约饭id获取报名集合列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetByUserId(int userId, int page_Index, int page_Size, out int totalCount)
        {
            var result= this._repoRiceDateUser.Table.Where(c => c.UserId == userId).OrderByDescending(c=>c.RiceDate).ToList();
            totalCount = result.Count;
            return result.Skip((page_Size - 1) * page_Size).Take(page_Size).ToList();
        }

        /// <summary>
        /// 获取报名我发布的拼饭的用户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetJoinMyRiceDate(int userId, int page_Index, int page_Size, out int totalCount)
        {
            var result = this._repoRiceDateUser.Table.Where(c => c.RiceDate.UserId == userId).OrderByDescending(c => c.RiceDate).ToList();
            totalCount = result.Count;
            return result.Skip((page_Size - 1) * page_Size).Take(page_Size).ToList();
        }
    }
}


