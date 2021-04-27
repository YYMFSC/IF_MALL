<%@ WebHandler Language="C#" Class="address" %>

using System;
using System.Web;
using DingKit;
using DingUI;
using System.Data;

public class address : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string fun = CFun.GetParam("do");
        if (fun == "Login")
        {
            Login(context);
        }
        if (fun == "GetAddress")
        {
            GetAddress(context);
        }
        if (fun == "EditAddress")
        {
            EditAddress(context);
        }
    }

    public void Login(HttpContext context)
    {
        string code = CFun.GetParam("code");

        string appid = "wx90376f9593b46729";//wxc8cd2c7633ea023a
        string secret = "8619cdc1cf317628d8ca2b059789b5e5";//ee5e5ba40fce3bb91c6b69a8ca2340e0
        string grant_type = "authorization_code";
        string url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type={3}",
            appid, secret, code, grant_type);
        string res = CHttp.GetHttpResponse(url, 5000);
        string openid = res.Split(',')[1].Split(':')[1].Split('"')[1];
        context.Response.ContentType = "text/plain";
        context.Response.Write(openid);
        string sql = "SELECT * FROM User WHERE openid='" + openid + "'";
        DataSet ds = CSql.CreateDataSet(sql, "UserInfo");
        if (ds.Tables[0].Rows.Count > 0)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("-1");
        }
    }

    public void GetAddress(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        string sql = "SELECT uid, name, phone, province, city, local, address FROM Address WHERE uid='" + uid + "'";

        DataSet ds = CSql.CreateDataSet(sql, "AddressData");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }
    public void EditAddress(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        string name = CFun.GetParam("name");
        string phone = CFun.GetParam("phone");
        string province = CFun.GetParam("province");
        string city = CFun.GetParam("city");
        string local = CFun.GetParam("local");
        string address = CFun.GetParam("address");

        string sql = "UPDATE FROM Address ";
        sql += string.Format(" SET name='{0}' AND phone='{1}' AND province='{2}' AND city='{3}' AND local='{4}' address='{5}' ",
            name, phone, province, city, local, address);
        sql += " WHERE uid='" + uid + "'";

        if (CSql.ExecuteSql(sql) == false)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("ExecuteSql success");
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}