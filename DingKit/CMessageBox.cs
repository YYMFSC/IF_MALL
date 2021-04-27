using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DingKit;
using System.Text;

/// <summary>
///CMessageBox 的摘要说明
/// </summary>
public class CMessageBox
{
    public const string Skin = "default";
    public CMessageBox()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 弹出框（不带按钮）
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void  AlertBox(System.Web.UI.Page page,string msg)
    {
        string str = " document.onLoad=artDialog({ id: 'warning',icon: 'warning',lock: true,content: " + msg + ",ok: true});";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "AlertBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "AlertBox", js);
        }
    }
   
    /// <summary>
    /// 出错提示框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void ErrorBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'error',lock: true,background: '#600',opacity: 0.87,skin: '" + Skin + "',content: '" + msg + "'});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ErrorBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ErrorBox", js);
        }
    }

    /// <summary>
    /// 重新登录对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    /// <param name="Url">登录页面Url</param>
    public static void ReLoadBox(System.Web.UI.Page page, string msg,string Url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'warning',lock: true,skin: '" + Skin + "',window: 'top',content: '" + msg + "',okValue:'确定',ok:function(){window.top.location='" + Url + "';},close:function(){window.top.location='" + Url + "';}});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReLoadBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReLoadBox", js);
        }
    }

    /// <summary>
    /// 跳转对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    /// <param name="Url">登录页面Url</param>
    public static void ReDirectBox(System.Web.UI.Page page, string msg, string Url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'warning',lock: true,skin: '" + Skin + "',window: 'self',content: '" + msg + "',okValue:'确定',ok:function(){location='" + Url + "';},close:function(){location='" + Url + "';}});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReDirectBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReDirectBox", js);
        }
    }

    /// <summary>
    /// 跳转对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    /// <param name="Url">登录页面Url</param>
    public static void ReDirectBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'warning',lock: true,skin: '" + Skin + "',window: 'self',content: '" + msg + "',okValue:'确定',ok:function(){window.history.go(-1);},close:function(){window.history.go(-1);}});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReDirectBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReDirectBox", js);
        }
    }

    /// <summary>
    /// 重新登录对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    /// <param name="Url">登录页面Url</param>
    public static void ReLoadBox1(System.Web.UI.Page page, string msg, string Url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'error',lock: true,background: '#600',opacity: 0.87,skin: '" + Skin + "',window: 'top',content: '" + msg + "',yesText:'确定',yesFn:function(){window.top.location='" + Url + "';}});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReLoadBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReLoadBox", js);
        }
    }


    /// <summary>
    /// 重新登录对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    /// <param name="Url">登录页面Url</param>
    public static void SuccReLoadBox(System.Web.UI.Page page, string msg, string Url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'succeed',lock: true,skin: '" + Skin + "',window: 'top',content: '" + msg + "',okVal:'确定',ok:function(){window.location='" + Url + "';}});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReLoadBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReLoadBox", js);
        }
    }

    /// <summary>
    /// 警告框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void WornBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'warning',lock: true,skin: '" + Skin + "',content: '" + msg + "'});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ErrorBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ErrorBox", js);
        }
    }


    /// <summary>
    /// 成功提示框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void SucceedBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'succeed',time:2,skin: '" + Skin + "',content: '" + msg + "'});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "SucceedBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "SucceedBox", js);
        }

    }

    /// <summary>
    /// 成功提示框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void WriteJS(System.Web.UI.Page page, string strJS)
    {

        string js = CFun.FortmatJavaScript(strJS);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "strJS"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "strJS", js);
        }

    }

    /// <summary>
    /// 成功提示框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void SucceedBoxSub(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'succeed',time:4,skin: '" + Skin + "',content: '" + msg + "'}); DialogCLoseFlash();");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "SucceedBoxSub"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "SucceedBoxSub", js);
        }

    }

    /// <summary>
    /// 成功提示框关闭窗口
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void SucceedBoxCloseSub(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'succeed',time:4,skin: '" + Skin + "',content: '" + msg + "'}); art.dialog.close();");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "SucceedBoxSub"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "SucceedBoxSub", js);
        }

    }

    /// <summary>
    /// 弹出框（不带按钮）
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void ConfirmBox(System.Web.UI.Page page, string msg)
    {
        string str = "ConfirmBox('" + msg + "');";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ConfirmBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ConfirmBox", js);
        }
    }



    public static string GetConfirmJs(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("art.dialog({icon: 'warning',lock: true,skin: '" + Skin + "',content: '" + msg + "',ok: true,cancel: function(){return false;}});");
        return sb.ToString(); 
    }
    /// <summary>
    /// 成功提示框
    /// </summary>
    /// <param name="msg">消息</param>
    public static void Reflesh(System.Web.UI.Page page)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("location.reload();");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "Reflesh"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "Reflesh", js);
        }

    }

    /// <summary>
    /// 弹出进度条页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="DoPage">页面名称（不含后缀）</param>
    /// <param name="Title">标题</param>
    public static void OpenProcessPage(System.Web.UI.Page page, string DoUrl, string DoPage, string Title, string Reflash)
    {
        string str = "StartProcess('" + DoUrl + "', '" + DoPage + "', '" + Title + "','" + Reflash + "')";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenProcessPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenProcessPage", js);
        }
    }


    /// <summary>
    /// 弹出进度条页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="DoPage">页面名称（不含后缀）</param>
    /// <param name="Title">标题</param>
    public static void OpenProcessPage(System.Web.UI.Page page, string DoUrl, string DoPage, string Title)
    {
        string str = "StartProcessNoFlesh('" + DoUrl + "', '" + DoPage + "', '" + Title + "')";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenProcessPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenProcessPage", js);
        }
    }




    /// <summary>
    /// 弹出页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="NewPageUrl">页面URL</param>
    /// <param name="Title">标题</param>
    public static void OpenPage(System.Web.UI.Page page, string NewPageUrl, string Title)
    {
        OpenPage(page, NewPageUrl, Title, true, false, "");
    }

    /// <summary>
    /// 弹出页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="NewPageUrl">页面URL</param>
    /// <param name="Title">标题</param>
    /// <param name="Reflash">是否刷新</param>
    public static void OpenPage(System.Web.UI.Page page, string NewPageUrl, string Title, bool Reflash)
    {
        OpenPage(page, NewPageUrl, Title, Reflash, false, "");
    }


    /// <summary>
    /// 弹出页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="NewPageUrl">页面URL</param>
    /// <param name="Title">标题</param>
    /// <param name="Reflash">是否刷新</param>
    /// <param name="FullScrean">是否全屏</param>
    public static void OpenPage(System.Web.UI.Page page, string NewPageUrl, string Title, bool Reflash, bool FullScrean)
    {
        OpenPage(page, NewPageUrl, Title, Reflash, FullScrean, "");
    }

    /// <summary>
    /// 弹出页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="NewPageUrl">页面URL</param>
    /// <param name="Title">标题</param>
    /// <param name="Reflash">是否刷新</param>
    /// <param name="FullScrean">是否全屏</param>
    /// <param name="ReLoadPage">关闭后重定向页面URL</param>
    public static void OpenPage(System.Web.UI.Page page, string NewPageUrl, string Title, bool Reflash, bool FullScrean, string ReLoadPage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" art.dialog.open('" + NewPageUrl + "', { ");
        sb.Append(" title: '" + Title + "',");
        sb.Append(" skin: 'facebook',");
        sb.Append(" lock: true,");
        if (FullScrean == true)
        {
            sb.Append(" width: '100%',");
            sb.Append(" height: '100%',");
            sb.Append(" left: '0%',");
            sb.Append(" top: '0%',");
            sb.Append(" fixed: true,");
            sb.Append(" resize: false,");
            sb.Append(" drag: false,");
        }
       
        if (ReLoadPage != "")
        {
            sb.Append(" okVal: '关闭',");
            sb.Append(" ok: function (){");
            sb.Append(" var win = art.dialog.open.origin;");
            sb.Append(" win.location='" + ReLoadPage + "'; ");
            sb.Append(" return false;");
            sb.Append(" }");
        }
        else
        {
            if (Reflash == true)
            {
                sb.Append(" okVal: '返回',");
                sb.Append(" ok: function (){");
                sb.Append(" var win = art.dialog.open.origin;");
                sb.Append(" win.location.reload(); ");
                sb.Append(" return false;");
                sb.Append(" }");
            }
            else
            {
                sb.Append(" okVal: '返回',");
                sb.Append(" ok: function (){");
                sb.Append(" return false;");
                sb.Append(" }");
            }
        }

        sb.Append("  });");


        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenPage", js);
        }
    }

    public static void ShowTips(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("art.dialog.tips('" + msg + "', 1.5);");
        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenPage", js);
        }
    }

    /// <summary>
    ///关闭弹出框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    public static void Close(System.Web.UI.Page page)
    {
        string str = " art.dialog.close();";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "close"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "close", js);
        }
    }

    /// <summary>
    /// 重新登录对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="Url">登录页面Url</param>
    public static void ReLoad(System.Web.UI.Page page,  string Url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("window.top.location='" + Url + "';");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReLoad"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReLoad", js);
        }
    }
}