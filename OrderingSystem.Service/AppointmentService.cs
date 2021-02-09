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
    public class AppointmentService: IAppointmentService
    {
        /// <summary>
        /// The Appointment biz
        /// </summary>
        private IAppointmentBusiness _AppointmentBiz;

        public AppointmentService(IAppointmentBusiness AppointmentBiz)
        {
            _AppointmentBiz = AppointmentBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Appointment GetById(int id)
        {
            return this._AppointmentBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Appointment Insert(Appointment model)
        {
            return this._AppointmentBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Appointment model)
        {
            this._AppointmentBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Appointment model)
        {
            this._AppointmentBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Appointment> GetManagerList(string userName, string phone, int pageNum, int pageSize, out int totalCount)
        {
            return this._AppointmentBiz.GetManagerList(userName, phone, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Appointment> GetAll()
        {
            return this._AppointmentBiz.GetAll();
        }

        /// <summary>
        /// 根据用户id获取预约信息
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        public List<Appointment> GetAppointmentListByUserId(int user_Id,int status_Id)
        {
            return this._AppointmentBiz.GetAppointmentListByUserId(user_Id, status_Id);
        }
    }
}
