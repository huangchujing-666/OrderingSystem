
using System;

using System.Collections.Generic;

using System.Text;

using System.Runtime.InteropServices;

using System.Threading;

using System.Drawing;

using System.Management;

using System.IO;

using LaisonTech.MediaLib;

//using LaisonTech.CommonBLL;

using Microsoft.Win32.SafeHandles;

 

namespace LaisonTech.MediaLib

{



    #region 结构体定义



    [StructLayout(LayoutKind.Sequential)]

    public struct OVERLAPPED

    {

        int Internal;

        int InternalHigh;

        int Offset;

        int OffSetHigh;

        int hEvent;

    };



    [StructLayout(LayoutKind.Sequential)]

    public struct PRINTER_DEFAULTS

    {



        public int pDatatype;



        public int pDevMode;



        public int DesiredAccess;



    }



    /// <summary>

    /// 对齐方式

    /// </summary>

    public enum eTextAlignMode

    {

        Left = 0,

        Middle = 1,

        Right = 2

    }



    #endregion



    /// <summary>

    /// 小票打印类

    /// 使用方法:

    /// 1 GetPrinterList获取已经安装的所有打印机列表.

    ///  Open 打开指定打印机

    /// 2 控制打印机动作、执行打印内容之前，必须先调用StartPrint，准备向打印机发送控制指令

    /// 3 调用SetLeft, SetBold, SetAlignMode, SetFontSize ... ...设置打印参数

    /// 4  PrintText 打印内容.注意：打印该行内容后会自动换行(本类会在该行内容末尾添加一个换行符)

    ///   PrintImageFile 或 PrintBitMap打印图片

    /// 5 控制指令和打印内容都发送完毕后,调用 EndPrint执行真正打印动作

    /// 6 退出程序前调用Close

    /// </summary>

    public class ReceiptHelper

    {

        #region 指令定义



        private static Byte[] Const_Init = new byte[] { 0x1B, 0x40,

            0x20, 0x20, 0x20, 0x0A,

            0x1B, 0x64,0x10};



        //设置左边距

        private const string Const_SetLeft = "1D 4C ";





        //设置粗体

        private const string Const_SetBold = "1B 45 ";

        private const String Const_Bold_YES = "01";

        private const String Const_Bold_NO = "00";





        //设置对齐方式

        private const string Const_SetAlign = "1B 61 ";

        private const String Const_Align_Left = "30";

        private const String Const_Align_Middle = "31";

        private const String Const_Align_Right = "32";



        //设置字体大小,与 SetBigFont 不能同时使用

        private const string Const_SetFontSize = "1D 21 ";



        //设置是否大字体,等同于 SetFontSize = 2

        //private const String Const_SetBigFontBold = "1B 21 38";

        //private const String Const_SetBigFontNotBold = "1B 21 30";

        //private const String Const_SetCancelBigFont = "1B 21 00";



        /// <summary>

        /// 打印并走纸

        /// </summary>

        private static Byte[] Const_Cmd_Print = new byte[] { 0x1B, 0x4A, 0x00 };

        //走纸

        private const string Const_FeedForward = "1B 4A ";

        private const string Const_FeedBack = "1B 6A ";



        //切纸

        private static Byte[] Const_SetCut = new byte[] { 0x1D, 0x56, 0x30 };



        //查询打印机状态

        private static Byte[] Const_QueryID = new byte[] { 0x1D, 0x67, 0x61 };



        //回复帧以 ID 开头

        private static String Const_ResponseQueryID = "ID";



        /// <summary>

        /// 设置图标的指令

        /// </summary>

        private static Byte[] Const_SetImageCommand = new Byte[] { 0x1B, 0x2A, 0x21 };



        #endregion



        #region 常量定义



        /// <summary>

        /// 最大字体大小

        /// </summary>

        public const Int32 Const_MaxFontSize = 8;

        /// <summary>

        /// 最大走纸距离

        /// </summary>

        public const Int32 Const_MaxFeedLength = 5000;



        /// <summary>

        /// 最大高宽

        /// </summary>

        public const Int32 Const_MaxImageLength = 480;



        /// <summary>

        /// 每次通信最多打印的行数

        /// </summary>

        public const Int32 Const_OncePrintRowCount = 24;



        public const Int32 Const_BrightnessGate = 100;



        /// <summary>

        /// 无效句柄

        /// </summary>

        public const Int32 Const_InvalidHandle = -1;

        #endregion



        #region 私有成员



        /// <summary>

        /// 打印机句柄

        /// </summary>

        private int m_Handle = -1;



        /// <summary>

        /// 是否已经初始化

        /// </summary>

        private Boolean m_Inited = false;





        #endregion



        #region 私有函数



        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter,

            out Int32 hPrinter, IntPtr pd);



        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool StartDocPrinter(Int32 hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);



        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool EndDocPrinter(Int32 hPrinter);



        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool StartPagePrinter(Int32 hPrinter);



        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool EndPagePrinter(Int32 hPrinter);



        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool WritePrinter(Int32 hPrinter, Byte[] pBytes, Int32 dwCount, out Int32 dwWritten);



        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool ClosePrinter(Int32 hPrinter);





        /// <summary>

        /// 发送指令

        /// </summary>

        /// <param name="cmd"></param>

        /// <returns></returns>

        private Boolean SendCommand(Byte[] cmd)

        {

            if (m_Handle == Const_InvalidHandle || cmd == null || cmd.Length < 2)

            {

                return false;

            }



            int writelen = 0;

            Boolean bl = WritePrinter(m_Handle, cmd, cmd.Length, out writelen);



            if (!bl) return false;

            return (writelen >= cmd.Length);

        }



        /// <summary>

        /// 发送文本格式的指令

        /// </summary>

        /// <param name="cmd"></param>

        /// <returns></returns>

        private Boolean SendCommand(String hexstrcmd)

        {

            if (m_Handle == Const_InvalidHandle || hexstrcmd == null || hexstrcmd.Length < 4)

            {

                return false;

            }



            byte[] mybyte = null;

            Boolean bl = DataFormatProcessor.HexStringToBytes(hexstrcmd, out mybyte);

            bl = SendCommand(mybyte);

            return bl;

        }





        #endregion



        #region 内部处理 - 打印图片



        /// <summary>

        /// 把图片转换为指令字节,图片最大高宽不能超过480

        /// </summary>

        /// <param name="image"></param>

        /// <param name="bmpbytes"></param>

        /// <returns></returns>

        public static Boolean LoadImage(Bitmap image,

            ref Byte[] bitarray, ref Int32 datawidth, ref Int32 dataheight)

        {

            Int32 newwidth = 0;

            Int32 newheight = 0;

            Bitmap destimage = image;

            Boolean bl = false;



            //如果高度超过范围,或宽度超过范围,需要进行缩小

            if (image.Width > Const_MaxImageLength || image.Height > Const_MaxImageLength)

            {

                //按照高度和宽度，较大的那一边，进行缩放

                if (image.Width > image.Height)

                {

                    newwidth = Const_MaxImageLength;

                    newheight = (Int32)(image.Height * newwidth / (float)image.Width);

                }

                else

                {

                    newheight = Const_MaxImageLength;

                    newwidth = (Int32)(newheight * image.Width / (float)image.Height);

                }



                bl = ImageProcessor.ResizeImage(image, newwidth, newheight, ref destimage);

            }



            //把数据转换为字节数组

            bl = GetBitArray(image, ref bitarray, ref datawidth, ref dataheight);

            return bl;

        }



        /// <summary>

        /// 把图片转换为指令字节,图片最大高宽不能超过480

        /// 如果图片的高度不是24的整数倍,则修改为24的整数倍

        /// </summary>

        /// <param name="image"></param>

        /// <param name="bmpbytes"></param>

        /// <returns></returns>

        public static Boolean LoadImageFromFile(String imagefilename, ref Byte[] bmpbytes,

            ref Int32 width, ref Int32 height)

        {

            Bitmap img = ImageProcessor.LoadBitImage(imagefilename);

            if (img == null)

            {

                return false;

            }



            Boolean bl = LoadImage(img, ref bmpbytes, ref width, ref height);

            return bl;

        }



        /// <summary>

        /// 把图片转换为位图数组,每个字节的每个比特位,对应当前像素 是否需要打印

        /// </summary>

        /// <param name="img"></param>

        /// <param name="allbitary"></param>

        /// <returns></returns>

        public static Boolean GetBitArray(Bitmap img,

            ref Byte[] allbitary, ref Int32 width, ref Int32 height)

        {

            if (img == null)

            {

                return false;

            }



            //ESC指令格式规定：

            //1 打印图片时，每条指令最多只打印24行;不足24行的,也要用全0填充满数据字节

            //2 打印24行数据时，按照光栅模式纵向获取数据

            //  即先获取所有x=0的点（第0列）转换为3个字节；

            //  再获取所有x=1的点转换为3个字节；...直到获取到最右侧一列的点

            //3 打印完当前24行数据后，再获取后续24行的数据内容,直到所有的数据获取完毕



            //获取亮度数组

            Boolean[] briary = null;

            Boolean bl = ImageProcessor.ToBooleanArray(img, Const_BrightnessGate, ref briary);

            if (!bl)

            {

                return false;

            }



            height = img.Height;//如果图像高度不是24整数倍,设置为24的整数倍       

            if (height % Const_OncePrintRowCount != 0)

            {

                height = height + Const_OncePrintRowCount - height % Const_OncePrintRowCount;

            }



            width = img.Width;//如果图像宽度不是8的整数倍,设置为8的整数倍

            if (width % 8 != 0)

            {

                width = width + 8 - width % 8;

            }



            Int32 bytelen = height * width / 8;//每个像素对应1个比特位,因此总字节数=像素位数/8



            allbitary = new Byte[bytelen];



            Int32 byteidxInCol = 0;//当前列里首个像素,在目标字节数组里的下标

            Int32 byteidx = 0;//当前像素在目标数组里的字节下标

            Int32 bitidx = 0;//当前像素在目标数组里当前字节里的比特位下标

            Int32 pixidxInCol = 0;//当前像素在当前列里的第几个位置



            Int32 pixidx = 0;//当前像素在原始图片里的下标



            Int32 rowidx = 0; //当前 处理的像素点所在行,不能超过 图像高度

            Int32 curprocrows = 0;//当前需要处理的行数量

            while (rowidx < height)

            {

                //按照纵向次序,把当前列的24个数据,转换为3个字节

                for (Int32 colidx = 0; colidx < img.Width; ++colidx)

                {

                    //如果当前还剩余超过24行没处理,处理24行

                    if (rowidx + Const_OncePrintRowCount <= img.Height)

                    {

                        curprocrows = Const_OncePrintRowCount;

                    }

                    else

                    {

                        //已经不足24行,只处理剩余行数

                        curprocrows = img.Height - rowidx;

                    }



                    pixidxInCol = 0; //本列里从像素0开始处理

                    for (Int32 y = rowidx; y < rowidx + curprocrows; ++y)

                    {

                        //原始图片里像素位置

                        pixidx = y * img.Width + colidx;



                        //获取当前像素的亮度值.如果当前像素是黑点,需要把数组里的对应比特位设置为1

                        if (briary[pixidx])

                        {

                            bitidx = 7 - pixidxInCol % 8;//最高比特位对应首个像素.最低比特位对应末个像素

                            byteidx = byteidxInCol + pixidxInCol / 8; //由于最后一段可能不足24行,因此不能使用byteidx++



                            DataFormatProcessor.SetBitValue(bitidx, true, ref allbitary[byteidx]);

                        }

                        pixidxInCol++;

                    }

                    byteidxInCol += 3;//每列固定24个像素,3个字节

                }



                rowidx += Const_OncePrintRowCount;

            }



            return true;

        }



