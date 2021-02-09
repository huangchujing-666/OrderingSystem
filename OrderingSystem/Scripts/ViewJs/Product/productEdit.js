$(function () {
    var id = $('#id'),
        name = $('#name'),
        orignPrice = $('#orignPrice'),
        realPrice = $('#realPrice'), 
        startDate = $('#StartDate'),
        endDate = $('#EndDate'),
        businessmanId = $('#businessmanId'),
        //businessmanIds = $('#businessmanIds'), 
        description = $('#description'),
        content = $('#content'),
        remark = $('#remark'),
        notice = $('#notice'),
        rules = $('#rules'),
        //imgInfoId = $('#imgInfoId'), 
        showImgInfoId = '';

    //var attrids = "";
    //var nutriids = "";
    var businessmanItems = "";

    var nolimit = $("input[name='nolimite']");
    
    var _nolimit = 0;

     
    //保存
    $('#btnSave').click(function () {

        //console.log();

        //验证
        if (id.val() == 0) {
            //businessmanItems = businessmanIds.val() == null ? "" : businessmanIds.val().join(",");
             
            //if (businessmanItems == '') {
            //    swal("提示", "请选择一个或多个商家");
            //    businessmanIds.focus();
            //    return false;
            //}

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

        if (nolimit.is(':checked') != true) {
            if (startDate.val() == null || $.trim(startDate.val()) == '') {
                swal("提示", "请选择有效期的开始日期");
                startDate.focus();
                return false;
            }
            if (endDate.val() == null || $.trim(endDate.val()) == '') {
                swal("提示", "请选择有效期的结束日期");
                endDate.focus();
                return false;
            }
        }else {
            _nolimit = 1;
        }
       
        
         
        ////重新上传图片时删除旧缩略图
        //if (imgInfoId.val() != $('#oldImgInfoId').val()) {
        //    $.post("/Uploader/DeleteFile", { Id: $('#oldImgInfoId').val() }, function (data) { $('#divShowImgHid').empty(); });
        //}
        ////获取商家展示图
        //var showImgIds = [];
        //$("input[name='hidFileShowImgId']").each(function () {
        //    showImgIds.push($(this).val());
        //});
        //if (showImgIds.length == 0)
        //    showImgInfoId = $('#showImgInfoId').val();
        //else
        //    showImgInfoId = showImgIds.join(',');
        ////重新上传图片时删除展示图
        //if (showImgIds.length > 0 && $('#showImgInfoId').val() != null && $('#showImgInfoId').val() != '') {
        //    $.post("/Uploader/DeleteFileImgs", { FileImgs: $('#showImgInfoId').val() }, function (data) { $('#divQualificationsImgHid').empty(); });
        //}

       // var _description = UE.getEditor('description').getContent();//获取编辑器内容

        ////验证图片
        //if (imgInfoId.val() == "0" || imgInfoId.val() == '' || imgInfoId.val() == null) {
        //    swal("提示", "请选择并上传封面图片");
        //    return false;
        //}
        //if (showImgInfoId == '' || imgInfoId == null) {
        //    swal("提示", "请选择并上传宣传图片");
        //    return false;
        //} 
       // introduces = UE.getEditor('description').getContent();
        var dataArr = {
            ProductId: id.val(),
            Name: name.val(),
            OrignPrice: orignPrice.val(),
            RealPrice: realPrice.val(), 
            BusinessInfoId: businessmanId.val(),
            Business_Ids: businessmanItems,
            StartDate: startDate.val(),
            EndDate: endDate.val(),
            UseDateLimit: _nolimit,
            Descript: description.val(),
            Content: content.val(),
            Remark: remark.val(),
            Notice: notice.val(),
            Rules: rules.val()
        }; 
        $.ajax({
            type: "post",
            url: "/Product/Edit",
            data: dataArr,
            dataType: "json",
            beforeSend: function () {
                window.parent.showModal();
            },
            success: function (data) {
                if (data.Status == 200) {
                    swal("提示", "操作成功");
                    setTimeout(function () {
                        window.location.href = '/Product/List?RefreshFlag=1';
                        //history.go(-1);
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
                window.parent.hideModal();
            },  
        });
    });
});
