//标识设置(默认为加载树)
var MarkCode = 1;
//当前定时器ID
var currentTimeID = -1;
//当前目录是否为收藏企业
var IsCollect = false;
//当前树节点
var CruuenttreeNode = null;
//dialog
var haveBtnDialog = null;
//是否绑定滚动条
var isBanding = true;
//当前子菜单Url
var currentChildUrl = "";
//默认选中排口编号
var defalutOutputCode = null;
//默认选中企业编号
var defalutEntCode = null;
//默认排口类型
var defaultOutputType = 0;
//当前模块父级ID
var isOpen = false;
//上一个父级模块ID
var UpModuleID = 0;
//左侧滚动条
var niceRight = null;
//定义定时器变量
var IntervalId;
var IntervalDataId;

//第一次加载页面设置
function SetLeftImage() {
    //默认选中行政区
//    $("#button_area")[0].src = "/Content/Image/LeftMenu/button_area_select.gif";
}
var Getpara = {
    GetOutputCode: function () {
        return defalutOutputCode;
    },
    GetEntCode: function () {
        return defalutEntCode;
    },
    GetOutPutType: function () {
        return defaultOutputType;
    }
}

var JqueryConfirm = {
    //带按钮的确认框
    JqueryHaveBtn: function (Msg, entCode, type) {
        $("#Msg").html(Msg);
        $("#dialog-confirm").dialog({
            resizable: false,
            height: 140,
            bgiframe: true,
            modal: true,
            hide: "explode",
            show: "blind",
            position: ["center", "center"],
            buttons: {
                "确定": function () {
                    $(this).dialog("close");
                    $(this).dialog("destroy");
                    EntToCollectHelper.EntAddToCollect(entCode, type);
                },
                "取消": function () {
                    $(this).dialog("close");
                    $(this).dialog("destroy");
                }
            }
        });
    },
    //不带按钮的确认框
    BaseJqueryDialog: function (Msg) {
        $("#Msg").html(Msg);
        $("#dialog-confirm").dialog({
            resizable: false,
            height: 140,
            bgiframe: true,
            modal: true,
            hide: "explode",
            show: "blind",
            position: ["center", "center"]
        });
    }
}
//左侧树构建
var Tree = {
    //通过Ajax动态添加数据
    AjaxLoadTree: function (showType, Type) {
        if (showType == 3)
            IsCollect = true;
        else
            IsCollect = false;
        MarkCode = 1;
        //加载时隐藏树
        $("#Lefttree").hide();
        //锁屏，防止重复操作
        $("#Block").block({ message: "<Img src=\"/Content/Image/Icons/Loading.gif\" />" });
        //保存当前选中值
        $("#showType").val(showType);
        $("#outputType").val(Type);
        //获取返回json数据
        AjaxHelper.JqueryAjaxHepler("POST", "Home/MenuTree", "type=" + showType + "&outputtype=" + Type);
    }
}

var AjaxHelper = {
    //ajax动态请求
    JqueryAjaxHepler: function (Rtype, url, data) {
        $.ajax({
            type: Rtype,
            url: url,
            cache: false,
            async: true,
            dataType: "html",
            data: data,
            success: AjaxHelper.AjaxSuccess
        });
    },
    //ajax请求成功回调
    AjaxSuccess: function (data) {
        if (data != "0") {
            switch (MarkCode) {
                case 1:
                    //序列化基础数据
                    var treeData = $.parseJSON(data.split("~")[0]);
                    defalutEntCode = data.split("~")[1];
                    defalutOutputCode = data.split("~")[2];
                    defaultOutputType = data.split("~")[3];
                    //初始化树
                    $.fn.zTree.init($("#Lefttree"), setting, treeData);
                    //                    var treeObj = $.fn.zTree.getZTreeObj("Lefttree");
                    //                    var node = treeObj.getNodeByParam("OutputID", defalutOutputCode, null);
                    //解锁
                    $("#Block").unblock();
                    //展示树结构
                    $("#Lefttree").show();
                    if (isBanding) {
                        //浏览器高度
                        var vbodyheight = document.body.clientHeight;
                        $("#Load").height(vbodyheight - 182);
                        //显示滚动条
                        $("#Load").niceScroll();
                        $("#right").niceScroll();
                        isBanding = false;
                    }
                    var nice = $("#Load").getNiceScroll();
                    niceRight = $("#right").getNiceScroll();
                    nice.resize();
                    nice.show();
                    ChildMenuClick.menuClick("RealTimeData/Index/", 1);
                    break;
                case 2:
                    EntToCollectHelper.AddToCollectComplete(data);
                    break;
            }
        }
        else {
            //提示无数据，解锁
            $("#Block").unblock();
        }
    }
}

