
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 衣品预约VM
    /// </summary>
    public class AppointmentVM : BaseImgInfoVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 基础字典信息
        /// </summary>
        public Appointment Appointment { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<Appointment> Paging { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Appointment> Appointments { get; set; }
        
        /// <summary>
        /// 衣品列表信息
        /// </summary>
        public List<Goods> GoodsList { get; set; }

        //查询条件
        public string QueryUserName { get; set; }
        public string QueryPhone { get; set; }
    }
}