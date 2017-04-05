$(function () {
    $("#yearid").hide();
    $("#yearvalue").hide();
    $.ajax({
        type: "Post",
        url: "../Tree/TreeData/5",
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $.fn.zTree.init($("#treeDemo"), settingthree, eval(data));
            }
        }
    });
    CommonParameterSet.SetOtherValue("", "", "", "../GasHistoryReport/ExportData/");
    var myNiceScroll = $("html").niceScroll();
    ConditionSearch();
    $(window).resize(function () {
        var currentWidth = $(document.body).width() - 155;
        $("#OnlineGrid").setGridWidth(currentWidth);
        $("#showgrid").width(currentWidth);
        $("#datachart").width(currentWidth);
        $("#RealDatachart_container").width(currentWidth); 
        myNiceScroll.resize();
    });
});
//查询参数
function ConditionSearch() {
    if ($("#StartTime").val() > $("#EndTime").val()) {
        var alertdialog = art.dialog({
            min: false,
            max: false,
            title: "提示",
            content: "开始时间不能大于结束时间"
        });
        return false;
    }
    else {
        var otherData = $("#formSearch").serializeArray();
        otherData = ArraytoJSON.toJSON(otherData);
        var ItemType = $("#ItemType").val();
        var id = $("#TreeNode").val();
        Tip();
        $.ajax({
            type: "Post",
            url: "../GasHistoryReport/GridChartData/",
            data: { id: id, TypeCode: ItemType, customPar: otherData },
            success: function (data) {
                $.unblockUI();
                $("#showgrid").html("");
                $("#showgrid").html(data);
            }
        });
        return false;
    }
}
function Transfer() {
    var otherData = $("#formSearch").serializeArray();
    otherData = ArraytoJSON.toJSON(otherData);
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { customPar: otherData });
    $("#OnlineGrid").jqGrid('setGridParam', { search: false });
}
function Edit() {
    var dialog = art.dialog({
        width: 500,
        height: 100,
        title: "选择列",
        lock: true,
        max: false,
        min: false,
        ok: function () {
                Tip();
                var id = "";
                var typecode = "";
                var ids = ChkCheckBox().split('|');
                for (var i = 0; i < ids.length; i++) {
                    if (i == 0) {
                        id = ids[i];
                    } else {
                        typecode = ids[i];
                    }
                }
                id = id.substr(0, id.length - 1)
                typecode = typecode.substr(0, typecode.length - 1)
                $.ajax({
                    type: "Post",
                    url: "../GasHistoryReport/GetEditData/",
                    data: { id: id, TypeCode: typecode},
                    success: function (data) {
                        var otherData = $("#formSearch").serializeArray();
                        otherData = ArraytoJSON.toJSON(otherData);
                        var id = $("#TreeNode").val();
                        var itemtype = $("#ItemType").val();
                        $.ajax({
                            type: "Post",
                            url: "../GasHistoryReport/GridChartData/",
                            data: { id: id, TypeCode: itemtype, customPar: otherData },
                            success: function (data) {
                                $.unblockUI();
                                $("#showgrid").html("");
                                $("#showgrid").html(data);
                            }
                        });
                    }
                });
        },
        cancel: function () {
            this.close();
        }
    });
    var itemtype = $("#ItemType").val();
    $.ajax({
        type: "Post",
        url: "../GasHistoryReport/Edit/" + itemtype,
        data: { id: itemtype },
        success: function (data) {
            dialog.content(data);
        }
    });
    return false;

}
//返回复选框状态
function ChkCheckBox() {
    var types = "";
    var istypes = "";
    var typeId = document.getElementsByName("chk");
    for (var i = 0; i < typeId.length; i++) {
        if (typeId[i].checked == false) {
            types += typeId[i].value + ",";
        } else {
            istypes += typeId[i].value + ",";
        }
    }
    var itemcodes = types + "|" + istypes;
    return itemcodes;
}

function  change() {
    var ReportType = $("#ReportType").val();
    if (ReportType == 4) {
        $("#StartTime").val("");
        $("#EndTime").val("");
        $("#yearid").show();
        $("#yearvalue").show();
        $("#starttimeid").hide();
        $("#starttimevalue").hide();
        $("#endtimeid").hide();
        $("#endtimevalue").hide();
    } else {
        $("#yearid").hide();
        $("#yearvalue").hide();
        $("#starttimeid").show();
        $("#starttimevalue").show();
        $("#endtimeid").show();
        $("#endtimevalue").show();
    }
}

function ChartData(id,charttype) {
    var otherData = $("#formSearch").serializeArray();
    otherData = ArraytoJSON.toJSON(otherData);
    $.blockUI({ message: $("#datachart").html = '<img src=\"/Content/Gis/Images/GisLoading.gif" alt="" />' });
    $.ajax({
        type: "Post",
        url: "../GasHistoryReport/HighChartData/",
        data: { id: id, TypeCode: charttype, customPar: otherData },
        success: function (data) {
            setTimeout($.unblockUI, 2000);
            $("#datachart").html("");
            $("#datachart").html(data);
        }
    });
}