        #endregion



        #region 公开函数



        private static ReceiptHelper m_instance = new ReceiptHelper();



        /// <summary>

        /// 当前使用的打印机名称

        /// </summary>

        public String PrinterName

        {

            get; private set;

        }



        /// <summary>

        /// 单件模式

        /// </summary>

        /// <returns></returns>

        public static ReceiptHelper GetInstance()

        {

            return m_instance;

        }



        /// <summary>

        /// 获取本机安装的所有打印机

        /// </summary>

        /// <returns></returns>

        public static List<String> GetPrinterList()

        {

            List<String> ret = new List<String>();

            if (PrinterSettings.InstalledPrinters.Count < 1)

            {

                return ret;

            }



            foreach (String printername in PrinterSettings.InstalledPrinters)

            {

                ret.Add(printername);

            }

            return ret;

        }



        /// <summary>

        /// 打开打印机

        /// </summary>

        /// <param name="printername"></param>

        /// <returns></returns>

        public Boolean Open(String printername)

        {

            if (m_Inited)

            {

                return true;

            }

            Boolean bl = OpenPrinter(printername.Normalize(), out m_Handle, IntPtr.Zero);



            m_Inited = (bl && m_Handle != 0);

            return true;

        }



        /// <summary>

        /// 开始打印，在打印之前必须调用此函数

        /// </summary>

        /// <returns></returns>

        public Boolean StartPrint()

        {

            if (!m_Inited)

            {

                return false;

            }

            DOCINFOA di = new DOCINFOA();

            di.pDocName = "My C#.NET RAW Document";

            di.pDataType = "RAW";

            //Start a document.

            Boolean bl = StartDocPrinter(m_Handle, 1, di);

            if (!bl)

            {

                return false;

            }

            // Start a page.

            bl = StartPagePrinter(m_Handle);

            return bl;

        }



        /// <summary>

        /// 结束打印，在打印结束之后必须调用此函数

        /// </summary>

        /// <returns></returns>

        public Boolean EndPrint()

        {

            if (!m_Inited)

            {

                return false;

            }

            Boolean bl = EndPagePrinter(m_Handle);

            bl = EndDocPrinter(m_Handle);

            return bl;

        }



        /// <summary>

        /// 销毁

        /// </summary>

        /// <returns></returns>

        public Boolean Close()

        {

            if (!m_Inited)

            {

                return true;

            }

            m_Inited = false;



            //关闭设备句柄

            ClosePrinter(m_Handle);

            m_Handle = -1;

            return true;

        }



        /// <summary>

        /// 打印文本.在调用本函数之前必须先调用正确的 设置字体、左边距

        /// </summary>

        /// <param name="content"></param>

        /// <returns></returns>

        public Boolean PrintText(String content)

        {

            if (!m_Inited)

            {

                return false;

            }



            byte[] bytes = null;

            if (content.Length < 1)

            {

                content = "  ";

            }



            if (content[content.Length - 1] != (char)0x0D &&

                content[content.Length - 1] != (char)0x0A)

            {

                content = content + (char)0x0A;

            }



            bytes = DataFormatProcessor.StringToBytes(content);

            bool bl = SendCommand(bytes);

            return bl;

        }



        /// <summary>

        /// 设置对齐方式

        /// </summary>

        /// <param name="left"></param>

        /// <returns></returns>

        public bool SetAlignMode(eTextAlignMode alignmode)

        {

            if (!m_Inited)

            {

                return false;

            }



            String code = String.Empty;

            switch (alignmode)

            {

                case eTextAlignMode.Left:

                    code = Const_Align_Left;

                    break;

                case eTextAlignMode.Middle:

                    code = Const_Align_Middle;

                    break;

                case eTextAlignMode.Right:

                    code = Const_Align_Right;

                    break;

                default:

                    code = Const_Align_Left;

                    break;

            }



            //注意：先低字节后高字节

            string str = Const_SetAlign + code;

            bool bl = SendCommand(str);

            return bl;

        }



        /// <summary>

        /// 设置左边距

        /// </summary>

        /// <param name="left"></param>

        /// <returns></returns>

        public bool SetLeft(int left)

        {

            if (!m_Inited)

            {

                return false;

            }



            //注意：先低字节后高字节

            String hexstr = left.ToString("X4");

            string str = Const_SetLeft + hexstr.Substring(2, 2) + hexstr.Substring(0, 2);

            bool bl = SendCommand(str);

            return bl;

        }



        /// <summary>

        /// 设置粗体

        /// </summary>

        /// <param name="bold"></param>

        /// <returns></returns>

        public Boolean SetBold(Boolean bold)

        {

            if (!m_Inited)

            {

                return false;

            }



            //注意：先低字节后高字节

            String str = String.Empty;

            if (bold)

            {

                str = Const_SetBold + Const_Bold_YES;

            }

            else

            {

                str = Const_SetBold + Const_Bold_NO;

            }

            bool bl = SendCommand(str);

            return bl;

        }



        /// <summary>

        /// 切纸

        /// </summary>

        /// <returns></returns>

        public bool Cut()

        {

            if (!m_Inited)

            {

                return false;

            }

            bool bl = SendCommand(Const_SetCut);

            return bl;

        }





        /// <summary>

        /// 打印图片

        /// </summary>

        /// <param name="bitmap"></param>

        /// <returns></returns>

        public bool PrintImageFile(String imgfilename)

        {

            if (!m_Inited)

            {

                return false;

            }

            Bitmap img = ImageProcessor.LoadBitImage(imgfilename);

            if (img == null)

            {

                return false;

            }



            Boolean bl = PrintBitmap(img);

            return bl;

        }



        /// <summary>

        /// 打印图片

        /// </summary>

        /// <param name="bitmap"></param>

        /// <returns></returns>

        public bool PrintBitmap(Bitmap bitmap)

        {

            if (!m_Inited)

            {

                return false;

            }



            if (bitmap == null ||

                bitmap.Width > Const_MaxImageLength ||

                bitmap.Height > Const_MaxImageLength)

            {

                return false;

            }



            Byte[] bitary = null;

            Int32 width = 0;

            Int32 height = 0;

            Boolean bl = GetBitArray(bitmap, ref bitary, ref width, ref height);



            bl = PrintBitmapBytes(bitary, bitmap.Width, bitmap.Height);

            return bl;

        }



        /// <summary>

        /// 打印图片

        /// </summary>

        /// <param name="bitmap"></param>

        /// <returns></returns>

        public bool PrintBitmapBytes(Byte[] imgbitarray, Int32 width, Int32 height)

        {

            if (!m_Inited)

            {

                return false;

            }

            Int32 bytes = width * height / 8;

            //检查是否尺寸符合要求

            if (width > Const_MaxImageLength || height > Const_MaxFeedLength ||

                width < 1 || height < 1 ||

                imgbitarray == null)

            {

                return false;

            }



            //每次获取24行的数据进行发送,这24行的字节数

            Int32 blockbytes = width * Const_OncePrintRowCount / 8;

            if (blockbytes < 1)

            {

                return false;

            }



            Boolean bl = false;



            //一共需要发送的块数量

            Int32 blocks = imgbitarray.Length / blockbytes;



            //每次发送的数据字节数 = 1B 2A 21 2字节长度 +　数据内容

            Byte[] cmdbytes = new Byte[5 + blockbytes];

            //指令

            Array.Copy(Const_SetImageCommand, cmdbytes, 3);

            //数据长度,即 每行的点数

            DataFormatProcessor.Int16ToBytes(width, ref cmdbytes, 3);

            //数据内容

            for (Int32 blockidx = 0; blockidx < blocks; ++blockidx)

            {

                Array.Copy(imgbitarray, blockidx * blockbytes, cmdbytes, 5, blockbytes);

                //发送当前指令

                bl = SendCommand(cmdbytes);

                if (!bl) return false;

                //休眠20毫秒

                Thread.Sleep(20);

                //发送 打印指令

                bl = SendCommand(Const_Cmd_Print);

                if (!bl) return false;

            }



            return bl;

        }



        /// <summary>

        /// 走纸

        /// </summary>

        /// <param name="length"></param>

        /// <returns></returns>

        public bool Feed(int length)

        {

            if (!m_Inited)

            {

                return false;

            }

            if (length < 1)

                length = 1;

            if (length > Const_MaxFeedLength)

            {

                length = Const_MaxFeedLength;

            }

            string len = length.ToString("X2");

            len = Const_FeedForward + len;

            bool bl = SendCommand(len);

            return bl;

        }



        /// <summary>

        /// 回退走纸

        /// </summary>

        /// <param name="length"></param>

        /// <returns></returns>

        public bool FeedBack(int length)

