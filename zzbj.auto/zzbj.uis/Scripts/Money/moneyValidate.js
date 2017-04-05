function minMon() {
    var regex = new RegExp("^[0-9]+(.[0-9]{1,3})?$"); //验证是正实数的表达式
    var minval = $("#minMoney").val();
    var maxval = $("#maxMoney").val();
    if (regex.test(minval)) {
    }
    else {
        ContentManager.MsgShow("输入错误，请输入数字！");
        $("#minMoney").val("0");
    }
}

function maxMon() {
    var regex = new RegExp("^[0-9]+(.[0-9]{1,3})?$"); //验证是正实数的表达式
    var maxval = $("#maxMoney").val();
    var minval = $("#minMoney").val();
    if (regex.test(maxval)) {
    }
    else {
        ContentManager.MsgShow("输入错误，请输入数字！");
        $("#maxMoney").val("0");
    }
}
function check(event) {
    var a = event.keyCode;
    if (!((a >= 48 && a <= 57) || (a >= 96 && a <= 105) || (a >= 37 && a <= 40) || a == 8 || a == 110 || a == 190)) { event.preventDefault ? event.preventDefault() : event.returnValue = false; }
}
function BefourSerch() {
    var minval = $("#minMoney").val();
    var maxval = $("#maxMoney").val();
    if (parseInt(maxval) < parseInt(minval)) {
        ContentManager.MsgShow("数据查询范围错误，最小值不应大于最大值！");
        return false;
    }
    else {
        Search();
        return false;
    }
}
//function minMon() {
//    var regex = new RegExp("^[0-9]+(.[0-9]{1,3})?$"); //验证是正实数的表达式
//    var minval = $("#minMoney").val();
//    var maxval = $("#maxMoney").val();
//    if (regex.test(minval)) {
//        if (parseFloat(maxval) > 0) {
//            if (parseInt(maxval) > parseInt(minval)) {
//            }
//            else {
//                ContentManager.MsgShow("数据查询范围错误，最小值不应大于最大值！");
//                $("#minMoney").val("0");
//            }
//        }
//    }
//    else {
//        ContentManager.MsgShow("输入错误，价格应该大于零！");
//        $("#minMoney").val("0");
//    }
//}

//function maxMon() {
//    var regex = new RegExp("^[0-9]+(.[0-9]{1,3})?$"); //验证是正实数的表达式
//    var maxval = $("#maxMoney").val();
//    var minval = $("#minMoney").val();
//    if (regex.test(maxval)) {

//        if (parseInt(maxval) > parseInt(minval)) {
//        }
//        else {
//            ContentManager.MsgShow("数据查询范围错误，最小值不应大于最大值！");
//            $("#minMoney").val("0");
//        }

//    }
//    else {
//        ContentManager.MsgShow("输入错误，价格应该大于零！");
//        $("#maxMoney").val("0");
//    }
//}
//var ContentManager = {
//    //添加
//    AutoCloseArt: function (art) {
//        timeoutId = setInterval(function () { art.close(); clearInterval(timeoutId); }, 2500);
//    },
//    MsgShow: function (msg) {
//        var alertdialog = art.dialog({
//            min: false,
//            max: false,
//            title: "提示"
//        });
//        alertdialog.content(msg);
//        ContentManager.AutoCloseArt(alertdialog);
//    }
//};

