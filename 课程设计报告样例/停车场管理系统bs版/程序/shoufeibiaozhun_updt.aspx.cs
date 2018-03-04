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

public partial class shoufeibiaozhun_updt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
   
        if (!IsPostBack)
        {
			// xingbie.Items.Add("male"); 
			// xingbie.Items.Add("female");
            string sql;
            sql = "select * from shoufeibiaozhun where id=" + Request.QueryString["id"].ToString().Trim() ;
            getdata(sql);
        }
    }



    private void getdata(string sql)
    {
        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);
        if (result != null)
        {
            if (result.Tables[0].Rows.Count > 0)
            {
                chexing.Text = result.Tables[0].Rows[0]["chexing"].ToString().Trim();linshijifei.Text = result.Tables[0].Rows[0]["linshijifei"].ToString().Trim();huiyuanjifei.Text = result.Tables[0].Rows[0]["huiyuanjifei"].ToString().Trim();jiarijifei.Text = result.Tables[0].Rows[0]["jiarijifei"].ToString().Trim();
                
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string sql;
        
        sql = "update shoufeibiaozhun set chexing='" + chexing.Text.ToString().Trim() + "',linshijifei='" + linshijifei.Text.ToString().Trim() + "',huiyuanjifei='" + huiyuanjifei.Text.ToString().Trim() + "',jiarijifei='" + jiarijifei.Text.ToString().Trim() + "' where id=" + Request.QueryString["id"].ToString().Trim();
        int result;
        result = new Class1().hsgexucute(sql);
        if (result == 1)
        {
            Response.Write("<script>javascript:alert('修改成功');</script>");
        }
        else
        {
            Response.Write("<script>javascript:alert('系统错误');</script>");
        }
    }
}

