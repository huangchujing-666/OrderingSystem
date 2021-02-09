using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    public class ProductApiController : ApiController
    {
        private readonly IProductService _productService = EngineContext.Current.Resolve<IProductService>();

        public ResponseModel<ProductDTO> GetProductById(int Bussiness_Id, int Product_Id)
        {
            var result = new ResponseModel<ProductDTO>();
            ProductDTO prd = new ProductDTO();
            //查询缓存数据
            string key = "GetProductById_" + Bussiness_Id + "_" + Product_Id;
            if (false)//RedisDb._redisHelper_11().KeyExists(key)
            {
                var data = RedisDb._redisHelper_11().StringGet<ProductDTO>(key);
                result.data = data;
            }
            else
            {
               var getPrd=  _productService.GetById(Product_Id);
                if (getPrd!=null)
                {
                    if (getPrd.IsDelete==(int)EnumHelp.IsDeleteEnum.有效&&getPrd.Status== (int)EnumHelp.EnabledEnum.有效&& getPrd.BusinessInfoId== Bussiness_Id)
                    {
                        prd.business_name = getPrd.BusinessInfo == null ? "" : getPrd.BusinessInfo.Name;
                        prd.descript = getPrd.Descript;
                        prd.end_date = getPrd.EndDate.ToString();
                        prd.notice = getPrd.Notice;
                        prd.orign_price = getPrd.OrignPrice.ToString();
                        prd.product_id = getPrd.ProductId;
                        prd.product_name = getPrd.Name;
                        prd.real_price = getPrd.RealPrice.ToString();
                        prd.start_date = getPrd.StartDate.ToString();
                        prd.remark = getPrd.Remark;
                        prd.rule = getPrd.Rules;
                        prd.content = getPrd.Content;
                        prd.product_image_list = ProductImageToDTO(getPrd.ProductImageList);
                        prd.lable = ProductLableListToStr(getPrd.ProductLableList);
                        result.data = prd;
                        //设置缓存数据
                        RedisDb._redisHelper_11().StringSet(key, prd, RedisConfig._defaultExpiry);
                    }
                    else
                    {
                        result.error_code = Result.ERROR;
                        result.message = "该产品不存在或参数有误";
                        result.data = null;
                    }
                }
            }
            return result;
        }

        private string ProductLableListToStr(List<ProductLable> productLableList)
        {
            StringBuilder sb = new StringBuilder(200);
            int i = 0;
            if (productLableList != null && productLableList.Count > 0)
            {
                foreach (var item in productLableList)
                {
                    if (i == 0)
                    {
                        sb.Append(item.Name);
                    }
                    else
                    {
                        sb.Append(",");
                        sb.Append(item.Name);
                    }
                    i++;
                }

            }
            return sb.ToString();
        }
        private List<BaseImageDTO> ProductImageToDTO(List<ProductImage> list)
        {
            List<BaseImageDTO> result = null;
            if (list != null)
            {
                result = new List<BaseImageDTO>();
                foreach (var item in list)
                {
                    result.Add(new BaseImageDTO()
                    {
                        img_id = item.BaseImageId,
                        img_path = item.BaseImage == null ? "" : (item.BaseImage.Source + item.BaseImage.Path)
                    });
                }
            }
            return result;
        }
    }
}
