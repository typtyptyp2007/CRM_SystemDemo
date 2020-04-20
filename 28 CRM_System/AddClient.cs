using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _28_CRM_System
{
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }

        public int Id { get; set; }

        Common com = new Common();
        Database db = new Database();

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddClient_Load(object sender, EventArgs e)
        {
            try
            {
                //绑定民族
                com.BindComboBox(cbbNation, "select id,nation from nation order by id asc");
                cbbMarriage.SelectedIndex = 0;
                cbbGender.SelectedIndex = 0;
                cbbSource.SelectedIndex = 0;
                cbbLevel.SelectedIndex = 0;
                cbbCulture.SelectedIndex = 0;
                cbbCon.SelectedIndex = 0;
                cbbEarning.SelectedIndex = 0;
                com.BindComboBox(cbbProvince, "select * from province order by id asc");

                if (Id != 0)
                {
                    Text = "客户资料修改";
                    btnAdd.Text = "修改";
                    ShowClient(Id);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //显示客户资料
        private void ShowClient(int t)
        {
            SqlDataReader dr;
            db.RunSql($"select * from customer where id = {t}", out dr);
            if (dr.Read())
            {
                tbName.Text = dr["username"].ToString();
                com.CbbIndexByText(cbbNation, dr["nation"].ToString());
                dtpBirthday.Value = DateTime.Parse(dr["birthday"].ToString());
                com.CbbIndexByText(cbbGender, dr["gender"].ToString());
                tbIdCard.Text = dr["IDcard"].ToString();
                tbHobby.Text = dr["hobby"].ToString();
                com.CbbIndexByText(cbbMarriage, dr["marriage"].ToString());
                com.CbbIndexByText(cbbSource, dr["source"].ToString());
                com.CbbIndexByText(cbbLevel, dr["levels"].ToString());
                com.CbbIndexByText(cbbEarning, dr["earning"].ToString());
                com.CbbIndexByText(cbbCon, dr["concernment"].ToString());
                tbMobile.Text = dr["mobile"].ToString();
                tbTel.Text = dr["tel"].ToString();
                tbQq.Text = dr["QQ"].ToString();
                tbEmail.Text = dr["email"].ToString();
                com.CbbIndexByText(cbbCulture, dr["culture"].ToString());
                com.CbbIndexByText(cbbProvince, dr["province"].ToString());
                com.CbbIndexByText(cbbCity, dr["city"].ToString());
                tbAddress.Text = dr["address"].ToString();
            }
            else
            {
                MessageBox.Show("你所要修改的项不存在，系统将关闭此窗体！");
                Close();
            }
            dr.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text.Trim()))
            {
                MessageBox.Show("请输入客户名称");
                tbName.Focus();
                btnAdd.DialogResult = DialogResult.None;
                return;
            }

            if (cbbGender.SelectedIndex < 0)
            {
                MessageBox.Show("请选择性别！");
                cbbGender.Focus();
                btnAdd.DialogResult = DialogResult.None;
                return;
            }

            if (cbbLevel.SelectedIndex < 0)
            {
                MessageBox.Show("请选择重要程度");
                cbbLevel.Focus();
                btnAdd.DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrEmpty(tbMobile.Text.Trim()) && string.IsNullOrEmpty(tbTel.Text.Trim()))
            {
                MessageBox.Show("请将电话号码和手机至少输入一个！");
                tbMobile.Focus();
                btnAdd.DialogResult = DialogResult.None;
                return;
            }

            string sql;
            var rStatus = "添加";
            if (Id == 0)
            {
                sql = $"insert into customer (username,nation,birthday,gender,IDcard,hobby," +
                          $"marriage,source,levels,earning,concernment,mobile,tel,QQ,culture," +
                          $"email,province,city,address,addTime) values ('{tbName.Text.Trim()}'," +
                          $"'{cbbNation.Text}','{dtpBirthday.Value}','{cbbGender.Text}'," +
                          $"'{tbIdCard.Text.Trim()}','{tbHobby.Text.Trim()}','{cbbMarriage.Text}'," +
                          $"'{cbbSource.Text}','{cbbLevel.SelectedItem}','{cbbEarning.Text}'," +
                          $"'{cbbCon.Text}','{tbMobile.Text.Trim()}','{tbTel.Text.Trim()}'," +
                          $"'{tbQq.Text.Trim()}','{cbbCulture.Text}','{tbEmail.Text.Trim()}'," +
                          $"'{cbbProvince.Text}','{cbbCity.Text}','{tbAddress.Text.Trim()}'," +
                          $"'{DateTime.UtcNow}')";
            }
            else
            {
                rStatus = "修改";
                sql = $"update customer set username = '{tbName.Text.Trim()}',nation = '{cbbNation.Text}'," +
                      $"birthday = '{dtpBirthday.Value}',gender = '{cbbGender.Text}',IDcard = '{tbIdCard.Text.Trim()}'," +
                      $"hobby = '{tbHobby.Text.Trim()}',marriage = '{cbbMarriage.Text}',source = '{cbbSource.Text}'," +
                      $"levels = '{cbbLevel.SelectedItem}',earning = '{cbbEarning.Text}',concernment = '{cbbCon.Text}'," +
                      $"mobile = '{tbMobile.Text.Trim()}',tel = '{tbTel.Text.Trim()}',QQ = '{tbQq.Text.Trim()}'," +
                      $"culture = '{cbbCulture.Text}',email = '{tbEmail.Text.Trim()}',province = '{cbbProvince.Text}'," +
                      $"city = '{cbbCity.Text}',address = '{tbAddress.Text.Trim()}' where id = {Id}";
            }

            db.RunSql(sql);
            var result = MessageBox.Show($"{rStatus}成功！");
            if (result == DialogResult.OK)
            {
                Close();
            }

        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbCity.Text = "";

            SqlDataReader dr;
            db.RunSql($"select * from province where province = '{cbbProvince.Text}'", out dr);
            if (dr.Read())
            {
                com.BindComboBox(cbbCity, $"select * from city where citycode like '{dr["provinceid"]}%' order by id asc");
            }
            //dr.Close();
            if (cbbCity.SelectedIndex >= 0)
            {
                cbbCity.SelectedIndex = 0;
            }
        }
    }
}
