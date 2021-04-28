<%@ WebHandler Language="C#" Class="customHandler" %>

using System;
using System.Web;
using System.Collections;
using IF_Model;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;

public class customHandler : BaseHandler
{
    public void GetById(HttpContext context)
    {
        string id = context.Request["id"];
        var r = mall.customMade.Find(id);
        WriteSuccess(context, r);
    }

    public void Add(HttpContext context)
    {
        //前台放在body里把json格式的文件传过来
        string data = context.Request["data"];
        //var session = new IF_Session();
        //var userid = session.UserKey;
        IF_EntityTools tools = new IF_EntityTools();
        List<DataField> ldf = tools.getDataFieldFromJson(data);

        try
        {
            customMade new1 = new customMade();
            new1 = (customMade)new ConvertUtil().SetValueFromDataField(new1, ldf);
            new1.cTime = DateTime.Now;
            //使用add方法把模型加进去
            mall.customMade.Add(new1);
            //加完一定要保存
            mall.SaveChanges();
            WriteSuccess(context, new1);
        }
        catch (Exception ex)
        {
            WriteFailure(context,ex.Message);
            return;

        }
    }
    public void Edit(HttpContext context)
    {
        //前台放在body里把json格式的文件传过来
        string data = context.Request["data"];
        IF_EntityTools tools = new IF_EntityTools();
        List<DataField> ldf = tools.getDataFieldFromJson(data);
        DataField iddf = ldf.Where(n => n.Fieldname == "id").SingleOrDefault<DataField>();
        
        try
        {
            customMade new1 = mall.customMade.Find(iddf.Value.ToString());
            new1 = (customMade)new ConvertUtil().SetValueFromDataField(new1, ldf);
            new1.cTime = DateTime.Now;
            //使用add方法把模型加进去
            mall.customMade.Attach(new1);
             mall.Entry(new1).State = EntityState.Modified;
            //加完一定要保存
            mall.SaveChanges();
            WriteSuccess(context, new1);
        }
        catch (Exception ex)
        {
            WriteFailure(context,ex.Message);
            return;

        }
    }
}