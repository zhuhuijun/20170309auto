/*内容管理JS 用于提示框弹出
* 可设置Btn样式
* 使用前先初始化参数
*/

//当前Form表单名称
var formName = "";
//当前请求的Url
var currentUrl = "";
//当前Grid对象，如果不用传入null
var currentGrid = null;
//请求返回结果
var reslut = null;
//编辑提示提示信息
var EditMsg = null;
//删除提示
var DeleteMsg = null;
//删除链接
var DeleteLink = null;
//导出链接
var ExportLink = null;
//批量提交的链接
var ApprovalLink = null;
var boolEdit = null;
var boolDelete = null;
//是否可以编辑的链接
var IsEditLink = null;
//弹窗时查询数据的链接
var WindowFloatLink = null;
$(function () {
    document.oncontextmenu = function () {//屏蔽浏览器右键事件
        return false;
    };
    //获取隐藏域的值
    var boolAdd = $("#Add").val();
    var boolSreach = $("#Sreach").val();
    var boolExport = $("#Export").val();
    boolEdit = $("#Edit").val();
    boolDelete = $("#Delete").val();
    if (boolEdit == "") {
        boolEdit = "none";
    }
    if (boolDelete == "") {
        boolDelete = "none";
    }
    if (boolAdd == "") {
        $("#addBtn").attr({ "disabled": "disabled" });
    }
    if (boolSreach == "") {
        $("#searchBtn").attr({ "disabled": "disabled" });
    }
    if (boolExport == "") {
        $("#btnexport").attr({ "disabled": "disabled" });
    }
});

function buttonize(cellvalue, options, rowobject) {
    return "<a style='display:" + boolEdit + "' onclick=ContentManager.EditMsg('" + EditMsg + "','" + rowobject[0] + "') href=\"#\"><div class=\"ui-icon ui-icon-pencil\" style=\"display:inline-block\"></div></a>" +
        "<a style='display:" + boolDelete + "' onclick=ContentManager.DelMsg('" + DeleteMsg + "','" + DeleteLink + rowobject[0] + "') href=\"#\"><div class=\"ui-icon ui-icon-trash\" style=\"display:inline-block\"></div></a>";
}
//审批按钮
function buttonApproval(cellvalue, options, rowobject) {
    return "<a style='display:" + boolEdit + "' onclick=ContentManager.EditMsg('" + EditMsg + "','" + rowobject[0] + "') href=\"#\"><div class=\"ui-icon ui-icon-pencil\" style=\"display:inline-block\"></div></a> ";
}
//查询参数
function Search() {
    var postData = currentGrid.jqGrid("getGridParam", "postData");
    var otherData = $("#formSearch").serializeArray();
    otherData = ArraytoJSON.toJSON(otherData);
    postData = $.extend(postData, { customPar: otherData });
    currentGrid.jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
    $('#search_wrapper').slideToggle();
    return false;
}
//查询出被勾选的项目
$(function () {
    var id = null;
    $("#approvalSubmit").click(function () {
        var arr = document.getElementsByName("all");
        id = $.map(arr, function (item) { if (item.checked) { return item.value; } });
        if (id == "undefined" || id.length < 1) {
            ContentManager.MsgShow("没有可操作的数据！");
        }
        else {
            $.ajax({
                type: "GET",
                url: ApprovalLink + id,
                success: function (data) {
                    ContentManager.MsgShow(data);
                    currentGrid.trigger("reloadGrid", [{ page: 1}]);
                },
                error: function (error) { }
            });
        }
    });

});



//提示信息
function Tip() {
    $.blockUI({ css: {
        border: 'none',
        padding: '15px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .5,
        color: '#fff'
    }
    });
}

//导出时的查询参数表单
function exportDataForm(value) {
    $("#ExportType").val(value);
    var myForm = $("#formSearch");
    var otherData = myForm.serializeArray();
    Tip();
    $.ajax({
        type: "POST",
        url: ExportLink,
        async: true,
        data: { id: ArraytoJSON.toJSON(otherData) },
        success: function (msg) {
            $.unblockUI();
            var http = window.location.protocol + "//" + window.location.host;
            window.location.href = http + "/" + msg;
        },
        error: function (error) {
            alert(error);
        }
    });
    return false;
}

$(function () {
    $("#btnexport")
		.button()
		.click(function () {
		})
			.click(function () {
			    var menu = $(this).parent().next().show().position({
			        my: "left top",
			        at: "left bottom",
			        of: $(this).parent()
			    });
			    document.getElementById("exportcontent").innerHTML = "<span style='text-align:left; float:left;'> <a href='#' onclick='exportDataForm(this.title)'  title='xls' ><img  src='/Content/css/images/xls.png' style='margin-bottom:-3px' /><span style=' font-size:12;'>Excel文档</span> </a></span><br /><span style='text-align:left; float:left;margin-top:4px;'><a href='#' onclick='exportDataForm(this.title)'  title='doc'><img  src='/Content/css/images/doc.png' style='margin-bottom:-3px'  /><span style=' font-size:12;'>Word文档</span></a></span>";
			    document.getElementById("exportcontent").style.display = "block";

			    $(document).one("click", function () {
			        menu.hide();
			    });
			    return false;
			})
                    .parent()
                        .buttonset()
                        .next()
                        	.hide()
                        	.menu();
});

