<%@ WebHandler Language="C#" Class="Order" %>

using System;
using System.Web;
using DingKit;
using DingUI;
using System.Data;

public class Order : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
           
        string fun = CFun.GetParam("do");
        if (fun == "GetPaymentData")
        {
            GetPaymentData(context);
        }
        if (fun == "Submit")
        {
            Submit(context);
        }
        if (fun == "GetOrderData")
        {
            GetOrderData(context);
        }
        if (fun == "Pay")
        {
            Pay(context);
        }
        if (fun == "GetOrderList")
        {
            GetOrderList(context);
        }
    }

    public void GetOrderList(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        string ispay = CFun.GetParam("ispay");
        context.Response.ContentType = "text/plain";
        context.Response.Write(OrderList.GetOrderListJSon(uid, ispay));
    }

    public void GetPaymentData(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        string idlist = CFun.GetParam("idlist");
        string id = string.Join("','", idlist.Split(' '));
        string sql = "SELECT id, proId, uid, img, title, originPrice, price, count, isPay FROM V_Cart ";
        sql += " WHERE isPay='0' ";
        sql += " AND uid='" + uid + "' AND id IN ('" + id + "') ";

        DataSet ds = CSql.CreateDataSet(sql, "PayData");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }

    public void Pay(HttpContext context)
    {
        string id = CFun.GetParam("orderid");
        string payway = CFun.GetParam("payway");
        string sql = "UPDATE Delivery SET isPay='1', payway='" + payway + "' WHERE id='" + id + "'";
        if (CSql.ExecuteSql(sql) == false)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
            return;
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(id);
        }
    }

    public void GetOrderData(HttpContext context)
    {
        string id = CFun.GetParam("orderid");
        string sql = "SELECT id, originPrice, discount, deliveryFee FROM Delivery WHERE id='" + id + "'";
        DataSet ds = CSql.CreateDataSet(sql, "OrderData");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }

    public void Submit(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        string sid = CFun.GetParam("sid");
        string cartIdList = CFun.GetParam("cartIdList");
        string cartid = string.Join("','", cartIdList.Split(' '));
        string sql = "UPDATE cart SET isPay='1' ";
        sql += " WHERE isPay='0' ";
        sql += " AND uid='" + uid + "' AND id IN ('" + cartid + "') ";
        if (CSql.ExecuteSql(sql) == false)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
            return;
        }
        string payway = CFun.GetParam("payway");
        string originPrice = CFun.GetParam("originPrice");
        string discount = CFun.GetParam("discount");
        string deliveryFee = CFun.GetParam("deliveryFee");
        string addressDm = "1";
        string remark = CFun.GetParam("remark");
        DateTime now = DateTime.Now;
        sql = "INSERT INTO Delivery( uid, sid, payway, cartIdList, originPrice, discount, deliveryFee, addressDm, remark, submitTime)";
        sql += string.Format(" VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
            uid, sid, payway, cartIdList, originPrice, discount, deliveryFee, addressDm, remark, now);

        if (CSql.ExecuteSql(sql) == false)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
            return;
        }
        else
        {
            sql = "SELECT id FROM Delivery ";
            sql += string.Format(" WHERE uid='{0}' and cartIdList='{1}' and submitTime='{2}'",
                uid, cartIdList, now);
            context.Response.ContentType = "text/plain";
            context.Response.Write(CSql.GetStringSingle(sql));
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }
}