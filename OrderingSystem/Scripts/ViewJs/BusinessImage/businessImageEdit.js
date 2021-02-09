$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            businessId = $('#businessId'),
            imgInfoId = $('#imgInfoId'),
            oldImgInfoId = $('#oldImgInfoId'),
            sortNo = $('#sortNo'),
            type = $('#type');
        //校验
        if (id.val() <= 0 && imgInfoId.val() == 0) {
            swal("提示", "请上传图片");
            return false;
        } 
        //重新上传图片时删除旧的图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        var dataArr = {
            BusinessImageId: id.val(),
            BaseImageId: imgInfoId.val(),
            BusinessInfoId: businessId.val(),
            SortNo: sortNo.val(),
            Type: type.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BusinessImage/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BusinessImage/List/' + businessId.val();
                }, 1500);
            }
            if (data.Status == 202) {
                swal("提示", "操作失败");
            }
            if (data.Status == 203) {
                swal("提示", "数据重复");
            }
        }).complete(function () { window.parent.hideModal(); });
    });
});