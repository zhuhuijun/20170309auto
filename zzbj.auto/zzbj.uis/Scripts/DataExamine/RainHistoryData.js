var letmyNiceScroll = null;
//初始加载树
$(function () {
    Tree();
    CommonParameterSet.SetOtherValue("", "", "", "../RainHistoryData/ExportData/");
    var myNiceScroll = $("html").niceScroll();
    letmyNiceScroll = $("#treeLeft").niceScroll();
    $(window).resize(function () {
        var currentWidth = $(document.body).width() - 180;
        $("#OnlineGrid").setGridWidth(currentWidth);
        $("#HighChart").setGridWidth(currentWidth);
        $("#RealDatachart_container").width(currentWidth); 
        myNiceScroll.resize();
    });
});
//加载树形结构
function Tree() {
    $.ajax({
        type: "Post",
        url: "../Tree/TreeData/",
        data: { id: 11},
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $("#treeDemo").html("");
                $.fn.zTree.init($("#treeDemo"), settingFour, eval(data));
            }
        }
    });
}
//查询
function SearchChangeType() {
    var point = $("#TreeNode").val();
    var StartTime = $("#StartTime").val();
    var EndTime = $("#EndTime").val();
    var charttype = "Spline";
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { id: point, TypeCode: 1, StartTime: StartTime, EndTime: EndTime });
    $("#OnlineGrid").jqGrid('setGridParam', { search: false }).trigger("reloadGrid", [{ page: 1}]);
    ChartData(charttype);
    return false;
}

//图标显示
function ChartData(charttype) {
    var point = $("#TreeNode").val();
    var StartTime = $("#StartTime").val();
    var EndTime = $("#EndTime").val();
    $.blockUI({ message: $("#HighChart").html = '<img src=\"/Content/Gis/Images/GisLoading.gif" alt="" />' });
    $.ajax({
        type: "Post",
        url: "../RainHistoryData/ChartData/",
        data: { id: point, TypeCode: 1, StartTime: StartTime, EndTime: EndTime, ChartType: charttype },
        success: function (data) {
            setTimeout($.unblockUI, 2000);
            $("#HighChart").html("");
            $("#HighChart").html(data);
        }
    });
}