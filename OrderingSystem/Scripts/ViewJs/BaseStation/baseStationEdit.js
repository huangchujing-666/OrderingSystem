$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            cityId = $('#cityId'),
            lineId = $('#lineId'),
            name = $('#name'),
            sortNo = $('#sortNo');
        //校验
        if (cityId.val() <= 0) {
            swal("提示", "请选择站点城市");
            return false;
        }
        if (lineId.val() <= 0) {
            swal("提示", "请选择站点线路");
            return false;
        }
        if (name.val() == null || name.val() == '') {
            name.focus();
            return false;
        }
        var dataArr = {
            BaseStationId: id.val(),
            BaseAreaId: cityId.val(),
            BaseLineId: lineId.val(),
            Name: name.val(),
            //SortNo: sortNo.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BaseStation/Edit', dataArr, function (data) { 
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BaseStation/List';
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
        var html = '<option value="0">请选择地区市</option>';
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].BaseAreaId + '">' + data[i].Name + '</option>';
        }
        $('#cityId').append(html);
        $('#lineId').empty().append('<option value="0">请选择线路</option>');
    });
});
$('#cityId').change(function () {
    $.post('/Area/GetCityIdByLines', { CityId: this.value }, function (data) {
        $('#lineId').empty();
        var html = '<option value="0">请选择线路</option>';
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].BaseLineId + '">' + data[i].LineName + '</option>';
        }
        $('#lineId').append(html);
    });
});