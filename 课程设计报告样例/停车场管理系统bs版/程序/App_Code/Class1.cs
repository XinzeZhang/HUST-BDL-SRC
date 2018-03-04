using System;
using System.Data;
//using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class Class1
{
    //SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["testConnectionString"]);
    public Class1()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    // public static string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Web.HttpContext.Current.Server.MapPath("~/App_Data/web.mdb");

    public static string connstring = ConfigurationManager.AppSettings["cn"];  //设置连接数据库的代码是webconfig里面的cn那句,即:server=.;database=net05qingongzhuxue;uid=sa;pwd=;
    public static DataSet GDS(string sql)
    {
        //OleDbDataAdapter dap = new OleDbDataAdapter(sql, connstring);
        SqlDataAdapter dap = new SqlDataAdapter(sql, connstring); 
        DataSet ds = new DataSet();
        dap.Fill(ds);
        return ds;
    }
    public static void Dsql(string sql)
    {
        //  OleDbConnection conn = new OleDbConnection();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = connstring;
        conn.Open();
        //  OleDbCommand cmd = new OleDbCommand(sql, conn);
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    public int hsgexucute(string sql)  //自定义函数:执行sql语句,如果执行成功,返回值1,否则0
    {
        //myConnection.Open();


        SqlConnection conn = new SqlConnection();   //定义新数据库连接
        conn.ConnectionString = connstring;  //设置该新连接字符串是connstr,即上面的webconfig里的cn值
        SqlCommand myCommand = new SqlCommand(sql, conn); //设置新执行命令

       // SqlCommand cmd = new SqlCommand(sql, conn); 


        try
        {
            conn.Open();  //打开数据库连接
            myCommand.ExecuteNonQuery(); //执行sql语句
            return 1;  //执行成功,返回1
        }
        catch
        {
            //Console.WriteLine("SqlException:{0}",SQLexc);
            return 0;  //执行失败,返回0

        }
        finally
        {
            conn.Close(); //关闭数据库连接
        }
    }

    public DataSet hsggetdata(string sql)  //自定义函数,查询数据库,将得到的数据以dataset型返回
    {
        //myConnection.Open();

        //SqlConnection myConnection=new SqlConnection(ConfigurationSettings.AppSettings["strConn"]);

        SqlConnection conn = new SqlConnection();  //这几句都与上相同
        conn.ConnectionString = connstring;
        SqlCommand myCommand = new SqlCommand(sql, conn);

        SqlCommand cmd = new SqlCommand(sql, conn);

        try
        {
            // myConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter(myCommand);  //定义一个新dataadapter,用于接收数据
            DataSet ds = new DataSet();  //定义新dataset用于接收数据
            da.Fill(ds);  //将da里的值赋给ds

            return ds;  //返回ds
        }
        catch
        {
            //Console.WriteLine("SqlException:{0}",SQLexc);
            return null;  //如果异常,返回null

        }
        finally
        {
            conn.Close();  //关闭数据库连接
        }
    }
    public bool IsTelephone(string str_telephone)  //验证电话
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
    }

    public bool IsPostalcode(string str_postalcode)  //验证邮编
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\d{6}$");
    }
    public bool IsIDcard(string str_idcard)  //验证身份证
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
    }
    public bool IsNumber(string str_number)  //验证数字
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"^[0-9]*$");
    }
    public bool IsHandset(string str_handset)  //验证手机号
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
    }
    public bool IsUrl(string source) //验证网址
    {
        return System.Text.RegularExpressions.Regex.IsMatch(source, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$");
    }
    public bool IsEmail(string source)  //验证邮箱
    {
        return System.Text.RegularExpressions.Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
    }
 
}
