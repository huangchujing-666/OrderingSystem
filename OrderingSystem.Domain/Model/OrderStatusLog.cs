using System;
namespace OrderingSystem.Domain.Model
{
	public class OrderStatusLog : IAggregateRoot
    {
		/// 
		/// </summary>
		public virtual int OrderStatusLogId { get; set; } 
        public virtual int OrderId { get; set; }
        public virtual int Status { get; set; }
        
        public virtual string StatusName { get; set; }
        public virtual DateTime CreateTime { get; set; }

    }
}

