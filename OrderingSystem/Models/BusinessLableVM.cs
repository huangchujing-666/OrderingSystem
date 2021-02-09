
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    ///商家标签VM
    /// </summary>
    public class BusinessLableVM : BaseImgInfoVM
    {

         
        public int Id { get; set; }
         
        public int BusinessInfoId { get; set; }

        public BusinessLable BusinessLable { get; set; }  

        public List<BusinessLable> BusinessLableList { get; set; }
         
        
    }
}