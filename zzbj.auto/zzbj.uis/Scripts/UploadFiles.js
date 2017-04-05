// $("#uploadify")内的ID勿改
$(function () {
    $("#uploadify").uploadify({
        swf: '/Scripts/uploadify/uploadify.swf',
        script: '../ImportSea/editUpload/ ',
        buttonText: '选择文件',
        onError: function (event, ID, fileObj, errorObj) { alert(errorObj.info); },
        onUploadSuccess: function (data) {
            alert(data);
        }

    });
});
