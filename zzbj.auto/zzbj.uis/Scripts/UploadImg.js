$(function () {

    $("#uploadify").uploadify({
        swf: '/Scripts/uploadify/uploadify.swf',
        uploader: '/RemoteHandlers/Upload.ashx?code=' + $("#code").val(),
        multi: false,
        buttonText: '<img src="/Content/css/images/attach.png" alt=""/>',
        height: 20,
        width: 20,
        onUploadSuccess: function (file, serverData, response) {
            $("#ImgUpload-queue").remove();
            if (serverData != "") {
                var _ServerData = serverData.split('/');
                var a = $("#iconpath").attr("value", _ServerData[3]);
                $("#fileQueue").html("<img src=\'" + serverData + "\' heith=\"60\" width=\"60\" alt=\"\" />");
            }
        }
    });
});

//鼠标移入移出事件
function mouseOverImg(id) {
    //debugger;
    $("#close"+id).show();
}
function mouseOutImg(id) {
    $("#close" + id).hide();
}
//删除图片方法
function deleteimg(id, imageName) {
    NiceMsg.NiceConfirm("确定删除？", function () {
        $("#Imgbox" + id).remove();
        $("#imageUrl").val($("#imageUrl").val().replace('|'+imageName, ''));
        // Ajax传后台将文件删除
        $.post("UploadImg/DelImg/", { ImgLocation: imageName }, function (result) {
        });
    });
}

//简单的风格统一的询问框，和alert
var NiceMsg = {
    NiceConfirm: function (Msg, okFunction, chanceFunction) {
        //页面没有这个层就构建一个层用于提示
        if (document.getElementById("dialog-confirm") == null) {
            var alertDiv = "<div id='dialog-confirm' title='提示' style=' height:auto;'><div style='height: auto;'>"
            + "<p><span class='ui-icon ui-icon-alert' style='float: left; '></span><label id='Msg'></label></p></div></div>";
            $("body").append(alertDiv);
        }
        //显示信息
        $("#Msg").html(Msg);
        //方法重载
        if (arguments.length == 1) {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 140,
                bgiframe: true,
                modal: true,
                hide: "explode",
                show: "blind",
                position: ["center", "center"],
                buttons: {
                    "确定": function () {
                        $(this).dialog("close");
                        $(this).dialog("destroy");
                    },
                    "取消": function () {
                        $(this).dialog("close");
                        $(this).dialog("destroy");
                    }
                }
            });
        } else if (arguments.length == 2) {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 140,
                bgiframe: true,
                modal: true,
                hide: "explode",
                show: "blind",
                position: ["center", "center"],
                buttons: {
                    "确定": function () {
                        $(this).dialog("close");
                        $(this).dialog("destroy");
                        okFunction();
                    },
                    "取消": function () {
                        $(this).dialog("close");
                        $(this).dialog("destroy");
                    }
                }
            });
        } else if (arguments.length == 3) {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 140,
                bgiframe: true,
                modal: true,
                hide: "explode",
                show: "blind",
                position: ["center", "center"],
                buttons: {
                    "确定": function () {
                        $(this).dialog("close");
                        $(this).dialog("destroy");
                        okFunction();
                    },
                    "取消": function () {
                        $(this).dialog("close");
                        $(this).dialog("destroy");
                        chanceFunction();
                    }
                }
            });
        }

    },
    //不带按钮的确认框
    NiceAlert: function (Msg) {
        //页面没有这个层就构建一个层用于提示
        if (document.getElementById("dialog-confirm") == null) {
            var alertDiv = "<div id='dialog-confirm' title='提示' style=' height:auto;'><div style='height: auto;'>"
            + "<p><span class='ui-icon ui-icon-alert' style='float: left; '></span><label id='Msg'></label></p></div></div>";
            $("body").append(alertDiv);
        }
        $("#Msg").html(Msg);
        $("#dialog-confirm").dialog({
            resizable: false,
            height: 140,
            bgiframe: true,
            modal: true,
            hide: "explode",
            show: "blind",
            position: ["center", "center"],
            buttons: {
                "确定": function () {
                    $(this).dialog("close");
                    $(this).dialog("destroy");
                }
            }
        });
    }
}