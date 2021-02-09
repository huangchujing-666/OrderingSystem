using OrderingSystem.Api.Models;
using OrderingSystem.Common;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Api.Controllers
{
    public class JourneyArticleApiController : ApiController
    {
        private readonly IJourneyArticleService _journeyArticleService = EngineContext.Current.Resolve<IJourneyArticleService>();
        private readonly IUserLikesService _userLikesService = EngineContext.Current.Resolve<IUserLikesService>();
        //public JourneyArticleApiController(IJourneyArticleService journeyArticleService)
        //{
        //    _journeyArticleService = journeyArticleService;

        //}

        /// <summary>
        /// 根据文章id 商家id获取文章内容
        /// </summary>
        /// <param name="JourneyArticle_Id"></param>
        /// <param name="User_Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<JourneyArticleDTO> GetJourneyArticleDetailById(int JourneyArticle_Id, int Module,int User_Id)
        {
            var result = new ResponseModel<JourneyArticleDTO>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            if (JourneyArticle_Id <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                return result;
            }
            JourneyArticleDTO article = new JourneyArticleDTO();
            var userLike = _userLikesService.GetByIds(JourneyArticle_Id, User_Id);
            var articleResult = _journeyArticleService.GetById(JourneyArticle_Id);
            if (userLike != null && articleResult != null)
            {
                articleResult.Reads += 1;
                _journeyArticleService.Update(articleResult);
                article.journey_article_id = articleResult.JourneyArticleId;
                article.name = articleResult.Name;
                article.reads = articleResult.Reads;
                article.likes = articleResult.Likes;
                article.user_name = articleResult.User == null ? "" : articleResult.User.NickName;
                article.create_time = articleResult.CreateTime.ToString("yyyy-MM-dd");
                article.path = articleResult.BaseImage == null ? "" : articleResult.BaseImage.Source + articleResult.BaseImage.Path;
                article.content = StringToolsHelper.HtmlToText(articleResult.Content);
                article.content_html = articleResult.Content;
                article.is_like = (int)EnumHelp.IsLike.点赞;
            }
            else if (articleResult != null)
            {
                articleResult.Reads += 1;
                _journeyArticleService.Update(articleResult);
                article.journey_article_id = articleResult.JourneyArticleId;
                article.name = articleResult.Name;
                article.reads = articleResult.Reads;
                article.likes = articleResult.Likes;
                article.user_name = articleResult.User == null ? "" : articleResult.User.NickName;
                article.create_time = articleResult.CreateTime.ToString("yyyy-MM-dd");
                article.path = articleResult.BaseImage == null ? "" : articleResult.BaseImage.Source + articleResult.BaseImage.Path;
                article.content = StringToolsHelper.HtmlToText(articleResult.Content);
                article.content_html = articleResult.Content;
                article.is_like = (int)EnumHelp.IsLike.未点赞;
            }
            else
            {
                result.message = "文章不存在";
                result.data = article;
                return result;
            }

            var journeyArticleAll = _journeyArticleService.GetAll();
            var recomlist = new List<RecommandArticle>();
            if (journeyArticleAll != null)
            {
                var nextResult = journeyArticleAll.Where(c => c.JourneyArticleId > articleResult.JourneyArticleId && c.BusinessInfoId == articleResult.BusinessInfoId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效&&c.Module== Module).OrderBy(c => c.JourneyArticleId).FirstOrDefault();
                var lastResult = journeyArticleAll.Where(c => c.JourneyArticleId < articleResult.JourneyArticleId && c.BusinessInfoId == articleResult.BusinessInfoId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效 && c.Module == Module).OrderByDescending(c => c.JourneyArticleId).FirstOrDefault();
                article.last_article_id = lastResult == null ? 0 : lastResult.JourneyArticleId;
                article.last_article_name = lastResult == null ? "" : lastResult.Name;
                article.next_article_name = nextResult == null ? "" : nextResult.Name;
                article.next_article_id = nextResult == null ? 0 : nextResult.JourneyArticleId;
                //var nextResult = journeyArticleAll.Fetch(c => c.Id > searchResult.Id && c.IsDelete == Model.IsDeleteEnum.否 && c.Status == Model.StatusEnum.有效).OrderBy(c => c.Id).FirstOrDefault();
                //var lastResult = _sylArticleRepository.Fetch(c => c.Id < searchResult.Id && c.IsDelete == Model.IsDeleteEnum.否 && c.Status == Model.StatusEnum.有效).OrderByDescending(c => c.Id).FirstOrDefault();
                //result.LastArticleId = lastResult == null ? 0 : lastResult.Id;
                //result.NextArticleId = nextResult == null ? 0 : nextResult.Id;
                var articleIds = journeyArticleAll.Where(c => c.JourneyArticleId != JourneyArticle_Id && c.BusinessInfoId == articleResult.BusinessInfoId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效 && c.Module == Module).Select(c => c.JourneyArticleId).ToArray();
                Random random = new Random();
                int[] rarticleIds = new int[3];
                int count = 0;
                if (articleIds.Length > 3)
                {
                    while (count < 3)
                    {
                        int id = random.Next(0, articleIds.Length);
                        if (rarticleIds.Contains(id))
                        {
                            continue;
                        }
                        rarticleIds[count] = id;
                        count++;
                    }

                }
                else
                {
                    for (int i = 0; i < articleIds.Length; i++)
                    {
                        rarticleIds[i] = articleIds[i];
                    }
                }
                var recomsearchlist = journeyArticleAll.Where(c => rarticleIds.Contains(c.JourneyArticleId)).ToList();

                if (recomsearchlist != null && recomsearchlist.Count > 0)
                {
                    foreach (var item in recomsearchlist)
                    {
                        recomlist.Add(new RecommandArticle()
                        {
                            journey_article_id = item.JourneyArticleId,
                            name = item.Name,
                            reads = item.Reads,
                            likes = item.Likes,
                            user_name = item.User == null ? "" : item.User.NickName,
                            create_time = item.CreateTime.ToString("yyyy-MM-dd"),
                            path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                            user_id = item.UserId
                        });
                    }
                }
            }
            article.recommand_article_list = recomlist;
            result.data = article;
            return result;
        }

        /// <summary>
        /// 根据商家id获取攻略列表
        /// </summary>
        /// <param name="Business_Id"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<JourneyArticleDTO>> GetJourneyArticleListById(int Business_Id=0, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<JourneyArticleDTO>>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            //if (Business_Id <= 0)
            //{
            //    result.error_code = Result.ERROR;
            //    result.message = "参数有误";
            //    return result;
            //}

            var articleResult = new List<JourneyArticle>();
            int total_count = 0;
            if (Business_Id > 0)
            {
                articleResult = _journeyArticleService.GetByBusinessId(Business_Id, Page_Index, Page_Size, out total_count);
            }
            else
            {
                var allList = _journeyArticleService.GetAll();
                total_count = allList.Count;
                articleResult = allList.Skip(Page_Size * (Page_Index - 1)).Take(Page_Size).ToList();
            }

            List<JourneyArticleDTO> articleList = new List<JourneyArticleDTO>();
            if (articleResult != null && articleResult.Count > 0)
            {
                foreach (var item in articleResult)
                {
                    articleList.Add(new JourneyArticleDTO()
                    {
                        user_id = item.UserId,
                        journey_article_id = item.JourneyArticleId,
                        name = item.Name,
                        reads = item.Reads,
                        likes = item.Likes,
                        user_name = item.User == null ? "" : item.User.NickName,
                        create_time = item.CreateTime.ToString("yyyy-MM-dd"),
                        path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                        content = StringToolsHelper.HtmlToText(item.Content),
                        content_html = item.Content
                    }
                        );
                }
            }
            result.data = articleList;
            return result;
        }

        /// <summary>
        /// 游玩攻略喜欢
        /// </summary>
        /// <param name="JourneyArticle_Id"></param>
        /// <param name="Business_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<string> PostJourneyArticleLikeById(JourneyArticleLikeDTO journeyArticleLikeDTO)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            if (journeyArticleLikeDTO.User_Id <= 0 || journeyArticleLikeDTO.JourneyArticle_Id < 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                return result;
            }
            var userLike = _userLikesService.GetByIds(journeyArticleLikeDTO.JourneyArticle_Id, journeyArticleLikeDTO.User_Id);
            if (userLike != null)
            {
                result.error_code = Result.ERROR;
                result.message = "此篇文章该用户已点赞";
                return result;
            }
            var articleResult = _journeyArticleService.GetById(journeyArticleLikeDTO.JourneyArticle_Id);
            if (articleResult != null && articleResult.JourneyArticleId > 0)
            {
                articleResult.Likes += 1;
                _journeyArticleService.Update(articleResult);
                _userLikesService.Insert(new UserLikes()
                {
                    CreateTime = System.DateTime.Now,
                    JourneyArticleId = journeyArticleLikeDTO.JourneyArticle_Id,
                    UserId = journeyArticleLikeDTO.User_Id
                });
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "游玩攻略id有误";
            }
            result.data = "";
            return result;
        }
    }
}
