

function tonodata() {
    window.location = bootPATH + "/nodata.htm";
}

function showTab(node) {
    var tabs = parent.mini.get("mainTabs");
    var id = "tab$" + node.text;
    var tab = tabs.getTab(id);

    if (!tab) {
        var count = 5;
        if (tabs.getTabs().length >= count) {

            mini.alert("不能同时打开" + count + "个以上的标签页", "提示");
            return;
        }
        else {
            tab = {};
            tab.name = id;
            tab.title = node.text;
            tab.showCloseButton = true;
            tab.url = node.url;
            tab.iconCls = node.iconCls + "small";
            tabs.addTab(tab);
        }
    }
    tabs.activeTab(tab);
}

function loadTab(nodetext) {
    var tree = parent.mini.get("tree");
    var nodes = tree.findNodes(function (node) {
        if (node.ML_NAME == nodetext) {
            showTab(node);
        }
    });
}

function setNavigation() {
    var title = document.title;
    var location = document.location.pathname + document.location.search;
    var nav = new Navigation(title, location);

    $.ajax({
        url: "../data/system.aspx?method=getnav",
        type: "post",
        data: { title: title, location: location },
        success: function (text) {
            var navigatlist = mini.decode(text);
            for (var i = 0; i < navigatlist.length; i++) {
                $("#navdiv").append("<a href=\"" + navigatlist[i].location + "\">" + navigatlist[i].title + "</a>>>");
            }

        }
    });
}

function setNavigationMain() {
    var title = document.title;
    var location = document.location.pathname + document.location.search;
    var nav = new Navigation(title, location);
    $.ajax({
        url: "data/system.aspx?method=getnav",
        type: "post",
        data: { title: title, location: location },
        success: function (text) {
            var navigatlist = mini.decode(text);
            for (var i = 0; i < navigatlist.length; i++) {
                $("#navdiv").append("<a href=\"" + navigatlist[i].location + "\">" + navigatlist[i].title + "</a>>>");
            }
        }
    });
}


function Navigation(title, url) {
    this.title = title;
    this.url = url;
}

function hidegrid(e, target, text) {
    var grid = document.getElementById(target);
    if (grid.style.display == 'block') {
        grid.style.display = 'none';
        e.setText("显示" + text);
    }
    else {
        grid.style.display = 'block';

        e.setText("隐藏" + text);
    }
}


function parentTiaozhuan(e) {
    window.parent.location.href = e;
}

function fromlabelModel(form) {
    var fields = form.getFields();
    for (var i = 0, l = fields.length; i < l; i++) {
        var c = fields[i];
        if (c.setReadOnly) c.setReadOnly(true);     //只读
        if (c.setIsValid) c.setIsValid(true);      //去除错误提示
        if (c.addCls) c.addCls("asLabel");          //增加asLabel外观
    }
}


function onCloseWin(e) {
    var win = mini.get(e);
    if (win.visible)
        win.hide();
}

/////////////////

//AJAX拦截器对于Session失效和权限判断
$(document).ajaxComplete(function (evt, request, settings) {
    //console.log("ajaxComplete");

    var sessionstatus = request.getResponseHeader("sessionstatus");
    // var text = request.responseText;
    //判断返回的数据内容，如果是超时，则跳转到登陆页面
    if (sessionstatus == "nopower") {
        var param = {
            permissionmethodname: decodeURI(request.getResponseHeader("permissionmethodname")),
            permissionmethodtype: decodeURI(request.getResponseHeader("permissionmethodtype")),
            permissiontype: decodeURI(request.getResponseHeader("permissiontype"))
        };
        location = bootPATH + "../noPower.html?" + $.param(param);
    }
    if (sessionstatus == "notlogin") {
        top.location = bootPATH + "../login.html";
    }
    if (sessionstatus == "error") {
        mini.alert("系统通讯失败，请重试", "提示");
    }
    if (settings.messageid) {
        mini.hideMessageBox(settings.messageid);
    }
});
$(document).ajaxError(function (event, request, settings) {
    //console.log("ajaxError");
    if (settings.messageid) {
        tools.showMessage("数据加载失败!", "系统提示", 3);
    }
});


