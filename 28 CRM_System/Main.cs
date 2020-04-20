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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public string User { get; set; }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("你确认退出系统吗？", "退出对话框",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Dispose();//窗体释放
                Application.Exit();//退出程序
            }
            else
            {
                e.Cancel = true;//取消事件
            }
        }

        public void CloseSystem()
        {
            var result = MessageBox.Show("你确认退出系统吗？", "退出对话框", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Dispose();//窗体释放
                Application.Exit();//退出程序
            }
        }

        private void lklblExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseSystem();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseSystem();
        }

        private void lklblAddClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ac = new AddClient();
            ac.Show();
        }

        private void lklblAddService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var addService = new AddService();
            addService.Show();
        }

        private void lklblAddComplaint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ac = new AddComplaint();
            ac.Show();
        }

        private void 添加客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ac = new AddClient();
            ac.Show();
        }

        private void 管理客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cli = new Client();
            cli.Show();
        }

        private void 添加服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addService = new AddService();
            addService.Show();
        }

        private void 添加投诉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ac = new AddComplaint();
            ac.Show();
        }

        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cp = new ChangePwd {User = User};
            cp.Show();
        }

        private void 关于软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ab = new About();
            ab.Show();
        }
    }
}
