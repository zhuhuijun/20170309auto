var flag = 0;//0代表未加载功能区
var Addflag = 0; //0代表加载
//--------------------------------Gis地图主页面上的函数-------------------------------//
//点位数据
var pointdata = null;
$(function () {
    //初始化地图
    var swfVersionStr = "10.0.0";
    var xiSwfUrlStr = "/Content/Gis/playerProductInstall.swf";
    var flashvars = {};
    var params = {};
    params.quality = "high";
    params.bgcolor = "#cce3ec";
    params.allowscriptaccess = "sameDomain";
    params.allowfullscreen = "true";
    if (swfobject.ua.ie && swfobject.ua.win) // http://code.google.com/p/swfobject/wiki/api
    {
        params.wmode = "opaque"; // workaround for cursor issue - https://bugs.adobe.com/jira/browse/SDK-26818
    }
    else {
        params.wmode = "transparent"
    }
    var attributes = {};
    attributes.id = "index";
    attributes.name = "index";

    attributes.align = "middle";
    swfobject.embedSWF(
            "/Content/Gis/index.swf",
                "flashContent",
                "100%", "98%",
                swfVersionStr, xiSwfUrlStr,
                flashvars, params, attributes);
    swfobject.createCSS("#flashContent", "display:block;text-align:left;");
    //隐藏菜单栏
    jQuery(".slideTxtBox").slide({ effect: "left" });
    jQuery("#doubleSlideTxt").slide({ titCell: ".parHd li", mainCell: ".parBdIn", effect: "top", trigger: "click" });
    //----------------------------c初始化功能菜单-------------------------------
    //海水
    $("#triggerSea").powerFloat({
        offsets: {
            x: -90,
            y: -82
        },
        target: $("#targetBoxSea")
       // reverseSharp: true //是否显示三角
    });
    //大气
    $("#triggerGas").powerFloat({
        offsets: {
            x: -90,
            y: -82
        },
        target: $("#targetBoxGas")
    });
    //噪声
    $("#triggerNoise").powerFloat({
        offsets: {
            x: -90,
            y: -82
        },
        target: $("#targetBoxNoise")
    });
    //地表水
    $("#triggerSurfacewater").powerFloat({
        offsets: {
            x: -90,
            y: -82
        },
        target: $("#targetBoxSurfaceWater")
    });
    //操作
    $("#triggeroperation").powerFloat({
        offsets: {
            x: -90,
            y: -82
        },
        target: $("#targetBoxOpertion")
    });
});
//添加点位
function ShowPointDetail(id) {
    var vv = document.getElementById("index");
    var checkid = document.getElementById("IsAdd");
    //清除专题图
    if (flag == 1) {
        vv.exItemClear();
        flag = 0;
    }
    if (Addflag == 1 && checkid.checked==false) {
        vv.MapClear();
    }
    else {
        Addflag =1;
    }
   ShowPointDetail1(id);
}
function ShowPointDetail1(id) {
    $.ajax({
        type: "POST",
        url: "../Gis/GetSeaWaterPointInfos/" + id,
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            var vv = document.getElementById("index");
            pointdata = data.toString();
            var ScaleList = "scaleEnable:true, maxScale:500000,minScale:0";
            vv.AddAdvancedMapPoint(pointdata, ScaleList);
        }
    });
}
//功能区显示
function onChartClick1(id) {
    //清除专题图方法
    if (flag == 1) {
        document.getElementById("index").exItemClear();
    }
    //清除点位方法
    document.getElementById("index").MapClear();
    $.ajax({
        type: "POST",
        url: "../Gis/GetFunInfos/",
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            var vv = document.getElementById("index");
            pointdata = data.toString();
            //添加专题图
            vv.exItemSelect(pointdata);
         // ShowPointDetail1(id);
        }
    });

    flag =1;
}
//添加饼状图
function onChartClick(id) {
    var vv = document.getElementById("index");
    //清除专题图
    vv.exItemClear();
    vv.MapClear();
    onChartClickDetail(id);
}
function onChartClickDetail(id) {
    $.ajax({
        type: "POST",
        url: "../Gis/GetPhotoPointInfos/" + id,
        cache: false,
        async: true,
        dataType: "html",
        success: function (data) {
            var vv = document.getElementById("index");
            pointdata = data.toString();
            vv.MapChartStr(pointdata);
        }
    });
}
//---------------------------------------------------------------------------------//

//-------------------------------处理数据弹出框-------------------------------------//
//打开数据提示框
function openPointDialogNew(id, TypeCode, name) {
    var dialog = art.dialog(
    {
        title: name + '->数据展示',
        min: false,
        max: false,
        width:924,
        height: 500
    });
    $.ajax({
        url: "../Gis/GetPointPage/" + id + "/" + TypeCode+"",
        success: function (data) {
            dialog.content(data);
        }
    });
}
//查询历史数据信息 
function ConditionHistorySearch() {
    var itemType = $("#ItemType").val();
    var startTime = $("#HisStartTime").val();
    var endTime = $("#HisEndTime").val();
    var PointIDs = $("#PointID").val();
    var PointType = $("#PointType").val();
    var timeType = $("#timeTypeValue").val();
     var timeyear=$("#drYears").val(); //年份
     PointType = PointIDs + ',' + PointType + ',' + itemType + ',' + startTime + ',' + endTime + ',' + timeType + ',' + timeyear;
    $.ajax({
        type: "Post",
        url: "../Gis/GetHistoryDataAndChart/",
        data: { id: PointType },
        beforeSend: function () { $("#showgrid").html="<img src='@Url.Content('~/Content/Gis/Images/GisLoading.gif')' alt='' />"; },
        success: function (data) {
            $("#showgrid").html("");
            $("#showgrid").html(data);
        }
    });
    return false;
}
//--------------------------------------------------------------------------------------//

