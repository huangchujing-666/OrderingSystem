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
    public class SysAccountBusiness:ISysAccountBusiness
    {
        private IRepository<SysAccount> _repoSysAccount;

        public SysAccountBusiness(
          IRepository<SysAccount> repoSysAccount
          )
        {
            _repoSysAccount = repoSysAccount;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAccount GetById(int id)
        {
            return this._repoSysAccount.GetById(id);
        }

        public SysAccount Insert(SysAccount model)
        {
            return this._repoSysAccount.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysAccount model)
        {
            this._repoSysAccount.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysAccount model)
        {
            this._repoSysAccount.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysAccount> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SysAccount>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.NickName.Contains(name) || m.Account.Contains(name));
            }
             
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);
 
            totalCount = this._repoSysAccount.Table.Where(where).Count();
            return this._repoSysAccount.Table.Where(where).OrderBy(p => p.SysAccountId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<SysAccount> GetAll()
        {
            var where = PredicateBuilder.True<SysAccount>();
              
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);
             
            return this._repoSysAccount.Table.Where(where).OrderBy(p => p.SysAccountId).ToList();
        }


        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoSysAccount.Table.Any(p => p.NickName == name);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysAccount Login(string account, string password)
        {
            return this._repoSysAccount.Table.Where(p => p.Account == account && p.PassWord == password).FirstOrDefault();
        }

        public SysAccount GetAccountByToken( string token_Str)
        {
            return this._repoSysAccount.Table.Where(p =>  p.Token.Equals(token_Str)).FirstOrDefault();
        }

        public bool UpdatePassWord(int account_id, string account, string oldPassword, string newPassword, out string msg)
        {
            msg = "";
            bool result = false;
            var sysBusiness = _repoSysAccount.Table.Where(c => c.BusinessInfoId == account_id && c.Account == account && c.PassWord == oldPassword).FirstOrDefault();
            if (sysBusiness != null)
            {
                sysBusiness.PassWord = newPassword;
                _repoSysAccount.Update(sysBusiness);
                result = true;
                msg = "更改密码成功";
            }
            else
            {
                msg = "您输入的原始密码错误，请重新输入";
            }
            return result;
        }
    }
}