//参数初始化
var CommonParameterSet = {
    //设置初始化参数
    SetValue: function (myFormName, CurrentUrl, currentgrid) {
        formName = myFormName;
        currentUrl = CurrentUrl;
        currentGrid = currentgrid;
    },
    SetOtherValue: function (editMsg, deleteMsg, deleteLink, exportLink) {
        EditMsg = editMsg;
        DeleteLink = deleteLink;
        DeleteMsg = deleteMsg;
        ExportLink = exportLink;
    },
    SetApprovalValue: function (approvalLink) {
        ApprovalLink = approvalLink;
    },
    SetIsEditValue: function (isEditLink) {
        IsEditLink = isEditLink;
    },
    SetWindowFloatValue: function (windowFloatLink) {
        WindowFloatLink = windowFloatLink;
    }
}
//ajax请求帮助处理
var AjaxHelper = {
    //设置请求参数
    RequestSet: function (Type, Url, rData, resultOp) {
        //通过ajax请求返回空实例
        $.ajax({
            type: Type,
            url: Url,
            data: rData,
            success: function (data) {
                if (resultOp == null) {
                    $.unblockUI();
                    ContentManager.MsgShow(data);
                    currentGrid.trigger("reloadGrid", [{ page: 1}]);
                }
                else {
                    if (resultOp == 1) {
                        myDialogAdd.content(data);
                    }
                    if (resultOp == 2) {
                        myDialogEdit.content(data);
                    }
                    if (resultOp == 4) {
                        art.dialog({
                            id: 'msg',
                            title: '公告',
                            content: data,
                            width: 320,
                            height: 240,
                            left: '100%',
                            top: '100%',
                            fixed: false,
                            drag: false,
                            resize: false
                        });
                    }
                }
            },
            error: function (msg) {
                //存在错误输出
                ContentManager.MsgShow(msg.responseText);
            }
        });
    },
    //判断状态是否允许编辑
    RequestApproval: function (Type, Url, rData) {
        //通过ajax请求状态的判断
        var status = 0;
        $.ajax({
            type: Type,
            url: Url,
            async: false,
            data: rData,
            success: function (data) {
                status = data;
                if (data == "3") {
                    status = 3;
                    $.unblockUI();
                    ContentManager.MsgShow("数据已经处理，不能操作！");
                    currentGrid.trigger("reloadGrid", [{ page: 1}]);
                    return status;
                }
            },
            error: function (msg) {
                //存在错误输出
                status = "2";
            }
        });
        return status;
    }
}
var ArraytoJSON = {
    toJSON: function (arr) {
        if (arr == null) {
            return "[]";
        }
        else {
            var jsonResult = "[";
            for (var i = 0; i < arr.length; i++) {
                jsonResult += "{";
                jsonResult += "'Name':" + "'" + arr[i].name + "',";
                jsonResult += "'Value':" + "'" + arr[i].value + "'},";
            }
            var result = jsonResult.substr(0, jsonResult.length - 1);
            result += "]";
            return result;
        }
        return "[]";
    }
}
//内容管理
var ContentManager = {
    //数据删除
    DelMsg: function (title, url) {
        art.dialog({
            min: false,
            max: false,
            title: "提示",
            lock:true,
            content: "数据删除后无法恢复，您确认删除吗？",
            ok: function () {
                Tip();
                AjaxHelper.RequestSet("GET", url, null, null);
            },
            cancel: function () {
                this.close();
            }
        });
    },
    //添加
    AddMsg: function (title) {
        myDialogAdd = art.dialog({
            okValue: '保存',
            title: title,
            min: false,
            max: false,
            lock: true,
            ok: function () {
                if ($("#" + formName).bValidator().validate()) {
                    Tip();
                    ContentManager.DataUpdate();
                }
                else {
                    return false;
                }
            },
            cancel: function () {
                this.close();
            }
        });
        AjaxHelper.RequestSet("GET", currentUrl, null, 1);
    },
    //修改
    EditMsg: function (title, id) {
        //判断状态
        var myUrl = IsEditLink + id;
        if (myUrl.toString().indexOf("null") < 0) {
            var tt = AjaxHelper.RequestApproval("GET", myUrl, null);
            if (tt == "3") {
                return false;
            }
        }

        myDialogEdit = art.dialog({
            okValue: '保存',
            title: title,
            min: false,
            max: false,
            lock: true,
            ok: function () {
                if ($("#" + formName).bValidator().validate()) {
                    Tip();
                    ContentManager.DataUpdate();
                }
                else {
                    return false;
                }
            },
            cancel: function () {
                this.close();
            }
        });
        AjaxHelper.RequestSet("GET", currentUrl + id, null, 2);
    },
    //报警提醒窗口
    WindowFloatMessage: function (url) {
        AjaxHelper.RequestSet("GET", url, null, 4);
    },
    //提示框
    MsgShow: function (msg) {
        var alertdialog = art.dialog({
            min: false,
            max: false,
            title: "提示"
        });
        alertdialog.content(msg);
        ContentManager.AutoCloseArt(alertdialog);
    },
    DataUpdate: function () {
        $.post(currentUrl,
                            $("#" + formName).serialize(),
                            function (data) {
                                $.unblockUI();
                                if (data.indexOf("成功")) {
                                    ContentManager.MsgShow(data);
                                    currentGrid.trigger("reloadGrid", [{ page: 1}]);
                                }
                                else {
                                    ContentManager.MsgShow(data);
                                }
                            });
    },
    //按钮样式设置
    BtnStyleSet: function () {
        $("button:first").button({
            icons: {
                primary: "ui-icon-gear"
            }
        }).next().button({
            icons: {
                primary: "ui-icon-home"
            }   
        });
        $("#search").button({
            icons: {
                primary: "ui-icon-home"
            }
        });
        $("#approvalSubmit").button({
            icons: {
                primary: "ui-icon-gear"
            }
        });
    },
    AutoCloseArt: function (art) {
        timeoutId = setInterval(function () { art.close(); clearInterval(timeoutId); }, 2500);
    }
}
