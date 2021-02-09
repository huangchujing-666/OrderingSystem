using OrderingSystem.Api.Models;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Common
{
    public class EntityToDTO
    {

        public static List<BaseDicDTO> BaseDicToDTO(List<BaseDic> list)
        {
            var result = new List<BaseDicDTO>();
            if (list == null || list.Count <= 0)
            {
                return result;
            }
            foreach (var item in list)
            {
                result.Add(new BaseDicDTO()
                {
                    base_dic_id = item.BaseDicId,
                    name = item.Name,
                    sort_no = item.SortNo
                });
            }
            return result;
        }
    }
}