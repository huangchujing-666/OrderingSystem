using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class SysMenu : IAggregateRoot
    {
        public virtual int SysMenuId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
        public virtual string Icon { get; set; }
        public virtual int Fid { get; set; }
        public virtual int SortNo { get; set; }
        public virtual int IsDelete { get; set; } 
        public virtual int Status { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; }

         
    }
}