var tools = {
    //定义一些常量
    /// <summary>
    /// web返回标识常量
    /// </summary>
    SystemResults: {
        SUCCESS: "Success",
        FAILURE: "Failure",
        NOTEXISTS: "NotExists"
    },
    DictionaryTb: {
        ROOT: "sys"
    }
};


/// <summary>
/// 遮罩
/// </summary>
tools.mask = function (optionObj) {
    mini.mask($.extend({}, {
        el: document.body,
        cls: "mini-mask-loading",
        html: "系统正在处理中，请稍候..."
    }, optionObj));
}
/// <summary>
/// 遮罩body
/// </summary>
tools.MaskBody = function (text) {
    if (text == undefined) {
        text = "系统处理中请稍等...";
    }
    mini.mask({
        el: document.body,
        cls: 'mini-mask-loading',
        html: text
    });
}
/// <summary>
/// 取消遮罩
/// </summary>
tools.UnMaskBody = function (messageId) {
    mini.unmask(typeof messageId == "undefined" ? document.body : messageId);
}

/// <summary>
///弹出消息框
/// <summary>
///<return>
/// messageId
///<return>
tools.showMessage = function (msg, title, type, otherPropObj) {
    var icon = "mini-messagebox-info";
    if (type == "i" || type == 1)
        icon = "mini-messagebox-info";
    else if (type == "w" || type == 2)
        icon = "mini-messagebox-warning";
    else if (type == "e" || type == 3)
        icon = "mini-messagebox-error";
    else if (type == "q" || type == 4)
        icon = "mini-messagebox-question";
    else if (type == "wt" || type == 5)
        icon = "mini-messagebox-waiting";
    else if ((typeof type === "string") && type.length > 2)
        icon = type;

    return mini.showMessageBox($.extend({}, {
        message: msg,
        title: title,
        buttons: ["ok"],
        iconCls: icon
    }, otherPropObj));
}

/// <summary>
///弹出消息Tips
/// <summary>
tools.showMsgTips = function (content, type, x, y, timeout) {
    var state = "default";
    if (type === "s") {
        state = "success";
    } else if (type === "w") {
        state = "warning";
    } else if (type === "d" || type === "e") {
        state = "danger";
    } else if (type === "i") {
        state = "info";
    }

    if (x == undefined) {
        x = "center";
    }
    if (y == undefined) {
        y = "center";
    }

    if (timeout == undefined) {
        timeout = 3000;
    }

    mini.showTips({
        content: content,
        state: state,
        x: x,
        y: y,
        timeout: timeout
    });
}


/// <summary>
///获取表单信息
/// </summary>
tools.getControlText = function (parent, except) {
    var data = [];
    $.each(parent.getFields(), function (index, control) {
        if (except == undefined || (except.indexOf("," + control.name + ",") < 0 && except != control.name)) {
            var controldata = new dataField(control.name, "", control.value);
            // data.push(new dataField(control.name,co))
            switch (control.type) {
                case "textboxlist":
                case "combobox":
                case "buttonedit":
                case "datepicker":
                case "checkbox":
                    controldata.Fieldtext = control.text;
                    break;
                case "radiobuttonlist":
                    controldata.Fieldtext = control.getSelected().text;
                    break;
                case "listbox":
                case "checkboxlist":
                    var text = "";
                    $.each(control.getSelecteds(), function (index, Selected) {
                        if (index == 0) {
                            text += Selected.text;
                        }
                        else {

                            text += "," + Selected.text;
                        }
                    });
                    controldata.Fieldtext = text;
                    type = 3;
                    break;
            }
            data.push(controldata);
        }
    });

    return data;
}
/// <summary>
///通过NAME设置表单信息（div）
/// </summary>
tools.setDataByName = function (parent, obj) {
    if (obj != null) {
        jQuery.each(obj, function (i, val) {
            //对DOM进行赋值
            var child;
            if (parent == null) {
                child = $("[name = " + i + "]");
            } else {
                child = parent.find("[name = " + i + "]");
            }
            if (child.length > 0) {
                if (child.attr("dateformat")) {//主要用于格式化日期型字段
                    child.html(mini.formatDate(val, child.attr("dateformat")));
                } else if (child.attr("renderer")) {//主要用于格式化枚举型字段
                    var NewValue = eval(child.attr("renderer") + "(" + val + ")");
                    child.html(NewValue);
                } else {
                    child.html(val);
                }
            }
            //对MINIUI进行赋值
            if (parent == null) {
                child = mini.getsbyName(i);
            }
            else {
                child = mini.getsbyName(name, parent);
            }
            if (child.length > 0) {
                for (var x = 0; x < child.length; x++) {
                    child[x].setValue(val);
                }
            }
        });
    }
}
/// <summary>
///通过ID设置表单信息（div）
/// </summary>
tools.setDataById = function (parent, obj) {
    if (obj != null) {
        jQuery.each(obj, function (i, val) {
            if (parent == null) {
                child = $("[id = " + i + "]");
            } else {
                child = parent.find("[id = " + i + "]");
            }

            if (child.length > 0) {
                if (child.attr("dateformat")) {//主要用于格式化日期型字段
                    child.html(mini.formatDate(val, child.attr("dateformat")));
                } else if (child.attr("renderer")) {//主要用于格式化枚举型字段
                    var NewValue = eval(child.attr("renderer") + "(" + val + ")");
                    child.html(NewValue);

                } else {
                    child.html(val);
                }
            }
        });
    }
}
/// <summary>
///从JS对象中获取对应文本
/// </summary>
tools.getTextFromEnum = function (value, obj) {
    var text = value;
    jQuery.each(obj, function (i, val) {
        if (val.id == value) {
            text = val.text;
            return false;
        }
    });
    return text;
}


