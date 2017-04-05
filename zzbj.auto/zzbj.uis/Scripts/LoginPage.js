if (window != top)
    top.location.href = location.href;

$(function () {
    var myFrom = $(this).find("form");
    myFrom.bValidator();

    document.oncontextmenu = function () {//屏蔽浏览器右键事件
        return false;
    };
    document.onkeydown = function (e) {
        var ev = document.all ? window.event : e;
        if (ev.keyCode == 13) {
            myFrom.submit(); //处理事件 
        }
    };
});

function errorTip(msg) {
    if (msg != null && msg != "") {
        var errordialog = art.dialog({
            title: "错误提示",
            content: msg,
            min: false,
            max: false
        });
        var setTimeOutId = setInterval(function () { errordialog.close(); clearInterval(setTimeout); }, 2500);
    }
}
