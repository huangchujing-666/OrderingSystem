using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IAppointmentService
    {
        Appointment GetById(int id);

        Appointment Insert(Appointment model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Appointment model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Appointment model);

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<Appointment> GetManagerList(string userName, string phone, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Appointment> GetAll();

        /// <summary>
        /// 根据用户id获取预约信息
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        List<Appointment> GetAppointmentListByUserId(int user_Id,int status_Id);
    }
}
