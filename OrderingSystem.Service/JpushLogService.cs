using OrderingSystem.Business;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using OrderingSystem.IService.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Service
{
    public class JpushLogService : IJpushLogService
    {
        /// <summary>
        /// The JourneyArticle biz
        /// </summary>
        private IJpushLogBusiness _JpushLogBiz;


        public JpushLogService(IJpushLogBusiness JpushLogBiz)
        {
            _JpushLogBiz = JpushLogBiz;
        }


        /// <summary>
        /// 推送消息给所有APP端
        /// </summary>
        /// <param name="request">request.PushMsg</param>
        /// <returns></returns>
        public SnsResponse JpushSendToAll(SnsRequest request)
        {
            return this._JpushLogBiz.JpushSendToAll(request);
        }

        /// <summary>
        /// 推送消息给别名用户
        /// </summary>
        /// <param name="request">request.PushUsers 以,分隔的多个别名</param>
        /// <returns></returns>
        public SnsResponse JpushSendToAlias(SnsRequest request)
        {
            return this._JpushLogBiz.JpushSendToAlias(request);
        }

        /// <summary>
        /// 推送消息给安卓客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SnsResponse JpushSendToAndroid(SnsRequest request)
        {
            return this._JpushLogBiz.JpushSendToAndroid(request);
        }
        /// <summary>
        /// 推送消息给Ios客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SnsResponse JpushSendToIos(SnsRequest request)
        {
            return this._JpushLogBiz.JpushSendToIos(request);
        }


        /// <summary>
        /// 推送消息给标签用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SnsResponse JpushSendToTag(SnsRequest request)
        {
            return this._JpushLogBiz.JpushSendToTag(request);
        }

        /// <summary>
        /// 检测某条消息是否推送成功
        /// </summary>
        /// <param name="request">request.PushMsgId</param>
        /// <returns></returns>
        public SnsResponse JpushSendCheck(SnsRequest request)
        {
            return this._JpushLogBiz.JpushSendCheck(request);
        }

        public JpushLog Insert(JpushLog model)
        {
            return this._JpushLogBiz.Insert(model);
        }
    }
}
