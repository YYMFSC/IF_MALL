<%@ WebHandler Language="C#" Class="mateHandler" %>


using System;
using System.Web;
using System.Collections;
using IF_Model;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;

public class mateHandler : BaseHandler
{
    public void GetById(HttpContext context)
    {
        string id = context.Request["id"];
        if(!string.IsNullOrEmpty(id))
        {
            int iid;
            try
            {
                iid = int.Parse(id);
            }
            catch (Exception x)
            {

                WriteFailure(context, x.Message);
                return;
            }
            var r = mall.mate.Find(iid);
            WriteSuccess(context, r);
        }
        else
        {
            WriteFailure(context);
        }


    }

    public void Add(HttpContext context)
    {
        //前台放在body里把json格式的文件传过来
        string data = context.Request["data"];
        var session = new IF_Session();
        var userid = session.UserKey;
        IF_EntityTools tools = new IF_EntityTools();
        List<DataField> ldf = tools.getDataFieldFromJson(data);

        try
        {
            mate new1 = new mate();
            new1 = (mate)new ConvertUtil().SetValueFromDataField(new1, ldf);
            //使用add方法把模型加进去
            new1.uid =111;
            mall.mate.Add(new1);
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
    public void Remove(HttpContext context)
    {
        string data = context.Request["data"];
        var ds = data.Split(',');
        try
        {
            foreach (var item in ds)
            {
                mall.mate.Remove(mall.mate.Find(int.Parse(item)));//删除
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
        Expression<Func<mate, bool>> seleWhere = o => true;//o.n_state == (int)Enum_BasicInfoStatus.Enable ;
        if (!string.IsNullOrEmpty(unitkey))
        {
            seleWhere = seleWhere.And(o => o.title.Contains(unitkey));
        }

        var linq = from v in mall.Set<mate>()
                   select v;

        linq = linq.Where(seleWhere);
        base.GetPagination(pager, linq);

        Hashtable result = new MSSQL_help().setDataHashtable(pager);
        WriteInfo(context, result);
    }

    public void GetMSGList(HttpContext context)
    {
        //这些是前台列表的一下筛选
        string unitkey = context.Request["key"];

        //接受一下筛选字段
        List<SearchField> sl = new IF_EntityTools().getSearchFieldFromJson(context.Request["searchfield"]);
        //新建一个分页对象
        IF_SQLPager pager = new MSSQL_help().setPager(sl, context.Request);
        //在查询筛选中加条件
        Expression<Func<mate, bool>> seleWhere = o => true;//o.n_state == (int)Enum_BasicInfoStatus.Enable ;
        if (!string.IsNullOrEmpty(unitkey))
        {
            seleWhere = seleWhere.And(o => o.title.Contains(unitkey));
        }


        var linq = from v in mall.Set<mate>()
                   select v;

        linq = linq.Where(seleWhere);

        WriteInfo(context, linq.ToList());
    }
}