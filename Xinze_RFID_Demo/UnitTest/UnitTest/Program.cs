using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace UnitTest
{
    class Program
    {
        private static string ConnectString = "server = 192.168.1.12; database=db_WSTLPF;uid=sa;pwd=1231";

        static void Main(string[] args)
        {
            string testsql = "select MAX(EnterOrderInfo.Id) as max_enterorderId from EnterOrderInfo;select MAX(EnterOrderInfo.AthleteOrder) as max_Athleteorder from EnterOrderInfo where EnterOrderInfo.CompetitionProjectId='527';";

            DataSet testsql_dataset = GetDataSetBySql(testsql);
            int ID = Convert.ToInt32(testsql_dataset.Tables[0].Rows[0][0].ToString().Trim())+1;
            int order =Convert.ToInt32(testsql_dataset.Tables[1].Rows[0][0].ToString().Trim())+1;

            Console.WriteLine("ID is : " + ID);
            Console.WriteLine("num is : " + order);
        }

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
    }
}
/*
                 * test sql
               insert into dbo.AthletesInfo
               (
               Id,
               Name,
               Sex,
               ContestantId)
               values
               (
               '2480',
               '测试人员',
               '女',
               '12'
               );

               insert into dbo.EnterOrderInfo
               (
               Id,
               CompetitionProjectId,
               AthleteId,
               AthleteOrder,
               AthleteStatu
               )
               values
               (
               '2480',
               '527',
               '2480',
               '4',
               '0'
               )

               update dbo.CompetitionProject
               set AthletesNum= '4'
               where Id='527'

               *test successful
               */

