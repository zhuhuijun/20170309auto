var letmyNiceScroll = null;
//初始加载树
$(function () {
//    $("#StartTime").datetimepicker();
//    $("#EndTime").datetimepicker();
    Tree();
    CommonParameterSet.SetOtherValue("", "", "", "../Audited/ExportData/");
    var myNiceScroll = $("html").niceScroll();
    letmyNiceScroll = $("#treeLeft").niceScroll();
    $(window).resize(function () {
        var currentWidth = $(document.body).width() - 150;
        $("#OnlineGrid").setGridWidth(currentWidth);
        myNiceScroll.resize();
    });
});
//加载树形结构
function Tree() {
    $.ajax({
        type: "Post",
        url: "../Tree/TreeData/",
        data: { id: 5},
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

function SearchChangeType() {
    var otherData = $("#formSearch").serializeArray();
    otherData = ArraytoJSON.toJSON(otherData);
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { customPar: otherData });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
    return false;
}