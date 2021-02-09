using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class RiceDateFeedbackDTO
    {
        public int RiceDateId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}