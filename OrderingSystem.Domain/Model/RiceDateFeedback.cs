using System;
namespace OrderingSystem.Domain.Model
{
    public class RiceDateFeedback : IAggregateRoot
    { 
        public virtual int RiceDateFeedbackId { get; set; }
 

        public virtual RiceDate RiceDate { get; set; }
        /// <summary>
        /// 拼饭id    
        /// </summary>
        public virtual int RiceDateId { get; set; }
        public virtual User User { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual int UserId { get; set; }
         
        /// <summary>
        /// 反馈内容
        /// </summary>
        public virtual string Content { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; } 

    }
}