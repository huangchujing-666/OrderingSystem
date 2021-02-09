$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            imgInfoId = $('#imgInfoId'),
            oldImgInfoId = $('#oldImgInfoId'),
            businessInfoId = $('#BusinessInfoId'),
            name = $('#Name'),
            content = UE.getEditor('content').getContent();
        //校验
        if (id.val() <= 0 && (imgInfoId.val() == '0' || imgInfoId.val() == null)) {
            swal("提示", "请上传文章图片");
            return false;
        }
     
        if (name.val() == null || name.val() == '') {
            name.focus();
            return false;
        }
        //重新上传图片时删除旧的图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        var dataArr = {
            JourneyArticleId: id.val(),
            BusinessInfoId: businessInfoId.val(),
            BaseImageId: imgInfoId.val(),
            Content: content,
            Name: name.val(),
            //SortNo: sortNo.val()
        };
        window.parent.showModal();
        //提交
        $.post('/JourneyArticle/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/JourneyArticle/List';
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