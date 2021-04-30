
function SinglesetuploadConfig(singleFilekindList) {
    ///<summary>单个上传附件形式</summary>
    ///<param name="singleFilekindList">SingleFilekind对象的数组</param>

    var messageid = mini.loading("正在读取附件...，请稍后", "系统处理中");
    $.each(singleFilekindList, function (i, n) {
        var objectid = singleFilekindList[i].objectid;
        var kindid = singleFilekindList[i].kindid;
        var uiid = singleFilekindList[i].uiid;
        var uiTitleid = singleFilekindList[i].uiTitleid;
        var isedit = singleFilekindList[i].isedit;
        var urltype = singleFilekindList[i].urltype;

        var singleFilekind = singleFilekindList[i];
        $.ajax({
            url: bootPATH + "../Controllers/Attachment/UploadBase.ashx?method=getSingleAttachmentKindAndFileInfo",
            type: "post",
            dataType: "json",
            data: {
                atid: kindid,
                objectid: objectid,
                urltype: urltype
            },
            success: function (retText) {
                var ret = mini.decode(retText);
                if (ret.return_code == "Success") {
                    var res = ret.objectClass;
                    var attachmentType = res.attachmentType;                //附件种类
                    var fileTypeList = res.fileTypeList;                    //文档类型
                    var attachmentList = res.attachmentList;                //附件

                    if (attachmentType == null) {
                        $("#" + uiid).html("对不起，没有这种附件类型，请检查参数是否正确");
                    }
                    else {
                        var isupload = false;
                        if (!isedit && res.isWorkFlow) {
                            if (functionlist.isAllUpload) {
                                isupload = true
                            } else if (functionlist.uploadIds && $.inArray(attachmentType.at_id.toLowerCase(), functionlist.uploadIds.toLowerCase().split(',')) > -1) {
                                isupload = true
                            }
                        } else {
                            isupload = isedit;
                        }
                        attachmentType = new AttachmentType(attachmentType, isupload);

                        $("#" + uiTitleid).html("<span class=\"tipyellow\" title=\"" + attachmentType.memo + "\">" + attachmentType.name + "：</span>");
                        var filehtml = _renderSingleHtml(attachmentType, attachmentList, isupload, urltype);
                        $("#" + uiid).html(filehtml);
                        renderGPYWin($("#" + uiid)); //绘制高拍仪窗口
                        mini.parse();

                        _singleUploadBindDatas(attachmentType, attachmentList, objectid);
                        _singleUploadBindFlyingAlbum([attachmentType], attachmentList, objectid, [singleFilekind]);

                        if (isupload) {
                            for (var a = 0; a < attachmentList.length; a++) {
                                var attachment = new Attachment(attachmentList[a]);
                                $("#delSinglefile" + attachment.no).unbind().bind("click", { attachment: attachment, attachmentType: attachmentType, singleFilekindList: [singleFilekind] }, function (event) {
                                    delSingleFilekindfile(event.data.attachment, event.data.attachmentType, event.data.singleFilekindList);
                                });
                            }

                            //绑定高拍仪窗口事件
                            bindGPYWinEvent([attachmentType], objectid, true, [singleFilekind]);

                            //绑定本地上传事件
                            var typeObj = getMimeType_upload(attachmentType, fileTypeList)
                            singleSetupload(objectid, typeObj, attachmentType, "Singleuploadfilelist" + attachmentType.no, [singleFilekind], urltype);
                        }
                    }
                } else {
                    $("#" + uiid).html("对不起，附件加载失败！");
                }
                mini.hideMessageBox(messageid);
            }
        });
    });
}

