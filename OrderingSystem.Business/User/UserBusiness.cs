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
    public class UserBusiness : IUserBusiness
    {
        private IRepository<User> _repoUser;

        public UserBusiness(
          IRepository<User> repoUser
          )
        {
            _repoUser = repoUser;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return this._repoUser.GetById(id);
        }

        public User Insert(User model)
        {
            return this._repoUser.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(User model)
        {
            this._repoUser.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(User model)
        {
            this._repoUser.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<User> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<User>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.NickName.Contains(name));
            }

            totalCount = this._repoUser.Table.Where(where).Count();
            return this._repoUser.Table.Where(where).OrderBy(p => p.UserId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoUser.Table.Any(p => p.NickName == name);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string phone, string password)
        {
            return this._repoUser.Table.Where(p => p.PhoneNo == phone).FirstOrDefault();
        }

        /// <summary>
        /// 判断是否电话存在
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool IsExistPhone(string phone)
        {
            return this._repoUser.Table.Any(p => p.PhoneNo == phone);
        }

        /// <summary>
        /// 根据手机号码和openId获取
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public User GetUserByPhoneOrOpenId(string mobile, string openId)
        {
            var where = PredicateBuilder.True<User>();
            if (!string.IsNullOrEmpty(mobile))
            {
                where = where.And(m => m.PhoneNo==mobile);
            }
            if (!string.IsNullOrEmpty(openId))
            {
                where = where.And(m => m.OpenId==openId);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            return this._repoUser.Table.Where(where).FirstOrDefault();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone">电话</param>
        /// <returns></returns>
        public User Login(string phone)
        {
            return this._repoUser.Table.Where(p => p.PhoneNo == phone).FirstOrDefault();
        }

        /// <summary>
        /// 根据openId获取用户
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public User GetUserByOpenId(string OpenId) {
            return this._repoUser.Table.Where(c => c.OpenId == OpenId&& c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).FirstOrDefault();
        }

    }
}


