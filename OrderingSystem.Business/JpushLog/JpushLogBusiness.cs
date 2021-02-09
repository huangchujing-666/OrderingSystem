using OrderingSystem.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;
using cn.jpush.api.common.resp;
using cn.jpush.api.common;
using OrderingSystem.IService.ResponseModel;
using cn.jpush.api.push.mode;
using System.Timers;
using cn.jpush.api;
using System.Configuration;
using cn.jpush.api.push.notification;
using OrderingSystem.Common;

namespace OrderingSystem.Business
{
    public class JpushLogBusiness:IJpushLogBusiness
    {
        private IRepository<JpushLog> _jpushLog;
        public JpushLogBusiness(IRepository<JpushLog> jpushLog)
        {
            _jpushLog = jpushLog;
        }
        private JPushClient _client;
        private String _rongAppKey;
        private String _rongAppSecret;
        private String _jpushAppKey;
        private String _jpushAppSecret;

        private String _jpushAppApn;  //IOS推送通道

        private string _pushGroupSize = "PUSHUSERCOUNT_PERTIMES"; //推送多人每组长度
        private int _defaultPushGroupSize = 5;
        #region  属性

        private bool JpushAppApn
        {
            get
            {
                return _jpushAppApn == "1" ? true : false;
            }
        }