function _renderSingleHtml(attachmentType, attachmentList, isupload, urltype) {
    var filehtml = "";
    if (isupload) {
        filehtml = "<div  id=\"flying_SingleFileUploader" + attachmentType.no + "\" style=\" margin-left:10px;\">";
        if ($.inArray("图片", attachmentType.filetype.split(',')) >= 0
            //|| $.inArray("文档", attachmentType.filetype.split(',')) >= 0
            )
            filehtml += "<a  class=\"SingleUploadLink\" href=\"javascript:;\"  id=\"btnOpenGPY" + attachmentType.no + urltype + "\"  style='margin-right: 20px;'>拍照上传</a>";

        filehtml += "<a  class=\"SingleUploadLink\" href=\"javascript:;\"  id=\"flying_SingleFileUploadbutton" + attachmentType.no + urltype + "\"    >本地上传</a><br>";
        filehtml += "<span class='filememo'>类型：(" + attachmentType.filetype + "）最多上传：(" + (attachmentType.uploadnum ? attachmentType.uploadnum : "无限") + "个） 大小：(" + attachmentType.size + "MB）";
        filehtml += "</div>";
    }
    filehtml += "<div style=\" width:100%; \" id=\"Singleuploadfilelist" + attachmentType.no + "\" class=\"singleuploadKind\" >"
    filehtml += "<table  class=\"Singlefiletable\" cellspacing=\"0\" border=\"0\" cellpadding=\"0\">"
    for (var a = 0; a < attachmentList.length; a++) {
        var attachment = new Attachment(attachmentList[a]);
        filehtml += "<tr ><td width=\"20\" ><img  class=\"SinglefileIcon\" src=\"" + bootPATH + "../plupload/image/" + attachment.extension + ".gif\"></img></td>";
        //filehtml += "<td ><a target=\"_blank\" id=\"Sinfile" + attachment.id + "\" href=\"../data/Uploader.ashx?method=download&fileid=" + attachment.id + "\" class=\"uploadbutton\">" + attachment.name + "</a></td>";
        filehtml += "<td class=\"filetd\"><span class=\"uploadbutton\" id=\"Sinfile" + attachment.id + "\" > " + attachment.name + "</span></td>";

        if (isupload) {
            filehtml += "<td width=\"60\"><a  id=\"delSinglefile" + attachment.no + "\"  href=\"javascript:;\" class=\"delbutton\">删除附件</a></td><tr>";
        }
    }
    filehtml += "</table>"
    filehtml += "</div>";
    return filehtml;
}

function singleSetupload(objectId, types, attachmentType, uiId, singleFilekindList, urltype) {
    var a = "";
    var uploadUrl = bootPATH + "../Controllers/Attachment/Uploader.ashx?method=Setupload";
    var container = "flying_SingleFileUploader" + attachmentType.no;
    var browsebutton = "flying_SingleFileUploadbutton" + attachmentType.no + urltype;

    /**
        关于ie下不支持分块上传导致文件过大，无法上传问题的解决办法：
        1、web.config下设置
            <system.web>
                <httpRuntime maxQueryStringLength="102400" />
            </system.web>
            设置maxQueryStringLength的最大长度，单位kb
        2、IIS下网站限制
            （1）、网站asp-->行为-->限制属性-->最大请求实体主题限制
            （2）、请求筛选-->编辑功能限制-->请求限制-->允许最大内容长度
            http://blog.163.com/da7_1@126/blog/static/1040726782011101694624/
    **/

    var uploader = new plupload.Uploader({
        runtimes: 'html5,flash',                                  //默认：html5,flash,silverlight,html4 
        browse_button: browsebutton,                        //触发文件选择对话框的DOM元素，当点击该元素后便后弹出文件选择对话框。该值可以是DOM元素对象本身，也可以是该DOM元素的id
        container: container,                               //用来指定Plupload所创建的html结构的父容器，默认为前面指定的browse_button的父元素。该参数的值可以是一个元素的id,也可以是DOM元素本身。
        url: uploadUrl,                                     //服务器端接收和处理上传文件的脚本地址，可以是相对路径(相对于当前调用Plupload的文档)，也可以是绝对路径
        flash_swf_url: bootPATH + '../plupload/Moxie.swf',
        silverlight_xap_url: bootPATH + '../plupload/Moxie.xap',
        unique_names: true,                                 //默认：false;当值为true时会为每个上传的文件生成一个唯一的文件名，并作为额外的参数post到服务器端，参数明为name,值为生成的文件名。
        filters: types,                                    //详见getMimeType_upload()方法
        max_file_size: attachmentType.size + 'mb',
        chunk_size: "3mb",

        init: {
            PostInit: function () {
            },
            BeforeUpload: function (up, file) {
                var str1 = {
                    "objectid": objectId,
                    "atid": attachmentType.id,
                    "filesize": file.size,
                    "fileOrgName": file.name,
                    "fileType": file.type,
                    "urltype": urltype
                };
                document.getElementById(uiId).innerHTML += '<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></div>';
                uploader.settings.url = uploadUrl;
                uploader.settings.multipart_params = str1;
            },
            FilesAdded: function (up, files) {
                uploader.start();
            },
            UploadProgress: function (up, file) {
                document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
            },
            UploadComplete: function (up, files) {
                SinglesetuploadConfig(singleFilekindList);
            },
            ChunkUploaded: function (uploader, file, responseObject) {//当使用文件小片上传功能时，每一个小片上传完成后触发

            },
            FileUploaded: function (up, file, res) {
                if (attachmentType.uploadAction) {
                    eval(attachmentType.uploadAction + "(up,files,res)");
                }
            },
            Error: function (up, err) {
                if (err.code == -600) {
                    alert("上传附件超过指定大小,该文件类型最大支持：" + up.settings.filters.max_file_size
                        + ",您上传的文件大小为：" + plupload.formatSize(err.file.size));
                }
            }
        }
    });
    uploader.init();
}