//树基本设置
var setting = {
    view: {
        addHoverDom: addHoverDom,
        removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    data: {
        simpleData: {
            enable: true
        },
        key: {
            title: "fullName"
        }
    },
    callback: {
        onClick: OnClick
    }
}
//树节点点击事件
function OnClick(e, treeId, treeNode) {
    defalutOutputCode = treeNode.OutputID;
    defalutEntCode = treeNode.EntID;
    defaultOutputType = treeNode.OutputType;
    if (defalutOutputCode.indexOf("|") > 0) {
        defalutOutputCode = defalutOutputCode.split("|")[0];
        //        defaultOutputType = 0;
    }
    ChildMenuClick.menuClick(null, 1);

}
//树回调方法
function addHoverDom(treeId, treeNode) {

    if (treeNode.EntID != null && treeNode.OutputType == 0) {

        var sObj = $("#" + treeNode.tId + "_span");
        if (treeNode.editNameFlag || $("#addBtn_" + treeNode.id).length > 0) return;
        if (!IsCollect) {
            var addStr = "<span class='button AddCollect' id='addBtn_" + treeNode.id
				+ "' title='加入收藏' onfocus='this.blur();'></span>";
            sObj.after(addStr);
            var btn = $("#addBtn_" + treeNode.id);
            if (btn) btn.bind("click", function () {
                var msg = "您确定将" + treeNode.fullName + "加入收藏吗?";
                //haveBtnDialog.html(msg).dialog("open");
                JqueryConfirm.JqueryHaveBtn(msg, treeNode.EntID, 1);
            });
        }
        else {
            var addStr = "<span class='button DelCollect' id='addBtn_" + treeNode.id
				+ "' title='从收藏删除企业' onfocus='this.blur();'></span>";
            sObj.after(addStr);
            var btn = $("#addBtn_" + treeNode.id);
            if (btn) btn.bind("click", function () {
                var msg = "您确定将" + treeNode.fullName + "从收藏中删除吗?";
                JqueryConfirm.JqueryHaveBtn(msg, treeNode.EntID, 2);
                CruuenttreeNode = treeNode;
            });
        }
    }
}
//树节点鼠标移开事件
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.id).unbind().remove();
}
//添加企业到收藏
var EntToCollectHelper = {
    //关闭提示框
    CloseDialog: function () {
        $("#dialog-confirm").dialog("close");
        $("#dialog-confirm").dialog("destroy");
        if (currentTimeID >= 0)
            window.clearTimeout(currentTimeID);
    },
    //企业加入收藏
    AddToCollect: function (currentEntCode, type) {
        var action = "Home/AddEntToCollect";
        if (type == 2)
            action = "Home/DelEntFromCollect";
        AjaxHelper.JqueryAjaxHepler("POST", action, "EntCode=" + currentEntCode);
        window.clearTimeout(currentTimeID);
        currentTimeID = -1;
    },
    EntAddToCollect: function (entCode, type) {
        currentEntCode = entCode;
        //获取返回json数据
        currentTimeID = window.setTimeout("EntToCollectHelper.AddToCollect(" + entCode + "," + type + ")", 400);
        MarkCode = 2;
    },
    //企业收藏完成
    AddToCollectComplete: function (msg) {
        if (msg.indexOf("删除") > 0 && msg.indexOf("成功") > 0) {
            var zTree = $.fn.zTree.getZTreeObj("Lefttree");
            zTree.removeNode(CruuenttreeNode);
        }
        JqueryConfirm.BaseJqueryDialog(msg);
        currentTimeID = window.setTimeout("EntToCollectHelper.CloseDialog()", 3000);
    }
}


