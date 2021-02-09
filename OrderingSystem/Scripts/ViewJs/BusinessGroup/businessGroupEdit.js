$(function () {

    $("#btnSave").click(function () {

        var id = $('#id'),
            imgInfoId = $('#imgInfoId'),
            oldImgInfoId = $('#oldImgInfoId'),
           name = $('#name'),
           businessTypeId =$("#businessTypeId"),
        sortNo = $('#sortNo');
        //数据校验
        if ($.trim(name.val()) == '' || name.val() == null) {
            name.focus();
            return false;
        }
        var dataArr = {
            BusinessGroupId: id.val(),
            Name: name.val(),
            BusinessTypeId: businessTypeId.val(),
            Sort: sortNo.val(),
            BaseImageId: imgInfoId.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BusinessGroup/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BusinessGroup/List';
                }, 1500);
            }
            if (data.Status == 202) {
                swal("提示", "操作失败");
            }
            if (data.Status == 203) {
                swal("提示", "数据重复");
            }
        }).complete(function () { window.parent.hideModal(); });
    })
});