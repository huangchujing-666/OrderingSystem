$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            imgInfoId = $('#imgInfoId'),
            Module = $("#module"),
            BusinessInfoId = $('#BusinessInfoId'),
            SortNo = $('#SortNo'),
            Descript = $('#Descript');
        var dataArr = {
            BusinessBannerImageId: id.val(),
            BaseImageId: imgInfoId.val(),
            Module:Module.val(),
            BusinessInfoId: BusinessInfoId.val(),
            SortNo: SortNo.val(),
            Descript: Descript.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BusinessBannerImage/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = "/BusinessBannerImage/List";
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