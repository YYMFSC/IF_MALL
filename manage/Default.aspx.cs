using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DingKit;
using DingUI;
using System.Data;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "select xh,xm from student ";
        DataSet ds = CSql.CreateDataSet(sql);
        Response.Clear();
        Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
        Response.End();
    }
}