/// <summary>
///页面数据控件对象
/// </summary>
function dataField(fieldName, fieldText, value) {
    this.Fieldname = fieldName;
    this.Fieldtext = fieldText;
    this.Value = value;
}

/// <summary>
///SearchField对象
/// </summary>
function SearchField(fieldName, op, value, andOr, type) {

    this.fieldname = fieldName;
    this.optype = op;
    this.value = value;
    this.AndOr = andOr;
    this.type = type;
}

if (!window.UserControl) window.UserControl = {};

UserControl.MultiSearchWindow = function () {

    UserControl.MultiSearchWindow.superclass.constructor.call(this);

    this.initComponents();
    this.bindEvents();
}

mini.extend(UserControl.MultiSearchWindow, mini.Window,
{
    uiCls: 'eflying-multisearchwindow',
    id: '',
    SearchFieldList: null,
    initComponents: function () {

        var that = this;
        // 创建WINDOW

        var toolbar = ' <a name="add" class="mini-button" iconCls="icon-add" plain="true">新增</a>';
        var footer = '<a name="submit" class="mini-button" iconCls="icon-ok" plain="true">确定</a>';
        //var body = '<div name="listform" class="form">' +
        //       '<ul class="MutiSearchBox" >' +
        //       '</ul>' +
        //       '</div>';
        var body = '<div id="' + this._id + '_form" class="form">' +
       '<ul class="MutiSearchBox" id="' + this._id + '_ul">' +
       '</ul>' +
       '</div>';
        var id = that.id;
        $(that.getToolbarEl()).empty();
        $(that.getFooterEl()).empty();
        $(that.getBodyEl()).empty();

        that.set({
            title: "添加高级查询条件",
            style: "width: 580px; height: 300px;",
            showFooter: true,
            showToolbar: true,
            footerStyle: "text-align:center;padding: 5px;",
            toolbarStyle: "text-align:left;padding: 5px;",
            toolbar: toolbar,
            body: body,
            footer: footer
        });

        //        this.setBody(body);
    },
    load: function (modellist) {
        var that = this;
        $.ajax({
            url: bootPATH + "../data/base.aspx?method=SearchFieldListFromModel",
            data: { model: modellist },
            async: false,
            success: function (text) {
                that.SearchFieldList = mini.decode(text);
            }

        });



    },
    bindEvents: function () {
        var that = this;
        this.addBtn = mini.getbyName('add', this);
        this.addBtn.on('click', function (e) {

            var ComboBoxData = [];
            $.each(that.SearchFieldList, function (name, value) {

                ComboBoxData.push({ id: value.fieldname, text: value.titlename });
            });
            var index = $("#" + that._id + "_ul").find("li").length + 1;
            var li = '<li id="' + that._id + '_ul_li' + index + '"><span id="' + that._id + '_ul_li_andor' + index + '"></span>字段选择：</li>';
            $("#" + that._id + "_ul").append(li);
            andor = $("#" + that._id + "_ul_li_andor" + index);
            //创建AND和OR
            var ANDORComboBox = new mini.ComboBox();
            ANDORComboBox.set({
                id: that._id + "_ANDOR_" + index,
                style: "width: 50px;",
                valueField: "id",
                textField: "text",
                required: true,
                data: [{ id: 1, text: '并且' }, { id: 2, text: '或者' }]

            });
            ANDORComboBox.render(andor[0]);
            ANDORComboBox.select(0);
            li = $("#" + that._id + "_ul_li" + index);
            //初始化筛选字段下拉框
            var ComboBox = new mini.ComboBox();
            ComboBox.set({
                id: that._id + "_select_" + index,
                style: "width: 100px;",
                valueField: "id",
                textField: "text",
                required: true,
                showPopupOnClick: true,
                data: mini.decode(ComboBoxData),
                onvaluechanged: function (e) {
                    that.setOPCombobox(that._id, e.value, index);

                }
            });
            ComboBox.render(li[0]);
            ComboBox.select(0);
            //初始化删除按钮
            var Button = new mini.Button();
            Button.set({
                id: that._id + "_del_" + index,
                iconCls: "icon-remove",
                text: "删除",
                onclick: function (e) {
                    that.removelist(this.id);

                }
            });
            Button.render(li[0]);
        });


        this.submitBtn = mini.getbyName('submit', this);
        this.submitBtn.on('click', function (e) {
            that.fire("startsearch");
        });
    },
    /// <summary>
    ///删除当前行
    /// </summary>
    removelist: function (id) {
        var c = mini.get(id).getEl().parentNode;
        var p = mini.get(id).getEl().parentNode.parentNode.removeChild(c);
    },
    /// <summary>
    ///创建运算符选择框和查询关键字输入框
    /// </summary>
    setOPCombobox: function (id, fieldname, index) {
        var that = this;
        $("#" + id + "_select_" + index + "_span").remove();
        //var SearchFieldList = mini.decode(mini.get(id + "_SearchFieldList").getValue());
        $.each(that.SearchFieldList, function (name, value) {
            if (value.fieldname == fieldname) {
                //初始化运算符选择下拉列表
                var ComboBox = new mini.ComboBox();
                ComboBox.set({
                    id: id + "_OPType_" + index,
                    style: "width: 60px;",
                    valueField: "OPType",
                    textField: "OPTypeText",
                    required: true,
                    showPopupOnClick: true,
                    data: mini.decode(value.clientOptype)

                });
                var li = $("#" + id + "_select_" + index);
                li.after('<span style=" padding-left:5px;" id="' + li.attr("id") + '_span">运算符：</span>');
                var span = $("#" + li.attr("id") + "_span");
                ComboBox.render(span[0]);
                ComboBox.select(0);

                span.append('<span style=";padding-left:5px">值：</span>');
                //初始化查询关键字输入框
                if (value.type == 1)//文本型
                {
                    var TextBox = new mini.TextBox();
                    TextBox.set({
                        name: id + "_SearchValue_" + index,
                        id: id + "_SearchValue_" + index,
                        style: "width: 100px;",
                        required: true

                    });
                    TextBox.render(span[0]);
                }
                if (value.type == 4)//日期型
                {
                    var DatePicker = new mini.DatePicker();
                    DatePicker.set({
                        name: id + "_SearchValue_" + index,
                        id: id + "_SearchValue_" + index,
                        style: "width: 100px;",
                        required: true,
                        format: "yyyy-MM-dd H:mm:ss",
                        timeFormat: "H:mm:ss",
                        showTime: true,
                        showOkButton: true
                    });

                    DatePicker.render(span[0]);
                }
                if (value.type == 2 || value.type == 6)//数值型
                {
                    var Spinner = new mini.Spinner();
                    Spinner.set({
                        name: id + "_SearchValue_" + index,
                        id: id + "_SearchValue_" + index,
                        style: "width: 100px;",
                        required: true,
                        maxValue: 9999999
                    });

                    Spinner.render(span[0]);
                    if (value.type == 6) {
                        Spinner.setDecimalPlaces(2);

                    }
                }
                if (value.type == 3)//下拉型
                {
                    var ComboBox = new mini.ComboBox();
                    ComboBox.set({
                        name: id + "_SearchValue_" + index,
                        id: id + "_SearchValue_" + index,
                        style: "width: 100px;",
                        valueField: "id",
                        textField: "text",
                        required: true,
                        showPopupOnClick: true
                    });
                    if (value.url == null) {
                        ComboBox.setData(mini.decode(value.data));
                    }
                    else {

                        ComboBox.setUrl(value.url);
                    }
                    ComboBox.render(span[0]);
                    ComboBox.select(0);
                }




            }

        });
    },
    GetSearchFieldList: function GetSearchFieldList() {

        var that = this;
        var datalist = [];
        var ul = $("#" + that._id + "_ul");
        if (ul.find("li").length > 0) {
            $("#" + that._id + "_ul li").each(function (index) {
                var data = that.getSearchField(index + 1);
                datalist.push(data);
            }
            );
        }
        return datalist;
    },

    /// <summary>
    ///根据Li返回SearchField对象
    /// </summary>
    getSearchField: function getSearchField(index) {

        var fieldname = mini.get(this._id + "_select_" + index).getValue();
        var optype = mini.get(this._id + "_OPType_" + index).getValue();
        var value = mini.get(this._id + "_SearchValue_" + index).getValue();
        var AndOr = mini.get(this._id + "_ANDOR_" + index).getValue();
        var control = mini.get(this._id + "_SearchValue_" + index).type;
        var type;
        switch (control) {
            case "TextBox":
                type = 1;
                break;
            case "Spinner":
                type = 2;
                break;
            case "ComboBox":
                type = 3;
                break;
            case "DatePicker":
                type = 4;
                break;
            case "CheckBox":
                type = 5;
                break;
            default:
                type = 1;
        }
        return new SearchField(fieldname, optype, value, AndOr, type);

    },
    /// <summary>
    ///验证高级查询表单是否合法
    /// </summary>
    isValid: function () {
        var form = new mini.Form("#" + this._id + "_form");
        var data = form.getData();
        form.validate();
        return form.isValid();
    },

    getAttrs: function (el) {
        var attrs = UserControl.MultiSearchWindow.superclass.getAttrs.call(this, el);
        mini._ParseString(el, attrs,
            ["onstartsearch"]
        );
        return attrs;
    }

});
mini.regClass(UserControl.MultiSearchWindow, "multisearchwindow");


