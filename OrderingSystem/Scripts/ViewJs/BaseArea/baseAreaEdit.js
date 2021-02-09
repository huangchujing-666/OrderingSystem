$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            fId = $('#fId'),
            name = $('#name');
        //校验
        if (name.val() == null || name.val() == '') {
            name.focus();
            return false;
        }
        var dataArr = {
            BaseAreaId: id.val(),
            FId: fId.val(),
            Name: name.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BaseArea/Edit', dataArr, function (data) { 
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    if (fId.val() > 0) {
                        history.go(-1);
                    } else {
                        window.location.href = '/BaseArea/List';
                    }
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