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

public partial class tingchejilu_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
        {
            
            tingrushijian.Text = DateTime.Now.ToString().Trim();
            chepai.Items.Add("请选择");
            addxiala("chezhuxinxi", "chepai", "kehubianhao");
            addxiala2("cheweixinxi", "cheweibianhao", "kehubianhao");
			//ghdhdnuuks  string sql = "select * from yifngpsiafnxfinxgi where id=" + Request.QueryString["id"].ToString().Trim();
            //ghdhdnuuks  DataSet result = new DataSet();
            //ghdhdnuuks  result = new Class1().hsggetdata(sql);
            //ghdhdnuuks  if (result != null)
            //ghdhdnuuks  {
            //ghdhdnuuks    if (result.Tables[0].Rows.Count > 0)
            //ghdhdnuuks     {
                    //lsiebigsaodguqdufz

            //ghdhdnuuks     }
            //ghdhdnuuks  }
        }
    }

    private void addxiala2(string ntable, string nzd, string nxlk)
    {
        string sql;
        sql = "select " + nzd + " from " + ntable + " where zhuangtai='空闲' order by id desc";
        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);
        if (result != null)
        {
            if (result.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for (i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    chewei.Items.Add(result.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
		if (new Class1().IsNumber(feiyong.Text.ToString().Trim().Replace(".",""))){ }else{ Response.Write("<script>javascript:alert('Numeric only!');history.back();</script>"); Response.End();}

        string sql;
	//sfdigdh ischongfu("zhujianquchong");

        sql="insert into tingchejilu(chepai,chexing,xingming,shenfenzheng,dianhua,chewei,tingrushijian,kaichushijian,tingcheshichang,jifeileixing,feiyong,beizhu) values('"+chepai.Text.ToString().Trim()+"','"+chexing.Text.ToString().Trim()+"','"+xingming.Text.ToString().Trim()+"','"+shenfenzheng.Text.ToString().Trim()+"','"+dianhua.Text.ToString().Trim()+"','"+chewei.Text.ToString().Trim()+"','"+tingrushijian.Text.ToString().Trim()+"','"+kaichushijian.Text.ToString().Trim()+"','"+tingcheshichang.Text.ToString().Trim()+"','"+jifeileixing.Text.ToString().Trim()+"','"+feiyong.Text.ToString().Trim()+"','"+beizhu.Text.ToString().Trim()+"') ";
        int result;
        result = new Class1().hsgexucute(sql);
        sql = "update cheweixinxi set zhuangtai='已占' where cheweibianhao='"+chewei.Text.ToString().Trim()+"'";
        result = new Class1().hsgexucute(sql);
        if (result == 1)
        {
            Response.Write("<script>javascript:alert('添加成功');</script>");
        }
        else
        {
            Response.Write("<script>javascript:alert('系统错误，请检查数据库设置问题');</script>");
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
                    chepai.Items.Add(result.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
        }
    }

	public void ischongfu(string sql)
    {
        DataSet result = new DataSet();
            result = new Class1().hsggetdata(sql);
            if (result != null)
            {
                if (result.Tables[0].Rows.Count > 0)
                {
                    Response.Write("<script>javascript:alert('提示,该数据已存在,不要重复添加');location.href='tingchejilu_add.aspx';</script>");
                    Response.End();
                }
            }
    }
    protected void chepai_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "";
        sql = "select * from chezhuxinxi where chepai='"+chepai.Text.ToString().Trim()+"'";
        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);
        if (result != null)
        {
            if (result.Tables[0].Rows.Count > 0)
            {
                xingming.Text = result.Tables[0].Rows[0]["xingming"].ToString().Trim();
                shenfenzheng.Text = result.Tables[0].Rows[0]["shenfenzheng"].ToString().Trim();
                dianhua.Text = result.Tables[0].Rows[0]["dianhua"].ToString().Trim();
                chexing.Text = result.Tables[0].Rows[0]["chexing"].ToString().Trim();
            }
        }
    }
}

