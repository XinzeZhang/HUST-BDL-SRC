using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CheckIn
{
    class DataAccess
    {
        private static string ConnectString = ConfigurationManager.ConnectionStrings["SqlString"].ConnectionString;
        ///<summary>
        ///根据表名获取数据集的表
        ///</summary>
        ///<param name="table">表名</param>
        ///<returns> DataTable </returns>
        public static DataTable GetDataSetByTableName(string table)
        {
            using(SqlConnection con = new SqlConnection(ConnectString))
            {
                string sql = "select * from " + table + ""; // search sql
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sql , con);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds , "table");
                    return ds.Tables[0];
                }
                catch(SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
        ///<summary>
        ///根据Sql语句获取数据集
        ///</summary>
        ///<param name="sql">sql</param>
        ///<returns> DataSet </returns>
        public static DataSet GetDataSetBySql(string sql)
        {
            using(SqlConnection con = new SqlConnection(ConnectString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql , con);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds , "table");
                    return ds;
                }
                catch(SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        ///<summary>
        ///update database
        ///</summary>
        public static void UpdataDateSet(DataSet ds , string sql)
        {
            using(SqlConnection con = new SqlConnection(ConnectString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sql , con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds , "table");
                }
                catch(SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        ///<summary>
        ///Sql命令，返回值为bool，成功为true，错误为false
        ///</summary>
        ///<param name="sql"></param>
        ///<returns>  </returns>
        public static bool sql_command(string sql)
        {
            using(SqlConnection con = new SqlConnection(ConnectString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sql , con);
                    if(command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        ///<summary>
        ///Sql命令，使用DataReader查询是否存在对象，返回值为bool，存在为true，不存在为false
        ///</summary>
        ///<param name="sql"></param>
        ///<returns>  </returns>
        public static bool sql_exist(string sql)
        {
            using(SqlConnection con = new SqlConnection(ConnectString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sql , con);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if(reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(SqlException ex)
                {
                    return false;
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
