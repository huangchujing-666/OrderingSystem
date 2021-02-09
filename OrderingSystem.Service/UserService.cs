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
    public class UserService: IUserService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private IUserBusiness _UserBiz;

        public UserService(IUserBusiness UserBiz)
        {
            _UserBiz = UserBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return this._UserBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public User Insert(User model)
        {
            return this._UserBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(User model)
        {
            this._UserBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(User model)
        {
            this._UserBiz.Delete(model);
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param> 
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._UserBiz.IsExistName(name);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<User> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._UserBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public User Login(string phone, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsExistPhone(string phone)
        {
           return this._UserBiz.IsExistPhone(phone);
        }

        public User Login(string phone)
        {
            throw new NotImplementedException();
        }

        public User GetUserByOpenId(string OpenId) {
           return  this._UserBiz.GetUserByOpenId(OpenId);
        }


        public User GetUserByPhoneOrOpenId(string mobile, string openId) {
            return this._UserBiz.GetUserByPhoneOrOpenId(mobile, openId);
        }

    }
}
