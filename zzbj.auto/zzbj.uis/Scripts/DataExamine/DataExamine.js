//手工补入

$(function () {
//    $("#MonitorTime").datepicker();
    $.ajax({
        type: "Post",
        url: "../Tree/TreeData/5",
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $.fn.zTree.init($("#treeDemo"), settingtwo, eval(data));
            }
        }
    });
    var myNiceScroll = $("html").niceScroll();
    $(window).resize(function () {
        $("#OnlineGrid").setGridWidth($(document.body).width() - 160);
        myNiceScroll.resize();
    });
});  
//查询
function Search() {
    var datetime = $("#MonitorTime").val();
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { datetime: datetime });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
}
//补入数据
function Edit(id, pointname) {
    var dialog = art.dialog({
        width: 500,
        height: 100,
        title: "数据补录>>" + pointname,
        lock: true,
        ok: function () {
            if ($("#form1").bValidator().validate()) {
                Tip();
                var value = $("#Value").val();
                var updatetime = $("#UpdateTime").val();
                var remark = $("#Remarks").val();
                $.ajax({
                    type: "Post",
                    url: "../HandEntering/EditData/",
                    data: { id: id, Value: value, Remarks: remark, UpdateTime: updatetime },
                    success: function (data) {
                        $.unblockUI();
                        ContentManager.MsgShow(data);
                        $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
                    }
                });
            } else {
                return false;
            }
        },
        cancel: function () {
            this.close();
        }
    });
    $.ajax({
        type: "Post",
        url: "../HandEntering/Edit/",
        data: { id: id },
        success: function (data) {
            dialog.content(data);
        }
    });
}
//上一天
function LastDay() {
    var nowdate = new Date($("#MonitorTime").val());
    var nextdate;
    var nowyear = nowdate.getFullYear();
    var day = nowdate.getDate();
    var month = nowdate.getMonth() + 1;
    var year = nowdate.getFullYear();
    if (month == 1 && day == 1) {
        nowyear = year - 1;
        month = 12;
    } else if (day == 1) {
        month = nowdate.getMonth();
    }
    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) {
        nextdate = nowdate.getDate() - 1;
        if (nextdate == 0 && nowyear == year) {
            nextdate = 31;
            month = nowdate.getMonth();
        } else if (nextdate == 0) {
            nextdate = 31;
        }
    } else if (month == 4 || month == 6 || month == 9 || month == 11) {
        nextdate = nowdate.getDate() - 1;
        if (nextdate == 0) {
            nextdate = 30;
            month = nowdate.getMonth();
        }
    } else {
        if ((year % 4 == 0 && year % 100 != 0) || (year % 100 == 0 && year % 400 == 0)) {
            nextdate = nowdate.getDate() - 1;
            if (nextdate == 0) {
                nextdate = 29;
                month = nowdate.getMonth();
            }
        } else {
            nextdate = nowdate.getDate() - 1;
            if (nextdate == 0) {
                nextdate = 28;
                month = nowdate.getMonth();
            }
        }
    }
    var yesterday = (nowyear + "/" + month + "/" + nextdate);
    $("#MonitorTime").attr("value", yesterday);
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { datetime: yesterday });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
}
//下一天
function NextDay() {
    var nowdate = new Date($("#MonitorTime").val());
    var nextdate;
    var nowyear = nowdate.getFullYear();
    var day = nowdate.getDate();
    var month = nowdate.getMonth() + 1;
    var year = nowdate.getFullYear();
    if (month == 12 && day == 31) {
        nowyear = year + 1;
        month = 1;
    } else if (day == 31) {
        month = nowdate.getMonth() + 2;
    }
    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) {
        nextdate = nowdate.getDate() + 1;
        if (nextdate == 32 && nowyear == year) {
            nextdate = 1;
            month = nowdate.getMonth() + 2;
        } else if (nextdate == 32) {
            nextdate = 1;
        }
    } else if (month == 4 || month == 6 || month == 9 || month == 11) {
        nextdate = nowdate.getDate() + 1;
        if (nextdate == 31) {
            nextdate = 1;
            month = nowdate.getMonth() + 2;
        }
    } else {
        if ((year % 4 == 0 && year % 100 != 0) || (year % 100 == 0 && year % 400 == 0)) {
            nextdate = nowdate.getDate() + 1;
            if (nextdate == 30) {
                nextdate = 1;
                month = nowdate.getMonth() + 2;
            }
        } else {
            nextdate = nowdate.getDate() + 1;
            if (nextdate == 29) {
                nextdate = 1;
                month = nowdate.getMonth() + 2;
            }
        }
    }
    var yesterday = (nowyear + "/" + month + "/" + nextdate);
    $("#MonitorTime").attr("value", yesterday);
    var postData = $("#OnlineGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { datetime: yesterday });
    $("#OnlineGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
}
//详细监测数据显示
function buttonExamine(pointcode, pointname) {
    var datetime = $("#MonitorTime").val();
    var dialog = art.dialog({
        width: 800,
        height: 450,
        title: pointname + ">>数据审核",
        lock:true,
        max: false,
        min:false
    });
    $.ajax({
        type: "Post",
        url: "../ManMadeExamine/ExamineData/",
        data: { id: pointcode, TypeCode: datetime },
        success: function (data) {
            dialog.content(data);
        }
    });
}
//数据审核
function Save(items) {
    if (ChkCheckBox() == true) {
        var datetime = $("#MonitorTime").val();
        var point = $("#Pointcode").val();
        var datatype = "";
        var typeId = document.getElementsByName("IsDataType");
        for (var i = 0; i < typeId.length; i++) {
            if (typeId[i].checked) {
                datatype += typeId[i].value + ",";
            }
        }
        datatype = datatype.substr(0, datatype.length - 1)
        //锁屏，防止重复操作
       // $.blockUI({ message: $("#datachart").html = '<img src=\"/Content/Gis/Images/GisLoading.gif" alt="" />' });
        $.ajax({
           // beforeSend: function () { Tip(); $.blockUI({ message: $("#datachart").html = '<img src=\"/Content/Gis/Images/GisLoading.gif" alt="" />' }); },
            type: "Post",
            url: "../ManMadeExamine/Edit/",
            data: { id: datatype, TypeCode: point, datetime: datetime },
            cache: false,
            async: true,
            dataType: "html",         
            success: function (data) {
                ContentManager.MsgShow(data);
            }
        });
    } else {
        alert("请选择数据类型！");
    }
}
//数据类型筛选
function SearchType() {
    var datatype = "";
    var typeId = document.getElementsByName("IsDataType");
    for (var i = 0; i < typeId.length; i++) {
        if (typeId[i].checked) {
            datatype += typeId[i].value + ",";
        }
    }
    datatype = datatype.substr(0, datatype.length - 1)
    var postData = $("#OnlinesmallGrid").jqGrid("getGridParam", "postData");
    postData = $.extend(postData, { datatype: datatype });
    $("#OnlinesmallGrid").jqGrid('setGridParam', { search: true }).trigger("reloadGrid", [{ page: 1}]);
}
//返回复选框状态
function ChkCheckBox() {
    var status = false;
    var typeId = document.getElementsByName("IsDataType");
    for (var i = 0; i < typeId.length; i++) {
        if (typeId[i].checked) {
            status = true;
            break;
        }
    }
    return status
}
//查询相应监测项图标
function SearchItemData() {
    var itemcode = $("#GasItemCode").val();
    var id = $("#Pointcode").val();
    var datatime = $("#Datatime").val();
    $.ajax({
        type: "Post",
        url: "../ManMadeExamine/HighChart/",
        data: { id: id, TypeCode: datatime, itemcode: itemcode },
        success: function (data) {
            $("#datachart").html("");
            $("#datachart").html(data);
        }
    });
}