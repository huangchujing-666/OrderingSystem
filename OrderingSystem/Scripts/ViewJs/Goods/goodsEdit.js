$(function () {
    var id = $('#id'),
        name = $('#name'),
        orignPrice = $('#orignPrice'),
        realPrice = $('#realPrice'), 
        //dishecategoryId = $('#dishecategoryId'),
        businessmanId = $('#businessmanId'),
        //businessmanIds = $('#businessmanIds'),
        size = $('#size'),
        description = $('#description'), 
        imgInfoId = $('#imgInfoId'), 
        showImgInfoId = '';

    //var attrids = "";
    //var nutriids = "";
    var businessmanItems = "";
     
    //保存
    $('#btnSave').click(function () {
        //验证
        //if (id.val() == 0) {
        //    businessmanItems = businessmanIds.val() == null ? "" : businessmanIds.val().join(",");
        //    if (businessmanItems == '') {
        //        swal("提示", "请选择一个或多个商家");
        //        businessmanIds.focus();
        //        return false;
        //    }
        //} else {
            if (businessmanId.val() <= 0) {
                swal("提示", "请选择所属商家");
                return false;
            }
        //}
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
       
        //if (dishecategoryId.val() <= 0) {
        //    swal("提示", "请选择所属类别");
        //    return false;
        //}
           //验证2
        //if ($.trim(mainMaterial.val()) == '') {
        //    swal("提示", "请输入主要材料");
        //    attributeId.focus();
        //    return false;
        //}
        //attrids = attributeId.val() == null ? "" : attributeId.val().join(",");
        //nutriids = nutritionId.val() == null ? "" : nutritionId.val().join(",");
        //if (attrids == '') {
        //    swal("提示", "请输入属性特征");
        //    attributeId.focus();
        //    return false;
        //}
        //if (nutriids == '') {
        //    swal("提示", "请输入营养成分");
        //    attributeId.focus();
        //    return false;
        //}
        //重新上传图片时删除旧缩略图
        if (imgInfoId.val() != $('#oldImgInfoId').val()) {
            $.post("/Uploader/DeleteFile", { Id: $('#oldImgInfoId').val() }, function (data) { $('#divShowImgHid').empty(); });
        }
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

        //验证图片
        if (imgInfoId.val() == "0" || imgInfoId.val() == '' || imgInfoId.val() == null) {
            swal("提示", "请选择并上传封面图片");
            return false;
        }
        //if (showImgInfoId == '' || imgInfoId == null) {
        //    swal("提示", "请选择并上传宣传图片");
        //    return false;
        //} 
       // introduces = UE.getEditor('description').getContent();
        var dataArr = {
            GoodsId: id.val(),
            Name: name.val(),
            OrignPrice: orignPrice.val(),
            RealPrice: realPrice.val(),
            //GoodsCategoryId: dishecategoryId.val(),
            BusinessInfoId: businessmanId.val(),
            Size: size.val(),
            Business_Ids: businessmanItems, 
            Descript: description.val(),
            BaseImageId: imgInfoId.val()
           // ShowImgInfoId: showImgInfoId,
        }; 
        $.ajax({
            type: "post",
            url: "/Goods/Edit",
            data: dataArr,
            dataType: "json",
            beforeSend: function () {
                window.parent.showModal();
            },
            success: function (data) {
                if (data.Status == 200) {
                    swal("提示", "操作成功");
                    setTimeout(function () {
                        window.location.href = '/Goods/List?RefreshFlag=1';
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
