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

public partial class tingchejilu_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            jifeileixing.Items.Add("所有");
            jifeileixing.Items.Add("临时计费");
            jifeileixing.Items.Add("会员计费");
            jifeileixing.Items.Add("假日计费");
	    // xingbie.Items.Add("所有"); 
	    // xingbie.Items.Add("male"); 
	    // xingbie.Items.Add("female");
            string sql;
            sql = "select * from tingchejilu order by id desc";
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
                DataGrid1.DataSource = result.Tables[0];
                DataGrid1.DataBind();
                Label1.Text = "以上数据中共" + result.Tables[0].Rows.Count+"条";
            }
            else
            {
                DataGrid1.DataSource = null;
                DataGrid1.DataBind();
                Label1.Text = "暂无任何数据";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql;
        sql = "select * from tingchejilu where 1=1";
        if (chepai.Text.ToString().Trim()!="所有" ){ sql=sql+" and chepai like '%" + chepai.Text.ToString().Trim() + "%'";}if (chexing.Text.ToString().Trim()!="" ){ sql=sql+" and chexing like '%" + chexing.Text.ToString().Trim() + "%'";}if (xingming.Text.ToString().Trim()!="" ){ sql=sql+" and xingming like '%" + xingming.Text.ToString().Trim() + "%'";}if (shenfenzheng.Text.ToString().Trim()!="" ){ sql=sql+" and shenfenzheng like '%" + shenfenzheng.Text.ToString().Trim() + "%'";}if (dianhua.Text.ToString().Trim()!="" ){ sql=sql+" and dianhua like '%" + dianhua.Text.ToString().Trim() + "%'";}if (chewei.Text.ToString().Trim()!="所有" ){ sql=sql+" and chewei like '%" + chewei.Text.ToString().Trim() + "%'";}if (jifeileixing.Text.ToString().Trim()!="所有" ){ sql=sql+" and jifeileixing like '%" + jifeileixing.Text.ToString().Trim() + "%'";}
        sql = sql + " order by id desc";

        getdata(sql);
    }

    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string sql;
        sql = "select * from tingchejilu order by id desc";
        getdata(sql);
        DataGrid1.CurrentPageIndex = e.NewPageIndex;
        DataGrid1.DataBind();
    }
	public string riqigeshi(object str)
    {
        string strTmp = str.ToString();
        DateTime dt = Convert.ToDateTime(strTmp);
        string ss = dt.ToShortDateString();
        return ss;
        
    } 
     protected void Button2_Click(object sender, EventArgs e)
    {
        string sql;
        sql = "select * from tingchejilu order by id desc";

        DataSet result = new DataSet();
        result = new Class1().hsggetdata(sql);

    
    }
}