//继承MINIUI系统COMBOBOX，加入同步异步属性、是否显示全部选项
if (!window.UserControl) window.UserControl = {};
UserControl.eflying_combobox = function () {

    UserControl.eflying_combobox.superclass.constructor.call(this);

    this.initComponents();
    this.bindEvents();
}
mini.extend(UserControl.eflying_combobox, mini.ComboBox, {
    uiCls: 'eflying_combobox',
    value: "-1",
    async: false,
    showallValue: "-1",
    showallText: "全部",
    bindEvents: function () {

    },
    initComponents: function () {

    },
    setShowall: function (value) {

        this.showall = value;
    },
    setShowallValue: function (value) {

        this.showallValue = value;
    },
    setShowallText: function (value) {

        this.showallText = value;
    },
    setAsync: function (value) {
        this.async = value;
    },
    setUrl: function (value) {
        this.url = value;
        this.load(value);
    },
    load: function (value) {
        var that = this;
        $.ajax({
            url: value,
            type: "post",
            async: that.async,
            success: function (text) {
                that.setData(mini.decode(text));
                if (that.showall == true) {
                    var mydata = that.getData();
                    mydata.add({ id: that.showallValue, text: that.showallText });
                    that.setData(mydata);
                    that.setValue(that.value);
                }
            }

        });


    },
    getAttrs: function (el) {
        attrs = UserControl.eflying_combobox.superclass.getAttrs.call(this, el);
        mini._ParseBool(el, attrs,
            ["async"]
        );
        mini._ParseBool(el, attrs,
         ["showall"]
     );
        mini._ParseString(el, attrs,
 ["showallValue"]
);
        mini._ParseString(el, attrs,
["showallText"]
);
        return attrs;
    }
});
mini.regClass(UserControl.eflying_combobox, "eflying_combobox");


