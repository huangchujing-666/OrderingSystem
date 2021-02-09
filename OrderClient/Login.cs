using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderClient
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var account = this.txtAccount.Text.Trim();
            var pwd = this.txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("请输入正确的账号和密码！");
                return;
            }

            string sql = string.Format("select BusinessInfoId from SysAccount where Account = '{0}' and PassWord='{1}'", account, GetMD5_32(pwd));

            var result = SqlDbHelper.ExecuteScalar(sql);

            if (result > 0)
            {
                this.Hide(); 
                OrderForm of = new OrderForm() { businessId = result };
                of.Show();
            }
            else
            {
                MessageBox.Show("请输入正确的账号和密码！");
            }
        }

        /// <summary>
        /// 处理回车键登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMD5_32(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.btnLogin.BackColor = Color.FromArgb(72, 138, 255);
        }

        #region 窗体圆角的实现

       

        private void Login_Resize(object sender, EventArgs e)
        {
             
        }

        #endregion

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
