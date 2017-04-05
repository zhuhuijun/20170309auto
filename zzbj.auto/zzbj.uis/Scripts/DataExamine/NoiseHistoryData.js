var letmyNiceScroll = null;
//初始加载树
$(function () {
    NoiseTypeTree();
    CommonParameterSet.SetOtherValue("", "", "", "../NoiseHistoryData/ExportData/");
    var myNiceScroll = $("html").niceScroll();
    letmyNiceScroll = $("#treeLeft").niceScroll();
    $(window).resize(function () {
        var currentWidth = $(document.body).width() - 250;
        $("#OnlineGrid").setGridWidth(currentWidth);
        $("#HighChart").setGridWidth(currentWidth);
        $("#RealDatachart_container").width(currentWidth); 
        myNiceScroll.resize();
    });

});
//根据噪声类型加载树形结构
function NoiseTypeTree() { 
   var type = $("#NoiseType").val();
   $.ajax({
       type: "Post",
       url: "../Tree/TreeData/",
       data: { id: 7, TypeCode: type },
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
    var type = $("#NoiseType").val();
    var point = $("#TreeNode").val();
    var StartTime = $("#StartTime").val();
    var EndTime = $("#EndTime").val();
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { id: type, TypeCode: point,StartTime:StartTime,EndTime:EndTime });
    $("#OnlineGrid").jqGrid('setGridParam', { search: false }).trigger("reloadGrid", [{ page: 1}]);
    $.ajax({
        type: "Post",
        url: "../NoiseHistoryData/ChartData/",
        data: { id:type,TypeCode:point,StartTime:StartTime,EndTime:EndTime },
        success: function (data) {
            $("#HighChart").html("");
            $("#HighChart").html(data);
        }
    });
    return false;
}
//图标显示
function ChartData(charttype) {
    var type = $("#NoiseType").val();
    var point = $("#TreeNode").val();
    var StartTime = $("#StartTime").val();
    var EndTime = $("#EndTime").val();
    $.blockUI({ message: $("#HighChart").html = '<img src=\"/Content/Gis/Images/GisLoading.gif" alt="" />' });
    $.ajax({
        type: "Post",
        url: "../NoiseHistoryData/ChartData/",
        data: { id: type, TypeCode: point, StartTime: StartTime, EndTime: EndTime, ChartType: charttype },        
        success: function (data) {
            setTimeout($.unblockUI, 2000);
            $("#HighChart").html("");
            $("#HighChart").html(data);
        }
    });
}

