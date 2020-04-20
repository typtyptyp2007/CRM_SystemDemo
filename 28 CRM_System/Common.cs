using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _28_CRM_System
{
    class Common
    {
        Database db = new Database();

        public string Md5(string pwd)
        {
            var returnPwd = "";
            var md5 = MD5.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(pwd);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            for (int i = 0; i < bytes.Length; i++)
            {
                returnPwd += Convert.ToString(bytes[i],16).PadLeft(2,'0');
            }

            return returnPwd.PadLeft(32, '0');
        }

        public void BindComboBox(ComboBox cb,string sql)
        {
            var dt = new DataTable();
            dt = CreateSource(sql);
            cb.DataSource = dt;
            cb.DisplayMember = dt.Columns[1].ColumnName;
            cb.ValueMember = dt.Columns[0].ColumnName;
        }

        public void BindListBox(ListBox lb,string sql)
        {
            var dt = new DataTable();
            dt = CreateSource(sql);
            lb.DataSource = dt;
            lb.DisplayMember = dt.Columns[1].ColumnName;
            lb.ValueMember = dt.Columns[0].ColumnName;
        }

        private DataTable CreateSource(string sql)
        {
            var dt = new DataTable();
            SqlDataReader dr;
            db.RunSql(sql, out dr);
            dt = db.ConvertDataReaderToDataTable(dr);
            dr.Close();
            return dt;
        }

        public void BindDGV(DataGridView dgv, string sql)
        {
            dgv.DataSource = CreateSource(sql);
        }

        public void CbbIndexByText(ComboBox cbb,string value)
        {
            for (int i = 0; i < cbb.Items.Count; i++)
            {
                if (cbb.GetItemText(cbb.Items[i]) == value)
                {
                    cbb.SelectedIndex = i;
                    break;
                }
            }
        }

    }
}