        {

            if (!m_Inited)

            {

                return false;

            }

            if (length < 1)

                length = 1;

            if (length > Const_MaxFeedLength)

            {

                length = Const_MaxFeedLength;

            }

            string len = length.ToString("X2");

            len = Const_FeedBack + len;

            bool bl = SendCommand(len);

            return bl;

        }



        /// <summary>

        /// 设置字体大小.本函数不可与SetBigFont同时使用

        /// </summary>

        /// <param name="sizerate">大小倍率,取值范围 1 - 8</param>

        /// <returns></returns>

        public bool SetFontSize(Int32 sizerate)

        {

            if (!m_Inited)

            {

                return false;

            }



            if (sizerate < 1)

            {

                sizerate = 1;

            }



            if (sizerate > Const_MaxFontSize)

            {

                sizerate = Const_MaxFontSize;

            }

            sizerate--;

            String sizecodestr = Const_SetFontSize + sizerate.ToString("X1") + sizerate.ToString("X1");

            bool bl = SendCommand(sizecodestr);

            return bl;

        }



        #endregion







    }

}

 

3.图像处理 ImageProcessor
using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Drawing;

using LaisonTech.CommonBLL;

using System.Drawing.Imaging;

using System.IO;

using System.Drawing.Drawing2D;

using System.Windows.Forms;

using AForge.Imaging.Filters;

 

namespace LaisonTech.MediaLib

{

    /// <summary>

    /// 图片格式

    /// </summary>

    public enum ePictureFileFormat

    {

        Bmp = 0,

        Gif = 1,

        Icon = 2,

        Jpeg = 3,

        Png = 4,

    }



    /// <summary>

    /// 转为灰度图像的方式

    /// </summary>

    public enum eGrayMode

    {

        /// <summary>

        /// 算数平均

        /// </summary>

        ArithmeticAverage = 0,

        /// <summary>

        /// 加权平均

        /// </summary>

        WeightedAverage = 1,

    }



    /// <summary>

    /// 比较2个图片的指定区域范围,像素的相同类型

    /// </summary>

    public enum eAreaDifferentType

    {

        /// <summary>

        /// 所有像素都相同

        /// </summary>

        AllSame = 0,

        /// <summary>

        /// 所有像素都不同

        /// </summary>

        AllDifferent = 1,

        /// <summary>

        /// 部分相同部分不同

        /// </summary>

        Partial = 2,

    }



    /// <summary>

    /// 图片文件处理

    /// </summary>

    public class ImageProcessor

    {

        #region 常量定义



        public const Byte Const_BrightnessWhite = 255;

        public const Byte Const_BrightnessBlack = 0;





        /// <summary>

        /// 比较结果的图片里，亮度相同部分的填充颜色

        /// </summary>

        public static Color Const_SameBrightnessColor = Color.Black;

        /// <summary>

        /// 比较结果的图片里，亮度相同部分的填充颜色

        /// </summary>

        public static Color Const_DifferentBrightnessColor = Color.White;



        public const Byte Const_BlackBrightness = 0;

        public const Byte Const_WhiteBrightness = 255;

        public const Int32 Const_MaxBrightness = 255;



        public const Int32 Const_MinBrightness = -255;



        /// <summary>

        /// 亮度的中间值

        /// </summary>

        public const Int32 Const_MiddleBrightness = 128;

        #endregion



        #region 屏幕截图,打印



        /// <summary>

        /// 获取屏幕分辨率

        /// </summary>

        /// <param name="width"></param>

        /// <param name="height"></param>

        public static void GetScreenSize(ref Int32 width, ref Int32 height)

        {

            height = Screen.PrimaryScreen.Bounds.Height;

            width = Screen.PrimaryScreen.Bounds.Width;

        }



        /// <summary>

        ///截图指定控件上显示的内容

        /// </summary>

        /// <param name="ctrl"></param>

        /// <returns></returns>

        public static Image CaptureControlImage(Control ctrl)

        {

            if (ctrl == null)

            {

                return null;

            }



            Control parent = ctrl;

            if (ctrl.Parent != null)

            {

                parent = ctrl.Parent;

            }

            Point screenPoint = parent.PointToScreen(ctrl.Location);



            Image ret = new Bitmap(ctrl.Width, ctrl.Height);

            Graphics g = Graphics.FromImage(ret);

            g.CopyFromScreen(screenPoint.X, screenPoint.Y,

                0, 0, ctrl.Size);

            g.DrawImage(ret, 0, 0);



            return ret;

        }





        #endregion



        #region 装载图片



        /// <summary>

        /// 装载图像文件

        /// </summary>

        /// <param name="filename"></param>

        /// <returns></returns>

        public static Image LoadImage(String filename)

        {

            //Boolean bl = FileProcessor.FileExist(filename);

            //if (!bl)

            //{

            //    return null;

            //}

            //Bitmap image = (Bitmap)Bitmap.FromFile(filename);

            //return image;



            //以上方法会导致图片文件被锁定,无法删除移动



            Byte[] photodata = null;

            Boolean bl = FileProcessor.FileExist(filename);

            if (!bl)

            {

                return null;

            }



            bl = FileProcessor.ReadFileBytes(filename, out photodata);

            if (!bl)

            {

                return null;

            }



            MemoryStream ms = null;

            Image myImage = null;

            try

            {

                ms = new MemoryStream(photodata);

                myImage = Bitmap.FromStream(ms);

                ms.Close();

            }

            catch (System.Exception ex)

            {

                Console.WriteLine("LoadImage error:" + ex.Message);

                myImage = null;

            }

            return myImage;

        }



        /// <summary>

        /// 装载图像文件

        /// </summary>

        /// <param name="filename"></param>

        /// <returns></returns>

        public static Bitmap LoadBitImage(String filename)

        {

            Bitmap ret = (Bitmap)LoadImage(filename);

            return ret;

        }



        /// <summary>

        /// 保存图片到指定路径

        /// </summary>

        /// <param name="img"></param>

        /// <param name="filename"></param>

        /// <returns></returns>

        public static Boolean SaveImage(Image img, String filename)

        {

            FileProcessor.DeleteFile(filename);

            if (img == null)

            {

                return false;

            }

            //获取保存图片的路径，如果路径不存在，新建

            String folder = FileProcessor.GetDirectoryName(filename);

            if (!FileProcessor.DirectoryExist(folder))

            {

                FileProcessor.CreateDirectory(folder);

            }

            img.Save(filename);

            Boolean bl = FileProcessor.FileExist(filename);

            return bl;

        }



        #endregion



        #region 转换图片格式



        /// <summary>

        /// 转换图片格式

        /// </summary>

        /// <param name="bmpfilename"></param>

        /// <param name="jpgfilename"></param>

        /// <returns></returns>

        public static Boolean BmpToJpg(String bmpfilename, String jpgfilename)

        {

            Boolean bl = ChangeFileFormat(bmpfilename, jpgfilename, ePictureFileFormat.Jpeg);

            return bl;

        }



        /// <summary>

        /// 转换图片格式

        /// </summary>

        /// <param name="srcfilename"></param>

        /// <param name="destfilename"></param>

        /// <param name="destformat"></param>

        /// <returns></returns>

        public static Boolean ChangeFileFormat(String srcfilename, String destfilename, ePictureFileFormat destformat)

        {

            Boolean bl = FileProcessor.FileExist(srcfilename);

            if (!bl)

            {

                return false;

            }

            Image image = Image.FromFile(srcfilename);



            ImageFormat IFMT = null;

            switch (destformat)

            {

                case ePictureFileFormat.Bmp:

                    IFMT = ImageFormat.Bmp;

                    break;

                case ePictureFileFormat.Gif:

                    IFMT = ImageFormat.Gif;

                    break;

                case ePictureFileFormat.Icon:

                    IFMT = ImageFormat.Icon;

                    break;

                case ePictureFileFormat.Jpeg:

                    IFMT = ImageFormat.Jpeg;

                    break;

                case ePictureFileFormat.Png:

                    IFMT = ImageFormat.Png;

                    break;

                default:

                    IFMT = ImageFormat.Jpeg;

                    break;

            }

            image.Save(destfilename, IFMT);

            image.Dispose();



            bl = FileProcessor.FileExist(destfilename);

            if (!bl)

            {

                return false;

            }



            Int32 filelen = FileProcessor.GetFileLength(destfilename);

            return (filelen > 0);

        }



        /// <summary>

        /// 变成黑白图

        /// </summary>

        /// <param name="srcbitmap">原始图</param>

        /// <param name="mode">模式。0:加权平均  1:算数平均</param>

        /// <returns></returns>

        public static Bitmap ToGray(Bitmap bitmap, eGrayMode mode = eGrayMode.ArithmeticAverage)

        {

            if (bitmap == null)

            {

                return null;

            }



            int width = bitmap.Width;

            int height = bitmap.Height;

            byte newColor = 0;

            try

            {

                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe

                {

                    byte* curpix = (byte*)srcData.Scan0.ToPointer();

                    if (mode == eGrayMode.ArithmeticAverage)// 算数平均

                    {

                        for (int y = 0; y < height; y++)

                        {

                            for (int x = 0; x < width; x++)

                            {

                                newColor = (byte)((float)(curpix[0] + curpix[1] + curpix[2]) / 3.0f);

                                curpix[0] = newColor;

                                curpix[1] = newColor;

                                curpix[2] = newColor;

                                curpix += 3;

                            }

                            curpix += srcData.Stride - width * 3;

                        }

                    }

                    else

                    {

                        // 加权平均

                        for (int y = 0; y < height; y++)

                        {

                            for (int x = 0; x < width; x++)

                            {

                                newColor = (byte)((float)curpix[0] * 0.114f + (float)curpix[1] * 0.587f + (float)curpix[2] * 0.299f);

                                curpix[0] = newColor;

                                curpix[1] = newColor;

                                curpix[2] = newColor;

                                curpix += 3;

                            }

                            curpix += srcData.Stride - width * 3;

                        }

                    }

                    bitmap.UnlockBits(srcData);

                }

            }

            catch

            {

                bitmap = null;

            }



            return bitmap;

        }



        /// <summary>

        /// 获取一幅图片对应的所有像素亮度数组

        /// </summary>

