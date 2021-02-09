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
    public class RoomImageBusiness:IRoomImageBusiness
    {
        private IRepository<RoomImage> _repoRoomImage;

        public RoomImageBusiness(
          IRepository<RoomImage> repoRoomImage
          )
        {
            _repoRoomImage = repoRoomImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomImage GetById(int id)
        {
            return this._repoRoomImage.GetById(id);
        }

        public RoomImage Insert(RoomImage model)
        {
            return this._repoRoomImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RoomImage model)
        {
            this._repoRoomImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RoomImage model)
        {
            this._repoRoomImage.Delete(model);
        }
        /// <summary>
        /// 管理后台列表
        /// </summary> 
        /// <returns></returns>
        public List<RoomImage> GetManagerList(int roomId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<RoomImage>();
            if (roomId > 0)
            {
                where = where.And(p => p.RoomId == roomId);
            }

            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoRoomImage.Table.Where(where).Count();
            return this._repoRoomImage.Table.Where(where).OrderBy(p => p.RoomImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

      
    }
}


