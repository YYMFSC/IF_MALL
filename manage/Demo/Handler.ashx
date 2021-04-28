<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Collections;
using IF_Model;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;

public class Handler : BaseHandler
{
    public void GetById(HttpContext context)
    {
        string id = context.Request["id"];
        var r = mall.Address.Find(id);
        WriteSuccess(context, r);
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
            Address new1 = new Address();
            new1 = (Address)new ConvertUtil().SetValueFromDataField(new1, ldf);
            new1.address1 = "";
            //new1.userid = userid;

            //使用add方法把模型加进去
            mall.Address.Add(new1);
            //加完一定要保存
            mall.SaveChanges();
        }
        catch (Exception ex)
        {
            WriteFailure(context,ex.Message);
            return;

        }
    }
    public void Edit(HttpContext context)
    {
        string data = context.Request["data"];

        IF_EntityTools tools = new IF_EntityTools();
        List<DataField> ldf = tools.getDataFieldFromJson(data);
        DataField iddf = ldf.Where(n => n.Fieldname == "id").SingleOrDefault<DataField>();
        Address address = mall.Address.Find(iddf.Value.ToString());
        address = (Address)new ConvertUtil().SetValueFromDataField(address, ldf);
        //address.address1 =。。。
        mall.Address.Attach(address);
        mall.Entry(address).State = EntityState.Modified;
        mall.SaveChanges();

    }
    public void GetList(HttpContext context)
    {
        //这些是前台列表的一下筛选
        string unitkey = context.Request["unitkey"];

        //这是存在session里的用户id
        var session = new IF_Session();
        var userid = session.UserKey;
        //用户的单位id
        var unit = session.UnitKey;
        //这里可以做权限筛选  未做

        //接受一下筛选字段
        List<SearchField> sl = new IF_EntityTools().getSearchFieldFromJson(context.Request["searchfield"]);
        //新建一个分页对象
        IF_SQLPager pager = new MSSQL_help().setPager(sl, context.Request);
        //在查询筛选中加条件
        Expression<Func<TestVO, bool>> seleWhere = o => true;//o.n_state == (int)Enum_BasicInfoStatus.Enable ;
        seleWhere = seleWhere.And(o => o.address.city == unitkey);

        var linq = from v in mall.Set<Address>()
                   join u in mall.Set<User>() on v.uid equals u.id.ToString()
                   select new TestVO
                   {
                       address = v,
                       user = u
                   };

        linq = linq.Where(seleWhere);
        base.GetPagination(pager, linq);

        Hashtable result = new MSSQL_help().setDataHashtable(pager);
        WriteInfo(context, result);
    }
}