function renderGPYWin() {
    ///<summary>绘制高拍仪上传窗口</summary>

    //如果存在则不绘制否则绘制
    if ($("#eflyingGPYWin", window.top.document.body).length == 0) {
        $(window.top.document.body).append('<div id="eflyingGPYWin" class="mini-window" title="拍照上传" style="width: 100%; height:100%" url="' + bootPATH + '../system/eloamGPY.html"></div>');
        if (top != self)
            top.mini.parse();
    }

}

function bindGPYWinEvent(newAttachmentTypes, objectid, isSingle, singleFilekindList) {
    ///<summary>绑定高拍仪窗口事件</summary>
    ///<param name="newAttachmentTypes">附件组的数组对象</param>
    ///<param name="objectid"></param>
    ///<param name="isSingle">是否为单已形式，关闭窗口刷新列表不同</param>

    var mapAttachmentTypes = $.grep(newAttachmentTypes, function (attachmentType, index) {
        return attachmentType.isupload && attachmentType.filetype.indexOf("图片") > -1;
    });

    $.each(mapAttachmentTypes, function (i, e) {
        $("#btnOpenGPY" + e.no).unbind("click").bind("click", { objid: objectid, types: mapAttachmentTypes, index: e, isSingle: isSingle, singleFilekindList: singleFilekindList },
            function (event) {

                //点击事件绑定函数
                var js_gpy = top.mini.get("eflyingGPYWin").getIFrameEl().contentWindow;
                if (!js_gpy.setFilekind) {
                    return;
                }
                js_gpy.setFilekind(event.data.objid, event.data.types, event.data.index);
                top.mini.get("eflyingGPYWin").visible ? '' : top.mini.get("eflyingGPYWin").max();
                js_gpy.OpenDevice();//打开高拍仪
                js_gpy.ClearAllImageNoCinfirm();


                //绑定高拍仪内部页面刷新事件
                var tops = $(top.mini.get("eflyingGPYWin").getIFrameEl());
                tops.unbind("load").on("load", { types: event.data.types, objid: event.data.objid, index: event.data.index }, function (event2) {
                    var js_gpy = top.mini.get("eflyingGPYWin").getIFrameEl().contentWindow;
                    if (!js_gpy.setFilekind) {
                        return;
                    }
                    js_gpy.setFilekind(event2.data.objid, event2.data.types, event2.data.index);
                    js_gpy.CloseDeviceFromParent();//关闭高拍仪
                    js_gpy.OpenDevice();//打开高拍仪
                });

                //绑定高拍仪外部窗口关闭按钮的事件
                $(top.mini.get("eflyingGPYWin").getHeaderEl()).find(".mini-tools-close").unbind("click")
                    .bind("click", { isSingle: event.data.isSingle, singleFilekindList: event.data.singleFilekindList },
                    function (event3) {

                        var js_gpy = $(top.mini.get("eflyingGPYWin").getBodyEl()).find("iframe")[0].contentWindow;
                        try {
                            if (event3.data.isSingle) {
                                SinglesetuploadConfig(singleFilekindList);
                            }
                            else {
                                //var attachmentTypes = js_gpy.getFilekind();
                                //for (var i = 0; i < attachmentTypes.length; i++) {
                                //    reloadFilelist(attachmentTypes[i], objectid);
                                //}
                                LoadInfo();
                            }
                            js_gpy.CloseDeviceFromParent();//关闭高拍仪
                        } catch (e) {

                        }
                    });

            });
    });

};

