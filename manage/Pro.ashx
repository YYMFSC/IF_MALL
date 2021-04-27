<%@ WebHandler Language="C#" Class="IF_Pro" %>

using System;
using System.Web;
using DingKit;
using DingUI;
using System.Data;

public class IF_Pro : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string fun = CFun.GetParam("do");

        if (fun == "GetProData")
        {
            GetProData(context);
        }
        if (fun == "GetCommonData")
        {
            GetCommonData(context);
        }
        if (fun == "GetNavbarData")
        {
            GetNavbarData(context);
        }
        if (fun == "GetSortData")
        {
            GetSortData(context);
        }
        if (fun == "GetCartData")
        {
            GetCartData(context);
        }
        if (fun == "Plus")
        {
            Plus(context);
        }
        if (fun == "Minus")
        {
            Minus(context);
        }
        if (fun == "GetCartCount")
        {
            GetCartCount(context);
        }
    }

    public void GetProData(HttpContext context)
    {
        // ProData获取的数据需要处理价格精度丢失问题
        // getparam对于没有传入的参数，会置为空
        string pid = CFun.GetParam("pid");  // 商品id
        string key = CFun.GetParam("key");  // 模糊查询
        string shopID = CFun.GetParam("shopID");    // 商店id
        string tid = CFun.GetParam("tid");  // 商品类型id
        string minPrice = CFun.GetParam("minPrice");    //价格下限和上限
        string maxPrice = CFun.GetParam("maxPrice");

        string sql = "SELECT id, img, title, originPrice, price, shopID, tid, shopName, shopIcon, tName FROM V_Products ";
        sql += " WHERE id is not null ";
        if (!string.IsNullOrEmpty(pid))
        {
            sql += " and id=" + pid + "";
        }
        if (!string.IsNullOrEmpty(key))
        {
            sql += " and title like'%" + key + "%' ";
        }
        if (!string.IsNullOrEmpty(shopID))
        {
            sql += " and shopID='" + shopID + "'";
        }
        if (!string.IsNullOrEmpty(tid))
        {
            sql += " and tid=" + tid + "";
        }
        if (!string.IsNullOrEmpty(minPrice))
        {
            sql += " and minPrice>=" + minPrice + "";
        }
        if (!string.IsNullOrEmpty(maxPrice))
        {
            sql += " and maxPrice>=" + maxPrice + "";
        }
        DataSet ds = CSql.CreateDataSet(sql, "ProData");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }

    public void GetCommonData(HttpContext context)
    {
        string type = CFun.GetParam("type");
        string sql = "SELECT * FROM CommonData ";
        if (!string.IsNullOrEmpty(type))
        {
            sql += "WHERE type='" + type + "'";
        }
        DataSet ds = CSql.CreateDataSet(sql, "CommonData");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }

    public void GetNavbarData(HttpContext context)
    {
        string sql = "SELECT text, className, url FROM Navbar";
        DataSet ds = CSql.CreateDataSet(sql, "Tabs");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }

    public void GetSortData(HttpContext context)
    {
        string sql = "SELECT tID,tName FROM productType";
        DataSet ds = CSql.CreateDataSet(sql, "SortData");
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonHelper.Convert2Json(ds.Tables[0]));
    }

    public void GetCartData(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        context.Response.ContentType = "text/plain";
        context.Response.Write(Cart.GetCartJSon(uid));
    }

    public void Plus(HttpContext context)
    {
        string pid = CFun.GetParam("pid");
        string uid = CFun.GetParam("uid");
        // Warning: 第一次加入购物车时会写入price，之后增加不会修改price
        string price = CFun.GetParam("price");
        string sql = string.Format("SELECT COUNT(*) FROM cart WHERE isPay='0' AND uid='{0}' and proid='{1}'", uid, pid);
        if (CSql.GetStringSingle(sql) == "0")
        {
            //context.Response.Write(sql);
            sql = "insert into  cart(proId, uId, price, count) values('" + pid + "','" + uid + "','" + price + "', '1')";
        }
        else
        {
            sql = string.Format("SELECT count FROM cart WHERE isPay='0' AND uid='{0}' and proid='{1}'", uid, pid);
            int count = int.Parse(CSql.GetStringSingle(sql)) + 1;
            if (count <= 99)
            {
                sql = string.Format("UPDATE cart SET count={0} WHERE isPay='0' AND uid='{1}' and proid='{2}'", count, uid, pid);
            }
        }
        if (CSql.ExecuteSql(sql) == false)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
        }
        else
        {
            sql = "select count(*) from cart where isPay='0' AND uid='" + uid + "'";
            context.Response.ContentType = "text/plain";
            context.Response.Write(CSql.GetStringSingle(sql));
        }
    }
    public void Minus(HttpContext context)
    {
        string pid = CFun.GetParam("pid");
        string uid = CFun.GetParam("uid");
        string sql = string.Format("SELECT COUNT(*) FROM cart WHERE isPay='0' AND uid='{0}' and proid='{1}'", uid, pid);
        if (CSql.GetStringSingle(sql) == "0")
        {
            context.Response.Write("-1");
            return;
        }
        else
        {
            sql = string.Format("SELECT count FROM cart WHERE isPay='0' AND uid='{0}' and proid='{1}'", uid, pid);
            int count = int.Parse(CSql.GetStringSingle(sql)) - 1;
            if (count == 0)
            {
                // Warning: 若小程序端不做限制，当count为1时继续减1，则从购物车中删除该条商品
                sql = string.Format("DELETE FROM cart WHERE isPay='0' AND uid='{0}' and proid='{1}'", uid, pid);
            }
            else
            {
                sql = string.Format("UPDATE cart SET count={0} WHERE isPay='0' AND uid='{1}' and proid='{2}'", count, uid, pid);
            }
        }
        if (CSql.ExecuteSql(sql) == false)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("-1");
        }
        else
        {
            sql = "select count(*) from cart WHERE isPay='0' AND uid='" + uid + "'";
            context.Response.ContentType = "text/plain";
            context.Response.Write(CSql.GetStringSingle(sql));
        }
    }

    public void GetCartCount(HttpContext context)
    {
        string uid = CFun.GetParam("uid");
        string sql = "select count(*) from cart where isPay='0' AND uid='" + uid + "'";
        context.Response.ContentType = "text/plain";
        context.Response.Write(CSql.GetStringSingle(sql));
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}