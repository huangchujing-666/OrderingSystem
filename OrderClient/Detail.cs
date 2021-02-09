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
    public partial class Detail : Form
    {
        public Detail()
        {
            InitializeComponent();
        }

        public DataTable OrderData;

        private void Detail_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(72, 138, 255);

            if (OrderData != null)
            {
                //set 
                foreach (DataRow myRow in OrderData.Rows)
                {
                    var orderNo = myRow[0].ToString();
                    this.lblOrderNo.Text = orderNo;
                    this.lblPayTime.Text = myRow["PayTime"].ToString();
                    this.lblSeatNo.Text = myRow["SeatNo"].ToString();
                    this.lblTotal.Text = "总金额：" + myRow["Total"].ToString() + " 元";

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
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
