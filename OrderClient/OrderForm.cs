using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderClient
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        public int businessId = 0;
        private List<string> orderlist = new List<string>();

        private void OrderForm_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;

            this.BackColor = Color.FromArgb(72, 138, 255);
            //初始化数据
            SearchData(this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            timer1.Interval = 5000;//设置时钟周期为1秒（1000毫秒）  
            timer1.Tick += new EventHandler(GetNewData);
            // Enable timer.  
            timer1.Enabled = true;


            //设置表格自适应宽度 
            this.dataGridView1.Columns[0].FillWeight = 20;
            this.dataGridView1.Columns[1].FillWeight = 10;
            this.dataGridView1.Columns[2].FillWeight = 10;
            this.dataGridView1.Columns[3].FillWeight = 10;
            this.dataGridView1.Columns[4].FillWeight = 10;
            this.dataGridView1.Columns[5].FillWeight = 10;
            this.dataGridView1.Columns[6].FillWeight = 10;
            this.dataGridView1.Columns[7].FillWeight = 10; 


        }

        /// <summary>
        /// 搜索查询
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void SearchData(DateTime begin, DateTime end)
        {
            var beginDate = begin.Date.ToString();
            var endDate = end.Date.AddDays(1).AddSeconds(-1).ToString();

            #region sql
            string sql = @"SELECT
a.OrderNo,
--a.OrignAmount,
a.RealAmount,
a.Remark,
o.Name as OrderStatusName,
--a.OrderTime,
a.PayTime,
a.SeatNo,
u.NickName,
u.PhoneNo

FROM
[Order] AS a
--LEFT JOIN OrderDetail AS b ON a.OrderNo = b.OrderNo
LEFT JOIN [User] AS u ON a.UserId = u.UserId
LEFT JOIN [OrderStatus] AS o ON a.OrderStatusId = o.OrderStatusId
WHERE a.OrderStatusId =2 and a.BusinessInfoId=" + businessId + " and a.PayTime >='" + beginDate + "' and a.PayTime <='" + endDate + "' ";

            string totalSql = @"SELECT
sum(a.RealAmount)
FROM
[Order] AS a 
LEFT JOIN [User] AS u ON a.UserId = u.UserId
LEFT JOIN [OrderStatus] AS o ON a.OrderStatusId = o.OrderStatusId
WHERE a.OrderStatusId =2 and a.BusinessInfoId=" + businessId + " and a.PayTime >='" + beginDate + "' and a.PayTime <='" + endDate + "' ";

            #endregion

            var data = SqlDbHelper.ExecuteDataTable(sql);
            var totalData = SqlDbHelper.ExecuteDataTable(totalSql);

            this.dataGridView1.DataSource = data;
            if (totalData != null && totalData.Rows.Count > 0)
            {
                var _tempValue = Convert.ToString(totalData.Rows[0][0]);
                this.lblTotalMoney.Text = string.IsNullOrEmpty(_tempValue) ? "总金额：0.00 元" : "总金额：" + _tempValue + " 元";
            }
            else
            {

            }


            //set 
            foreach (DataRow myRow in data.Rows)
            {
                orderlist.Add(myRow[0].ToString());
            }
        }


        public void GetNewData(object Sender, EventArgs e)
        {
            //MessageBox.Show("1");

            #region sql

            string payTime = DateTime.Now.Date.ToString();

            //查询新付款订单
            string sql = @"SELECT a.OrderNo FROM [Order] AS a WHERE a.OrderStatusId = 2 and a.BusinessInfoId=" + businessId + " and a.PayTime >'" + payTime + "'";

            //查询订单详情
            string detail_sql = @"SELECT
                                        a.OrderNo,
                                        a.PayTime,bi.Name as BName,a.Remark,
                                        a.RealAmount as Total,
                                        a.SeatNo,
                                        d.Name as ProductName,
                                        b.RealAmount,
                                        b.[Count],b.DishesSpecDetailIds
                                        FROM
                                        dbo.[Order] AS a
                                        LEFT JOIN dbo.OrderDetail AS b ON a.OrderNo = b.OrderNo
                                        LEFT JOIN dbo.Dishes AS d ON b.DishesId = d.DishesId
                                        LEFT JOIN dbo.BusinessInfo AS bi ON bi.BusinessInfoId = a.BusinessInfoId
                                         WHERE a.OrderNo='{0}'";

            #endregion

            var data = SqlDbHelper.ExecuteDataTable(sql);

            //set 
            foreach (DataRow myRow in data.Rows)
            {
                var orderNo = myRow[0].ToString();
                //判断是否是新订单
                if (!orderlist.Contains(orderNo))
                {
                    var items = SqlDbHelper.ExecuteDataTable(string.Format(detail_sql, orderNo));

                    MessageForm msf = new MessageForm() { OrderData = items };
                    msf.Show();

                    orderlist.Add(orderNo);
                }
            }

            if (data.Rows.Count > 0)
            {
                //初始化数据
                SearchData(this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            }
        }



        private void OrderForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData(this.dateTimePicker1.Value, this.dateTimePicker2.Value);
        }

        private void OrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

   
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex==-1)
            {
                return;
            }

            var orderNo = this.dataGridView1["OrderNo", e.RowIndex].Value.ToString();

            //MessageBox.Show(orderNo);

            if (string.IsNullOrWhiteSpace(orderNo))
            {
                return;
            }

            #region
            //查询订单详情
            string detail_sql = @"SELECT
                                        a.OrderNo,
                                        a.PayTime,
                                        a.RealAmount as Total,
                                        a.SeatNo,
                                        d.Name as ProductName,
                                        b.RealAmount,
                                        b.[Count],b.DishesSpecDetailIds
                                        FROM
                                        dbo.[Order] AS a
                                        LEFT JOIN dbo.OrderDetail AS b ON a.OrderNo = b.OrderNo
                                        LEFT JOIN dbo.Dishes AS d ON b.DishesId = d.DishesId
                                         WHERE  a.OrderNo='{0}'";

            #endregion


            var items = SqlDbHelper.ExecuteDataTable(string.Format(detail_sql, orderNo));

            Detail msf = new Detail() { OrderData = items };

            msf.Show();
        }




        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void OrderForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void OrderForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void OrderForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }



        private void pictureBox5_Click(object sender, EventArgs e)
        { 
            this.WindowState = FormWindowState.Minimized; 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized; 
            }
            else
            { 
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OrderForm_Resize(object sender, EventArgs e)
        {

            //设置表格自适应宽度 
            this.dataGridView1.Columns[0].FillWeight = 20;
            this.dataGridView1.Columns[1].FillWeight = 10;
            this.dataGridView1.Columns[2].FillWeight = 10;
            this.dataGridView1.Columns[3].FillWeight = 10;
            this.dataGridView1.Columns[4].FillWeight = 10;
            this.dataGridView1.Columns[5].FillWeight = 10;
            this.dataGridView1.Columns[6].FillWeight = 10;
            this.dataGridView1.Columns[7].FillWeight = 10;
        }
    }
}