//---------------------------------------切换图片的处理----------------------------------------//
var picUrl = "";
//修改图片
function modifyPhoto() {
    var moduleID = $("#drModule").val();//模块
    var idsize = $("#drPhotoSize").find("option:selected").text();//图片大小
     var photoColor = $("#colorChose").val();  //取色
     if(photoColor == null || photoColor == "") {
        var alertdialog = art.dialog({
             min: false,
             max: false,
             title: "提示",
             content:"请选择颜色"
         });
         return false;
     }
     if (picUrl == null || picUrl == "") {
         var alertdialog = art.dialog({
             min: false,
             max: false,
             title: "提示",
             content: "请选择图片"
         });
         return false;
     }
     else {
         $.ajax({
             url: "../Gis/editPhoto/",
             data: { id: moduleID, TypeCode: picUrl, PhotoSize: idsize, photoColor: photoColor },
             success: function (data) {
                 var alertdialogSuccess = art.dialog({
                     min: false,
                     max: false,
                     time: 2,
                     title: "提示",
                     content: data
                 });
                 $("#floatDiv").getNiceScroll().hide();
                 mydialog.close();
                 ShowPointDetail(moduleID);
             }
         });
         picUrl = "";
     }
}
//选择图片加上背景色的处理/Content/Gis/Images/GisPointPhoto/
function takePhoto(type) {
   var img = $("#"+type);
   if (picUrl != "") {
       $("#" + picUrl).removeClass();  //删除背景色属性
   }
   img.addClass("PhotoBackColor");
   $("#imgId").attr("src", "/Content/Gis/Images/GisNewImage/" + type + ".png");
    picUrl = type;
}
//弹出修改图片页面 mydialog
function ChangePhoto() {
    mydialog = art.dialog(
                {
                    title: '切换展示页面',
                    min: false,
                    max: false,
                    width: 445,
                    height: 320,
                    close: function () { $("#floatDiv").getNiceScroll().hide(); }
                });
    $.ajax({
        url: "/Gis/GetPhotoNodeList/" + "",
        type: "POST",
        success: function (data) {
            mydialog.content(data);
        }
    });
}
function SearchPhotoData() {
    var img = $("#imgId");
    var st = $("#drPhotoSize").find("option:selected").text();
    img.css("width", st);
    img.css("Height", st);
}
//-------------------------------------------------------------------------------//

//--------------------------------隐藏/显示侧边栏-------------------------------//
function OpenBar() {
    var barobj = document.getElementById("GisRight1");
    var curleft = barobj.style.width;
    curw = parseInt(curleft);
    if (curw >100) {
        $("#GisRight1").animate({ width: "20px" });
        document.getElementById("dragImgUrl").src = "/Content/Gis/Images/drag_btuNew1.png";
    }
    else {
        $("#GisRight1").animate({ width: "165px" });
        document.getElementById("dragImgUrl").src = "/Content/Gis/Images/drag_btuNew2.png";
    }
}
//--------------------------------------------------------------------------------//
//-----------------------更多----------------
function MoreDetailedData(GasPointCode, GasPointTime) {
  var pointName = $("#pointName").val();
    var dialog = art.dialog({
        width: 800,
        height: 300,
        title: pointName,
        lock: true,
        max: false,
        min: false
    });
    $.ajax({
        type: "Post",
        url: "../Gis/MoreDetailedData/",
        data: { id: GasPointCode, TypeCode: GasPointTime},
        success: function (data) {
            dialog.content(data);
        }
    });
    return false;
}
//---------------------除大气之外的  更多----------
function OverWriteMoreDetailedData(GasPointCode, GasPointTime) {
    var pointName = $("#pointName").val();
    var dialog = art.dialog({
        width: 800,
        height: 300,
        title: "详细信息",
        lock: true,
        max: false,
        min: false
    });
    $.ajax({
        type: "Post",
        url: "../Gis/OverWriteMoreDetailedData/",
        data: { id: GasPointCode, TypeCode: GasPointTime },
        success: function (data) {
            dialog.content(data);
        }
    });
    return false;
}
//------------------------根据类型获取监测项
function ItemTypeChart() {
    var itemtype = $("#GasItemType").val();
    $.ajax({
        type: "Post",
        url: "../Gis/ItemTypeData/",
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
//-------------------------------------------查看不同监测项曲线
function ItemChart() {
    var pointcode = $("#pointcode").val();
    var item = $("#GasItems").val();
    $.ajax({
        type: "Post",
        url: "../Gis/MoreChartData/",
        data: { id: pointcode, TypeCode: item },
        success: function (data) {
            $("#HighChart").html("");
            $("#HighChart").html(data);
        }
    });
}
//-------------------------------------历史数据里面时间类型改变是触发的时间
function Timechange() {
    var ReportType = $("#timeTypeValue").val();
    if (ReportType == 3) {
        $("#yeartd").show();
        $("#yearvalue").show();
        $("#starttimeid").hide();
        $("#starttimevalue").hide();
        $("#endtimeid").hide();
        $("#endtimevalue").hide();
    } else {
        $("#yeartd").hide();
        $("#yearvalue").hide();
        $("#starttimeid").show();
        $("#starttimevalue").show();
        $("#endtimeid").show();
        $("#endtimevalue").show();
    }
}