using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
    
    /// <summary>
    /// 乐模块房间表
    /// </summary>
    public class Room : IAggregateRoot
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public virtual int RoomId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Window { get; set; } 
        public virtual string Breakfast { get; set; } 
        public virtual string Area { get; set; } 
        public virtual string Internet { get; set; } 
        /// <summary>
        /// 床的尺寸，如180*120
        /// </summary>
        public virtual string Bed { get; set; } 
        /// <summary>
        /// 大床 双床等
        /// </summary>
        public virtual string BedType { get; set; }
        public virtual string Bathroom { get; set; } 
        public virtual string Floor { get; set; }
        public virtual string AirConditioner { get; set; } 
        public virtual int Remain { get; set; }
        public virtual decimal OrignPrice { get; set; }
        public virtual decimal RealPrice { get; set; } 
        /// <summary>
        /// 购买须知
        /// </summary>
        public virtual string Notice { get; set; }
        /// <summary>
        /// 使用规则
        /// </summary>
        public virtual string Rules { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }


        public virtual int Status { get; set; }
        public virtual int IsDelete { get; set; }
         

        public virtual BusinessInfo BusinessInfo { get; set; }

        public virtual List<RoomImage> RoomImageList { get; set; }

    }
}

