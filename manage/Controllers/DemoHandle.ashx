<%@ WebHandler Language="C#" Class="DemoHandle" %>

using System;
using System.Web;
using DingKit;
using DingUI;
using System.Data;
using Model;

public class DemoHandle : IHttpHandler
{
    public MDbContext mDbContext=new MDbContext();
    public void ProcessRequest (HttpContext context) {
        string fun = CFun.GetParam("do");
        if (fun == "Add")
        {
            Add(context);
        }

    }
    public void Add(HttpContext context)
    {
        Address address1 = new Address();
        address1.uid = CFun.GetParam("uid");
        address1.name = CFun.GetParam("name");
        address1.phone = CFun.GetParam("phone");
        address1.province = CFun.GetParam("province");
        address1.city = CFun.GetParam("city");
        address1.local = CFun.GetParam("local");
        address1.address1 = CFun.GetParam("address");
        try
        {
                mDbContext.Addresses.Add(address1);
                mDbContext.SaveChanges();
                context.Response.ContentType = "text/plain";
                context.Response.Write("ExecuteSql success");
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(ex.Message);
        }
        
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}