using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _28_CRM_System
{
    class Database
    {
        private SqlConnection con;

        /// <summary>
        /// 开启 连接数据库
        /// </summary>
        private void Open()
        {
            if (con == null)
            {
                var constr = "server=.;database=CRM_database;user=sa;pwd=sa;MultipleActiveResultSets=true";
                con = new SqlConnection(constr);
            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        private void Close()
        {
            con?.Close();
        }

        private SqlCommand CreateCommand(string sqlStr)
        {
            Open();
            var com = new SqlCommand(sqlStr, con);
            return com;
        }

        public void RunSql(string sqlStr)
        {
            var cmd = CreateCommand(sqlStr);
            cmd.ExecuteNonQuery();
            Close();
        }

        public int RunSqlInt(string sqlStr)
        {
            var cmd = CreateCommand(sqlStr);
            var t = (int)cmd.ExecuteScalar();
            Close();
            return t;
        }

        public SqlDataReader RunSql(string sqlStr, out SqlDataReader dr)
        {
            var cmd = CreateCommand(sqlStr);
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public DataTable ConvertDataReaderToDataTable(SqlDataReader dataReader)
        {
            var dt = new DataTable();
            try
            {
                //动态添加表的数据列
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    var myDataColumn = new DataColumn()
                    {
                        DataType = dataReader.GetFieldType(i),
                        ColumnName = dataReader.GetName(i)
                    };
                    dt.Columns.Add(myDataColumn);
                }

                //添加表的数据
                while (dataReader.Read())
                {
                    var myDataRow = dt.NewRow();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        myDataRow[i] = dataReader[i].ToString();
                    }

                    dt.Rows.Add(myDataRow);
                    myDataRow = null;
                }
                //关闭数据读取器
                dataReader.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw new Exception("转换出错", e);
            }
        }
    }
}
