﻿;
(function ($) {
    $.extend({
        toJSON: function (arr) {
            if (arr == null) {
                return "[]";
            } else {
                var jsonResult = "[";
                for (var i = 0; i < arr.length; i++) {
                    //获得操作符
                    jsonResult += "{";
                    jsonResult += "'paraname':" + "'" + arr[i].name + "',";
                    var op = $("#" + arr[i].name).attr("data_op");
                    if (op) {
                        jsonResult += "'searchop':" + "'" + op + "' ,";
                    }
                    jsonResult += "'paravalue':" + "'" + arr[i].value.trim() + "'},";
                }
                var result = jsonResult;
                if (result.trim().length > 1) {
                    result = result.substr(0, result.length - 1);
                }
                result += "]";
                return result;
            }
            return "[]";
        }
    });
})(jQuery);
/**
 * 
 * 消息提示的方法帮助类
 */
var msgHelper = (function () {
    function msgHelper() { };
    //使用弹窗展示的方法
    msgHelper.msgcall = function (data) {
        switch (data.status) {
            case "11-001":
                msgHelper.msgcallImg(data.msg, "succeed");
                break;
            case "11-002":
            case "11-003":
            case "11-004":
                msgHelper.msgcallImg(data.msg, "error");
                break;
        }
    };
    /**
     * 加上标示
     * @param {} msg 
     * @param {} icon 
     * @returns {} 
     */
    msgHelper.msgcallImg = function (msg, icon) {
        var alertdialog = art.dialog({
            icon: icon,
            min: false,
            max: false,
            title: "提示"
        });
        alertdialog.content(msg);
        msgHelper.AutoCloseArt(alertdialog);
    };
    /**
     * 关闭弹窗
     * @param {} art 
     * @returns {} 
     */
    msgHelper.AutoCloseArt = function (art) {
        var timeoutId = setInterval(function () {
            art.close();
            clearInterval(timeoutId);
        }, 2500);
    }
    //消息提示没有选中数据行
    msgHelper.msgShow = function (title, msgcon) {
        swal({
            title: title,
            text: msgcon
        });
    };
    //未选中数据行
    msgHelper.emptySelect = function () {
        msgHelper.msgcallImg("请先选择一条数据记录,然后再进行操作!", "warning");
    };
    /**
     * 提示的锁屏
     * @returns {} 
     */
    msgHelper.Tip = function () {
        $.blockUI({
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            }
        });
    }
    return msgHelper;
})();
/**
 * 窗口帮助类用于弹窗和消息提示使用
 */
