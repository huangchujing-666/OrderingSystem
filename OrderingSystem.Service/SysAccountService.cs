using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class SysAccountService : ISysAccountService
    {
        /// <summary>
        /// The SysAccount biz
        /// </summary>
        private ISysAccountBusiness _SysAccountBiz;

        public SysAccountService(ISysAccountBusiness SysAccountBiz)
        {
            _SysAccountBiz = SysAccountBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAccount GetById(int id)
        {
            return this._SysAccountBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysAccount Insert(SysAccount model)
        {
            return this._SysAccountBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysAccount model)
        {
            this._SysAccountBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysAccount model)
        {
            this._SysAccountBiz.Delete(model);
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param> 
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._SysAccountBiz.IsExistName(name);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SysAccount> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._SysAccountBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public List<SysAccount> GetAll()
        {
            return this._SysAccountBiz.GetAll();
        }

        public SysAccount Login(string account, string password)
        {
            return this._SysAccountBiz.Login(account, password);
        }

        public SysAccount GetAccountByToken(string token_Str)
        {
            return this._SysAccountBiz.GetAccountByToken(token_Str);
        }

        public bool UpdatePassWord(int account_id, string account, string oldPassword, string newPassword, out string msg)
        {
            return this._SysAccountBiz.UpdatePassWord(account_id, account, oldPassword, newPassword, out msg);
        }
    }
}
