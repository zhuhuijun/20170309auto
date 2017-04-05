Nice = null;
var AjaxHelper = {
    //设置请求参数
    RequestSet: function (Type, Url, rData, resultOp) {
        $.ajax({
            type: Type,
            url: Url,
            data: rData,
            success: function (data) {
                if (resultOp == null) {
                    $.unblockUI();
                    ContentManager.MsgShow(data);
                    currentGrid.trigger("reloadGrid", [{ page: 1}]);
                }
                else {
                    if (resultOp == 1) {
                        art.dialog({
                            id: 'msg',
                            title: '报警信息',
                            min: false,
                            close: function () {
                                Nice.hide();
                            },
                            max: false,
                            content: data,
                            width: 400,
                            height: 300,
                            left: '100%',
                            top: '100%',
                            fixed: false,
                            drag: false,
                            resize: false
                        });
                    }
                }
            }
        });
    }
};
//调用方法
var Mananger = {
    //报警提醒窗口
    WindowFloatMessage: function (url) {
        AjaxHelper.RequestSet("GET", url, null, 1);
    }
}

//页面离开事件
window.onbeforeunload = onclose;
function onclose() {
    $.ajax({
        type: "POST",
        url: "/Admin/SearchLoginOff",
        success: function (data) {
            alert(data);
        }
    });
}