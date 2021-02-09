using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface ITicketRelateRoomBusiness
    {


        TicketRelateRoom GetById(int id);

        TicketRelateRoom Insert(TicketRelateRoom model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(TicketRelateRoom model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(TicketRelateRoom model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<TicketRelateRoom> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="relateRoomId"></param>
        /// <returns></returns>
        bool IsExistName(int relateRoomId);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<TicketRelateRoom> GetAll();
    }
}
