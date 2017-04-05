//报表查询用的JS正在加载中.....
function reprotSreach(divId) {
    var alertdialog = art.dialog({
        title: "搜索条件",
        content: document.getElementById(divId),
        ok: function () {
            $.blockUI({ message: $("#content").html = '<img src="/Content/Gis/Images/GisLoading.gif"  alt="" />',
                css: {
                    left: '48%', 
                    width: 100,
                    height: 100,
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#616264',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .4
                }
             });
            document.form1.submit();
        },
        max: false,
        min: false,
        lock: true,
        zIndex: 9999,
        cancel: true //为true等价于function(){}
    });
}
