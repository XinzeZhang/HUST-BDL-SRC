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

public partial class tingchejilu_updt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
   
        if (!IsPostBack)
        {
			// xingbie.Items.Add("male"); 
			// xingbie.Items.Add("female");
            string sql;
            sql = "select * from tingchejilu where id=" + Request.QueryString["id"].ToString().Trim() ;
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
                chepai.Text = result.Tables[0].Rows[0]["chepai"].ToString().Trim();chexing.Text = result.Tables[0].Rows[0]["chexing"].ToString().Trim();xingming.Text = result.Tables[0].Rows[0]["xingming"].ToString().Trim();shenfenzheng.Text = result.Tables[0].Rows[0]["shenfenzheng"].ToString().Trim();dianhua.Text = result.Tables[0].Rows[0]["dianhua"].ToString().Trim();chewei.Text = result.Tables[0].Rows[0]["chewei"].ToString().Trim();tingrushijian.Text = result.Tables[0].Rows[0]["tingrushijian"].ToString().Trim();kaichushijian.Text = result.Tables[0].Rows[0]["kaichushijian"].ToString().Trim();tingcheshichang.Text = result.Tables[0].Rows[0]["tingcheshichang"].ToString().Trim();jifeileixing.Text = result.Tables[0].Rows[0]["jifeileixing"].ToString().Trim();feiyong.Text = result.Tables[0].Rows[0]["feiyong"].ToString().Trim();beizhu.Text = result.Tables[0].Rows[0]["beizhu"].ToString().Trim();
                
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string sql;
        
        sql = "update tingchejilu set chepai='" + chepai.Text.ToString().Trim() + "',chexing='" + chexing.Text.ToString().Trim() + "',xingming='" + xingming.Text.ToString().Trim() + "',shenfenzheng='" + shenfenzheng.Text.ToString().Trim() + "',dianhua='" + dianhua.Text.ToString().Trim() + "',chewei='" + chewei.Text.ToString().Trim() + "',tingrushijian='" + tingrushijian.Text.ToString().Trim() + "',kaichushijian='" + kaichushijian.Text.ToString().Trim() + "',tingcheshichang='" + tingcheshichang.Text.ToString().Trim() + "',jifeileixing='" + jifeileixing.Text.ToString().Trim() + "',feiyong='" + feiyong.Text.ToString().Trim() + "',beizhu='" + beizhu.Text.ToString().Trim() + "' where id=" + Request.QueryString["id"].ToString().Trim();
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