        private String JpushAppKey
        {
            get
            {
                if (string.IsNullOrEmpty(_jpushAppKey))
                {
                    _jpushAppKey = ConfigurationManager.AppSettings["jpushAppKey"];
                }
                return _jpushAppKey;
            }
        }
        private String JpushAppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(_jpushAppSecret))
                {
                    _jpushAppSecret = ConfigurationManager.AppSettings["jpushAppSecret"];

                }
                return _jpushAppSecret;
            }
        }

        private JPushClient JPushClient
        {
            get
            {
                if (_client == null)
                {
                    _client = new JPushClient(JpushAppKey, JpushAppSecret);
                }
                return _client;
            }
        }
        #endregion

        #region ***jpush推送服务***


        private PushPayload GetPushPayload(SnsRequest request)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();

            pushPayload.options.apns_production = JpushAppApn; //JpushAppApn
            //pushPayload.notification = new Notification().setAlert(request.PushMsg);

            AndroidNotification androidNotification = new AndroidNotification();
            IosNotification iosNotification = new IosNotification();
            androidNotification.setAlert(request.PushMsg);
            iosNotification.setAlert(request.PushMsg);
            if (request.PushExtras != null)
            {
                foreach (var item in request.PushExtras)
                {
                    androidNotification.AddExtra(item.Key, item.Value);
                    iosNotification.AddExtra(item.Key, item.Value);
                }
            }

            if ((int)request.PushType != 0)
            {
                androidNotification.AddExtra("type", (int)request.PushType);
                iosNotification.AddExtra("type", (int)request.PushType);
            }

            cn.jpush.api.push.mode.Notification noti = new cn.jpush.api.push.mode.Notification();
            noti.setAndroid(androidNotification);
            noti.setIos(iosNotification);
            //pushPayload.notification = new Notification().setAndroid(noty1);
            //pushPayload.notification = new Notification().setIos(noty2);

            pushPayload.notification = noti;
            return pushPayload;
        }

        private void CustPush(object sender, ElapsedEventArgs e)
        {

        }

        /// <summary>
        /// 推送消息给别名用户
        /// </summary>
        /// <param name="request">request.PushUsers 以,分隔的多个别名</param>
        /// <returns></returns>
        public SnsResponse JpushSendToAlias(SnsRequest request)
        {
            PushPayload pushPayload = GetPushPayload(request);
            var userlist = request.PushUsers.Split(',');

            //写记录
            System.Threading.Tasks.Task.Run(() =>
            {
                JpushLog _model = new JpushLog();
                _model.CreateTime = DateTime.Now;
                _model.PushId = request.UserId;
                _model.IsToAll = 0;
                _model.BePushId = request.PushUsers;
                _model.PushMsg = request.PushMsg;
                if (request.PushExtras != null)
                {
                    _model.ParamString = request.PushExtras.ToString();
                }
                _jpushLog.Insert(_model);
            });

            SnsResponse response = new SnsResponse();


            int splitSize = _defaultPushGroupSize;//分割的块大小  
            Object[] subAry = StringToolsHelper.splitAry(userlist, splitSize);//分割后的子块数组  

            //分批次推送操作
            for (int i = 0; i < subAry.Length; i++)
            {
                string[] aryItem = (string[])subAry[i];
                var itemStr = string.Join(",", aryItem);
                try
                {

                    pushPayload.audience = Audience.s_alias(aryItem);

                    var result = JPushClient.SendPush(pushPayload);
                    response.JpushMsgId = result.msg_id;

                    #region 推送日志

                    //System.Threading.Tasks.Task.Run(() =>
                    //{
                    //    //写日志  
                    //    Logger.Error("SnsService———>JpushSendToAlias：" + string.Format("认证用户发送jpush用户ID列表:{0}", itemStr));
                    //});

                    #endregion

                }
                catch (Exception e)
                {
                    //Logger.Error("SnsService———>JpushSendToAlias：" + string.Format("认证用户发送jpush:{0},提供的错误信息：{1},id列表：{2}", e.Message, ((cn.jpush.api.common.APIRequestException)e).ErrorMessage, itemStr));

                }
                //休息一秒 避免：Request times of the app_key exceed the limit of current time window
                System.Threading.Thread.Sleep(100);
            }

            return response;
        }

        /// <summary>
        /// 推送消息给特定标签用户
        /// </summary>
        /// <param name="request">request.PushUsers 以,分隔的多个标签</param>
        /// <returns></returns>
        public SnsResponse JpushSendToTag(SnsRequest request)
        {
            JpushLog _model = new JpushLog();
            _model.CreateTime = DateTime.Now;
            _model.PushId = request.UserId;
            _model.IsToAll = 0;
            _model.BePushId = request.PushUsers;
            _model.PushMsg = request.PushMsg;
            if (request.PushExtras != null)
            {
                _model.ParamString = request.PushExtras.ToString();
            }
            _jpushLog.Insert(_model);

            PushPayload pushPayload = GetPushPayload(request);
            var userlist = request.PushUsers.Split(',');
            pushPayload.audience = Audience.s_tag(userlist);

            var result = JPushClient.SendPush(pushPayload);
            SnsResponse response = new SnsResponse();
            response.JpushMsgId = result.msg_id;
            return response;
        }

        /// <summary>
        /// 推送消息给所有APP端
        /// </summary>
        /// <param name="request">request.PushMsg</param>
        /// <returns></returns>
        public SnsResponse JpushSendToAll(SnsRequest request)
        {
            PushPayload pushPayload = GetPushPayload(request);

            //写记录
            System.Threading.Tasks.Task.Run(() =>
            {
                JpushLog _model = new JpushLog();
                _model.CreateTime = DateTime.Now;
                _model.PushId = request.UserId;
                _model.IsToAll = 1;
                _model.BePushId = request.PushUsers;
                _model.PushMsg = request.PushMsg;
                if (request.PushExtras != null)
                {
                    _model.ParamString = request.PushExtras.ToString();
                }
                _jpushLog.Insert(_model);
            });

            var result = JPushClient.SendPush(pushPayload);
            SnsResponse response = new SnsResponse();
            response.JpushMsgId = result.msg_id;
            return response;
        }

        /// <summary>
        /// 推送消息给安卓客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SnsResponse JpushSendToAndroid(SnsRequest request)
        {
            PushPayload pushPayload = GetPushPayload(request);
            pushPayload.platform = Platform.android();

            var result = JPushClient.SendPush(pushPayload);
            SnsResponse response = new SnsResponse();
            response.JpushMsgId = result.msg_id;
            return response;
        }

        /// <summary>
        /// 推送消息给Ios客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SnsResponse JpushSendToIos(SnsRequest request)
        {
            PushPayload pushPayload = GetPushPayload(request);
            pushPayload.platform = Platform.ios();

            var result = JPushClient.SendPush(pushPayload);
            SnsResponse response = new SnsResponse();
            response.JpushMsgId = result.msg_id;
            return response;
        }



        /// <summary>
        /// 检测某条消息是否推送成功
        /// </summary>
        /// <param name="request">request.PushMsgId</param>
        /// <returns></returns>
        public SnsResponse JpushSendCheck(SnsRequest request)
        {
            SnsResponse response = new SnsResponse();
            try
            {
                //如需查询上次推送结果执行下面的代码
                //var apiResult = _client.getReceivedApi(result.msg_id.ToString());
                //var apiResultv3 = _client.getReceivedApi_v3(result.msg_id.ToString());
                //如需查询某个messageid的推送结果执行下面的代码 
                var querResultWithV3 = _client.getReceivedApi_v3(request.PushMsgId);
            }
            catch (APIRequestException e)
            {
                //response.Message.Result = MessageResult.FAILED;
                //response.Message.Content = string.Format("Http Status:{0} Error Message:{1}", e.Status, e.ErrorMessage);
                //response.Message.MessageCode = e.ErrorCode.ToString();
                //response.Message.MessageID = request.PushMsgId;
            }
            catch (APIConnectionException e)
            {
                //response.Message.Result = MessageResult.FAILED;
                //response.Message.Content = string.Format("APIConnectionException Error Message:{0}", e.Message);
                //response.Message.MessageID = request.PushMsgId;
            }

            return response;
        }

        public JpushLog Insert(JpushLog model)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