function closeWin(winid, formid) {
    var win = mini.get(winid);
    if (win.visible) {
        win.hide();
        if (formid != undefined) {
            clearForm(formid);
        }
    }
}

function clearForm(id) {
    var form = new mini.Form("#" + id);
    form.reset();
}

function ShowMessage(title, msg, type) {
    var icon = "mini-messagebox-info";
    if (type == 2)
        icon = "mini-messagebox-warning";
    else if (type == 3)
        icon = "mini-messagebox-error";

    mini.showMessageBox({
        //        width: 250,
        title: title,
        buttons: ["ok"],
        message: msg,
        iconCls: icon
    });
}

function getBirthdayFromIdCard(idCard) {
    var birthday = "";
    if (idCard != null && idCard != "") {
        if (idCard.length == 15) {
            birthday = "19" + idCard.substr(6, 6);
        } else if (idCard.length == 18) {
            birthday = idCard.substr(6, 8);
        }
        birthday = birthday.replace(/(.{4})(.{2})/, "$1-$2-");
    }
    return birthday;
}

function isPoneAvailable(str) {
    var myreg = /^[1][3,4,5,7,8][0-9]{9}$/;
    if (!myreg.test(str)) {
        return false;
    } else {
        return true;
    }
}

function checkNumber(theObj) {
    var reg = /^[0-9]+.?[0-9]*$/;
    if (reg.test(theObj)) {
        return true;
    }
    return false;
}

