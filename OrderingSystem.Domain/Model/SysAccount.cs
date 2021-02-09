using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class SysAccount : IAggregateRoot
    {
        public virtual int SysAccountId { get; set; }
        public virtual int SysRoleId { get; set; }
        public virtual int BusinessInfoId { get; set; }
        public virtual string Account { get; set; }
        public virtual string PassWord { get; set; }
        public virtual int BaseImageId { get; set; }
        public virtual string MobilePhone { get; set; }
        public virtual string NickName { get; set; }  
        public virtual string Remarks { get; set; }
        public virtual int IsDelete { get; set; }
        public virtual int Status { get; set; } 
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; }
        public virtual string Token { get; set; }
        public virtual BaseImage BaseImage { get; set; }
        public virtual SysRole SysRole { get; set; }
        public virtual BusinessInfo BusinessInfo { get; set; }

        public virtual DateTime LoginTime { get; set; }

    }
}
