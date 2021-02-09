$(function () {
    $('#btnSave').click(function () {
        var ActivityDiscountId = $('#ActivityDiscountId'),
           Name = $('#Name'),
           BusinessInfoId = $('#BusinessInfoId'),
            Discount = $('#Discount');

        //校验
        if (Discount.val() == null || Discount.val() == '') {
            swal("提示", "请输入商家折扣");
            Discount.focus();
            return false;
        }
        else if (parseFloat(Discount.val()) <= 0 ||parseFloat( Discount.val()) > 1) {
            swal("提示", "请输入0.01至1以内的数字");
            Discount.focus();
            return false;

        }
        var dataArr = {
            ActivityDiscountId: ActivityDiscountId.val(),
            //Name: name.val(),
            Discount: Discount.val(),
            Name: Name.val(),
            BusinessInfoId: BusinessInfoId.val()
        };
        window.parent.showModal();
        //提交
        $.post('/ActivityDiscount/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/ActivityDiscount/List';
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