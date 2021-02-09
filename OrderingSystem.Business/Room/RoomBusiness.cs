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
    public class RoomBusiness : IRoomBusiness
    {
        private IRepository<Room> _repoRoom;

        public RoomBusiness(
          IRepository<Room> repoRoom
          )
        {
            _repoRoom = repoRoom;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room GetById(int id)
        {
            return this._repoRoom.GetById(id);
        }

        public Room Insert(Room model)
        {
            return this._repoRoom.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Room model)
        {
            this._repoRoom.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Room model)
        {
            this._repoRoom.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Room> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            var where = PredicateBuilder.True<Room>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                where = where.And(m => m.BusinessInfo.Name.Contains(businessName));
            }
            if (businessId != 0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }

            where = where.And(m => m.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoRoom.Table.Where(where).Count();
            return this._repoRoom.Table.Where(where).OrderBy(p => p.RoomId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoRoom.Table.Any(p => p.Name == name);
        }

        public List<Room> GetAll()
        {
            return this._repoRoom.Table.ToList();
        }
        /// <summary>
        /// 根据商家ID获取房间列表
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public List<Room> GetListByBusinessId(int businessId)
        {
            var where = PredicateBuilder.True<Room>();

            where = where.And(m => m.BusinessInfoId == businessId);

            where = where.And(m => m.IsDelete == (int)IsDeleteEnum.有效);

            return this._repoRoom.Table.Where(where).ToList();
        }
    }
}


