//全部选择
$("#all").click(function () {
    $("input[name='chk']").each(function () {
        $(this).attr("checked", true);
    });
    return false;
});

//取消选择
$("#delAll").click(function () {
    $("input[name='chk']").each(function () {
        $(this).attr("checked", false);
    });
    return false;
});

//反向选择
$("#antiAll").click(function () {
    $("input[name='chk']").each(function () {
        $(this).attr("checked", !this.checked);
    });
    return false;
});
