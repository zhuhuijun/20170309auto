/* ImportGasData.js --导入监测数据Excles的相关操作
*/
$(function () {
    $("#search").button({
        icons: {
            primary: "ui-icon-gear"
        }
    });
    $("#chooserow").button({
        icons: {
            primary: "ui-icon-home"
        }
    });
    $(window).resize(function () {
        $("#OnlineGrid").setGridWidth(850);
    });
    $("#uploadify").uploadify({
        swf: '/Scripts/uploadify/uploadify.swf',
        uploader: '/RemoteHandlers/UploadFiles.ashx ',
        buttonText: '<img src="/Content/css/images/attach.png" style="  margin-top:3px" alt="选择文件"/>',
        height: 20,
        width: 20,
        onUploadSuccess: function (file, serverData, response) {
            if (file.type == ".xlsx" || file.type == ".xls") {
                if (serverData != "") {
                    var _ServerData = serverData.split('/');
                    $("#iconpath").attr("value", _ServerData[3]);
                }
            }
            else {
                var alertdialog = art.dialog({
                    min: false,
                    time: 2,
                    max: false,
                    title: "提示",
                    content: "文件类型有误，请重新上传！"
                });
            }
        }
    });
});
//---------------------------预览数据---------------
function ViewSeaData() {
    var types = $("#drtypes").val();
    if (types == "") {
        var alertdialogtype = art.dialog({
            min: false,
            time: 2,
            max: false,
            title: "提示",
            content: "请选择文件类型"
        });
    }
    else {
        var FilePath = $("#iconpath").val();
        //判断上传文件是否为空
        if (FilePath == "") {
            var alertdialog = art.dialog({
                min: false,
                time: 2,
                max: false,
                title: "提示",
                content: "请选择文件"
            });
        }
        else {
            $.blockUI({ message: $("#DataDetail").html = '<h1>查询中...</h1>' });
            $.ajax({
                url: "/ImportNoise/ShowDataGrid/" + FilePath + "/" + types,
                type: "POST",
                success: function (data) {
                    $.unblockUI();
                    $("#DataDetail").html("");
                    $("#DataDetail").html(data);
                }
            });
        }
    }
}
//---------文件不匹配--------------------
function ShowLookError(xhr) {
    if (xhr.toString().indexOf("错误") > 0) {
        var alertdialogtype = art.dialog({
            min: false,
            time: 2,
            max: false,
            title: "提示",
            content: xhr
        });
    }
}
//-------------------导入数据------------------------
function ImportDataToSql() {
    var FilePath = $("#iconpath").val();
    //判断上传文件是否为空
    var types = $("#drtypes").val();
    if (types == "") {
        var alertdialogtype = art.dialog({
            min: false,
            time: 2,
            max: false,
            title: "提示",
            content: "请选择文件类型"
        });
    }
    else {
        //判断上传文件是否为空
        if (FilePath == "") {
            var alertdialog = art.dialog({
                min: false,
                time: 2,
                max: false,
                title: "提示",
                content: "请选择文件"
            });
        }
        else {
            $.blockUI({ message: $("#DataDetail").html = '<h1>数据导入中...</h1>' });
            $.ajax({
                url: "/ImportNoise/InsertIntoTables/" + FilePath + "/" + types,
                type: "POST",
                success: function (data) {
                    $.unblockUI();
                    var alertdialog = art.dialog({
                        min: false,
                        time: 2,
                        max: false,
                        title: "提示",
                        content: data
                    });
                }
            });
        }
    }
}
