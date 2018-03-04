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

public partial class chezhuxinxi_updt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
   
        if (!IsPostBack)
        {
            gudingchewei.Items.Add("");
            addxiala("cheliangleixing", "leixing", "kehubianhao");
            addxiala2("cheweixinxi", "cheweibianhao", "kehubianhao");
			// xingbie.Items.Add("male"); 
			// xingbie.Items.Add("female");
            string sql;
            sql = "select * from chezhuxinxi where id=" + Request.QueryString["id"].ToString().Trim() ;
            getdata(sql);
        }
    }
    private void addxiala2(string ntable, string nzd, string nxlk)
    {
        string sql;
        sql = "select " + nzd + " from " + ntable + " order by id desc";
        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);
        if (result != null)
        {
            if (result.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for (i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    gudingchewei.Items.Add(result.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
        }
    }

    private void addxiala(string ntable, string nzd, string nxlk)
    {
        string sql;
        sql = "select " + nzd + " from " + ntable + " order by id desc";
        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);
        if (result != null)
        {
            if (result.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for (i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    chexing.Items.Add(result.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
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
                xingming.Text = result.Tables[0].Rows[0]["xingming"].ToString().Trim();shenfenzheng.Text = result.Tables[0].Rows[0]["shenfenzheng"].ToString().Trim();dianhua.Text = result.Tables[0].Rows[0]["dianhua"].ToString().Trim();dizhi.Text = result.Tables[0].Rows[0]["dizhi"].ToString().Trim();chepai.Text = result.Tables[0].Rows[0]["chepai"].ToString().Trim();chexing.Text = result.Tables[0].Rows[0]["chexing"].ToString().Trim();gudingchewei.Text = result.Tables[0].Rows[0]["gudingchewei"].ToString().Trim();
                
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string sql;
        
        sql = "update chezhuxinxi set xingming='" + xingming.Text.ToString().Trim() + "',shenfenzheng='" + shenfenzheng.Text.ToString().Trim() + "',dianhua='" + dianhua.Text.ToString().Trim() + "',dizhi='" + dizhi.Text.ToString().Trim() + "',chepai='" + chepai.Text.ToString().Trim() + "',chexing='" + chexing.Text.ToString().Trim() + "',gudingchewei='" + gudingchewei.Text.ToString().Trim() + "' where id=" + Request.QueryString["id"].ToString().Trim();
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

