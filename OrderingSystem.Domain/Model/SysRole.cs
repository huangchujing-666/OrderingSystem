using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class SysRole : IAggregateRoot
    {
        public virtual int SysRoleId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Remarks { get; set; }
        public virtual int IsDelete { get; set; } 
        public virtual int Status { get; set; } 
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; }

         
    }
}
