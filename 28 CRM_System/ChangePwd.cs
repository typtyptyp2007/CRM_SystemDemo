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
    public partial class ChangePwd : Form
    {
        public ChangePwd()
        {
            InitializeComponent();
        }

        Common com = new Common();
        Database db = new Database();
        public string User { get; set; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOldPwd.Text.Trim()))
            {
                MessageBox.Show("请输入原密码");
                tbOldPwd.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbNewPwd.Text.Trim()))
            {
                MessageBox.Show("请输入新密码");
                tbNewPwd.Focus();
                return;
            }

            if (String.CompareOrdinal(tbNewPwd.Text.Trim(), tbConfirmPwd.Text.Trim()) != 0)
            {
                MessageBox.Show("两次输入的密码不一致，请重新输入！");
                tbNewPwd.Focus();
                return;
            }

            var sql =
                $"select count(1) from manager where username = '{User}' and pwd = '{com.Md5(tbOldPwd.Text.Trim())}'";
            var t = db.RunSqlInt(sql);
            if (t > 0)
            {
                var updateSql = $"update manager set pwd = '{com.Md5(tbNewPwd.Text.Trim())}' where username = '{User}'";
                db.RunSql(updateSql);
                var result = MessageBox.Show("密码修改成功");
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("旧密码输入错误");
                tbOldPwd.Focus();
            }
        }
    }
}
