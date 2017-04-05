//************************************************//
//定时器时间间隔
var IntervalSeconds;
var seed = 60;
var stopseed = 60;
var refresh = false;
//监测点编码
var pointcode;
var itemtype;
//是否刷新图表
var refreshsinglechart = false;
//曲线一个新的series
var newseries;
//tree滚动条
var letmyNiceScroll = null;
//***********************************************//
//加载左侧树
$(function () {
    $("#content").tabs();
    $.ajax({
        type: "Post",
        url: "../Tree/TreeData/5",
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $.fn.zTree.init($("#treeDemo"), settingFour, eval(data));
            }
        }
    });
    //设置滚动条
    var myNiceScroll = $("html").niceScroll();
    letmyNiceScroll = $("#treeLeft").niceScroll();
    $(window).resize(function () {
        //多点监测Grid表格宽度
        $("#OnlineMorePointGrid").setGridWidth($(document.body).width() - 40);
        myNiceScroll.resize();
        itemtype = $("#ItemType").val();
        pointcode = $("#TreeNode").val();
        var currentWidth = $(document.body).width() - 220;
        //单位Grid表格宽度
        $("#OnlineGrid").setGridWidth(currentWidth);
        //单点位Highcharts Div 宽度
        $("#showdata").width(currentWidth);
    });
    //Jquery.ui按钮样式
    $("#TimeSelect").button({
        icons: {
            primary: "ui-icon-gear"
        }
    });
    //绑定刷新频率点击Float
    var ishow = false;
    $(function () {
        $("#settime").powerFloat({
            reverseSharp: false, //是否显示三角
            eventType: "click",
            target: $("#exportcontent")
        });
    });
});
//多点监测Grid数据更新定时器
function IntervalSecond() {
    if (!refresh) {
        IntervalSeconds = window.setInterval(Second, 1000);
        refresh = true;
    }
}
//刷新频率更新展示
function Second() {
    seed--;
    if (seed == 0) {
        seed = stopseed;
        ConditionSearch();
        RefGrid();
        IntervalSingleChart();
    }
    $("#Refresh").html(seed + " 秒");
}

//定时刷新grid数据
function RefGrid() {
    var ItemType = $("#ItemType").val();
    var TreeNode = $("#TreeNode").val();
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { id: TreeNode, TypeCode: ItemType });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
}
//查询
function ConditionSearch() {
    var RegionCode = $("#RegionCode").val();
    var PointCode = $("#PointCode").val();
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { id: RegionCode, TypeCode: PointCode });
    $("#OnlineMorePointGrid").jqGrid('setGridParam', { search: false }).trigger("reloadGrid", [{ page: 1}]);
}

