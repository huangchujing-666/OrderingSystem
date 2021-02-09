$(function () {
    var id = $('#id'),
        name = $('#name'),
        orignPrice = $('#orignPrice'),
        realPrice = $('#realPrice'),
        businessmanId = $('#businessmanId'),
        special = $('#special'),
        remark = $('#remark'),
        notice = $('#notice'),
        bindCard = $('#bindCard'),
        useCount = $('#useCount'),
        rules = $('#rules');


    //var attrids = "";
    //var nutriids = "";
    var businessmanItems = "";

    //保存
    $('#btnSave').click(function () {

        //验证

        if (businessmanId.val() <= 0) {
            swal("提示", "请选择所属商家");
            return false;
        }
        if (name.val() == null || $.trim(name.val()) == '') {
            swal("提示", "请输入名称");
            name.focus();
            return false;
        }
        if (orignPrice.val() == null || orignPrice.val() == '' || orignPrice.val() == 0) {
            swal("提示", "请输入价格");
            orignPrice.focus();
            return false;
        }

        // introduces = UE.getEditor('description').getContent();
        var dataArr = {
            TicketId: id.val(),
            Name: name.val(),
            BusinessInfoId: businessmanId.val(),
            OrignPrice: orignPrice.val(),
            RealPrice: realPrice.val(),
            Special: special.val(),
            Remark: remark.val(),
            BindCard: bindCard.val(),
            UseCount: useCount.val(),
            Notice: notice.val(),
            //Rules: rules.val(), 
            Rules: UE.getEditor('rules').getContent(),
        };
        $.ajax({
            type: "post",
            url: "/Ticket/Edit",
            data: dataArr,
            dataType: "json",
            beforeSend: function () {
                //window.parent.showModal();
            },
            success: function (data) {
                if (data.Status == 200) {
                    swal("提示", "操作成功");
                    setTimeout(function () {
                        //window.location = '/Ticket/List?RefreshFlag=1';
                        history.go(-1);
                    }, 1500);
                }
                if (data.Status == 202) {
                    swal("提示", "操作失败");
                }
                if (data.Status == 203) {
                    swal("提示", "数据重复");
                }
            },
            complete: function () {
                //window.parent.hideModal();
            },
        });
    });
});