function _singleUploadBindDatas(attachmentType, attachmentList, objectid) {
    $("#Singleuploadfilelist" + attachmentType.no).data("filekind", attachmentType);
    for (var j = 0; j < attachmentList.length; j++) {
        var file = attachmentList[j];
        var attachment = new Attachment(file);
        $("#Sinfile" + attachment.id).parents("tr").first().data("file", attachment);
    }
}

function _singleUploadBindFlyingAlbum(newAttachmentTypes, attachmentList, objectid, singleFilekindList) {
    var option = {
        url: bootPATH + '../Controllers/Attachment/UploadBase.ashx?method=GetFlyingAlbumList',     //请求url地址，如果未设置datas，则发起ajax请求
        param: {},                                                  //请求参数对象
        noPreFun: function () {
            top.mini.showMessageBox({
                showModal: false,
                width: 250,
                title: "提示",
                showCloseButton: false,
                iconCls: "mini-messagebox-info",
                message: "当前为第一张图片",
                timeout: 1500
            });
        },
        noNextFun: function () {
            top.mini.showMessageBox({
                showModal: false,
                width: 250,
                title: "提示",
                showCloseButton: false,
                iconCls: "mini-messagebox-info",
                message: "当前为最后一张图片",
                timeout: 1500
            });
        },
        hideFun: function (inst) {
            SinglesetuploadConfig(singleFilekindList);
        },
        toolBtns: [{
            title: '再看一次',
            styleCls: 'albumReCycle',
            text: '再看一次',
            clickFun: function (event) {
                var inst = event.data;
                var $mainDiv = $("#" + this._mainWinId).find("." + this._mainDivClass).first();
                var $minBoxLis = $mainDiv.find("." + this._oMinBoxListClass).find("li");
                $minBoxLis.first().trigger("click");
            }
        }]
    };

    for (var a = 0; a < attachmentList.length; a++) {
        var file = attachmentList[a];
        var albumFormats = ['jpg', 'gif', 'png', 'bmp'];
        if ($.inArray(file.am_extension.toLowerCase(), albumFormats) >= 0) {
            var _option = $.extend(true, {}, option);
            for (var i = 0; i < newAttachmentTypes.length; i++) {
                if (file.ar_atid.toLowerCase() == newAttachmentTypes[i].id.toLowerCase() && newAttachmentTypes[i].isupload) {
                    var delBtn = {
                        title: '删除图片',
                        styleCls: 'albumDelPic',
                        text: '删除图片',
                        clickFun: function (event) {
                            var inst = event.data;
                            var curPicData = this.getPicData();
                            var index = this.index;
                            var len = this.getPicLen();
                            var isClose = true;
                            if (len > 1) {
                                isClose = false;
                                var nextNum = index == 0 ? index + 1 : index - 1;
                                var nextPic = this.getPicData(nextNum);
                                inst.id = "Sinfile" + nextPic[this._get(inst, "picId")];
                            }
                            var filekind = $("#" + inst.id).parents(".singleuploadKind").data("filekind");
                            delfileAlbum.apply(this, [curPicData["ar_id"], objectid, inst, curPicData["am_id"], isClose]);
                        }
                    };
                    _option.toolBtns.push(delBtn);
                }
            };

            _option.befoReqFun = function (inst) {
                var filekind = $("#" + inst.id).parents(".singleuploadKind").data("filekind");
                var file = $("#" + inst.id).parents("tr").first().data("file");
                var _param = {
                    id: file.arid
                };
                inst.settings.param = _param;
            };
            $("#Sinfile" + file.am_id).flyingAlbum(_option);

        } else {
            $("#Sinfile" + file.am_id).click([file.am_fileUrl], function (event) {
                window.open(event.data, "_blank");
            });
        }
    }
}


