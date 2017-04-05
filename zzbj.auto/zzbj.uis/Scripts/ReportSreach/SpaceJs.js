//空间查询JS
$(function () {
    $("#addSpace").button({
        icons: {
            primary: "ui-icon-gear"
        }
    });
});
//站点下拉站点初始化
function innitPage(para,spaceID,spaceTdID){
    $.ajax({
        type: "Post",
        url: "../SeaWaterAllPointYearUpperCount/PointData/",
        data: { id: para },
        async: false,
        success: function (data) {
            var spaceIDs = "#" + spaceID;
            var spaceTdIDs = "#" + spaceTdID;
            $(spaceIDs).remove();
            $(spaceTdIDs).html("");
            var myData = eval(data);
            var str = "<select id=\"FunctionCode\" name=\"FunctionCode\" multiple=\"multiple\" size=\"5\" style=\"width: 185px;\">"; ;
            for (var i = 0; i < myData.length; i++) {
                str += "<option onclick=getItem(this.value) onchange=getItem(this.value) value=\"" + myData[i].MonitorPK_ID + "\">" + myData[i].PointName + "</option>";
            }
            $(spaceTdIDs).html(str + "</select>");
            $(spaceIDs).multiselect({ selectedList: 3 });
        }
    });
};
//空间下拉站点初始化
function innitSpacePage(spaceID, spaceTdID) {
    $.ajax({
        type: "Post",
        url: "../SeaWaterPointAreaCount/SpaceData/",
        async: false,
        success: function (data) {
            var spaceIDs = "#" + spaceID;
            var spaceTdIDs = "#" + spaceTdID;
            $(spaceIDs).remove();
            $(spaceTdIDs).html("");
            var myData = eval(data);
            var str = "<select id=" + spaceID + " name="+spaceID+" multiple=\"multiple\" style=\"width: 120px;\">"; ;
            for (var i = 0; i < myData.length; i++) {
                str += "<option onclick=getItem(this.value) onchange=getItem(this.value) value=\"" + myData[i].MonitorPK_ID + "\">" + myData[i].SpaceName + "</option>";
            }
            $(spaceTdIDs).html(str + "</select>");
            $(spaceIDs).multiselect({ selectedList: 3 });
        }
    });
};
//弹出新建空间对话框type:1需要更新站点下拉框2不需要更新站点下拉框
function addSpaceFun(type) {
    var Spacedialog = art.dialog(
    {
        title: '->添加空间',
        min: false,
        max: false,
        width: 604,
        height: 250,
        ok: function () {
            //            $("#form2").submit();
            //            return false;
            $.post("../Space/Edit/",
                            $("#form2").serialize(),
                            function (data) {
                                if (data.indexOf("成功")) {
                                    alert(data);
                                    if(type==1)
                                    {
                                        innitSpace();
                                    }
                                    else
                                    {
                                        innitSpacePage('SpaceList', 'FunctionCodeAdd');
                                    }
                                }
                                else {
                                    alert(data);
                                }
                            });
        },
        cancel: true //为true等价于function(){}
    });
    $.ajax({
        url: "../Space/Edit/",
        success: function (data) {
            Spacedialog.content(data);
        }
    });
    return false;
}
//加载空间
function innitSpace() {
    $.ajax({
        type: "Post",
        url: "../SeaWaterAllPointYearUpperCount/SpaceData/",
        async: false,
        success: function (data) {
            $("#SpaceList").html("");
            var myData = eval(data);
            var str = "<option onclick=getItem(this.value) onchange=getItem(this.value) value=\"\">请选择空间</option>";
            for (var i = 0; i < myData.length; i++) {
                str += "<option onclick=getItem(this.value) onchange=getItem(this.value) value=\"" + myData[i].MonitorPK_ID + "\">" + myData[i].SpaceName + "</option>";
            }
            $("#SpaceList").html(str);
        }
    });
};