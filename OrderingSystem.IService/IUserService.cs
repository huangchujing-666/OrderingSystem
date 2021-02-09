using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IUserService
    {
        User GetById(int id);

        User Insert(User model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(User model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(User model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<User> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExistName(string name);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Login(string phone, string password);

        /// <summary>
        /// 判断是否电话存在
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool IsExistPhone(string phone);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone">电话</param>
        /// <returns></returns>
        User Login(string phone);

        /// <summary>
        /// 根据ipenId获取用户信息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        User GetUserByOpenId(string OpenId);

        /// <summary>
        /// 根据OpenId手机号码获取用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        User GetUserByPhoneOrOpenId(string mobile, string openId);
    }
}