function showUrlWin(flowid, id, title, url) {
    var url = url + "?instanceid=" + id + "&flowid=" + flowid;
    var win = mini.open({
        url: url,
        title: title,
        width: 900,
        height: 600,
        showModal: false,
        showMaxButton: true,
        showCollapseButton: true,
        showShadow: true,
        allowResize: true
    });
    win.max();
    win.show();
}

function DX(num) {
    var strOutput = "";
    var strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分';
    num += "00";
    var intPos = num.indexOf('.');
    if (intPos >= 0)
        num = num.substring(0, intPos) + num.substr(intPos + 1, 2);
    strUnit = strUnit.substr(strUnit.length - num.length);
    for (var i = 0; i < num.length; i++)
        strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(num.substr(i, 1), 1) + strUnit.substr(i, 1);
    return strOutput.replace(/零角零分$/, '整').replace(/零[仟佰拾]/g, '零').replace(/零{2,}/g, '零').replace(/零([亿|万])/g, '$1').replace(/零+元/, '元').replace(/亿零{0,3}万/, '亿').replace(/^元/, "零元");
}


function getSex(idCard) {
    if (idCard.length == 15) {
        return idCard.substring(14, 15) % 2;
    } else if (idCard.length == 18) {
        return idCard.substring(14, 17) % 2;
    } else {
        //不是15或者18,null  
        return null;
    }
}

/*根据出生日期和指定算出年龄*/
function jsGetAge(strBirthday,d) {
    var returnAge;
    var strBirthdayArr = strBirthday.split("-");
    var birthYear = strBirthdayArr[0];
    var birthMonth = strBirthdayArr[1];
    var birthDay = strBirthdayArr[2];

    var nowYear = d.getFullYear();
    var nowMonth = d.getMonth() + 1;
    var nowDay = d.getDate();

    if (nowYear == birthYear) {
        returnAge = 0;//同年 则为0岁  
    }
    else {
        var ageDiff = nowYear - birthYear; //年之差  
        if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay;//日之差  
                if (dayDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
            else {
                var monthDiff = nowMonth - birthMonth;//月之差  
                if (monthDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
        }
        else {
            returnAge = -1;//返回-1 表示出生日期输入错误 晚于今天  
        }
    }

    return returnAge;//返回周岁年龄  

}