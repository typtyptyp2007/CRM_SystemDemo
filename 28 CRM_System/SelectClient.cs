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
    public partial class SelectClient : Form
    {
        public SelectClient()
        {
            InitializeComponent();
        }

        public string Person { get; set; }
        Common com = new Common();

        private void SelectClient_Load(object sender, EventArgs e)
        {
            com.BindListBox(lbAllClient,"select id,username from customer order by id asc");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Person = "";
            for (int i = 0; i < lbSelectClient.Items.Count; i++)
            {
                Person += lbSelectClient.Items[i].ToString() + ",";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lbAllClient.SelectedIndex != -1)
            {
                if (lbSelectClient.Items.Count < 2)
                {
                    lbSelectClient.Items.Add(lbAllClient.Text);
                }
                else
                {
                    MessageBox.Show("每次最多添加两位用户");
                }
            }
            else
            {
                MessageBox.Show("请选择要添加的客户！");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbSelectClient.SelectedIndex != -1)
            {
                lbSelectClient.Items.RemoveAt(lbSelectClient.SelectedIndex);
            }
            else
            {
                MessageBox.Show("请选择移除的客户！");
            }
        }
    }
}
