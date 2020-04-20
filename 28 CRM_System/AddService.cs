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
    public partial class AddService : Form
    {
        public AddService()
        {
            InitializeComponent();
        }

        Database db = new Database();

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var sc = new SelectClient();
            var result = sc.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbServiceClient.Text = sc.Person;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbServiceTitle.Text.Trim()))
            {
                MessageBox.Show("请输入投诉的标题！");
                tbServiceTitle.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbServiceClient.Text.Trim()))
            {
                MessageBox.Show("请点击右侧按钮进行选择客户！");
                btnSelect.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbServiceContent.Text.Trim()))
            {
                MessageBox.Show("请输入服务内容！");
                tbServiceContent.Focus();
                return;
            }

            db.RunSql($"insert into services (title,userName,serviceType,serviceMode," +
                      $"startTime,endTime,serviceContent,feedback,remark,result,addTime) values (" +
                      $"'{tbServiceTitle.Text.Trim()}','{tbServiceClient.Text.Trim()}'," +
                      $"'{cbbServiceType.Text}','{cbbServiceMode.Text}','{dtpStartTime.Value}'," +
                      $"'{dtpEndTime.Value}','{tbServiceContent.Text.Trim()}','{cbbServiceFeedback.Text}'," +
                      $"'{tbServiceNote.Text.Trim()}','{cbbResult.Text}','{DateTime.UtcNow}')");
            var result = MessageBox.Show("添加成功！");
            if (result == DialogResult.OK)
            {
                Close();
            }
        }
    }
}
