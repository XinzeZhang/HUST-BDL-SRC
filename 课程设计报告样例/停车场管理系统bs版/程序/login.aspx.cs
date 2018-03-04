using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cx.Items.Add("超级管理员");
            cx.Items.Add("普通管理员");
         
            
            
        }
    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text.ToString().Trim() == "" || TextBox1.Text.ToString().Trim() == "")
        {
            Response.Write("<script>javascript:alert('请输入完整');location.href='login.aspx';</script>");
            Response.End();
        }
        if (Session["code"].ToString().Trim().Equals(yzm.Text.Trim()))
        {

        }
        else
        {
            Response.Write("<script>javascript:alert('验证码有误');location.href='login.aspx';</script>");
            Response.End();
        }
        string sql = "";
        if (cx.Text.ToString().Trim() == "超级管理员")
        {
            sql = "select * from allusers where username='" + TextBox1.Text.ToString().Trim() + "' and pwd='" + TextBox2.Text.ToString().Trim() + "' and cx='超级管理员' ";
        }
        if (cx.Text.ToString().Trim() == "普通管理员")
        {
            sql = "select * from allusers where username='" + TextBox1.Text.ToString().Trim() + "' and pwd='" + TextBox2.Text.ToString().Trim() + "' and cx='普通管理员' ";
        }
   

        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);
        // result = new TestOnline.Class1().hsggetdata(sql);
        if (result != null)
        {
            if (result.Tables[0].Rows.Count > 0)
            {
                Session["username"] = TextBox1.Text.ToString().Trim();

               
                    Session["cx"] = cx.Text.ToString().Trim();

               


                Response.Redirect("main.aspx");
            }
            else
            {
                Response.Write("<script>javascript:alert('对不起，用户名或密码不正确，或您的帐号未经审核!');</script>");
            }
        }
        else
        {
            Response.Write("<script>javascript:alert('对不起，系统错误，请不要越权操作!');</script>");
        }
    }
}