var windowHelper = (function () {
    var windowHelper = function () { };

    /**========================================*/
    /**
     * 添加窗口使用的
     * @param {} htmlcontent 
     * @param {} para 
     * @param {} callback 
     * @returns {} 
     */
    windowHelper.addUIWindow = function (htmlcontent, para, callback) {
        if (htmlcontent.indexOf('<!DOCTYPE html>') > 0) {
            msgHelper.msgcallImg('您无权访问此页面!', 'error');
        } else {
            art.dialog({
                okValue: '保存',
                title: para.AddTitle,
                content: htmlcontent,
                min: false,
                max: false,
                lock: true,
                ok: function () {
                    if ($(para.FormId).bValidator().validate()) {
                        msgHelper.Tip();
                        callback();
                    }
                    else {
                        return false;
                    }
                },
                cancel: function () {
                    this.close();
                }
            });
        }

    };
    /**
     * 修改窗口
     * @param {} htmlcontent 
     * @param {} para 
     * @param {} callback 
     * @returns {} 
     */
    windowHelper.editUIWindow = function (htmlcontent, para, callback) {
        if (htmlcontent.indexOf('<!DOCTYPE html>') > 0) {
            msgHelper.msgcallImg('您无权访问此页面!', 'error');
        } else {
            art.dialog({
                okValue: '保存',
                title: para.AddTitle,
                content: htmlcontent,
                min: false,
                max: false,
                lock: true,
                ok: function () {
                    if ($(para.FormId).bValidator().validate()) {
                        msgHelper.Tip();
                        callback();
                    } else {
                        return false;
                    }
                },
                cancel: function () {
                    this.close();
                }
            });
        }
    };
    /**========================================*/
    return windowHelper;
})();
(function ($, exports) {
    var crud = exports.crud || crud;
    /**
     * 操作路径的
     */
    var OptionObj = {
        EditUI: 'Edit',
        Edit: 'Edit',
        AddUI: 'Add',
        Add: 'Add',
        DeleteUrl: 'Delete',
        RoleMenuUrl: 'SetPrivilege'
    };
    /**
     * 
     * 处理url
     * @param {} urls 
     * @returns {} 
     */
    var createurl = function (urls) {
        var res = ""
        if (urls.length < 1) {
            return res;
        } else {
            var urlarr = urls.split('/');
            for (var j = 0; j < urlarr.length - 1; j++) {
                res += urlarr[j] + "/";
            }
            return res;
        }
    };
    /**
     * 初始化样式
     * @returns {} 
     */
    var initBtnStyle = function () {
        $("#add").button({
            icons: {
                primary: "ui-icon-info"
            }
        });
        $("#delete").button({
            icons: {
                primary: "ui-icon-script"
            }
        });
        $("#edit").button({
            icons: {
                primary: "ui-icon-alert"
            }
        });
        $("#setprivilege").button({
            icons: {
                primary: "ui-icon-user"
            }
        });
        $("#search").button({
            icons: {
                primary: "ui-icon-search"
            }
        });
        $("#searchBtn").button({
            icons: {
                primary: "ui-icon-search"
            }
        });

    };
    /**
     * 构造函数
     * @param {} getdataurl 
     * @param {} gridid 
     * @returns {} 
     */
    var CRUD = function (getdataurl, gridid) {
        this.GetDataUrl = getdataurl;
        initBtnStyle();
        /**
         * 默认的一些参数值
         */
        this.defaults = {
            'GridId': '#list4',
            'FormId': '#formui',
            'SearchForm': '#searchform',
            "GridPager": "#gridPager",
            'AddTitle': '添加',
            'EditTitle': '修改',
            'DeleteTitle': '删除',
            'DeleteText': '数据不可恢复,确认删除此项记录?',
            'AddWindow': [800, 500],//[宽，高]
            'EditWindow': [800, 500],
            'AddBtn': ['保存', '取消'],
            'PrimaryId': 'id'
        };
        this.options = $.extend({}, this.defaults, gridid);
        //正则表达式获得GetData之前的路径截取出来
        var urlbase = createurl(getdataurl);
        this.EditUI = urlbase + OptionObj["EditUI"];
        this.Edit = urlbase + OptionObj["Edit"];
        this.AddUI = urlbase + OptionObj["AddUI"];
        this.Add = urlbase + OptionObj["Add"];
        this.DeleteUrl = urlbase + OptionObj["DeleteUrl"];
        this.CurRow = null;
        this.UrlBase = urlbase;
        this.RoleMenuUrl = urlbase + OptionObj.RoleMenuUrl;
        //初始化样式
    };
    CRUD.fn = CRUD.prototype;
    /**
     * 初始化窗口的参数如窗口的宽高，窗口的标题窗口的
     * @param {} paras 
     * @returns {} 
     */
    CRUD.fn.initWindowPara = function (paras) {
        this.options = $.extend({}, this.defaults, paras);
    }
    /**
    * 添加窗口的展现
    * @returns {} 
    */
    CRUD.fn.AddUIWindow = function () {
        var that = this;
        $.ajax({
            type: "GET",
            url: that.AddUI,
            success: function (data) {
                console.info(data);
                windowHelper.addUIWindow(data, that.options, function () {
                    $.post(that.Add,
                    $(that.options.FormId).serialize(),
                    function (datacall) {
                        $.unblockUI();
                        msgHelper.msgcall(datacall);
                        //刷新数据源
                        $(that.options.GridId).jqGrid('setGridParam', { search: true, mtype: 'POST' }).trigger("reloadGrid", [{ page: 1 }]);
                    });
                });
            }
        });
    };
    /**
     * 
     * 修改窗口的触发事件
     * @returns {} 
     */
    CRUD.fn.EditUIWindow = function () {
        var that = this;
        var priid = that.options.PrimaryId;
        if (that.CurRow) {
            var primaryid = this.CurRow[priid];
            $.ajax({
                type: "GET",
                url: that.EditUI,
                data: { id: primaryid },
                success: function (data) {
                    windowHelper.editUIWindow(data, that.options, function () {
                        $.post(that.Edit,
                        $(that.options.FormId).serialize(),
                        function (datacall) {
                            $.unblockUI();
                            msgHelper.msgcall(datacall);
                            that.CurRow = null;
                            //刷新数据源
                            $(that.options.GridId).jqGrid('setGridParam', { search: true, mtype: 'POST' }).trigger("reloadGrid", [{ page: 1 }]);
                        });
                    });
                }
            });
        } else {
            msgHelper.emptySelect();
        }

    };
    /**
     * 删除一条记录
     * @returns {} 
     */
    CRUD.fn.DeleteRecord = function () {
        var that = this;
        var priid = that.options.PrimaryId;
        if (that.CurRow) {
            var primaryid = this.CurRow[priid];
            art.dialog.confirm(that.options.DeleteText, function () {
                var callmethod = function (datares) {
                    that.CurRow = null;
                    msgHelper.msgcall(datares);
                    //刷新数据源
                    $(that.options.GridId).jqGrid('setGridParam', { search: true, mtype: 'POST' }).trigger("reloadGrid", [{ page: 1 }]);
                };
                $.post(that.DeleteUrl, { id: primaryid }, callmethod);
            }, function () {
                msgHelper.msgcallImg("已取消", "您取消了删除操作！", "error");
            });
        } else {
            msgHelper.emptySelect();
        }
    };
    /**
     * 查询方法的执行
     * @returns {} 
     */
    CRUD.fn.SearchMethod = function () {
        var that = this;
        var currentGrid = $(that.options.GridId);
        var postData = currentGrid.jqGrid("getGridParam", "postData");
        var otherData = $(that.options.SearchForm).serializeArray();
        otherData = $.toJSON(otherData);
        $.extend(postData, { customPar: otherData });
        currentGrid.jqGrid('setGridParam', { search: true, mtype: 'POST' }).trigger("reloadGrid", [{ page: 1 }]);
        return false;
    };
    /**
     * 设置权限的页面
     * @returns {} 
     */
    CRUD.fn.SetPrivilege = function () {
        var that = this;
        var priid = that.options.PrimaryId;
        if (that.CurRow) {
            var primaryid = this.CurRow[priid];
            art.dialog.open(that.RoleMenuUrl + "/" + primaryid, {
                id: 'myp',
                title: '分配权限行为窗口',
                min: false,
                max: false,
                lock: true,
                ok: function () {
                    var tt = this.iframe.contentWindow;;
                    if (tt != null) {
                        var paramethod = new tt.getParamater();
                        var menus = paramethod.getmenus();
                        var roleid = paramethod.getroleid();
                        var checklength = menus.length;
                        if (checklength < 1) {
                            layer.msg("您尚未勾选任何菜单,请重试！", { icon: 2 });
                            return;
                        } else {
                            var menuids = "";
                            for (var j = 0; j < checklength; j++) {
                                var yh = menus[j];
                                menuids += yh.id + ",";
                            }
                            var saveur = that.RoleMenuUrl;
                            $.post(saveur, { roleid: roleid, menuids: menuids }, function (datacall) {
                                msgHelper.msgcall(datacall);
                            });
                            art.dialog.list['myp'].close();
                        }

                    }

                },cancel:true
            });
            //layer.full(index);
        } else {
            msgHelper.emptySelect();
        }

    };
    /**
     * 初始化表头的方法
     * @param {} coltitle 
     * @param {} cols 
     * @returns {} 
     */
    CRUD.fn.initTableCol = function (coltitle, cols) {
        var that = this;
        $(that.options.GridId).jqGrid({
            url: that.GetDataUrl,
            datatype: "json",
            mtype: 'POST',
            height: 360,//高度，表格高度。可为数值、百分比或'auto'
            //width:1000,//这个宽度不能为百分比
            autowidth: true,//自动宽
            shrinkToFit: true,
            colNames: coltitle,
            colModel: cols,
            rownumbers: true,//添加左侧行号
            rowNum: 15,//每页显示记录数
            rowList: [15, 20, 25],//用于改变显示行数的下拉列表框的元素数组。
            onSelectRow: function (id) {
                that.CurRow = $(that.options.GridId).getRowData(id);
            },
            pager: $(that.options.GridPager),
            viewrecords: true,//是否在浏览导航栏显示记录总数
            // caption: "jqGrid 示例1",
            hidegrid: false
        });
        //绑定按钮的事件
        $("body").on("click", "button", function () {
            var curop = $(this).attr("op");
            switch (curop) {
                case "search":
                    that.SearchMethod();
                    break;
                case "add":
                    that.AddUIWindow();
                    break;
                case "delete":
                    that.DeleteRecord();
                    break;
                case "edit":
                    that.EditUIWindow();
                    break;
                case "setprivilege":
                    that.SetPrivilege();
                    break;
            }
        });
    };

    /**
     * 公布接口
     */
    exports.crud = CRUD;
})(jQuery, window);