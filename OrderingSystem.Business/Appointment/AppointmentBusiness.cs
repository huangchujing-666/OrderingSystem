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
    public class AppointmentBusiness:IAppointmentBusiness
    {
        private IRepository<Appointment> _repoAppointment;

        public AppointmentBusiness(
          IRepository<Appointment> repoAppointment
          )
        {
            _repoAppointment = repoAppointment;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Appointment GetById(int id)
        {
            return this._repoAppointment.GetById(id);
        }

        public Appointment Insert(Appointment model)
        {
            return this._repoAppointment.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Appointment model)
        {
            this._repoAppointment.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Appointment model)
        {
            this._repoAppointment.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Appointment> GetManagerList(string userName, string phone, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Appointment>();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                where = where.And(p => p.UserName.Contains(userName));
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                where = where.And(p => p.Phone.Contains(phone));
            }
            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoAppointment.Table.Where(where).Count();
            return this._repoAppointment.Table.Where(where).OrderBy(p => p.AppointmentId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        public List<Appointment> GetAll()
        { 
            return this._repoAppointment.Table.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效).OrderByDescending(p => p.AppointmentTime).ToList();
        }

        /// <summary>
        /// 根据用户id获取预约列表
        /// </summary>
        /// <returns></returns>
        public List<Appointment> GetAppointmentListByUserId(int user_Id,int status_Id)
        {
            var where = PredicateBuilder.True<Appointment>();
            if (status_Id>0)
            {
                where = where.And(p => p.Status== status_Id);
            }
            where = where.And(p => p.UserId == user_Id&& p.IsDelete == (int)IsDeleteEnum.有效);
            return this._repoAppointment.Table.Where(where).OrderBy(p => p.AppointmentId).ToList();
        }
    }
}


