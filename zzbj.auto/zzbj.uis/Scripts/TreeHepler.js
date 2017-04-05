//当前请求获取数据路径
var Url = null;
//节点编号
var Code = null;

var setting = {
    check: {
        enable: true
    },
    callback: {
        beforeCheck: zTreeBeforeCheck,
        onCheck: zTreeOnCheck
    },
    data: {
        simpleData: {
            enable: true
        }
    }
};
var settingtwo = {
    check: {
        enable: false
    },
    callback: {
        onClick: OnClick
    },
    data: {
        simpleData: {
            enable: true
        }
    }
};
var settingthree= {
    check: {
        enable: false
    },
    callback: {
        onClick: OnClickTree
    },
    data: {
        simpleData: {
            enable: true
        }
    }
};
var settingFour = {
    check: {
        enable: false
    },
    callback: {
        onClick: OnClickTwo,
        onExpand: onExpand
    },
    data: {
        simpleData: {
            enable: true
        }
    }
};
function OnClick(e, treeId, treeNode) {
    var Code = treeNode.code;
    var ParentCode=treeNode.parentcode;
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { code: Code,parentcode:ParentCode});
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
}
function OnClickTwo(e, treeId, treeNode) {
    var Code = treeNode.code;
    $("#TreeNode").val(Code);
    var TreeName = treeNode.name;
    $("#OnlineGrid").setCaption(TreeName+"历史数据列表");
    SearchChangeType();
}
function OnClickTree(e, treeId, treeNode) {
    var Code = treeNode.code;
    $("#TreeNode").val(Code);
    var TreeName = treeNode.name;
    $("#OnlineGrid").setCaption(TreeName + "历史数据列表");
    var ParentCode = treeNode.parentcode;
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { code: Code, parentcode: ParentCode });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
    var otherData = $("#formSearch").serializeArray();
    otherData = ArraytoJSON.toJSON(otherData);
    $.ajax({
        type: "Post",
        url: "../GasHistoryReport/HighChartData/",
        data: { id: Code, customPar: otherData },
        success: function (data) {
            $("#datachart").html("");
            $("#datachart").html(data);
        }
    });
}
function onExpand(event, treeId, treeNode) {
    if ($("#treeLeft") != null && letmyNiceScroll != null) {
        letmyNiceScroll.resize();
    }
}
function zTreeOnCheck(event, treeId, treeNode) {
       return true;

}
function zTreeBeforeCheck(treeId, treeNode) {
    return true;
}
function TreeShowMsg(title, url, id) {
    var dialog = art.dialog({
        min: false,
        max: false,
        title: title,
        lock: true,
        ok: function () {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            var nodes = zTree.getCheckedNodes(true);
            var nodecodes = "";
            var type = 0;
            if (nodes != null && nodes.length > 0) {
                for (var i = 0; i < nodes.length; i++) {
                    var code = nodes[i].code;
                    var parentcode = nodes[i].parentcode;
                    var gradecode = nodes[i].gradecode;
                    if (code != null && parentcode != 0 && gradecode == 0) {
                        nodecodes += code + ",";
                    }
                    if (type == 0) {
                        type = nodes[i].type;
                    }
                }
                nodecodes = nodecodes.substr(0, nodecodes.length - 1)
            }
            switch (type) {
                case 1:
                    $("#DEPTID").val(nodecodes);
                    break;
                case 2:
                    $("#RoleId").val(nodecodes);
                    break;
                case 3:
                case 4:
                    var url = "../Role/TreeData/";
                    if (type == 4) {
                        url = "../Modules/TreeData/";
                    }
                    Tip();
                    $.ajax({
                        type: "Post",
                        url: url,
                        data: { id: nodecodes, TypeCode: id },
                        success: function (data) {
                            $.unblockUI();
                            ContentManager.MsgShow(data);
                        }
                    });
                    break;
                case 6:
                    $("#ItemTypeID").val(nodecodes);
                    break;
                    //空间选择
                case 13:
                    //$("#pointDetailIds").val(nodecodes);
                    $("#PointDetailID").val(nodecodes);
                    break;
            }
        },
        cancel: function () {
            this.close();
        }
    });
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            dialog.content(data);
        }
    });
}

