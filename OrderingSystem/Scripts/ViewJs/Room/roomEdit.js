$(function () {
    var id = $('#id'),
        name = $('#name'),
        orignPrice = $('#orignPrice'),
        realPrice = $('#realPrice'),  
        businessmanId = $('#businessmanId'), 
        content = $('#content'),
        remark = $('#remark'), 
        rules = $('#Rules'),
        window = $('#Window'),
        breakfast = $('#Breakfast'),
        area = $('#Area'),
        internet = $('#Internet'),
        bed = $('#Bed'),
        bedType = $('#BedType'), 
        bathroom = $('#Bathroom'),
        airConditioner = $('#airConditioner'),
        notice = $('#Notice'),
        remain = $('#Remain');
         

    //var attrids = "";
    //var nutriids = "";
    var businessmanItems = "";
 
    //保存
    $('#btnSave').click(function () {
         
        //验证
        if (id.val() == 0) { 

            if (businessmanId.val() <= 0) {
                swal("提示", "请选择所属商家");
                return false;
            }
        } else {
            if (businessmanId.val() <= 0) {
                swal("提示", "请选择所属商家");
                return false;
            }
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
            RoomId: id.val(),
            Name: name.val(),
            OrignPrice: orignPrice.val(),
            RealPrice: realPrice.val(), 
            BusinessInfoId: businessmanId.val(),
            Business_Ids: businessmanItems,   
            Window: window.val(),
            Breakfast: breakfast.val(),
            Area: area.val(),
            Internet:internet.val(),
            Notice: notice.val(),
            Bed: bed.val(),
            BedType:bedType.val(),
            Bathroom: bathroom.val(),
            AirConditioner: airConditioner.val(),
            Remain: remain.val(), 
            Notice: notice.val(),
            Rules: rules.val()
        }; 
        $.ajax({
            type: "post",
            url: "/Room/Edit",
            data: dataArr,
            dataType: "json",
            beforeSend: function () {
                //window.parent.showModal();
            },
            success: function (data) {
                if (data.Status == 200) {
                    swal("提示", "操作成功");
                    setTimeout(function () {
                        //window.location = '/Room/List?RefreshFlag=1';
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