//图片菜单点击事件
var ImageClick = {
    LeftImage: function (img, type) {
        //获取div下的所有Img
        var imgList = $("#div_leftMeau img");
        //循环修改值
        for (var i = 0; i < imgList.length; i++) {
            if (img.id == imgList[i].id) {
                imgList[i].src = "/Content/Image/LeftMenu/" + img.id + "_select.gif";
                continue;
            }
            imgList[i].src = "/Content/Image/LeftMenu/" + imgList[i].id + ".gif";
        }
        $("#showType").val(type);
        var outputType = $("#outputType").val();
        //            if (type == 3) {
        //                outputType = 0;
        //            }
        //完成后请求数据
        Tree.AjaxLoadTree(type, outputType);
    },
    LeftTopImage: function (img, Outputtype) {
        //获取div下的所有Img
        var imgList = $("#Div_LeftSide img");
        //循环修改值
        for (var i = 0; i < imgList.length; i++) {
            if (img.id == imgList[i].id) {
                imgList[i].src = "/Content/Image/LeftMenu/" + img.id + "_b.gif";
                continue;
            }
            imgList[i].src = "/Content/Image/LeftMenu/" + imgList[i].id + "_a.gif";
        }
        var type = $("#showType").val();
        //完成后请求数据
        Tree.AjaxLoadTree(type, Outputtype);
    }
}
//子菜单点击事件
var ChildMenuClick = {
    menuClick: function (url, moduleId) {
        if (url != "RealTimeData/Index/") {
            GetCancel();
        }
        if ((defalutEntCode == null || defalutOutputCode == null) && moduleId != 23) {
            JqueryConfirm.BaseJqueryDialog("请先选择企业或者排口，数据才能加载！");
            return;
        }
        if (moduleId == 23 && !isOpen) {
            closePanel();
            isOpen = true;
        }
        if (isOpen && moduleId != 23) {
            closePanel();
            isOpen = false;
        }
        //设置页面请求
        if (url != null) {
            currentChildUrl = url;
        }
        //锁屏，防止重复操作
        $.blockUI({ message: "<Img src=\"/Content/Image/Icons/Loading.gif\" />" });
        ChildMenuClick.MeunAjaxHepler("POST", currentChildUrl + defalutEntCode + "/" + defalutOutputCode + "/" + defaultOutputType, null);
    },
    //ajax动态请求
    MeunAjaxHepler: function (Rtype, url, data) {
        $.ajax({
            type: Rtype,
            url: url,
            cache: false,
            async: true,
            dataType: "html",
            data: data,
            success: ChildMenuClick.MenuAjaxSuccess
        });
    },
    MenuAjaxSuccess: function (data) {
        $.unblockUI();
        $("#right").html(data);
        niceRight = $("#right").getNiceScroll();
        niceRight.resize();
        niceRight.show();
    }
}
//定时加载曲线点
function ChartEventsLoad() {
    var series = this.series;
    IntervalId = setInterval(function () {
        $.ajax({
            type: "Post",
            url: "RealTimeData/Chart/" + Getpara.GetEntCode() + "/" + Getpara.GetOutputCode() + "/" + Getpara.GetOutPutType(),
            cache: false,
            async: true,
            dataType: "html",
            success: function (data) {
                var myData = eval(data);
                for (var i = 0; i < myData.length; i++) {
                    for (var s in series) {
                        if (myData[i].PollutantName == series[s].name) {
                            var x = Date.parse(myData[i].X);
                            var y = parseFloat(myData[i].Y);
                            series[s].addPoint([x, y], true, true);
                        }
                    }
                }
            }
        });
    }, 50000);
}
//定时获取最新因子数据
function GetOneData(count) {
    var grid = this.grid;
    IntervalDataId = setInterval(function () {
        $.ajax({
            type: "Post",
            url: "RealTimeData/GetOneData/" + Getpara.GetEntCode() + "/" + Getpara.GetOutputCode() + "/" + Getpara.GetOutPutType(),
            cache: false,
            async: true,
            dataType: "html",
            success: function (data) {
                alert(data);
                //$("#OnlineGrid").jqGrid(data); 
            }
        });
    }, 50000);
    if (count < 1) {
        GetCancel();
    }
}
//取消定时器
function GetCancel() {
    window.clearInterval(IntervalId);
    window.clearInterval(IntervalDataId);
}
