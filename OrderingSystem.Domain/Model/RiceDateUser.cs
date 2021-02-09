using System;
namespace OrderingSystem.Domain.Model
{
    public class RiceDateUser : IAggregateRoot
    { 
        public virtual int RiceDateUserId { get; set; }
 
        /// <summary>
        /// 拼饭ID
        /// </summary>
        public virtual int RiceDateId { get; set; }

        public virtual RiceDate RiceDate { get; set; }
        public virtual User User { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int ApplyStatus { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; } 

    }
}