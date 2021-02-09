using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class SysRoleMenu : IAggregateRoot
    {
        public virtual int SysRoleMenuId { get; set; } 
        public virtual int SysMenuId { get; set; }
        public virtual int SysRoleId { get; set; } 
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; }

         public virtual SysMenu SysMenu { get; set; }
    }
}
