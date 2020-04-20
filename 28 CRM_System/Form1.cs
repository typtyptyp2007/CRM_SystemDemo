using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _28_CRM_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //判断是否输入用户名
            if (string.IsNullOrEmpty(tbUser.Text.Trim()))
            {
                MessageBox.Show("请输入用户名！");
                tbUser.Focus();
                return;
            }
            //判断是否已输入密码
            if (string.IsNullOrEmpty(tbPwd.Text.Trim()))
            {
                MessageBox.Show("请输入密码！");
                tbPwd.Focus();
                return;
            }
            //创建common对象
            var com = new Common();
            //创建database对象
            var db = new Database();
            //统计所输入的用户名和密码在对应数据表中的个数的查询语句
            var sql = $"select count(1) from manager where username = '{tbUser.Text.Trim()}' and pwd = '{com.Md5(tbPwd.Text.Trim())}'";
            //将对应表中数据条数放在变量t中
            var t = db.RunSqlInt(sql);
            if (t > 0)
            {
                this.Hide();
                var fmain = new Main();
                fmain.User = tbUser.Text.Trim();
                fmain.Show();
            }
            else
            {
                MessageBox.Show("用户名或者密码错误！");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbUser.TabIndex = 0;
        }

    }
}
