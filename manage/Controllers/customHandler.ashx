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
            customMade new1 = mall.customMade.Find(int.Parse(iddf.Value.ToString()));
            new1 = (customMade)new ConvertUtil().SetValueFromDataField(new1, ldf);
            //使用add方法把模型加进去
            mall.customMade.Attach(new1);
            mall.Entry(new1).State = EntityState.Modified;
            //加完一定要保存
            mall.SaveChanges();
            WriteSuccess(context, new1);
        }
        catch (Exception ex)
        {
            WriteFailure(context, ex.Message);
            return;

        }
    }
    public void EditKind(HttpContext context)
    {
          //前台放在body里把json格式的文件传过来
        string data = context.Request["data"];
        IF_EntityTools tools = new IF_EntityTools();
        List<DataField> ldf = tools.getDataFieldFromJson(data);
        DataField iddf = ldf.Where(n => n.Fieldname == "id").SingleOrDefault<DataField>();
        DataField kind = ldf.Where(n => n.Fieldname == "kind").SingleOrDefault<DataField>();
        try
        {
            customMade new1 = mall.customMade.Find(int.Parse(iddf.Value.ToString()));
            new1.kind =int.Parse(kind.Value.ToString());
            //使用add方法把模型加进去
            mall.customMade.Attach(new1);
            mall.Entry(new1).State = EntityState.Modified;
            //加完一定要保存
            mall.SaveChanges();

            Message message = new Message();
            message.uid = 1111;
            message.kind = new1.kind;
            if(new1.kind ==(int)Enum_MessageKind.YIXIADAN)//下单
            {
                message.img = "/uploadfile/XIADAN.jpg";
                message.title = "您线上定制的旗袍的订单状态已改变";
                message.neirong="亲，您线上定制的旗袍已下单!";
            }
            if(new1.kind ==(int)Enum_MessageKind.YIJIAGON)//加工
            {
                 message.img =  "/uploadfile/JIAGON.png";
                 message.title = "您线上定制的旗袍的的订单状态已改变";
                message.neirong="亲，您线上定制的旗袍已在加工!";
            }
            if(new1.kind ==(int)Enum_MessageKind.YIFAHUO)//发货
            {
                 message.img = "/uploadfile/FAHUO.png";
                message.title = "您线上定制的旗袍的订单状态已改变";
                message.neirong="亲，您线上定制的旗袍已发货!";
            }
            if(new1.kind ==(int)Enum_MessageKind.YISHOUHUO)//收货
            {
                message.img = "/uploadfile/SHOUHUO.png";
                message.title = "您线上定制的旗袍的订单状态已改变";
                message.neirong="亲，您线上定制的旗袍已收货!感谢您对本店的支持！";
            }
            mall.Message.Add(message);
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
    public void Remove(HttpContext context)
    {
        string data = context.Request["data"];
        var ds = data.Split(',');
        try
        {
            foreach (var item in ds)
            {
                mall.customMade.Remove(mall.customMade.Find(int.Parse(item)));//删除
                mall.SaveChanges();
            }
            WriteSuccess(context);
        }
        catch (Exception ex)
        {
            WriteFailure(context, ex.Message);
            return;
        }

    }

    public void GetList(HttpContext context)
    {
        //这些是前台列表的一下筛选
        string unitkey = context.Request["key"];
        string jk = context.Request["jk"];
            
        //这是存在session里的用户id
        //var session = new IF_Session();
        //var userid = session.UserKey;
        //用户的单位id
        //var unit = session.UnitKey;
        //这里可以做权限筛选  未做

        //接受一下筛选字段
        List<SearchField> sl = new IF_EntityTools().getSearchFieldFromJson(context.Request["searchfield"]);
        //新建一个分页对象
        IF_SQLPager pager = new MSSQL_help().setPager(sl, context.Request);
        //在查询筛选中加条件
        Expression<Func<customMade, bool>> seleWhere = o => true;//o.n_state == (int)Enum_BasicInfoStatus.Enable


        var linq = from v in mall.Set<customMade>()
                   select v;

        linq = linq.Where(seleWhere);
        base.GetPagination(pager, linq);

        Hashtable result = new MSSQL_help().setDataHashtable(pager);
        WriteInfo(context, result);
    }
}