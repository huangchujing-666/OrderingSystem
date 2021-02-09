using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{

    public class TopSearchApiController : ApiController
    {
        private readonly ITopSearchService _topSearchService = EngineContext.Current.Resolve<ITopSearchService>();

        public ResponseModel<List<TopSearchDTO>> GetTopSearch(int Type_Id,int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<TopSearchDTO>>();
            result.error_code = Result.SUCCESS;
            int totalCount;
            var getList = _topSearchService.GetManagerList("", Type_Id, Page_Index, Page_Size, out totalCount);
            result.data = TopSearchToDTO(getList);
            result.total_count = totalCount;
            return result;
        }

        public List<TopSearchDTO> TopSearchToDTO(List<TopSearch> list)
        {
            List<TopSearchDTO> result = new List<TopSearchDTO>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(new TopSearchDTO()
                    {
                        SortNo = item.SortNo,
                        Contents = item.Contents,
                        TopSearchId = item.TopSearchId
                    });
                }
            }
            return result;
        }
    }
}