//选择监测项
function Choose() {
    var dialog = art.dialog({
        width: 300,
        height: 250,
        title: "选择监测项",
        lock: true,
        max: false,
        min: false,
        ok: function () {
            var items = "";
            var uitems = "";
            $("#GasItem option").each(function () {
                items += $(this).val() + ",";
            })
            $("#UGasItem option").each(function () {
                uitems += $(this).val() + ",";
            })
            items = items.substr(0, items.length - 1)
            uitems = uitems.substr(0, uitems.length - 1)
            $.ajax({
                type: "Post",
                url: "../GasRealData/GetEditData/",
                data: { id: uitems, TypeCode: items },
                success: function (data) {
                    var RegionCode = $("#RegionCode").val();
                    var PointCode = $("#PointCode").val();
                    $.ajax({
                        type: "Post",
                        url: "../GasRealData/GetGridData/",
                        data: { id: RegionCode, TypeCode: PointCode },
                        success: function (data) {
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
    $.ajax({
        type: "Post",
        url: "../GasRealData/Edit",
        success: function (data) {
            dialog.content(data);
        }
    });
    return false;
}
//单个监测项右移
function UGasItemList() {
    var movetext = $("#UGasItem option:selected").text();
    var movevalue = $("#UGasItem option:selected").val();
    $("#GasItem").append("<option value=" + movevalue + ">" + movetext + "</option>");
    $("#UGasItem option:selected").remove();
}
//单个监测项左移
function GasItemList() {
    var movetext = $("#GasItem option:selected").text();
    var movevalue = $("#GasItem option:selected").val();
    $("#UGasItem").append("<option value=" + movevalue + ">" + movetext + "</option>");
    $("#GasItem option:selected").remove();

}
//全部监测项右移
function AllUGasItemList() {
    var leftvalue = "";
    $("#UGasItem option").each(function () {
        leftvalue += "<option>" + $(this).text() + "</option>";
        $(this).remove();
    })
    $("#GasItem").append(leftvalue);
}
//全部监测项左移
function AllGasItemList() {
    var leftvalue = "";
    $("#GasItem option").each(function () {
        leftvalue += "<option>" + $(this).text() + "</option>";
        $(this).remove();
    })
    $("#UGasItem").append(leftvalue);
}
//选择监测项类型
function SearchChangeType() {
    var ItemType = $("#ItemType").val();
    var TreeNode = $("#TreeNode").val();
    itemtype = ItemType;
    pointcode = TreeNode;
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { id: TreeNode, TypeCode: ItemType });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
    $.ajax({
        type: "Post",
        url: "../GasRealData/SingleChartData/",
        data: { id: TreeNode, TypeCode: ItemType },
        success: function (data) {
            $("#showchartdata").html("");
            $("#showchartdata").html(data);
        }
    });
}
//更多
function MoreDetailedData(GasPointCode, GasPointName) {
    var dialog = art.dialog({
        width: 800,
        height: 300,
        title: GasPointName,
        lock: true,
        max: false,
        min: false
    });
    $.ajax({
        type: "Post",
        url: "../GasRealData/DetailedData/",
        data: { id: GasPointCode },
        success: function (data) {
            dialog.content(data);
        }
    });
    return false;
}
//查看不同监测项曲线
function ItemChart() {
    var pointcode = $("#pointcode").val();
    var item = $("#GasItems").val();
    $.ajax({
        type: "Post",
        url: "../GasRealData/ChartData/",
        data: { id: pointcode, TypeCode: item },
        success: function (data) {
            $("#HighChart").html("");
            $("#HighChart").html(data);
        }
    });
}
//根据监测项类型选择对应的监测项
function ItemTypeChart() {
    var itemtype = $("#GasItemType").val();
    $.ajax({
        type: "Post",
        url: "../GasRealData/ItemTypeData/",
        data: { id: itemtype },
        success: function (data) {
            $("#GasItems").html("");
            var myData = eval(data);
            var str = "";
            for (var i = 0; i < myData.length; i++) {
                str += "<option value=\"" + myData[i].ItemCode + "\">" + myData[i].ItemName + "</option>";
            }
            $("#GasItems").html(str);
        }
    });
}

//单点监测曲线显示
function SingleChart(charttype) {
    var pointcode = $("#TreeNode").val();
    var itemtype = $("#ItemType").val();
    $.blockUI({ message: $("#showchartdata").html = '<img src=\"/Content/Gis/Images/GisLoading.gif"  alt="" />' });
    $.ajax({
        type: "Post",
        url: "../GasRealData/SingleChartData/",
        data: { id: pointcode, TypeCode: itemtype, charttype: charttype },
        success: function (data) {
            setTimeout($.unblockUI, 2000);
            $("#showchartdata").html("");
            $("#showchartdata").html(data);
        }
    });
}

function Reseting() {
    window.clearInterval(IntervalSeconds);
    var settime = $("#SetTime").val();
    seed = settime * 60;
    stopseed = settime * 60
    refresh = false;
    IntervalSecond();
}
//取消定时器
function ClearIntervalMore() {
    window.clearInterval(IntervalSeconds);
    refresh = false;
}

function IntervalSingleChart() {
    $.ajax({
        type: 'Post',
        url: "../GasRealData/NewSingleChartData/",
        data: { id: pointcode, TypeCode: itemtype },
        cache: false,
        async: true,
        dataType: 'html',
        success: function (data) {
            var myData = eval(data);
            if (myData != null) {
                for (var i = 0; i < myData.length; i++) {
                    for (var s in newseries) {
                        if (myData[i].GasItemName == newseries[s].name) {
                            var x = Date.parse(myData[i].X);
                            var y = parseFloat(myData[i].Y);
                            var length = newseries[s].data.length;
                            if (length > 0) {
                                var seriesData = newseries[s].data[length - 1];
                                if (seriesData.x != x || seriesData.y != y) {
                                    newseries[s].addPoint([x, y], true, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    });
}