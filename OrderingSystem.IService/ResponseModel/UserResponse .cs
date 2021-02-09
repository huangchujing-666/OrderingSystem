
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.IService.ResponseModel
{
    /// <summary>
    /// 用户响应
    /// </summary>
    public class UserResponse : ResponseModelBase
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public IList<User> UserList
        {
            get;
            set;
        }

        public User UserReturn
        {
            get;
            set;
        }
  
    }
}
