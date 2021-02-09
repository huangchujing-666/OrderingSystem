using OrderingSystem.Api.Models;
using OrderingSystem.Common.SMS;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    /// <summary>
    /// 图片接口
    /// </summary>
    public class BaseImageApiController : ApiController
    {
        private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();
        private readonly ICommentImageService _commentImageService = EngineContext.Current.Resolve<ICommentImageService>();
        private static readonly object imgLock = new object();

        /// <summary>
        /// 上传单个图片
        /// </summary>
        /// <param name="ImageInfo">图片信息</param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<UploadImageDTO> UploadCommentImage(UploadImage ImageInfo)
        {

            var result = new ResponseModel<UploadImageDTO>();
            var uploadImageDTO = new UploadImageDTO();
            try
            {
                result.error_code = Result.SUCCESS;
                if (string.IsNullOrWhiteSpace(ImageInfo.Base64) || string.IsNullOrWhiteSpace(ImageInfo.SuffixType))
                {
                    result.error_code = Result.ERROR;
                    result.message = "参数无效";
                    return result;
                }
                //string nnn = "%2f9j%2f4AAQSkZJRgABAQEAYABgAAD%2f2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCAcHCg0KCgsMDAwMBwkODw0MDgsMDAz%2f2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz%2fwAARCABeAF8DASIAAhEBAxEB%2f8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL%2f8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4%2bTl5ufo6erx8vP09fb3%2bPn6%2f8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL%2f8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3%2bPn6%2f9oADAMBAAIRAxEAPwD5bHh2YNnj8GoOizg%2fdH51rC%2bUfw%2frTHutzZCNj61%2b9clj%2bA1XqvdGamlXCE9qkhtJU61e84lehHtuqXRNBvvE9%2bLSwsby%2fumBYQ20TSyEDqdqgmk7JXZadSb5UrtlJYJFPUD8akZHAGWFXdZ8JajoDYvdM1CzIPPnwPHj8wKzJUiLdT%2bBoTTV0TKEk%2bWasywY1%2fimQH0NMlaFV%2f1mT3x0qKRIiV5b86FhTH8VDb6E8sUK8q9iTUZO4dakCxjimvGCeM4qbvqPToMzikLc96cUo2H0NMZOtq2eeKSSzyfvmla6%2bagXB3df0rQz97oH2If3mrs%2fBPii9%2bEHwF%2bM%2fjjTb6607U%2fDvg%2bSDT7u3kMctvc3dzBbRurDkMN7EH2rjGucHrXqfhbSrWT9kPX%2fALfHHNYeJ%2fGej6RdxvyJoIUnunTH1VD%2bFZT5JTpwqK8XOCa7rmV191z7Xw8w862f0P7vNL7otr8bHy58Ov8AgrD%2b0DoSQq%2fxI1HVYo8AR6rZ2t8GHuZYyx%2fOvv8A%2fwCCS%2fx6vv8Agoz8RPEXhj4haT8Mp7rStOW%2btJH8KRh7r59rhijr0yOg71%2bRHjbwyfhf8S9Z0GRiw026eON%2bvmR5yjfipU19Yf8ABG749D4Oft1eELn7T5NvrBl0qfnAYSqcA%2f8AAgtf0vm3A%2fDuP4cxNbB4SlCuqblCUYRjK6XN0S3St8z9xnia6xcIYhuUL2aeq7dT9mPEH%2fBKH4feJAfP8H%2bFCex069u9O%2fQFhXnvjn%2fgjP4DttNlvH07xbpEMA3MdL1m3vBj%2fdnRT%2btfSdh8cbc%2f8vK5%2btS%2bIvi0niCxtNOiuFMmo31tbgA9QZVJ%2fQGv5IeBxlL3tbLV6y269bfgevieHcmru1TDQbf9yK%2fFJP8AE%2fJL9vD9l3Sv2SvjrdeFdI1m71i0t0B866iVJQ2ASDt4Iycf8BNeMeXlBXvn%2fBTHxS3i39rbxFOX3lJHQ89P3shH%2fjpFeAu7KOn616GUTqVMFTqVXeTVz%2bZ%2bMMLh8NnWJw%2bDjywjKyXa2%2b%2fncQx%2b1Ki4ambm96VN249a9JaHzWoCDLUhi%2bf%2fAOvUoPzGmZ5qtx8zQeUSeM%2fga6H9pbx3H8Lf2R%2fhTarLsl13xNqusSJn7wgtvsyk%2fQyfrWFB1r0XWLf4RfG%2fwJ4c0b4iaBqF7J4Zt5YrG5hu7i1a0MpUylGgf5txUH5426Vg8TDDYijiKsJThGV2oq7%2bGSWl19po%2fRfDDGYShms6uMqxprkaTk7K7cevpc%2fPT48%2bLofF%2fi2HVYyPPeMQz4P3tv3T%2bXH4CpfgV49fwT8VfDWrRttbT9Tt5wc9NsgJr7I8S%2f8ABKf4EfEaATeD%2fi94k8O3kh%2f1GswQXsan0UH7NIf%2fAB4%2fWvOfFH%2fBGv4h%2bHJPtHhrxb4J8TwowZI2uZtNuX5yDtmTyv8AyKa%2fZ%2bHvFDII0%2fqtWv7PS1qicNPWSS%2fE%2feKmB%2btL22FaqLvFqX5M%2fQDSv2uUlUN54%2bbn71elfs8%2ftF2fjH44eF7W71G1tbaC4ku3knnWNMxwyFVySBksVwO9bH%2fBGT%2fglxFH8PbDxp8UtPtdU8TaiGlstNuil5ZaVbq7RrKygtHPLIyOUzujCAPhty7f0V%2bLreHvgz8OLnUNX1G8tdNto%2fLEYuW2ynGFiSHPlknoF24%2bgBr8P4w8RMBRVfA5fTUocsouo3pqmm4%2bS6O%2bp7mFyyUUsRiJWtrb011PwF%2fao18eJf2hvFF4GDCW76g8cKoOPxBrz%2bQ1%2bin%2fAAUe%2fYe8N%2bMPg4fix4H0RvDupW9smoanpiWoto721ZlVphCvyxyxllLBMI8ZLgKVJf8AOmZctxz9K4uHcxo4vBQdH7KS%2fD9T%2bWuP8jxWX5vUqV9Y1nKpFrZqTb%2b9X1HZ%2bepVkCrz%2fKoQcNT3Py17u58QxMfPRnD%2fAHsfhR%2bNI%2fSmMkQ5b1r9Iv23%2fwBlX9n39i74ZeDdfm%2bHUviSXxRGu%2bzHji50%2b6QmJZDJHEYpd8QzhmyNpZBzu4%2fNxOWr9Nfj%2fL8EP2gv2r%2fg%2fwCMPEvxO8Har4N8P%2bHY9K17RpL8jyJ4I5ZYmwARJE8rqrhTn5B95WO35%2fN6nJiKLm5KCU21FyV7JWXutfLU%2fV%2fDHC4GvRxixUKcp3pKLqKDtdz5muZPyvbyOb8M%2fsM%2fBf43%2fAD4dePND8Iaz4bPiyTUZZ9PfxBNe7Vtbg2wQy7YyVY%2fOdqq2QozgNu7uH9gXSYfCgi0fzdFghJdPsqKvJ6lj1boPvZ6VB%2b0%2fwDts%2fCP9nPwD4D034SXeg%2bKtF0nUtSF1pNndOjWMN3L9pYxFhwBLuCjDKFO3jg1H4b%2fAOCwfwrutAEV7ZeLNMmK4aP7HHOAf94SD%2bVfC4zC5hi26sIzlTbfKm22lfTdt7dfxP2rA5zwtlVX6up0aVVJczioxT06NJL5J%2fI%2bhfhp4j0HSP2QlsfEWttoumQ%2bE9OgvL%2bKYxvB5NnFay4xyxFzBMhTnccrg7sV4%2f4P8J%2bOf25P2ZorjXL24tdT8IXzTeDtcuiY11qMH7tzbHcrL8iASHce3zYff5Td%2fwDBUP4Vz3kujPo%2fie98Naq0huXmsIHNk8mPMKRPKRLDLtUvCxT5gXV1YsG9kj%2f4KX%2fCmz0GJIPH%2fheCCCIJHBHpeqJLGoGAghFr5YwOAPNx%2ftYr5PM%2bFcXXrqWIhJQcWrWervpf%2fDuut%2btjpqZpkuZ1nJYuDpqLTSkle%2bqb12jumtb9eh2nx68capb%2fALJ2vXHjZdNstbXwfqv9qJaybreOWWxmtowD%2ftTzwLgEjc2ATwT%2bKoBxX1P%2b3b%2fwUOb9ojST4V8LRaha%2bGXmSa%2fvb1Vju9ZdP9WrRoWWKBCSVjDNlvmZmIUJ8ssCT%2f8AWr9Z4Ryqtg8O3WveVt97Lq%2fN3PwvxW4nwmZ4ujhsFLnjRTTl3btfXray12u2II8tSv0pynanNRDp1%2fWvrT8n3FUYNWbDSLnXo9UjsTay32nabPqiWTyMtxqEcADzJAAjB5Ei8yUqSMpC%2bMsApqt8wHy5%2fGut%2bEPgLXfiTdeJ9L0S08J%2bbcaFcR2WoavHYJc2GrsjLpX2C5nU3Ed612V8tLE%2faJCmFV9uBw5niJUMJUrw3jFtfJXt532tpfa63PoeF8upY%2fNqGDrJuM5JO3Z9fK27etlrZ7GXqMPgLT%2fDNxqFr8b%2fAIS6rNFbtNHY2qeIVuLpgMiJDLpKRhyeBvdVyeWA5rXh%2bGVtp%2bjW174i8ffDTwpBc6A3ipWutWudURdKF5HYi78zS7a8iwbyTyPK3%2bcjxvvjQDcfWbzxB%2b1F43PjTQ9f8R%2ftiaH4L0J%2fDUVtqc%2fhHxlBqHi6wsNL1CHVjbrFZXSW8t5dS28gF55aMgQzMroQNH4L%2fte%2bO%2fj%2fAKh%2bzvollY%2fHz4j%2bH9N8Fs58VeF4vFF7eRySeIdR06ea9TT9Y02WJLiOwjeOW5nlFukToLeXdmL4d8T4uV4UWr80VrbaUZSutFbbVPXRq17H7xT8JMohatiU3BRv7rkvevG8XeUr2u1eNlez1R4VcfDq81Lxn4Q0bwbfWXxRl8daNNruiv4OstSvWu7eG5ubaX9xNaQ3AZJLSbP7rGFBzivTPif%2bwn4t%2bEHw0h8V%2bJLu58OacmgQ63qMeteCPFVsdJdwSbea4h0uezRgcL89whBYb1jrtfFnwi8Z%2bMP2%2b73xLP4N%2bJVtpnh7RtZ8MNd%2fEfwx4t2X9lM81ssqXWor4stntIBJ5pnuILe1YSszQjJIsfHP9mrQtW%2bCHizSPhkfhh4k1jxd4NltL288NaHZ6lcaYs7Nm4UeHPh%2fFdSWTG3Kx3L3NvBM28YOznlxHFuL%2bqRq09Lpu%2bl3%2fE5YdtlFuVt%2fuLwnhLkzxlWFRNpNRSu7K3s%2bae93q5K19vvOR%2bHn%2fBO%2fxx8UfACeJtF1C1vNHl02DVYp4vBnjWRbqCYxhDbsmglbknzVOLcyfLuf7isw8%2b%2bKP7PWufDHx5H4VmvLO98VT6Lca7b6I2i6%2fpGpX1vCw3iCDU9NtHmfYJpAEBBW2mAO8KjdP%2byT4QsPCWpaTqvibx18JvDUeqt8K9etrfV5fAuvX32Wy8MRx3EklnrWpW82mFWkTFxFH9qwwMUbqSa8%2b8a%2fAzWtf8TaR4auLH4e%2bI9A1H4UeJ3%2fALRt7nw%2fr4%2b3XnivxG%2blR6VfGZla%2fmeRRHb2dyk0jo0bDKOF1xPFGNpV%2bVO8by6LpT50tu9420d4s2p%2bEmRTwt0nz%2fu7u7%2b3Kzdubpu3Zq0kdJrf7G3xn8OaTpF7e%2fCH4mpbarp0mpOY%2fCmpOdNRJpYil1%2fo4EMn7kybSc%2bW8bHG6rFr%2bx38RlTRrnVdDt%2fCOnazpP8Aba6h4r1CHw3aWtqJ3hkd5NQMAYxqizOsPmMsU0LEfvFB9K%2bIn7PHh7xh8HrvxRqf7Lnxcj8VXuqQ2Avbb4BeI7O%2bsLWDTEhg2wjxe3k2sKRoi4Y26FAv2YkFjV%2fZS0rTfAPifwiL%2fwCAHiXQ7Pw38LPPk1Lx1YwXdmXudR1YNpmuapc%2bGruPS7Wa2nnnW4jWwj8qYLcNcRsksetTi3FRpTaS5oyXTo6c5rrd7Jaau1lq0jzaXhFlM8RBOUlCUb%2fEr3U4xl9myW%2brdle7slc8S%2bKnwv1X4Q61ZWerXHhq8TV7dr7TbvRPEVhrNrqFpvKJco9rNJtjk2kp5gQsAeODjmHru%2f2u9c%2ftv40aH5ektLbw6PGF8U2v%2fCO3ek%2bJWEFvG62OoaJo%2bnQXsNoY%2fs25%2fMKGIoFg2sh4VjkV9fkeNq4rCRrVt7taeTa%2b%2fv59FsfjvGmSYfKs3qYPC35EotX31in92unl1e4FvlrA8Z%2bANL8eW6RalF5qJyBn8a2v%2bWdNZcgV6k4RmuWSuj5zD1alGaqUpNNdUcsnwd0WJUURMUj%2b6pCED%2fx2o4vgh4finkkS3aIzDa%2bzaNw%2fKuuPBprtgCsvqtH%2bVHd%2famMX%2fL1%2fec0fg1of2byDE7xZzsbaR%2f6DVaL4BeF4WJjsERiOSAp757iuwJyaVPvH%2fdpfVKP8qD%2b1calpVl95zOh%2fBPw1oO8w6dHuk%2b8WOM%2fliqlz%2bzp4Uu7oz%2fYNkhOfkbHP5V2h7fQUqNg0PB0GrOC%2b4hZvjoyc1Wld%2bbOUHwY0WO4SXbP5sQAVtwyuOnaov%2bFIeH95ZrZpCxy2%2fad36V2DnLUKcDnn09qf1Wj%2fAComOa4y%2fwDFf3mF4X8AaZ4NlZrCE2%2b8YKA8flW4vJpknD0sb4rWEIwXLFWRhWq1Ksueo7vuz%2f%2fZ";
                //过滤特殊字符即可   
                string dummyData = ImageInfo.Base64.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
                if (dummyData.Length % 4 > 0)
                {
                    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                }
                //上传图片至服务器
                lock (imgLock)
                {
                    //string Base64 = ImageInfo.Base64.Replace(' ', '+');
                    string path = string.Format("/UploadImge/" + DateTime.Now.ToString("yyyyMMdd") + "/");
                    string Base64 = System.Web.HttpUtility.UrlDecode(dummyData, System.Text.Encoding.UTF8); //UrlDecode解码
                    byte[] imgByte = Convert.FromBase64String(ImageInfo.Base64);
                    MemoryStream ms = new MemoryStream(imgByte);
                    Image image = System.Drawing.Image.FromStream(ms);
                    string fileName = string.Format("{0}." + ImageInfo.SuffixType, DateTime.Now.ToString("yyyyMMddhhmmssffff") + Assistant.GetRandomNumber(6));
                    string filePath = string.Format("{0}{1}", path, fileName);
                    string serverPath = HttpContext.Current.Server.MapPath("~" + path);
                    if (!Directory.Exists(serverPath))
                    {
                        Directory.CreateDirectory(serverPath);
                    }
                    //保存图片
                    image.Save(serverPath + fileName);
                    string ImageUrl = WebConfigHelper.GetAppSettingsInfo("ImageUrl");
                    BaseImage baseimage = _baseImageService.Insert(new BaseImage()
                    {
                        Title = "H5点餐系统评价图片",
                        CreateTime = System.DateTime.Now,
                        Path = filePath,
                        Source = ImageUrl
                    });
                    uploadImageDTO.baseImageId = baseimage.BaseImageId;
                    uploadImageDTO.path = ImageUrl + filePath;
                    result.data = uploadImageDTO;
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;

        }
        /// <summary>
        /// 根据评价图片Id删除评价图片
        /// </summary>
        /// <param name="CommentImageId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel DeleteCommentImage(int CommentImageId)
        {
            var result = new ResponseModel();
            result.error_code = Result.SUCCESS;
            if (CommentImageId > 0)
            {
                var getresult = _commentImageService.GetById(CommentImageId);
                if (getresult != null)
                {
                    var baseImage = _baseImageService.GetById(getresult.BaseImageId);
                    if (baseImage != null && File.Exists(HttpContext.Current.Server.MapPath("~" + baseImage.Path)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath("~" + baseImage.Path));
                        _baseImageService.Delete(baseImage);
                        _commentImageService.Delete(getresult);
                        result.message = "删除成功";
                    }
                    else
                    {
                        result.error_code = Result.ERROR;
                        result.message = "图片不存在";
                    }

                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
            }
            return result;
        }
    }
}
