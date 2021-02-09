using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface ISysAccountBusiness
    {


        SysAccount GetById(int id);

        SysAccount Insert(SysAccount model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(SysAccount model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(SysAccount model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<SysAccount> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        List<SysAccount> GetAll();

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExistName(string name);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysAccount Login(string account, string password);
        SysAccount GetAccountByToken( string token_Str);
        bool UpdatePassWord(int account_id, string account, string oldPassword, string newPassword, out string msg);
    }
}