        /// <param name="bitmap">原始图</param>

        /// <param name="brightnessary">亮度值数组</param>

        /// <param name="mode">模式。0:加权平均  1:算数平均</param>

        /// <returns></returns>

        public static Boolean GetImageBrightness(Bitmap bitmap, ref Byte[] brightnessary,

            eGrayMode mode = eGrayMode.WeightedAverage)

        {

            if (bitmap == null)

            {

                return false;

            }



            int width = bitmap.Width;

            int height = bitmap.Height;

            if (width < 1 || height < 1)

            {

                return false;

            }



            brightnessary = new Byte[width * height];

            Boolean bl = false;

            Int32 rowredundancy = 0;//每一行像素，对应的数组长度 与 实际像素点数的差值

            Int32 pixidx = 0;//像素下标

            try

            {

                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                rowredundancy = srcData.Stride - width * 3;//每行末尾还有这么多的冗余字节



                unsafe

                {

                    byte* curpix = (byte*)srcData.Scan0.ToPointer();

                    if (mode == eGrayMode.ArithmeticAverage)// 算数平均

                    {

                        for (int y = 0; y < height; y++)

                        {

                            for (int x = 0; x < width; x++)

                            {

                                brightnessary[pixidx] = (byte)((float)(curpix[0] + curpix[1] + curpix[2]) / 3.0f);

                                ++pixidx;

                                curpix += 3;

                            }

                            curpix += rowredundancy;

                        }

                    }

                    else

                    {

                        // 加权平均

                        for (int y = 0; y < height; y++)

                        {

                            for (int x = 0; x < width; x++)

                            {

                                brightnessary[pixidx] = (byte)((float)curpix[0] * 0.114f + (float)curpix[1] * 0.587f + (float)curpix[2] * 0.299f);

                                ++pixidx;

                                curpix += 3;

                            }

                            curpix += rowredundancy;

                        }

                    }

                    bitmap.UnlockBits(srcData);

                }

                bl = true;

            }

            catch (Exception ex)

            {

                bl = false;

                Console.WriteLine("Get brightness ary error:" + ex.Message);

            }



            return bl;

        }



        /// <summary>

        /// 变成黑白图,每个元素都是一个像素的亮度

        /// </summary>

        /// <param name=" bitmap ">原始图</param>

        /// <param name=" graybitmap ">黑白图</param>

        /// <param name=" brightnessbytes ">黑白所有像素点亮度</param>

        /// <param name="mode">模式。0:加权平均  1:算数平均</param>

        /// <returns></returns>

        public static Boolean ToGray(Bitmap bitmap, ref Bitmap graybitmap, ref Byte[] brightnessbytes,

            eGrayMode mode = eGrayMode.WeightedAverage)

        {

            if (bitmap == null)

            {

                return false;

            }



            brightnessbytes = new Byte[bitmap.Width * bitmap.Height];



            int width = bitmap.Width;

            int height = bitmap.Height;

            //Clone 可能引发 GDI+异常

            graybitmap = new Bitmap(bitmap);



            byte newColor = 0;

            Int32 bytesidx = 0;

            Boolean bl = false;

            try

            {

                BitmapData srcData = graybitmap.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe

                {

                    byte* curpix = (byte*)srcData.Scan0.ToPointer();

                    if (mode == eGrayMode.ArithmeticAverage)// 算数平均

                    {

                        for (int y = 0; y < height; y++)

                        {

                            for (int x = 0; x < width; x++)

                            {

                                newColor = (byte)((float)(curpix[0] + curpix[1] + curpix[2]) / 3.0f);



                                brightnessbytes[bytesidx] = newColor;

                                ++bytesidx;



                                curpix[0] = newColor;

                                curpix[1] = newColor;

                                curpix[2] = newColor;

                                curpix += 3;

                            }

                            curpix += srcData.Stride - width * 3;

                        }

                    }

                    else

                    {

                        // 加权平均

                        for (int y = 0; y < height; y++)

                        {

                            for (int x = 0; x < width; x++)

                            {

                                newColor = (byte)((float)curpix[0] * 0.114f + (float)curpix[1] * 0.587f + (float)curpix[2] * 0.299f);



                                brightnessbytes[bytesidx] = newColor;

                                ++bytesidx;



                                curpix[0] = newColor;

                                curpix[1] = newColor;

                                curpix[2] = newColor;

                                curpix += 3;

                            }

                            curpix += srcData.Stride - width * 3;

                        }

                    }

                    graybitmap.UnlockBits(srcData);

                }



                bl = true;

            }

            catch (Exception ex)

            {

                graybitmap = null;

                Console.WriteLine("ToGray error:" + ex.Message);

                bl = false;

            }



            return bl;

        }





        /// <summary>

        /// 把图片转换为非黑即白的二色图.

        /// </summary>

        /// <param name="bitmap">原始图</param>

        /// <param name="brightnessGate">亮度门限.超过此亮度认为白点,否则认为黑点</param>

        /// <param name="bitary">每个像素点是否为黑点的数组</param>

        /// <param name="trueAsblack">true-每个元素黑点为true,白点为false; false-每个元素白点为true,黑点为false</param>

        /// <returns></returns>

        public static Boolean ToBooleanArray(Bitmap bitmap, Byte brightnessGate, ref Boolean[] bitary, Boolean trueAsblack = true)

        {

            if (bitmap == null)

            {

                return false;

            }



            bitary = new Boolean[bitmap.Width * bitmap.Height];



            int width = bitmap.Width;

            int height = bitmap.Height;



            byte curcolor = 0;

            Int32 pixidx = 0;

            Boolean bl = false;

            try

            {

                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe

                {

                    byte* curpix = (byte*)srcData.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            curcolor = (byte)((float)(curpix[0] + curpix[1] + curpix[2]) / 3.0f);



                            if (trueAsblack)//true为黑点

                            {

                                bitary[pixidx] = (curcolor < brightnessGate);

                            }

                            else

                            {

                                //true为白点

                                bitary[pixidx] = (curcolor > brightnessGate);

                            }

                            ++pixidx;

                            curpix += 3;

                        }

                        curpix += srcData.Stride - width * 3;

                    }

                    bitmap.UnlockBits(srcData);

                }



                bl = true;

            }

            catch (Exception ex)

            {

                Console.WriteLine("ToGray error:" + ex.Message);

                bl = false;

            }



