$(function () {
    $('#btnSave').click(function () {
        var ActivityMinusId = $('#ActivityMinusId'),
            BusinessInfoId = $('#BusinessInfoId'),
            AchiveAmount = $('#AchiveAmount'),
            MinusAmount = $('#MinusAmount');
        var a = parseFloat(AchiveAmount.val());

        var b = parseFloat(MinusAmount.val());
        //校验
        if (AchiveAmount.val() == null || AchiveAmount.val() == '') {
            swal("提示", "请输入满足金额");
            AchiveAmount.focus();
            return false;
        }
        if (MinusAmount.val() == null || MinusAmount.val() == '') {
            swal("提示", "请输入满减金额");
            MinusAmount.focus();
            return false;
        }
        if (AchiveAmount.val() <= 0) {
            swal("提示", "请输入大于0元的满足金额");
            AchiveAmount.focus();
            return false;
        }
        if (MinusAmount.val() < 0) {
            swal("提示", "请输入大于或等于0元的满减金额");
            MinusAmount.focus();
            return false;
        }
        if (a < b) {
            swal("提示", "满减金额必须小于或等于满足金额");
            AchiveAmount.focus();
            return false;
        }
        var dataArr = {
            ActivityMinusId: ActivityMinusId.val(),
            BusinessInfoId: BusinessInfoId.val(),
            AchiveAmount: AchiveAmount.val(),
            MinusAmount: MinusAmount.val()
        };
        window.parent.showModal();
        //提交
        $.post('/ActivityMinus/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/ActivityMinus/List?BusinessInfoId=' + BusinessInfoId.val();
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