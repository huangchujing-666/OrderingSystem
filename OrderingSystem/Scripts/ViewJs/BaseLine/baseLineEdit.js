$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            imgInfoId = $('#imgInfoId'),
            oldImgInfoId = $('#oldImgInfoId'),
            cityId = $('#cityId'),
            lineName = $('#lineName'),
            lineNumber = $('#lineNumber'),
            sortNo = $('#sortNo');
        //校验
        if (id.val() <= 0 && (imgInfoId.val() == '0' || imgInfoId.val() == null)) {
            swal("提示", "请上传线路图片");
            return false;
        }
        if (cityId.val() == null || cityId.val() == '0') {
            cityId.focus();
            return false;
        }
        if (lineName.val() == null || lineName.val() == '') {
            lineName.focus();
            return false;
        }
        //重新上传图片时删除旧的图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        var dataArr = {
            BaseLineId: id.val(),
            BaseAreaId: cityId.val(),
            BaseImageId: imgInfoId.val(),
            LineName: lineName.val(),
            LineNumber: lineNumber.val(),
            //SortNo: sortNo.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BaseLine/Edit', dataArr, function (data) { 
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BaseLine/List';
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
//值改变事件
$('#provinceId').change(function () {
    $.post('/Area/GetProvinceIdByCitys', { ProvinceId: this.value }, function (data) {
        $('#cityId').empty();
        var html = '';
        if (data.length <= 0) {
            $('#cityId').append('<option value="0">请选择地区市</option>');
        }
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
        }
        $('#cityId').append(html);
    });
});