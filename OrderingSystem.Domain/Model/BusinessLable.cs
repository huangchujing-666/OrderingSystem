using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class BusinessLable : IAggregateRoot
    {
        public int BusinessLableId { get; set; }

        public string Name { get; set; }

        public int BusinessInfoId { get; set; }

        public virtual BusinessInfo BusinessInfo { get; set; }
    }
}