function delfileAlbum(arid, objectid, inst, amid, isClose) {//删除附件（图册形式）
    //this，为$.flyingAlbum对象
    var self = this;
    top.mini.confirm("确定删除附件？", "确定？",
       function (action) {
           if (action == "ok") {
               var messageid = top.mini.loading("正在处理您的请求，请稍后", "系统处理中");
               $.ajax({
                   url: bootPATH + "../Controllers/Attachment/UploadBase.ashx?method=delUploadfile",
                   type: "post",
                   dataType: "json",
                   data: { id: arid },
                   success: function (ret) {
                       top.mini.hideMessageBox(messageid);
                       if (isClose) {
                           self.hideWin();
                       } else {
                           //self._updateAlbum(inst);
                           $("#" + inst.id).trigger("click");
                           $("#Sinfile" + amid).parents("tr").first().remove();
                       }
                   },
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       top.mini.hideMessageBox(messageid);
                       top.mini.alert("操作失败", "提示");
                   }
               });
           }
       }
   );
}

//删除附件（单一形式）
function delSingleFilekindfile(attachment, attachmentType, singleFilekind) {
    mini.confirm("确定删除附件？", "确定？",
           function (action) {
               if (action == "ok") {
                   var messageid = mini.loading("正在处理您的请求，请稍后", "系统处理中");
                   $.ajax({
                       url: bootPATH + "../Controllers/Attachment/UploadBase.ashx?method=delUploadfile",
                       type: "post",
                       dataType: "json",
                       data: { id: attachment.arid },
                       success: function (ret) {
                           var res = mini.decode(ret);
                           mini.hideMessageBox(messageid);
                           if (res.return_code == "Success") {
                               SinglesetuploadConfig(singleFilekind);
                           }
                           else {
                               mini.alert("操作失败", "提示");
                           }

                       },
                       error: function (XMLHttpRequest, textStatus, errorThrown) {
                           mini.hideMessageBox(messageid);
                           mini.alert("操作失败", "提示");
                       }
                   });
               }
           }
       );
}

function getMimeType_upload(attachmentType, fileTypeList) {
    ///<summary>获取上传文件的格式类型</summary>
    ///<param name="attachmentType">单个文件组上传类型</param>
    ///<param name="fileTypeList">数组，文件类型键值对</param>
    ///return: 
    ///    mime_types : [ //只允许上传图片和zip文件
    ///      { title : "Image files", extensions : "jpg,gif,png" }, 
    ///      { title : "Zip files", extensions : "zip" }
    ///    ]


    var mimeTypes = [], mimeExtensions = [];
    for (var x = 0; x < fileTypeList.length; x++) {
        if (attachmentType.filetype.indexOf(fileTypeList[x].name) >= 0) {
            var mimeType = {
                title: fileTypeList[x].name + "(后缀为：" + fileTypeList[x].extensions + "的文件)",
                extensions: fileTypeList[x].extensions
            }
            mimeTypes.push(mimeType);
            mimeExtensions.push(fileTypeList[x].extensions);
        }
    }
    if (mimeExtensions.length > 0) {
        var mimeTypeFist = {
            title: "可上传文件（后缀为：" + mimeExtensions.join(',') + "的文件）",
            extensions: mimeExtensions.join(',')
        }
        mimeTypes.splice(0, 0, mimeTypeFist);
    }
    return mimeTypes;
}



function SingleFilekind(objectid, kindid, uiid, uiTitleid, isedit, urltype) {
    this.objectid = objectid;
    this.kindid = kindid;
    this.uiid = uiid;
    this.uiTitleid = uiTitleid;
    this.isedit = isedit;
    this.urltype = urltype;
}

function ParseAttachmentType(Eflying_AttachmentType, isupload) {
    this.no = Eflying_AttachmentType.AutoGrow;
    this.id = Eflying_AttachmentType.UID;
    this.name = Eflying_AttachmentType.Name;
    this.belong = Eflying_AttachmentType.PID;
    this.filetype = Eflying_AttachmentType.FileType;
    this.uploadnum = Eflying_AttachmentType.UploadNumMax;
    this.size = Eflying_AttachmentType.UploadSizeMax;
    this.isupload = isupload;

    this.uploadAction = null;
    this.deleteAction = null;

}

