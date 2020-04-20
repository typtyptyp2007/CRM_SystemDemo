using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _28_CRM_System
{
    public partial class AddComplaint : Form
    {
        public AddComplaint()
        {
            InitializeComponent();
        }

        Database db = new Database();

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelectClient_Click(object sender, EventArgs e)
        {
            var sc = new SelectClient();
            var result = sc.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbClient.Text = sc.Person;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbTitle.Text.Trim()))
            {
                MessageBox.Show("请输入投诉的主题！");
                tbTitle.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbClient.Text.Trim()))
            {
                MessageBox.Show("请选择投诉客户！");
                tbClient.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbContent.Text.Trim()))
            {
                MessageBox.Show("请输入投诉内容！");
                tbContent.Focus();
                return;
            }

            db.RunSql($"insert into inform (title,client,types,mode,informTime,informObject," +
                      $"informContent,addtime) values ('{tbTitle.Text.Trim()}','{tbClient.Text.Trim()}'," +
                      $"'{cbbComplaintType.Text}','{cbbComplaintMode.Text}','{dtpComplaintTime.Value}'," +
                      $"'{cbbObject.Text}','{tbContent.Text.Trim()}','{DateTime.UtcNow}')");
            var result = MessageBox.Show("添加投诉成功！");
            if (result == DialogResult.OK)
            {
                Close();
            }
        }
    }
}
