using System;
namespace OrderingSystem.Domain.Model
{
    public class Level : IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int LevelId { get; set; }
        /// <summary>
        /// 评价等级名称
        /// </summary>
        public virtual string Name { get; set; }

    }
}