function AttachmentType(Eflying_AttachmentType, isupload) {
    this.no = Eflying_AttachmentType.at_no;
    this.id = Eflying_AttachmentType.at_id;
    this.name = Eflying_AttachmentType.at_name;
    this.belong = Eflying_AttachmentType.at_belong;
    this.filetype = Eflying_AttachmentType.at_filetype;
    this.order = Eflying_AttachmentType.at_order;
    this.state = Eflying_AttachmentType.at_state;
    this.size = Eflying_AttachmentType.at_size;
    this.memo = Eflying_AttachmentType.at_memo;
    this.has = Eflying_AttachmentType.at_has;
    this.uploadnum = Eflying_AttachmentType.at_uploadnum;
    this.uploadAction = Eflying_AttachmentType.at_uploadAction;
    this.deleteAction = Eflying_AttachmentType.at_deleteAction;
    this.isupload = isupload;
}

function Attachment(file) {
    this.no = file.am_autogrow
    this.id = file.am_id;
    this.name = file.am_name;
    this.extension = file.am_extension;
    this.fileurl = file.am_fileUrl;
    this.size = file.am_size;
    this.image = file.am_image;
    this.createMan = file.am_createMan;
    this.createTime = file.am_createTime;
    this.arno = file.ar_autogrow;
    this.arid = file.ar_id;
    this.atid = file.ar_atid;
    this.object = file.ar_object;
    this.state = file.ar_state;
}


function AlbumHasData(jqueryObj, kindid, objectid) {
    ///<summary>图册外部调用单个attachmentType</summary>
    ///<param name="jqueryObj">触发的jquery对象</param>
    ///<param name="objectid">业务主表ID</param>
    ///<param name="kindid">附件种类的id</param>

    var option = {
        url: bootPATH + "../Controllers/Attachment/UploadBase.ashx?method=getSingleAttachmentKindAndFileInfo",     //请求url地址，如果未设置datas，则发起ajax请求
        param: {
            atid: kindid,
            objectid: objectid
        },                                                  //请求参数对象
        noPreFun: function () {
            top.mini.showMessageBox({
                showModal: false,
                width: 250,
                title: "提示",
                showCloseButton: false,
                iconCls: "mini-messagebox-info",
                message: "当前为第一张图片",
                timeout: 1500
            });
        },
        noNextFun: function () {
            top.mini.showMessageBox({
                showModal: false,
                width: 250,
                title: "提示",
                showCloseButton: false,
                iconCls: "mini-messagebox-info",
                message: "当前为最后一张图片",
                timeout: 1500
            });
        },
        preloadFun: function (inst, retData) {
            var data = {};
            data.pics = retData.attachmentList;
            data.kindInfo = retData.attachmentType;
            data.currentPic = null;
            return data;
        }
    };
    $(jqueryObj).flyingAlbum(option);
}

///<summary>通过附件的CODE编号获取附件类型的ID</summary>
///<param name="code">附件的CODE编号集合，中间以逗号分隔</param>
function setAttachmentType(codes,obid,isedit) {
    eflying.MaskBody("正在获取附件类型，请稍候...");
    $.ajax({
        url: bootPATH + "../Controllers/Attachment/AttachmentAction.ashx?method=GetAttachmentTypeByCodes",
        type: 'GET',
        //async: false,
        data: {
            codes: codes
        },
        success: function (text) {
            eflying.UnMaskBody();
            var res = mini.decode(text);
            if (res.return_code == "Success") {
                var attachmentTypes = res.objectClass;
                var kind;
                var filekind = new Array();
                $.each(attachmentTypes, function (i, e) {
                    kind = new SingleFilekind(obid, e.at_id, e.at_code, "", isedit);
                    filekind.push(kind);
                });
                SinglesetuploadConfig(filekind, obid);
            } else
                mini.alert(res.return_msg);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            eflying.UnMaskBody();
            ShowMessage("系统提示", "网络异常，服务器连接失败！", "mini-messagebox-error");
        }
    });
}

///<summary>通过附件的CODE编号获取附件类型的ID</summary>
///<param name="obid">业务主表的ID</param>
///<param name="atid">附件类型的ID</param>
///<param name="tdfile">显示附件的控件ID</param>
///<param name="thfile">显示附件类型名称的控件ID，可不填</param>
///<param name="isedit">是否可上传</param>
function setFormSingleFile(obid, atid, tdfile, thfile, isedit) {
    var kind;
    var filekind = new Array();
    kind = new SingleFilekind(obid, atid, tdfile, thfile, isedit, true);
    filekind.push(kind);
    SinglesetuploadConfig(filekind, obid);
}