            return bl;

        }



        /// <summary>

        /// 亮度差数组变成bool数组.true表示亮度不同,false表示亮度相同

        /// </summary>

        /// <param name="bridiffary">亮度差数组</param>

        /// <param name="brightnessGate">亮度门限.超过此亮度认为白点,否则认为黑点</param>

        /// <returns></returns>

        public static Boolean BrightnessToBoolean(Byte[] bridiffary, Byte brightnessGate, ref Boolean[] boolary)

        {

            if (bridiffary == null || bridiffary.Length < 4)

            {

                return false;

            }



            boolary = new Boolean[bridiffary.Length];



            for (Int32 idx = 0; idx < bridiffary.Length; ++idx)

            {

                boolary[idx] = (bridiffary[idx] > brightnessGate);

            }



            return true;

        }



        #endregion



        #region 图片调整



        /// <summary>

        /// 调整亮度

        /// </summary>

        /// <param name="bitmap">原始图</param>

        /// <param name="degree">亮度,取值范围-255 - 255</param>

        /// <returns></returns>

        public static Bitmap SetBrightness(Bitmap srcbitmap, int brightnessOffset)

        {

            if (srcbitmap == null)

            {

                return null;

            }



            CommonCompute.SetInt32Range(ref brightnessOffset, Const_MinBrightness, Const_MaxBrightness);

            int width = srcbitmap.Width;

            int height = srcbitmap.Height;



            Bitmap bitmap = (Bitmap)srcbitmap.Clone();



            try

            {

                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                unsafe

                {

                    byte* curpix = (byte*)data.Scan0.ToPointer();

                    Int32 curcolor = 0;

                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            curcolor = curpix[0] + brightnessOffset;

                            CommonCompute.SetInt32Range(ref curcolor, 0, Const_MaxBrightness);

                            curpix[0] = (byte)curcolor;



                            curcolor = curpix[1] + brightnessOffset;

                            CommonCompute.SetInt32Range(ref curcolor, 0, Const_MaxBrightness);

                            curpix[1] = (byte)curcolor;



                            curcolor = curpix[2] + brightnessOffset;

                            CommonCompute.SetInt32Range(ref curcolor, 0, Const_MaxBrightness);

                            curpix[2] = (byte)curcolor;



                            curpix += 3;

                        }

                        curpix += data.Stride - width * 3;

                    }

                }



                bitmap.UnlockBits(data);

            }

            catch

            {

                bitmap = null;

            }



            return bitmap;

        }



        /// <summary>

        /// 调整图像对比度

        /// </summary>

        /// <param name="bitmap">原始图</param>

        /// <param name="degree">对比度 0 - 100</param>

        /// <returns></returns>

        public static Bitmap SetContrast(Bitmap srcbitmap, int contrast)

        {

            if (srcbitmap == null)

            {

                return null;

            }



            //对比度取值范围,0-100

            CommonCompute.SetInt32Range(ref contrast, 0, 100);



            Int32 curcolor = 0;

            Bitmap bitmap = (Bitmap)srcbitmap.Clone();

            int width = bitmap.Width;

            int height = bitmap.Height;



            //调整对比度基本思路：0时，所有像素点的亮度都设置为中间值128

            //100 时，把亮度大于128的像素，亮度设置为255；小于128的设置为0

            //即:50时，保持不变；小于50，所有点的亮度向中间值128偏移；大于50，所有点亮度值向两端偏移



            //如果当前像素点的亮度是130, 对比度为50时，结果仍然要是130,此时rate为1.0

            //对比度为100时，结果要变成255，此时rate >= 128

            //对比度为0时，结果要变成128，此时rate = 0

            //因此可知对比度与rate的对应关系

            //对比度:  0  50  100

            //rate  :  0  1   127              

            double rate = 0;

            if (contrast == 50)

            {

                rate = 1;

            }

            else if (contrast < 50)

            {

                rate = contrast / 50.0;//小于50的，对比度比率必须是纯小数,0-1 之间

            }

            else

            {

                rate = 1 + Const_MiddleBrightness * ((contrast - 50.0) / 50.0);//大于50的,比率必须是1到128之间的值

            }



            try

            {



                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe

                {

                    byte* curpix = (byte*)data.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            for (int i = 0; i < 3; i++) //R,G,B 3个通道

                            {

                                //对于 刚好亮度等于中间值的点,需要把亮度调高或调低1

                                //否则将无法实现对该点 提高对比度

                                if (curpix[i] == Const_MiddleBrightness)

                                {

                                    curpix[i] = (byte)(curpix[i] + 1);

                                }



                                //调整该像素对比度

                                curcolor = (Int32)(Const_MiddleBrightness + (curpix[i] - Const_MiddleBrightness) * rate);

                                CommonCompute.SetInt32Range(ref curcolor, Const_MinBrightness, Const_MaxBrightness);

                                curpix[i] = (byte)curcolor;

                                ++curpix;

                            }

                        }



                        curpix += data.Stride - width * 3;

                    }

                }

                bitmap.UnlockBits(data);

            }

            catch

            {

                bitmap = null;

            }



            return bitmap;

        }



        /// <summary>

        /// 任意角度旋转

        /// </summary>

        /// <param name="srcbitmap">原始图Bitmap</param>

        /// <param name="angle">旋转角度</param>

        /// <param name="bkColor">背景色</param>

        /// <returns>输出Bitmap</returns>

        public static Bitmap Rotate(Bitmap srcbitmap, float angle, Color bkColor)

        {

            int w = srcbitmap.Width + 2;

            int h = srcbitmap.Height + 2;



            PixelFormat pf;



            if (bkColor == Color.Transparent)

            {

                pf = PixelFormat.Format32bppArgb;

            }

            else

            {

                pf = srcbitmap.PixelFormat;

            }



            Bitmap tmp = new Bitmap(w, h, pf);

            Graphics g = Graphics.FromImage(tmp);

            g.Clear(bkColor);

            g.DrawImageUnscaled(srcbitmap, 1, 1);

            g.Dispose();



            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(0f, 0f, w, h));

            Matrix mtrx = new Matrix();

            mtrx.Rotate(angle);

            RectangleF rct = path.GetBounds(mtrx);



            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);

            g = Graphics.FromImage(dst);

            g.Clear(bkColor);

            g.TranslateTransform(-rct.X, -rct.Y);

            g.RotateTransform(angle);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            g.DrawImageUnscaled(tmp, 0, 0);

            g.Dispose();



            tmp.Dispose();



            return dst;

        }



        /// <summary>

        /// Gamma校正

        /// </summary>

        /// <param name="srcbitmap">输入Bitmap</param>

        /// <param name="val">[0 <-明- 1 -暗-> 2]</param>

        /// <returns>输出Bitmap</returns>

        public static Bitmap SetGamma(Bitmap srcbitmap, float val)

        {

            if (srcbitmap == null)

            {

                return null;

            }



            // 1表示无变化，就不做

            if (val == 1.0000f) return srcbitmap;



            try

            {

                Bitmap b = new Bitmap(srcbitmap.Width, srcbitmap.Height);

                Graphics g = Graphics.FromImage(b);

                ImageAttributes attr = new ImageAttributes();



                attr.SetGamma(val, ColorAdjustType.Bitmap);

                g.DrawImage(srcbitmap, new Rectangle(0, 0, srcbitmap.Width, srcbitmap.Height), 0, 0, srcbitmap.Width, srcbitmap.Height, GraphicsUnit.Pixel, attr);

                g.Dispose();

                return b;

            }

            catch

            {

                return null;

            }

        }



        /// <summary>         

        /// 重新设置图片尺寸     

        /// </summary> 

        /// <param name="srcbitmap">original Bitmap</param>         

        /// <param name="newW">new width</param>         

        /// <param name="newH">new height</param>         

        /// <returns>worked bitmap</returns> 

        public static Boolean ResizeImage(Bitmap srcimg, int newW, int newH, ref Bitmap destimage)

        {

            if (srcimg == null)

            {

                return false;

            }



            destimage = new Bitmap(newW, newH);

            Graphics graph = Graphics.FromImage(destimage);

            Boolean bl = true;

            try

            {

                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graph.DrawImage(srcimg, new Rectangle(0, 0, newW, newH),

                    new Rectangle(0, 0, srcimg.Width, srcimg.Height),

                    GraphicsUnit.Pixel);



                graph.Dispose();

            }

            catch (Exception ex)

            {

                Console.WriteLine("ResizeImage error" + ex.Message);

                bl = false;

            }

            return bl;

        }





        /// <summary>

        /// 去除噪点

        /// </summary>

        /// <param name="noisypointsize">噪点的尺寸</param>

        /// <param name="bitmap">待处理的图片信息</param>

        /// <returns></returns>

        public static Boolean RemoveNoisypoint(Int32 noisypointsize, ref Bitmap bitmap)

        {

            if (bitmap == null || noisypointsize < 1 ||

                noisypointsize * 2 >= bitmap.Width || noisypointsize * 2 >= bitmap.Height)

            {

                return false;

            }



            // 创建过滤器

            BlobsFiltering blobfilter =

                new BlobsFiltering();

            // 设置过滤条件(对象长、宽至少为70)

            blobfilter.CoupledSizeFiltering = true;

            blobfilter.MinWidth = noisypointsize;

            blobfilter.MinHeight = noisypointsize;

            blobfilter.ApplyInPlace(bitmap);



            return true;

        }



        /// <summary>

        /// 把图片里指定区域的内容复制到另一个图片里

        /// </summary>

        /// <param name="srcimg"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <param name="width"></param>

        /// <param name="height"></param>

        /// <param name="destimg"></param>

        /// <returns></returns>

        public static Boolean CutImage(Bitmap srcimg, Int32 x, Int32 y, Int32 width, Int32 height, ref Bitmap destimg)

        {

            if (srcimg == null || x < 0 || y < 0 || width < 1 || height < 1 ||

                x + width > srcimg.Width || y + height > srcimg.Height)

            {

                return false;

            }



            destimg = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics graph = Graphics.FromImage(destimg);

            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graph.DrawImage(srcimg, new Rectangle(0, 0, width, height),

                    new Rectangle(x, y, width, height), GraphicsUnit.Pixel);

            graph.Dispose();

            return true;

        }



        #endregion



        #region 亮度处理







        /// <summary>

        /// 获取指定坐标处的亮度值

        /// </summary>

        /// <param name="bitmap"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns></returns>

        public static Boolean GetPixBrightness(Bitmap bitmap, Int32 x, Int32 y, eGrayMode mode, ref Byte brightness)

        {

            if (bitmap == null)

            {

                return false;

            }



            if (x < 0 || x >= bitmap.Width ||

                y < 0 || y >= bitmap.Height)

            {

                return false;

            }



            Color curColor = bitmap.GetPixel(x, y);

            //利用公式计算灰度值（加权平均法）

            if (mode == eGrayMode.ArithmeticAverage)

            {

                brightness = (Byte)(curColor.R * 0.299f + curColor.G * 0.587f + curColor.B * 0.114f);

            }

            else

            {

                brightness = (Byte)((curColor.R + curColor.G + curColor.B) / 3.0f);

            }

            return true;

        }



        /// <summary>

        /// 获取指定坐标处的亮度值

        /// </summary>

        /// <param name="bitmap"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns></returns>

        public static Boolean GetPixBrightness(Byte[] bribytes, Int32 width, Int32 height,

            Int32 x, Int32 y, ref Byte brightness)

        {

            if (bribytes == null || width < 1 || height < 1 ||

                x < 0 || x >= width ||

                y < 0 || y >= height ||

                bribytes.Length != width * height)

            {

                return false;

            }



            brightness = bribytes[y * width + x];



            return true;

        }



        /// <summary>

        /// 获取指定坐标处的亮度值

        /// </summary>

        /// <param name="bitmap"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns></returns>

        public static Boolean GetPixBrightnessByRate(Byte[] bribytes, Int32 width, Int32 height,

            double xRate, double yRate, ref Byte brightness)

        {

            int x = (int)(width * xRate);

            int y = (int)(height * yRate);



            if (bribytes == null || width < 1 || height < 1 ||

                x < 0 || x >= width ||

                y < 0 || y >= height ||

                bribytes.Length != width * height)

            {

                return false;

            }



            brightness = bribytes[y * width + x];

            return true;

        }



        /// <summary>

        /// 获取指定坐标处的颜色

        /// </summary>

        /// <param name="bitmap"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns></returns>

        public static Boolean GetPixColor(Bitmap bitmap, Int32 x, Int32 y, ref Color curColor)

        {

            if (bitmap == null)

            {

                return false;

            }



            if (x < 0 || x >= bitmap.Width ||

                y < 0 || y >= bitmap.Height)

            {

                return false;

            }



            curColor = bitmap.GetPixel(x, y);

            return true;

        }



        /// <summary>

        /// 获取指定坐标处的颜色

        /// </summary>

        /// <param name="bitmap"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns></returns>

        public static Boolean GetPixColorByRate(Bitmap bitmap, double xRate, double yRate, ref Color curColor)

        {

            if (bitmap == null)

            {

                return false;

            }



            int width = bitmap.Width;

            int height = bitmap.Height;

            int X = (int)(width * xRate);

            int Y = (int)(height * yRate);



            Boolean bl = GetPixColor(bitmap, X, Y, ref curColor);

            return bl;

        }



        /// <summary>

        /// 把颜色转换为亮度值

        /// </summary>

        /// <param name="bitmap"></param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns></returns>

        public static Boolean GetBrightnessByColor(Color curColor, eGrayMode mode, ref Byte brightness)

        {

            if (curColor == null)

            {

                return false;

            }



            //利用公式计算灰度值（加权平均法）

            if (mode == eGrayMode.ArithmeticAverage)

            {

                brightness = (Byte)(curColor.R * 0.299f + curColor.G * 0.587f + curColor.B * 0.114f);

            }

            else

            {

                brightness = (Byte)((curColor.R + curColor.G + curColor.B) / 3.0f);

            }

            return true;

        }



        #endregion



        #region 图片比较



        /// <summary>

        /// 根据2个图片的亮度值,比较图片的差异部分

        /// </summary>

        /// <param name="brightnessDiff"></param>

        /// <param name="compareret"></param>

        /// <returns></returns>

        public static Boolean CompareImageBrightness(Byte brightnessDiff,

            Byte[] brightness1, Byte[] brightness2,

            ref Boolean[] diffPixArray)

        {



            if (brightness1 == null || brightness2 == null ||

                brightness1.Length < 1 || brightness2.Length < 1 ||

                brightness1.Length != brightness2.Length)

            {

                return false;

            }



            Int32 arylen = brightness1.Length;

            diffPixArray = new Boolean[brightness1.Length];

            Byte bri1 = 0;

            Byte bri2 = 0;



            for (Int32 byteidx = 0; byteidx < arylen; ++byteidx)

            {

                bri1 = brightness1[byteidx];

                bri2 = brightness2[byteidx];



                //亮度差超过指定范围

                if (bri1 >= bri2 + brightnessDiff ||

                     bri2 >= bri1 + brightnessDiff)

                {

                    diffPixArray[byteidx] = true;

                }

            }



            return true;

        }



        /// <summary>

        /// 把2个图片的尺寸设置为一样大

        /// </summary>

        /// <param name="image1"></param>

        /// <param name="image2"></param>

        /// <returns></returns>

        public static Boolean ResizeImageToSame(ref Bitmap image1, ref Bitmap image2)

        {

            if (image1 == null || image2 == null ||

                image1.Width == 0 || image1.Height == 0 ||

                image2.Width == 0 || image2.Height == 0)

            {

                return false;

            }



            //如果2个图片尺寸不一样,把大的尺寸压缩小了再比较

            Boolean bl = false;

            Bitmap tmpimg = null;

            if (image1.Width > image2.Width && image1.Height < image2.Height)

            {

                return false;

            }

            if (image1.Width < image2.Width && image1.Height > image2.Height)

            {

                return false;

            }



            //image1 比较大,把image2放大到 与1一样大

            if (image1.Width > image2.Width && image1.Height > image2.Height)

            {

                bl = ResizeImage(image2, image1.Width, image1.Height, ref tmpimg);

                image2 = tmpimg;

            }



            //image 2比较大,把image1放大到 与2一样大

            if (image1.Width < image2.Width && image1.Height < image2.Height)

            {

                bl = ResizeImage(image1, image2.Width, image2.Height, ref tmpimg);

                image1 = tmpimg;

            }



            return true;

        }



        /// <summary>

        /// 根据2个图片的像素颜色值,比较图片的差异部分

        /// </summary>

        /// <param name="compareparam"></param>

        /// <param name="compareret"></param>

        /// <returns></returns>

        public static Boolean CompareImage(ImageCompareParameter compareparam,

            ref ImageCompareResult compareret)

        {

            Bitmap image1 = compareparam.Image1;

            Bitmap image2 = compareparam.Image2;



            Int32 briDiff = compareparam.BrightnessDiff;

            Color diffColor = compareparam.DifferentAreaFillColor;

            Color samecolor = compareparam.SameAreaFillColor;

            //是否需要填充相同或不同部分的像素的颜色

            Boolean filldiffcolor = (diffColor != Color.Transparent);

            Boolean fillsamecolor = (samecolor != Color.Transparent);



            //如果图片尺寸不一样,修改为一样大

            Boolean bl = ResizeImageToSame(ref image1, ref image2);

            if (!bl)

            {

                return false;

            }



            Bitmap imagediff = (Bitmap)image1.Clone();



            //不同区域的左上下右位置

            Int32 areaLeft = imagediff.Width;

            Int32 areaTop = imagediff.Height;

            Int32 areaRight = -1;

            Int32 areaBottom = -1;



            int width = image1.Width;

            int height = image1.Height;



            long allpixcnt = height * width;//所有像素点数量

            long diffpixcnt = 0;//不同像素点数量

            long samepixcnt = 0;//相同像素点数量



            //3张图片的各像素亮度数组

            Int32 briaryidx = 0;

            Byte[] briary1 = new Byte[allpixcnt];

            Byte[] briary2 = new Byte[allpixcnt];

            Byte[] briaryret = new Byte[allpixcnt];



            Byte diffB = diffColor.B;

            Byte diffG = diffColor.G;

            Byte diffR = diffColor.R;

            Byte sameB = samecolor.B;

            Byte sameG = samecolor.G;

            Byte sameR = samecolor.R;



            Byte samebri = 0;

            Byte diffbri = 0;

            GetBrightnessByColor(diffColor, eGrayMode.WeightedAverage, ref samebri);

            GetBrightnessByColor(diffColor, eGrayMode.WeightedAverage, ref diffbri);



            try

            {



                BitmapData data1 = image1.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                BitmapData data2 = image2.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                BitmapData datadiff = imagediff.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                byte bri1 = 0;

                byte bri2 = 0;



                //每个像素是否相同.1相同,0不同

                compareret.PixIsDifferent = new Boolean[width * height];



                //当前像素是否不同

                Boolean curpixIsdiff = false;

                unsafe

                {

                    byte* curpix1 = (byte*)data1.Scan0.ToPointer();

                    byte* curpix2 = (byte*)data2.Scan0.ToPointer();

                    byte* curpixdiff = (byte*)datadiff.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            //利用公式计算灰度值（加权平均法）

                            //按BGR的顺序存储

                            bri1 = (Byte)(curpix1[0] * 0.114f + curpix1[1] * 0.587f + curpix1[2] * 0.299f);

                            bri2 = (Byte)(curpix2[0] * 0.114f + curpix2[1] * 0.587f + curpix2[2] * 0.299f);



                            //以1作为基准,比较1和2之间的差距,如果超过阀值,认为当前像素有差异

                            //否则认为当前像素没有差异

                            curpixIsdiff = false;

                            if (bri1 >= bri2 + briDiff ||

                                bri2 >= bri1 + briDiff)

                            {

                                curpixIsdiff = true;

                            }



                            briary1[briaryidx] = bri1;

                            briary2[briaryidx] = bri2;



                            if (curpixIsdiff) //如果有差异,设置图像1里的当前像素为 不同颜色

                            {

                                if (filldiffcolor)

                                {

                                    curpixdiff[0] = diffB;

                                    curpixdiff[1] = diffG;

                                    curpixdiff[2] = diffR;

                                }

                                ++diffpixcnt;



                                if (x < areaLeft) //记忆最左边的像素位置

                                {

                                    areaLeft = x;

                                }

                                if (x > areaRight) //记忆最右边的像素位置

                                {

                                    areaRight = x;

                                }

                                if (y < areaTop) //记忆最上边的像素位置

                                {

                                    areaTop = y;

                                }

                                if (y > areaBottom) //记忆最下边的像素位置

                                {

                                    areaBottom = y;

                                }



                                //记忆当前像素的比较结果的亮度

                                briaryret[briaryidx] = diffbri;

                            }

                            else //没有差异,设置结果里的当前像素为 相同颜色

                            {

                                if (fillsamecolor)

                                {

                                    curpixdiff[0] = sameB;

                                    curpixdiff[1] = sameG;

                                    curpixdiff[2] = sameR;

                                }

                                ++samepixcnt;



                                //记忆当前像素的比较结果的亮度

                                briaryret[briaryidx] = samebri;

                            }



                            // 比较结果的亮度数组下标

                            ++briaryidx;



                            //像素是否不同的标志

                            compareret.PixIsDifferent[y * width + x] = curpixIsdiff;



                            curpix1 += 3;

                            curpix2 += 3;

                            curpixdiff += 3;

                        }

                        curpix1 += data1.Stride - width * 3;

                        curpix2 += data1.Stride - width * 3;

                        curpixdiff += datadiff.Stride - width * 3;

                    }

                }



                image1.UnlockBits(data1);

                image2.UnlockBits(data2);

                imagediff.UnlockBits(datadiff);



                compareret.RateDifferent = diffpixcnt / (double)allpixcnt;

                compareret.RateSame = samepixcnt / (double)allpixcnt;



                compareret.CompareResultImage = imagediff;

                compareret.BrightnessDiff = briDiff;



                compareret.BrightnessBytesImage1 = briary1;

                compareret.BrightnessBytesImage2 = briary2;

                compareret.BrightnessBytesResult = briaryret;



                //保存区域范围

                //compareret.DiffAreaTop = areaTop;

                //compareret.DiffAreaLeft = areaLeft;

                //compareret.DiffAreaRight = areaRight;

                //compareret.DiffAreaBottom = areaBottom;

                //compareret.CalculateAreaRate();



                bl = true;

            }

            catch (Exception ex)

            {

                Console.WriteLine("CompareImage error:" + ex.Message);

                bl = false;

            }



            return bl;

        }





        /// <summary>

        /// 2张图片亮度值相减,得到新图片以及亮度值

        /// </summary>

        /// <param name="image1"></param>

        /// <param name="image2"></param>

        /// <param name="retimage"></param>

        /// <param name="brightnessary"></param>

        /// <returns></returns>

        public static Boolean SubtractImageBrightness(Bitmap image1, Bitmap image2,

            ref Bitmap imagediff, ref Byte[] brightnessary)

        {

            if (image1 == null || image2 == null)

            {

                return false;

            }



            Boolean bl = ResizeImageToSame(ref image1, ref image2);

            if (!bl)

            {

                return false;

            }



            int width = image1.Width;

            int height = image1.Height;



            long allpixcnt = height * width;//所有像素点数量             



            brightnessary = new Byte[allpixcnt];

            imagediff = new Bitmap(image1);

            Int32 pixidx = 0;//当前像素下标

            byte bri1 = 0;

            byte bri2 = 0;

            BitmapData data1 = null;

            BitmapData data2 = null;

            BitmapData datadiff = null;

            //每行末尾还有这么多的冗余字节

            Int32 rowredundancy = 0;



            try

            {

                data1 = image1.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                data2 = image2.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                datadiff = imagediff.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                rowredundancy = datadiff.Stride - width * 3;//每行末尾还有这么多的冗余字节

                Byte bridiff = 0;

                unsafe

                {

                    byte* curpix1 = (byte*)data1.Scan0.ToPointer();

                    byte* curpix2 = (byte*)data2.Scan0.ToPointer();

                    byte* cmpretpix = (byte*)datadiff.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            bri1 = (byte)((float)(curpix1[0] + curpix1[1] + curpix1[2]) / 3.0f);

                            bri2 = (byte)((float)(curpix2[0] + curpix2[1] + curpix2[2]) / 3.0f);



                            bridiff = (bri1 > bri2) ? (Byte)(bri1 - bri2) : (Byte)(bri2 - bri1); //计算当前像素点的亮度值

                            brightnessary[pixidx] = bridiff;//保存亮度值

                            ++pixidx;



                            cmpretpix[0] = bridiff;//把亮度值设置到结果图像里

                            cmpretpix[1] = bridiff;

                            cmpretpix[2] = bridiff;



                            curpix1 += 3;

                            curpix2 += 3;

                            cmpretpix += 3;

                        }

                        curpix1 += rowredundancy;

                        curpix2 += rowredundancy;

                        cmpretpix += rowredundancy;

                    }

                }



                image1.UnlockBits(data1);

                image2.UnlockBits(data2);

                imagediff.UnlockBits(datadiff);

                bl = true;

            }

            catch (Exception ex)

            {

                Console.WriteLine("CompareImage error:" + ex.Message);

                bl = false;

            }



            return bl;

        }



        /// <summary>

        /// 根据2个图片的亮度值,比较图片的差异部分,并对比较结果的图片执行去噪点处理

        /// </summary>

        /// <param name="image1"></param>

        /// <param name="image2"></param>

        /// <param name="bridiff">亮度容差</param>

        /// <param name="noisypointsize">噪点边长</param>

        /// <param name="imagediff">比较结果的图片</param>

        /// <param name="diffary">每个像素是否相同</param>

        /// <returns></returns>

        public static Boolean CompareImageByBrightness(Bitmap image1, Bitmap image2,

            Int32 briDiff, Int32 noisypointsize,

            ref Bitmap imagediff, ref Boolean[] diffary)

        {

            if (image1 == null || image2 == null)

            {

                return false;

            }



            Boolean bl = ResizeImageToSame(ref image1, ref image2);

            if (!bl)

            {

                return false;

            }



            if (briDiff < 1 || briDiff > 255)

            {

                return false;

            }



            if (noisypointsize < 1 || noisypointsize * 2 > image1.Height)

            {

                return false;

            }



            int width = image1.Width;

            int height = image1.Height;



            long allpixcnt = height * width;//所有像素点数量



            imagediff = new Bitmap(image1);



            //每个像素是否相同.1相同,0不同

            diffary = new Boolean[width * height];



            Int32 pixidx = 0;//当前像素下标

            byte bri1 = 0;

            byte bri2 = 0;

            BitmapData data1 = null;

            BitmapData data2 = null;

            BitmapData datadiff = null;

            //每行末尾还有这么多的冗余字节

            Int32 rowredundancy = 0;



            try

            {

                data1 = image1.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                data2 = image2.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                datadiff = imagediff.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                rowredundancy = datadiff.Stride - width * 3;//每行末尾还有这么多的冗余字节



                unsafe

                {

                    byte* curpix1 = (byte*)data1.Scan0.ToPointer();

                    byte* curpix2 = (byte*)data2.Scan0.ToPointer();

                    byte* cmpretpix = (byte*)datadiff.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            bri1 = (byte)((float)(curpix1[0] + curpix1[1] + curpix1[2]) / 3.0f);

                            bri2 = (byte)((float)(curpix2[0] + curpix2[1] + curpix2[2]) / 3.0f);



                            //比较2个像素亮度值之差,如果有差异,设置图像1里的当前像素为 不同颜色

                            if (bri1 >= bri2 + briDiff ||

                              bri2 >= bri1 + briDiff)

                            {

                                diffary[pixidx] = true;

                                cmpretpix[0] = Const_WhiteBrightness;

                                cmpretpix[1] = Const_WhiteBrightness;

                                cmpretpix[2] = Const_WhiteBrightness;

                            }

                            else

                            {

                                diffary[pixidx] = false;

                                cmpretpix[0] = Const_BlackBrightness;

                                cmpretpix[1] = Const_BlackBrightness;

                                cmpretpix[2] = Const_BlackBrightness;

                            }



                            ++pixidx;

                            curpix1 += 3;

                            curpix2 += 3;

                            cmpretpix += 3;

                        }

                        curpix1 += rowredundancy;

                        curpix2 += rowredundancy;

                        cmpretpix += rowredundancy;

                    }

                }



                image1.UnlockBits(data1);

                image2.UnlockBits(data2);

                imagediff.UnlockBits(datadiff);

                bl = true;

            }

            catch (Exception ex)

            {

                Console.WriteLine("CompareImage error:" + ex.Message);

                bl = false;

            }



            //现在对图像执行去噪点处理

            RemoveNoisypoint(noisypointsize, ref imagediff);



            //获取去除噪点后各像素亮度

            Byte pixbri = 0;//当前像素亮度

            pixidx = 0;

            try

            {

                datadiff = imagediff.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe

                {

                    byte* cmpretpix = (byte*)datadiff.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            pixbri = (byte)((float)(cmpretpix[0] + cmpretpix[1] + cmpretpix[2]) / 3.0f);



                            //去除噪点后,已经变得非黑即白.如果是黑色,表示相同,白色,表示不同

                            diffary[pixidx] = (pixbri > briDiff);

                            ++pixidx;

                            cmpretpix += 3;

                        }

                        cmpretpix += rowredundancy;

                    }

                }



                imagediff.UnlockBits(datadiff);

                bl = true;

            }

            catch (Exception ex)

            {

                Console.WriteLine("CompareImage error:" + ex.Message);

                bl = false;

            }

            return bl;

        }



        /// 根据2个图片的亮度值,比较图片的差异部分

        /// </summary>

        /// <param name="compareparam"></param>

        /// <param name="compareret"></param>

        /// <returns></returns>

        public static Boolean CompareImageByBrightness(ImageCompareParameter compareparam,

            ref ImageCompareResult compareret)

        {

            Bitmap image1 = compareparam.Image1;

            Bitmap image2 = compareparam.Image2;



            Byte[] imagebri1 = compareparam.BrightnessBytesImage1;

            Byte[] imagebri2 = compareparam.BrightnessBytesImage2;



            Int32 briDiff = compareparam.BrightnessDiff;

            Color diffColor = compareparam.DifferentAreaFillColor;

            Color samecolor = compareparam.SameAreaFillColor;

            //是否需要填充相同或不同部分的像素的颜色

            Boolean filldiffcolor = (diffColor != Color.Transparent);

            Boolean fillsamecolor = (samecolor != Color.Transparent);



            Boolean bl = false;



            Bitmap imagediff = new Bitmap(image1);



            //不同区域的左上下右位置

            Int32 areaLeft = imagediff.Width;

            Int32 areaTop = imagediff.Height;

            Int32 areaRight = -1;

            Int32 areaBottom = -1;



            int width = image1.Width;

            int height = image1.Height;



            long allpixcnt = height * width;//所有像素点数量

            long diffpixcnt = 0;//不同像素点数量

            long samepixcnt = 0;//相同像素点数量



            if (imagebri1 == null || imagebri2 == null ||

                imagebri2.Length != imagebri2.Length ||

                imagebri2.Length != allpixcnt)

            {

                return false;

            }



            //3张图片的各像素亮度数组

            Int32 briaryidx = 0;

            Byte[] briaryret = new Byte[allpixcnt];



            Byte diffB = diffColor.B;

            Byte diffG = diffColor.G;

            Byte diffR = diffColor.R;

            Byte sameB = samecolor.B;

            Byte sameG = samecolor.G;

            Byte sameR = samecolor.R;



            Byte samebri = 0;

            Byte diffbri = 0;

            GetBrightnessByColor(diffColor, eGrayMode.WeightedAverage, ref samebri);

            GetBrightnessByColor(diffColor, eGrayMode.WeightedAverage, ref diffbri);



            Int32 currowfirstx = 0;//当前行的首个像素的下标

            try

            {



                BitmapData data1 = image1.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                BitmapData data2 = image2.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                BitmapData datadiff = imagediff.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                byte bri1 = 0;

                byte bri2 = 0;



                //每个像素是否相同.1相同,0不同

                compareret.PixIsDifferent = new Boolean[width * height];



                //当前像素是否不同

                Boolean curpixIsdiff = false;

                unsafe

                {

                    byte* curpix1 = (byte*)data1.Scan0.ToPointer();

                    byte* curpix2 = (byte*)data2.Scan0.ToPointer();

                    byte* cmpretpix = (byte*)datadiff.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        currowfirstx = y * width;

                        for (int x = 0; x < width; x++)

                        {

                            bri1 = imagebri1[currowfirstx + x];

                            bri2 = imagebri2[currowfirstx + x];



                            //以1作为基准,比较1和2之间的差距,如果超过阀值,认为当前像素有差异

                            //否则认为当前像素没有差异

                            curpixIsdiff = false;

                            if (bri1 >= bri2 + briDiff ||

                                bri2 >= bri1 + briDiff)

                            {

                                curpixIsdiff = true;

                            }



                            if (curpixIsdiff) //如果有差异,设置图像1里的当前像素为 不同颜色

                            {

                                if (filldiffcolor)

                                {

                                    cmpretpix[0] = diffB;

                                    cmpretpix[1] = diffG;

                                    cmpretpix[2] = diffR;

                                }

                                ++diffpixcnt;



                                if (x < areaLeft) //记忆最左边的像素位置

                                {

                                    areaLeft = x;

                                }

                                if (x > areaRight) //记忆最右边的像素位置

                                {

                                    areaRight = x;

                                }

                                if (y < areaTop) //记忆最上边的像素位置

                                {

                                    areaTop = y;

                                }

                                if (y > areaBottom) //记忆最下边的像素位置

                                {

                                    areaBottom = y;

                                }



                                //记忆当前像素的比较结果的亮度

                                briaryret[briaryidx] = diffbri;

                            }

                            else //没有差异,设置结果里的当前像素为 相同颜色

                            {

                                if (fillsamecolor)

                                {

                                    cmpretpix[0] = sameB;

                                    cmpretpix[1] = sameG;

                                    cmpretpix[2] = sameR;

                                }

                                ++samepixcnt;



                                //记忆当前像素的比较结果的亮度

                                briaryret[briaryidx] = samebri;

                            }



                            // 比较结果的亮度数组下标

                            ++briaryidx;



                            //像素是否不同的标志

                            compareret.PixIsDifferent[currowfirstx + x] = curpixIsdiff;



                            curpix1 += 3;

                            curpix2 += 3;

                            cmpretpix += 3;

                        }

                        curpix1 += data1.Stride - width * 3;

                        curpix2 += data1.Stride - width * 3;

                        cmpretpix += datadiff.Stride - width * 3;

                    }

                }



                image1.UnlockBits(data1);

                image2.UnlockBits(data2);

                imagediff.UnlockBits(datadiff);



                compareret.RateDifferent = diffpixcnt / (double)allpixcnt;

                compareret.RateSame = samepixcnt / (double)allpixcnt;



                compareret.CompareResultImage = imagediff;

                compareret.BrightnessDiff = briDiff;

                compareret.BrightnessBytesResult = briaryret;



                bl = true;

            }

            catch (Exception ex)

            {

                Console.WriteLine("CompareImage error:" + ex.Message);

                bl = false;

            }



            return bl;

        }



        /// <summary>

        /// 获取一个区域的实际坐标

        /// </summary>

        /// <param name="area"></param>

        /// <param name="width"></param>

        /// <param name="height"></param>

        /// <param name="x1"></param>

        /// <param name="y1"></param>

        /// <param name="x2"></param>

        /// <param name="y2"></param>

        public static void GetAreaPositionInImage(ImageAreaInfo area,

            Int32 width, Int32 height,

            ref Int32 x1, ref Int32 y1, ref Int32 x2, ref Int32 y2)

        {

            if (area.PositionType == ePositionType.ByPix)

            {

                x1 = (Int32)area.X1;

                y1 = (Int32)area.Y1;

                x2 = (Int32)area.X2;

                y2 = (Int32)area.Y2;

            }

            else

            {

                x1 = (Int32)(area.X1 * (double)width);

                y1 = (Int32)(area.Y1 * (double)height);

                x2 = (Int32)(area.X2 * (double)width);

                y2 = (Int32)(area.Y2 * (double)height);

            }

        }



        /// <summary>

        /// 检查指定区域的图像是否与方案里的指定值一样(都是相同或者不同)

        /// </summary>

        /// <param name="briDiffary">每个元素对应2张图片的每个像素亮度相同还是不同.true不同,false相同</param>

        /// <param name="area"></param>

        /// <returns></returns>

        public static Boolean ValidateImageArea(Boolean[] briDiffary, ImageAreaInfo area, Int32 width, Int32 height)

        {

            if (briDiffary == null || briDiffary.Length < 4 || area == null ||

                width < 1 || height < 1 || width * height != briDiffary.Length)

            {

                return false;

            }



            Int32 x1 = 0;

            Int32 x2 = 0;

            Int32 y1 = 0;

            Int32 y2 = 0;



            //获取该区域在图像里的实际坐标范围

            GetAreaPositionInImage(area, width, height,

                ref x1, ref y1, ref x2, ref y2);



            //获取该区域里的像素匹配类型

            eAreaDifferentType difftype = eAreaDifferentType.Partial;

            Boolean bl = GetImageAreaDifferentType(briDiffary, width, height,

                x1, y1, x2, y2, ref difftype);

            if (!bl)

            {

                return false;

            }



            //如果是期待所有像素都是相同,要求必须每个像素都相同.任何一个不同,就认为失败

            if (area.ExpectDispMode == eDrawType.ExpectHide &&

                difftype != eAreaDifferentType.AllSame)

            {

                return false;

            }



            //如果是期待像素不同,只要有1个像素不同就可以.所有像素都相同,认为失败

            if (area.ExpectDispMode == eDrawType.ExpectShow &&

                difftype == eAreaDifferentType.AllSame)

            {

                return false;

            }

            return true;

        }



        /// <summary>

        /// 检查指定区域的图像是否与方案里的指定值一样(都是相同或者不同)

        /// </summary>

        /// <param name="pixDiffary"></param>

        /// <param name="area"></param>

        /// <returns></returns>

        public static Boolean ValidateImageArea(Byte[] briDiffary, ImageAreaInfo area, Int32 width, Int32 height)

        {



            Boolean[] blary = new Boolean[briDiffary.Length];

            for (Int32 idx = 0; idx < briDiffary.Length; ++idx)

            {

                blary[idx] = (briDiffary[idx] > 0);

            }



            Boolean bl = ValidateImageArea(blary, area, width, height);

            return bl;

        }



        /// <summary>

        /// 检查图片的比较结果里，某个区域是否与期待的一致

        /// </summary>

        /// <param name="compareret"></param>

        /// <param name="area"></param>

        /// <returns>true-与期待一致;false-不一致</returns>

        public static Boolean ValidateImageArea(ImageCompareResult compareret, ImageAreaInfo area)

        {

            Boolean[] pixDiffary = compareret.PixIsDifferent;



            Bitmap tmp = new Bitmap(compareret.CompareResultImage);

            Int32 width = tmp.Width;

            Int32 height = tmp.Height;

            Boolean bl = ValidateImageArea(compareret.PixIsDifferent, area, width, height);



            return bl;

        }



        /// <summary>

        /// 获取1个 比较结果里,指定的区域范围,是全都相同,还是不同

        /// 只有所有像素都是相同,才认为是整个区域相同

        /// 如果有1个像素不同,则认为整个区域不同

        /// </summary>

        /// <param name="pixDiffary"></param>

        /// <param name="width"></param>

        /// <param name="height"></param>

        /// <param name="startX"></param>

        /// <param name="startY"></param>

        /// <param name="endX"></param>

        /// <param name="endY"></param>

        /// <returns> </returns>

        public static Boolean GetImageAreaDifferentType(Boolean[] pixDiffary, Int32 width, Int32 height,

            Int32 x1, Int32 y1, Int32 x2, Int32 y2, ref eAreaDifferentType difftype)

        {

            Int32 areawidth = x2 - x1;

            Int32 areaheight = y2 - y1;



            if (pixDiffary == null || width < 1 || height < 1 ||

                areawidth < 1 || areaheight < 1 ||

                width < areawidth || height < areaheight ||

                pixDiffary.Length < width * height)

            {

                return false;

            }



            Boolean allissame = false; //假设所有像素相同

            Boolean allisdiff = false; //假设所有像素不同



            Int32 currowFirstPix = 0;

            for (Int32 y = y1; y <= y2; ++y)

            {

                currowFirstPix = y * width;

                for (Int32 x = x1; x <= x2; ++x)

                {

                    if (pixDiffary[currowFirstPix + x]) //当前像素点不同

                    {

                        allisdiff = true;

                    }

                    else//当前像素相同

                    {

                        allissame = true;

                    }



                    //如果已经有部分相同部分不同,退出

                    if (allisdiff && allissame)

                    {

                        difftype = eAreaDifferentType.Partial;

                        return true;

                    }

                }

            }



            //现在，所有像素都相同，或都不同

            if (allisdiff)

            {

                difftype = eAreaDifferentType.AllDifferent;

            }

            else

            {

                difftype = eAreaDifferentType.AllSame;

            }

            return true;

        }



        /// <summary>

        /// 根据亮度容差,把图片转换为非黑即白的图片

        /// </summary>

        /// <param name="briimg"></param>

        /// <param name="brigate"></param>

        /// <returns></returns>

        public static Boolean GetBlackWhiteImage(Bitmap briimg, Byte[] briDiffary, Int32 brigate, ref Bitmap blackwhiteimage)

        {

            if (briimg == null)

            {

                return false;

            }



            int width = briimg.Width;

            int height = briimg.Height;



            long allpixcnt = height * width;//所有像素点数量            



            if (briDiffary == null || briDiffary.Length != allpixcnt)

            {

                return false;

            }



            blackwhiteimage = new Bitmap(briimg);

            Int32 pixidx = 0;//当前像素下标

            BitmapData datasrc = null;

            BitmapData dataret = null;



            //每行末尾还有这么多的冗余字节

            Int32 rowredundancy = 0;



            Byte curpixBri = 0;//当前的亮度

            try

            {

                datasrc = briimg.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                dataret = blackwhiteimage.LockBits(new Rectangle(0, 0, width, height),

                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);



                rowredundancy = datasrc.Stride - width * 3;//每行末尾还有这么多的冗余字节

                unsafe

                {

                    byte* pixret = (byte*)dataret.Scan0.ToPointer();



                    for (int y = 0; y < height; y++)

                    {

                        for (int x = 0; x < width; x++)

                        {

                            //亮度差值大于门限的,认为是不同部分,用白色填充

                            curpixBri = (briDiffary[pixidx] > brigate) ? Const_BrightnessWhite : Const_BrightnessBlack;

                            pixret[0] = curpixBri;//把亮度值设置到结果图像里

                            pixret[1] = curpixBri;

                            pixret[2] = curpixBri;

                            ++pixidx;

                            pixret += 3;

                        }

                        pixret += rowredundancy;

                    }

                }



                briimg.UnlockBits(datasrc);

                blackwhiteimage.UnlockBits(dataret);

            }

            catch (Exception ex)

            {

                Console.WriteLine("GetBlackWhiteImage error:" + ex.Message);

                return false;

            }



            return true;

        }

        #endregion



        #region 内部实现





        /// <summary>

        /// 比较2个数值之间的差是否大于指定值

        /// </summary>

        /// <param name="val1"></param>

        /// <param name="val2"></param>

        /// <param name="diff"></param>

        /// <returns>超过指定值返回true;否则返回false</returns>

        private static Boolean CheckDiffOver(Int32 val1, Int32 val2, Int32 diff)

        {

            if (diff < 1)

            {

                return false;

            }

            if (val1 > val2 && val1 > val2 + diff)

            {

                return true;

            }

            if (val2 > val1 && val2 > val1 + diff)

            {

                return true;

            }

            return false;

        }

        #endregion

    }

}