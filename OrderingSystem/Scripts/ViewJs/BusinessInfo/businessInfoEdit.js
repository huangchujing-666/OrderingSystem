$(function () {
    var id = $('#id'),
        roleId = $('#roleId'),
        cityId = $('#cityId'),
        lineId = $('#lineId'),
        stationId = $('#stationId'),
        businessTypeId = $('#businessTypeId'),
        businessGroupId = $('#businessGroupId'),
        name = $('#name'),
        openDate = $('#openDate'),
        refreshDate = $('#refreshDate'),
        totalRooms = $('#totalRooms'),
        totalFloors = $('#totalFloors'),
        mobile = $('#mobile'),
        //zone = $('#zone'),
        address = $('#address'),
        //businessType = $('#businessType'),
        //module = $('#module'),
        //sortNo = $('#sortNo'),
        longitude = $('#longitude'),
        latitude = $('#latitude'),
        orderCountPerMonth = $('#orderCountPerMonth'),
        averagePay = $('#averagePay'),
        grade = $('#grade'),
        businessHour = $('#businessHour'),
        sortNo = $('#sortNo'),
        notic = $('#notic'),
        introduction = $('#introduction'),
        services = $('#services'),
        imgInfoId = $('#imgInfoId');
    // showImgInfoId = '',
    //qualificationsImgInfoId = '';
    var showImgIds = [];

    //保存
    $('#btnSave').click(function () {
        if (cityId.val() <= 0) {
            swal("提示", "请选择商家所在城市");
            return false;
        }
        if (lineId.val() <= 0) {
            swal("提示", "请选择商家所在线路");
            return false;
        }
        if (stationId.val() <= 0) {
            swal("提示", "请选择商家所在站点");
            return false;
        }
        if (stationId.val() <= 0) {
            swal("提示", "请选择商家所在站点");
            return false;
        }
        if (name.val() == null || name.val() == '') {
            swal("提示", "请输入商家名称");
            name.focus();
            return false;
        }

        if (name.val() == null || name.val() == '') {
            swal("提示", "请输入商家名称");
            name.focus();
            return false;
        }
        if ($('#businessTypeId option:selected').val() == 4) {
            if (openDate.val() == null || openDate.val() == '') {
                swal("提示", "请输入开业时间");
                openDate.focus();
                return false;
            }
            if (refreshDate.val() == null || refreshDate.val() == '') {
                swal("提示", "请输入装修时间");
                refreshDate.focus();
                return false;
            }
            if (totalRooms.val() == null || totalRooms.val() == '') {
                swal("提示", "请输入总房间数");
                totalRooms.focus();
                return false;
            }

            if (totalFloors.val() == null || totalFloors.val() == '') {
                swal("提示", "请输入楼层总数");
                totalFloors.focus();
                return false;
            }
        }


        if (address.val() == null || address.val() == '') {
            swal("提示", "请输入联系地址");
            address.focus();
            return false;
        }
        if (longitude.val() == null || longitude.val() == '') {
            swal("提示", "请输入精度");
            longitude.focus();
            return false;
        }
        if (latitude.val() == null || latitude.val() == '') {
            swal("提示", "请输入精度");
            latitude.focus();
            return false;
        }
        if (orderCountPerMonth.val() == '') {
            swal("提示", "请输入月销售数量");
            orderCountPerMonth.focus();
            return false;
        }
        if (averagePay.val() == '') {
            swal("提示", "请输入人均消费");
            averagePay.focus();
            return false;
        }
        if (grade.val() == '') {
            swal("提示", "请输入商家等级");
            grade.focus();
            return false;
        }
        if (businessHour.val() == '') {
            swal("提示", "请输入营业时间");
            businessHour.focus();
            return false;
        }
        if (notic.val() == '') {
            swal("提示", "请输入公告内容");
            notic.focus();
            return false;
        }
        if (introduction.val() == '') {
            swal("提示", "请输入商家介绍");
            introduction.focus();
            return false;
        }
        // introduces = UE.getEditor('introduces').getContent();
        ////根据地址转换经纬度
        //$.ajax({
        //    type: "get",
        //    dataType: "json",
        //    async: false,
        //    url: "http://restapi.amap.com/v3/geocode/geo?key=39b079727546ead1997c830e74bf0ab2&s=rsv3&city=35&address=" + $('#address').val(),
        //    success: function (data) {
        //        var datajson = data.geocodes[0].location.split(',');
        //        longitude = datajson[0];
        //        latitude = datajson[1];
        //    }
        //});
        //重新上传图片时删除旧缩略图
        if (imgInfoId.val() != $('#oldImgInfoId').val()) {
            $.post("/Uploader/DeleteFile", { Id: $('#oldImgInfoId').val() }, function (data) { $('#divShowImgHid').empty(); });
        }
        ////获取商家展示图 
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

        //if (cityId.val() <= 0 || lineId.val() <= 0 || stationId.val() <= 0 || stationId.val() <= 0 || brandId.val() <= 0 || (name.val() == null || name.val() == '')
        //    || (telePhone.val() == null || telePhone.val() == '') || module.val() <= 0 || zone.val() == ""
        //    || (businessType.val() == null || businessType.val() == '' || businessType.val() == '请商家类型')
        //    ) {
        //    swal("提示", "请完善商家基本信息资料");
        //    return false;
        //}

        // introduces = UE.getEditor('introduces').getContent();

        ////获取商家资质图
        //var qualificationsImgIds = [];
        //$("input[name='hidFileQualificationsImgId']").each(function () {
        //    qualificationsImgIds.push($(this).val());
        //});
        //if (qualificationsImgIds.length == 0)
        //    qualificationsImgInfoId = $('#qualificationsImgInfoId').val();
        //else
        //    qualificationsImgInfoId = qualificationsImgIds.join(',');
        ////重新上传图片时删除展示图
        //if (qualificationsImgIds.length > 0 && $('#qualificationsImgInfoId').val() != null && $('#qualificationsImgInfoId').val() != '') {
        //    $.post("/Uploader/DeleteFileImgs", { FileImgs: $('#qualificationsImgInfoId').val() }, function (data) { $('#divQualificationsImgHid').empty(); });
        //};
        //验证图片
        if (imgInfoId.val() == '0' || imgInfoId.val() == '' || imgInfoId.val() == null) {
            swal("提示", "请选择并上传封面图片");
            return false;
        }
        //if (showImgInfoId == '' || imgInfoId == null) {
        //    swal("提示", "请选择并上传宣传图片");
        //    return false;
        //} if (qualificationsImgInfoId == '' || qualificationsImgInfoId == null) {
        //    swal("提示", "请选择并上传商家资质图");
        //    return false;
        //}
        var dataArr = {
            BusinessInfoId: id.val(),
            BaseAreaId: cityId.val(),
            BaseLineId: lineId.val(),
            BaseStationId: stationId.val(),
            BaseImageId: imgInfoId.val(),
            BusinessTypeId: businessTypeId.val(),
            BusinessGroupId: businessGroupId.val(),
            Name: name.val(),
            OpenDate: openDate.val(),
            RefreshDate: refreshDate.val(),
            TotalRooms: totalRooms.val(),
            TotalFloors: totalFloors.val(),
            Mobile: mobile.val(),
            Latitude: latitude.val(),
            Address: address.val(),
            Introduction: introduction.val(),
            //Services: services.val(),
            Services: UE.getEditor('services').getContent(),
            Longitude: longitude.val(),
            Notic: notic.val(),
            Grade: grade.val(),
            BusinessHour: businessHour.val(),
            Notic: notic.val(),
            SortNo: sortNo.val(),
            AveragePay: averagePay.val(),
            OrderCountPerMonth: orderCountPerMonth.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BusinessInfo/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    //如果操作角色是后台管理员，（roleid=1001），则跳转到列表
                    if (roleId.val() == 1000) {
                        window.location.href = '/BusinessInfo/List';
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
        $('#stationId').empty().append('<option value="0">请选择站点</option>');
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
        $('#stationId').empty().append('<option value="0">请选择站点</option>');
    });
});
$('#lineId').change(function () {
    $.post('/Area/GetLineIdByStations', { LineId: this.value }, function (data) {
        $('#stationId').empty();
        var html = '<option value="0">请选择站点</option>';
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].BaseStationId + '">' + data[i].Name + '</option>';
        }
        $('#stationId').append(html);
    });
});
