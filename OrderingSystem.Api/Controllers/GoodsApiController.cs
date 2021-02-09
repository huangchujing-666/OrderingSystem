using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    public class GoodsApiController : ApiController
    {
        private readonly IGoodsService _goodsService = EngineContext.Current.Resolve<IGoodsService>();

        /// <summary>
        /// 根据衣品类别获取衣品列表
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="BusinessInfoId"></param>
        /// <returns></returns>
        public ResponseModel<List<GoodsDTO>> GetBusinessGoodsByCategory(int CategoryId, int BusinessInfoId, int Page_Index, int Page_Size)
        {
            var result = new ResponseModel<List<GoodsDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            int totalCount;
            var data = new List<GoodsDTO>();
            if (CategoryId <= 0)
            {
                throw new Exception("衣品类别id无效");
            }
            if (BusinessInfoId <= 0)
            {
                throw new Exception("商家id无效");
            }
            var goodsList = _goodsService.GetGoodsByBusinessIdAndCategoryId(CategoryId, BusinessInfoId, Page_Index, Page_Size, out totalCount);
            if (goodsList != null && goodsList.Count > 0)
            {
                foreach (var item in goodsList)
                {
                    data.Add(
                        new GoodsDTO()
                        {
                            base_image_id = item.BaseImageId,
                            descript = item.Descript,
                            goods_id = item.GoodsId,
                            name = item.Name,
                            orign_price = item.OrignPrice.ToString(),
                            real_price = item.RealPrice.ToString(),
                            path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                            image_list = GoodsImageToDTO(item.GoodsImageList)
                        }
                        );
                }
            }
            result.data = data;
            result.total_count = totalCount;
            return result;
        }

        private List<GoodsImageDTO> GoodsImageToDTO(List<Domain.Model.GoodsImage> goodsImageList)
        {
            List<GoodsImageDTO> result = null;
            if (goodsImageList != null && goodsImageList.Count > 0)
            {
                result = new List<GoodsImageDTO>();
                foreach (var item in goodsImageList)
                {
                    result.Add(new GoodsImageDTO()
                    {
                        base_image_id = item.BaseImageId,
                        path = item.BaseImage == null ? "" : (item.BaseImage.Source + item.BaseImage.Path),
                        type = item.Type
                    });
                }
            }
            return result;
        }
    }
}
