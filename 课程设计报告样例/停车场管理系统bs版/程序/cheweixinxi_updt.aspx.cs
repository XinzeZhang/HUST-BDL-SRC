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

public partial class cheweixinxi_updt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
   
        if (!IsPostBack)
        {
            zhuangtai.Items.Add("空闲");
            zhuangtai.Items.Add("已占");
            leixing.Items.Add("固定车位");
            leixing.Items.Add("自由车位");
			// xingbie.Items.Add("male"); 
			// xingbie.Items.Add("female");
            string sql;
            sql = "select * from cheweixinxi where id=" + Request.QueryString["id"].ToString().Trim() ;
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
                cheweibianhao.Text = result.Tables[0].Rows[0]["cheweibianhao"].ToString().Trim();daxiao.Text = result.Tables[0].Rows[0]["daxiao"].ToString().Trim();zhuangtai.Text = result.Tables[0].Rows[0]["zhuangtai"].ToString().Trim();leixing.Text = result.Tables[0].Rows[0]["leixing"].ToString().Trim();beizhu.Text = result.Tables[0].Rows[0]["beizhu"].ToString().Trim();
                
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string sql;
        
        sql = "update cheweixinxi set cheweibianhao='" + cheweibianhao.Text.ToString().Trim() + "',daxiao='" + daxiao.Text.ToString().Trim() + "',zhuangtai='" + zhuangtai.Text.ToString().Trim() + "',leixing='" + leixing.Text.ToString().Trim() + "',beizhu='" + beizhu.Text.ToString().Trim() + "' where id=" + Request.QueryString["id"].ToString().Trim();
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

