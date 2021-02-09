using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;

namespace OrderClient
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
        }
        /// <summary>   
        /// 窗体动画函数    注意：要引用System.Runtime.InteropServices;   
        /// </summary>   
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>   
        /// <param name="dwTime">指定动画持续的时间</param>   
        /// <param name="dwFlags">指定动画类型，可以是一个或多个标志的组合。</param>   
        /// <returns></returns>   
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //下面是可用的常量，根据不同的动画效果声明自己需要的   
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志   
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志   
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志   
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志   
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展   
        private const int AW_HIDE = 0x10000;//隐藏窗口   
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志   
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略   
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果   

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);
        [DllImport("winmm.dll", SetLastError = true)]
        static extern long mciSendString(string strCommand,
            StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        private static extern long sndPlaySound(string lpszSoundName, long uFlags);

        public DataTable OrderData;

        private void MessageForm_Load(object sender, EventArgs e)
        {
            if (OrderData != null)
            {
                //set 
                foreach (DataRow myRow in OrderData.Rows)
                {
                    var orderNo = myRow[0].ToString();
                    this.lblOrderNo.Text = orderNo;
                    this.lblPayTime.Text = myRow["PayTime"].ToString();
                    this.lblSeatNo.Text = myRow["SeatNo"].ToString();
                    var _total = myRow["Total"].ToString();
                    this.lblTotal.Text = string.IsNullOrEmpty(_total) ? "总金额：0.00 元" : "总金额：" + _total + " 元";

                    var specids = myRow["DishesSpecDetailIds"].ToString();
                    if (!string.IsNullOrWhiteSpace(specids))
                    {
                        //查询商品规格
                        string specDetails = @"select 
Descript = STUFF((SELECT ',' + CONVERT(NVARCHAR(50), ds.Descript) FROM
DishesSpecDetail ds WHERE DishesSpecDetailId in({0}) FOR XML PATH('')),1,1,'') ";
                        var items = SqlDbHelper.ExecuteDataTable(string.Format(specDetails, specids));

                        if (items != null && items.Rows.Count > 0)
                        {
                            myRow["ProductName"] += "（" + items.Rows[0][0].ToString() + "）";
                        }
                    }
                     
                }
                this.dataGridView1.DataSource = OrderData;
            }


            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示   
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);

            //添加音乐提醒 
            Play(System.Environment.CurrentDirectory + "/notice.wav");

            //调用打印
            PrintOrder(OrderData);
        }



        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        [Flags]
        public enum SoundFlags
        {
            /// <summary>play synchronously (default)</summary>  
            SND_SYNC = 0x0000,
            /// <summary>play asynchronously</summary>  
            SND_ASYNC = 0x0001,
            /// <summary>silence (!default) if sound not found</summary>  
            SND_NODEFAULT = 0x0002,
            /// <summary>pszSound points to a memory file</summary>  
            SND_MEMORY = 0x0004,
            /// <summary>loop the sound until next sndPlaySound</summary>  
            SND_LOOP = 0x0008,
            /// <summary>don’t stop any currently playing sound</summary>  
            SND_NOSTOP = 0x0010,
            /// <summary>Stop Playing Wave</summary>  
            SND_PURGE = 0x40,
            /// <summary>don’t wait if the driver is busy</summary>  
            SND_NOWAIT = 0x00002000,
            /// <summary>name is a registry alias</summary>  
            SND_ALIAS = 0x00010000,
            /// <summary>alias is a predefined id</summary>  
            SND_ALIAS_ID = 0x00110000,
            /// <summary>name is file name</summary>  
            SND_FILENAME = 0x00020000,
            /// <summary>name is resource name or atom</summary>  
            SND_RESOURCE = 0x00040004
        }

        public static void Play(string strFileName)
        {
            PlaySound(strFileName, UIntPtr.Zero,
               (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_SYNC | SoundFlags.SND_NOSTOP));
        }
        public static void mciPlay(string strFileName)
        {
            string playCommand = "open " + strFileName + " type WAVEAudio alias MyWav";
            mciSendString(playCommand, null, 0, IntPtr.Zero);
            mciSendString("play MyWav", null, 0, IntPtr.Zero);

        }
        public static void sndPlay(string strFileName)
        {
            sndPlaySound(strFileName, (long)SoundFlags.SND_SYNC);
        }

        //**********************请先填打印机编号和KEY，再测试**************************
        //public static string USER = "13322991610@qq.com";  //*必填*：登录管理后台的账号名
        //public static string UKEY = "CSYhTJRJMfPNrtVj";//*必填*: 注册账号后生成的UKEY
        //public static string SN = "218501540";        //*必填*：打印机编号，必须要在管理后台里手动添加打印机或者通过API添加之后，才能调用API
        //public static string SNKEY = "rgrgmc68";        //*必填*：打印机编号，必须要在管理后台里手动添加打印机或者通过API添加之后，才能调用API

        public static string USER = ConfigurationManager.AppSettings["USER"];  //*必填*：登录管理后台的账号名
        public static string UKEY = ConfigurationManager.AppSettings["UKEY"];//*必填*: 注册账号后生成的UKEY
        public static string SN = ConfigurationManager.AppSettings["SN"];        //*必填*：打印机编号，必须要在管理后台里手动添加打印机或者通过API添加之后，才能调用API
        public static string SNKEY = ConfigurationManager.AppSettings["SNKEY"];        //*必填*：打印机编号，必须要在管理后台里手动添加打印机或者通过API添加之后，才能调用API

        public static string URL = "http://api.feieyun.cn/Api/Open/";//不需要修改


        private void PrintOrder(DataTable dt)
        {
            //拼凑订单内容时可参考如下格式
            string orderInfo = "";

            if (OrderData != null)
            {
                //orderInfo = "<CB>测试打印</CB><BR>";//标题字体如需居中放大,就需要用标签套上
                //orderInfo += "番　　　　　　 1.0    1   1.0<BR>";
                //orderInfo += "番茄　　　　　 10.0   10  10.0<BR>";
                //orderInfo += "番茄炒　　　　 10.0   100 100.0<BR>";
                //orderInfo += "番茄炒粉　　　 100.0  100 100.0<BR>";
                //orderInfo += "番茄炒粉粉　　 1000.0 1   100.0<BR>";
                //orderInfo += "番茄炒粉粉粉粉 100.0  100 100.0<BR>";
                //orderInfo += "番茄炒粉粉粉粉 15.0   1   15.0<BR>";

                var orderNo = "";
                var paytime = "";
                var seatNo = "";
                var total = "";
                var bName = "";
                var remark = "";

                var orderDetail = "";

                //set 
                foreach (DataRow myRow in OrderData.Rows)
                {
                    orderNo = myRow[0].ToString();
                    paytime = myRow["PayTime"].ToString();
                    bName = myRow["BName"].ToString();
                    seatNo = myRow["SeatNo"].ToString();
                    remark = myRow["Remark"].ToString();
                    total = myRow["Total"].ToString();
                    total = string.IsNullOrEmpty(total) ? "总金额：0.00 元" : "总金额：" + total + " 元";

                    var specids = myRow["DishesSpecDetailIds"].ToString();

                    //已经查询过了，这里无需再次处理
//                    if (!string.IsNullOrWhiteSpace(specids))
//                    {
//                        //查询商品规格
//                        string specDetails = @"select 
//Descript = STUFF((SELECT ',' + CONVERT(NVARCHAR(50), ds.Descript) FROM
//DishesSpecDetail ds WHERE DishesSpecDetailId in({0}) FOR XML PATH('')),1,1,'') ";
//                        var items = SqlDbHelper.ExecuteDataTable(string.Format(specDetails, specids));

//                        if (items != null && items.Rows.Count > 0)
//                        {
//                            myRow["ProductName"] += "（" + items.Rows[0][0].ToString() + "）";
//                        }
//                    }

                    orderDetail += myRow["ProductName"] + " x" + myRow["Count"] + "  " + myRow["RealAmount"] + "<BR>";

                }

                orderInfo = "<CB>"+ bName + "</CB><BR>";//标题字体如需居中放大,就需要用标签套上
                orderInfo += "订单编号：" + orderNo + "<BR>";
                orderInfo += "桌号：" + seatNo + "<BR>";
                orderInfo += "商品名称　　　　　 　  数量 金额<BR>";
                orderInfo += "--------------------------------<BR>";
                orderInfo += orderDetail;
                orderInfo += "备注："+ remark + "<BR>";
                orderInfo += "--------------------------------<BR>";
                orderInfo += total + "<BR>";
                //orderInfo += "联系电话：138000000000<BR>";
                orderInfo += "下单时间：" + paytime + "<BR>";
                orderInfo += "打印时间：" + DateTime.Now + "<BR>";
                orderInfo += "----------请扫描二维码----------";
                orderInfo += "<QR>http://m.leadyssg.com</QR>";//把二维码字符串用标签套上即可自动生成二维码
                orderInfo += "<BR>";

            }

            //==================添加打印机接口（支持批量）==================
            //***返回值JSON字符串***
            //正确例子：{"msg":"ok","ret":0,"data":{"ok":["sn#key#remark#carnum","316500011#abcdefgh#快餐前台"],"no":["316500012#abcdefgh#快餐前台#13688889999  （错误：识别码不正确）"]},"serverExecutedTime":3}
            //错误：{"msg":"参数错误 : 该帐号未注册.","ret":-2,"data":null,"serverExecutedTime":37}

            //提示：打印机编号(必填) # 打印机识别码(必填) # 备注名称(选填) # 流量卡号码(选填)，多台打印机请换行（\n）添加新打印机信息，每次最多100行(台)。
            //string snlist = "218501540#rgrgmc68#remark1#carnum1\nsn2#key2#remark2#carnum2";
            string snlist = "218501540#rgrgmc68";
            string method = addprinter(snlist);
            System.Console.WriteLine(method);



            //==================方法1.打印订单==================
            //***返回值JSON字符串***
            //成功：{"msg":"ok","ret":0,"data":"xxxxxxx_xxxxxxxx_xxxxxxxx","serverExecutedTime":5}
            //失败：{"msg":"错误描述","ret":非0,"data":"null","serverExecutedTime":5}

            string method1 = print(orderInfo);
            System.Console.WriteLine(method1);


        }


        private static string addprinter(string snslist)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";

            UTF8Encoding encoding = new UTF8Encoding();

            string postData = "printerContent=" + snslist;

            int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
            string stime = itime.ToString();
            string sig = sha1(USER, UKEY, stime);

            //公共参数
            postData += ("&user=" + USER);
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=" + "Open_printerAddlist");

            byte[] data = encoding.GetBytes(postData);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream resStream = req.GetRequestStream();

            resStream.Write(data, 0, data.Length);
            resStream.Close();

            HttpWebResponse response;
            string strResult;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                strResult = response.StatusCode.ToString();//错误信息
            }

            response.Close();
            req.Abort();

            return strResult;
        }


        //方法1
        private static string print(string orderInfo)
        {
            //标签说明：
            //单标签: 
            //"<BR>"为换行,"<CUT>"为切刀指令(主动切纸,仅限切刀打印机使用才有效果)
            //"<LOGO>"为打印LOGO指令(前提是预先在机器内置LOGO图片),"<PLUGIN>"为钱箱或者外置音响指令
            //成对标签：
            //"<CB></CB>"为居中放大一倍,"<B></B>"为放大一倍,"<C></C>"为居中,<L></L>字体变高一倍
            //<W></W>字体变宽一倍,"<QR></QR>"为二维码,"<BOLD></BOLD>"为字体加粗,"<RIGHT></RIGHT>"为右对齐

            ////拼凑订单内容时可参考如下格式
            //string orderInfo;
            //orderInfo = "<CB>测试打印</CB><BR>";//标题字体如需居中放大,就需要用标签套上
            //orderInfo += "名称　　　　　 单价  数量 金额<BR>";
            //orderInfo += "--------------------------------<BR>";
            //orderInfo += "番　　　　　　 1.0    1   1.0<BR>";
            //orderInfo += "番茄　　　　　 10.0   10  10.0<BR>";
            //orderInfo += "番茄炒　　　　 10.0   100 100.0<BR>";
            //orderInfo += "番茄炒粉　　　 100.0  100 100.0<BR>";
            //orderInfo += "番茄炒粉粉　　 1000.0 1   100.0<BR>";
            //orderInfo += "番茄炒粉粉粉粉 100.0  100 100.0<BR>";
            //orderInfo += "番茄炒粉粉粉粉 15.0   1   15.0<BR>";
            //orderInfo += "备注：快点送到<BR>";
            //orderInfo += "--------------------------------<BR>";
            //orderInfo += "合计：xx.0元<BR>";
            //orderInfo += "送货地点：xxxxxxxxxxxxxxxxx<BR>";
            //orderInfo += "联系电话：138000000000<BR>";
            //orderInfo += "订餐时间：2011-01-06 19:30:10<BR>";
            //orderInfo += "----------请扫描二维码----------";
            //orderInfo += "<QR>http://www.dzist.com</QR>";//把二维码字符串用标签套上即可自动生成二维码
            //orderInfo += "<BR>";

            orderInfo = Uri.EscapeDataString(orderInfo);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();

            string postData = "sn=" + SN;
            postData += ("&content=" + orderInfo);
            postData += ("&times=" + "1");//默认1联

            int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
            string stime = itime.ToString();
            string sig = sha1(USER, UKEY, stime);

            //公共参数
            postData += ("&user=" + USER);
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=" + "Open_printMsg");

            byte[] data = encoding.GetBytes(postData);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream resStream = req.GetRequestStream();

            resStream.Write(data, 0, data.Length);
            resStream.Close();

            HttpWebResponse response;
            string strResult;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                strResult = response.StatusCode.ToString();//错误信息
            }

            response.Close();
            req.Abort();
            //服务器返回的JSON字符串，建议要当做日志记录起来
            return strResult;

        }

        //方法2
        private static string queryOrderState(string orderid)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";

            UTF8Encoding encoding = new UTF8Encoding();

            string postData = "orderid=" + orderid;

            int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
            string stime = itime.ToString();
            string sig = sha1(USER, UKEY, stime);

            //公共参数
            postData += ("&user=" + USER);
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=" + "Open_queryOrderState");

            byte[] data = encoding.GetBytes(postData);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream resStream = req.GetRequestStream();

            resStream.Write(data, 0, data.Length);
            resStream.Close();

            HttpWebResponse response;
            string strResult;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                strResult = response.StatusCode.ToString();//错误信息
            }

            response.Close();
            req.Abort();

            return strResult;
        }


        //方法3
        private static string queryOrderInfoByDate(string strdate)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";

            UTF8Encoding encoding = new UTF8Encoding();

            string postData = "sn=" + SN;
            postData += ("&date=" + strdate);

            int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
            string stime = itime.ToString();
            string sig = sha1(USER, UKEY, stime);

            //公共参数
            postData += ("&user=" + USER);
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=" + "Open_queryOrderInfoByDate");

            byte[] data = encoding.GetBytes(postData);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream resStream = req.GetRequestStream();

            resStream.Write(data, 0, data.Length);
            resStream.Close();

            HttpWebResponse response;
            string strResult;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                strResult = response.StatusCode.ToString();//错误信息
            }

            response.Close();
            req.Abort();

            return strResult;
        }


        //方法4
        private static string queryPrinterStatus()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";

            UTF8Encoding encoding = new UTF8Encoding();

            string postData = "sn=" + SN;

            int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
            string stime = itime.ToString();
            string sig = sha1(USER, UKEY, stime);

            //公共参数
            postData += ("&user=" + USER);
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=" + "Open_queryPrinterStatus");

            byte[] data = encoding.GetBytes(postData);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream resStream = req.GetRequestStream();

            resStream.Write(data, 0, data.Length);
            resStream.Close();

            HttpWebResponse response;
            string strResult;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                strResult = response.StatusCode.ToString();//错误信息
            }

            response.Close();
            req.Abort();

            return strResult;

        }


        //签名USER,UKEY,STIME
        public static string sha1(string user, string ukey, string stime)
        {
            var buffer = Encoding.UTF8.GetBytes(user + ukey + stime);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString().ToLower();

        }


        private static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); return (int)(time - startTime).TotalSeconds;
        }

    }
}
