using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface ITicketRelateTicketBusiness
    {


        TicketRelateTicket GetById(int id);

        TicketRelateTicket Insert(TicketRelateTicket model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(TicketRelateTicket model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(TicketRelateTicket model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<TicketRelateTicket> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExistName(int relateTicketId);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<TicketRelateTicket> GetAll();
    }
}
