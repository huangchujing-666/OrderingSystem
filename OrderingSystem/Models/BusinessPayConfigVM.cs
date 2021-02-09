
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    ///商家支付配置VM
    /// </summary>
    public class BusinessPayConfigVM 
    {

         
        public int Id { get; set; }
         
        public int BusinessInfoId { get; set; }

        public BusinessPayConfig BusinessPayConfig { get; set; }  

        public List<BusinessPayConfig> BusinessPayConfigList { get; set; }
         
        
    }
}