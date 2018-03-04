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

public partial class mod : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.ToString().Trim() == "" || TextBox2.Text.ToString().Trim() == "" || TextBox3.Text.ToString().Trim() == "")
        {
            Response.Write("<script>javascript:alert('请填写完整');history.back();</script>");
        }
        else
        {
            if (TextBox2.Text.ToString().Trim() != TextBox3.Text.ToString().Trim())
            {
                Response.Write("<script>javascript:alert('两次密码不一至，请确认');history.back();</script>");
            }
            else
            {
                string sql;
                sql = "select * from allusers where username='" + Session["username"].ToString().Trim() + "' and pwd='" + TextBox1.Text.ToString().Trim() + "'";

                DataSet result = new DataSet();
                result = new Class1().hsggetdata(sql);
                if (result != null)
                {
                    if (result.Tables[0].Rows.Count > 0)
                    {
                        sql = "update allusers set pwd='" + TextBox2.Text.ToString().Trim() + "' where username='" + Session["username"].ToString().Trim() + "'";
                        int result2;
                        result2 = new Class1().hsgexucute(sql);
                        if (result2 == 1)
                        {
                            Response.Write("<script>javascript:alert('修改成功');</script>");
                        }
                        else
                        {
                            Response.Write("<script>javascript:alert('系统错误');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>javascript:alert('原密码不正确');</script>");
                    }
                }


            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("sy.aspx");
    }
}
