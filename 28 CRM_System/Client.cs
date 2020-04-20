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
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        Common com = new Common();
        Database db = new Database();

        //显示客户的方法
        private void ShowCustomer()
        {
            com.BindDGV(dataGridView1,"select * from customer order by id desc");
        }

        private void Client_Load(object sender, EventArgs e)
        {
            ShowCustomer();
        }

        //添加客户
        private void ShowForm(int t)
        {
            var ac = new AddClient();
            ac.Id = t;
            var result = ac.ShowDialog();
            if (result == DialogResult.OK)
            {
                ShowCustomer();
            }
        }

        //修改客户
        private void ModClient()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    var value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        ShowForm(int.Parse(value));
                    }
                    else
                    {
                        MessageBox.Show("所选择行不存在数据");
                    }
                }
                else
                {
                    MessageBox.Show("请选择一行要修改的数据！");
                }
            }
            else
            {
                MessageBox.Show("目前没有任何数据");
            }
        }

        private void DelClient()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    var selectId = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    if (!string.IsNullOrEmpty(selectId))
                    {
                        db.RunSql($"delete customer where id = {selectId}");
                    }
                }
                ShowCustomer();
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tslblAdd")
            {
                var ac = new AddClient();
                var result = ac.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ShowCustomer();
                }
            }
            if (e.ClickedItem.Name == "tslblMod")
            {
                ModClient();
            }
            if (e.ClickedItem.Name == "tslblDel")
            {
                var status = MessageBox.Show("你确认删除所选择的行吗？","确认对话框",MessageBoxButtons.YesNo);
                if (status == DialogResult.Yes)
                {
                    DelClient();
                }
            }
        }
    }
}
