using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class TicketRelateRoomBusiness : ITicketRelateRoomBusiness
    {
        private IRepository<TicketRelateRoom> _repoTicketRelateRoom;

        public TicketRelateRoomBusiness(
          IRepository<TicketRelateRoom> repoTicketRelateRoom
          )
        {
            _repoTicketRelateRoom = repoTicketRelateRoom;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TicketRelateRoom GetById(int id)
        {
            return this._repoTicketRelateRoom.GetById(id);
        }

        public TicketRelateRoom Insert(TicketRelateRoom model)
        {
            return this._repoTicketRelateRoom.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(TicketRelateRoom model)
        {
            this._repoTicketRelateRoom.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(TicketRelateRoom model)
        {
            this._repoTicketRelateRoom.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<TicketRelateRoom> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<TicketRelateRoom>();

            where = where.And(p => p.TicketId == ticketId);

            totalCount = this._repoTicketRelateRoom.Table.Where(where).Count();
            return this._repoTicketRelateRoom.Table.Where(where).OrderBy(p => p.TicketRelateRoomId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="relateRoomId"></param>
        /// <returns></returns>
        public bool IsExistName(int relateRoomId)
        {
            return this._repoTicketRelateRoom.Table.Any(p => p.RoomId == relateRoomId);
        }

        public List<TicketRelateRoom> GetAll()
        {
            return this._repoTicketRelateRoom.Table.ToList();
        }

    }
}


