$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            imgInfoId = $('#imgInfoId'),
            oldImgInfoId = $('#oldImgInfoId'),
            roleId = $('#roleId'),
            businessInfoId = $('#businessInfoId'),
            account = $('#account'),
            passWord = $('#passWord'),
            nickName = $('#nickName'),
            mobilePhone = $('#mobilePhone'),
            remarks = $('#remarks');
        //校验
        if (roleId.val() == 0) {
            swal("提示", "请选择账号角色");
            return false;
        }
        
        if (roleId.val() == 2 && businessInfoId.val()<=0) {
            swal("提示", "请输入帐号对应的商家ID");
            return false;
        }

        if (account.val() == null || account.val() == '') {
            account.focus();
            return false;
        }
        if (id.val() == 0 && (passWord.val() == null || passWord.val() == '' || passWord.val().length < 6 || passWord.val().length > 18)) {
            swal("提示", "请输入6至18位数字或字母密码");
            passWord.focus();
            return false;
        }
        //删除旧图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        var dataArr = {
            SysAccountId: id.val(),
            SysRoleId: roleId.val(),
            BusinessInfoId: businessInfoId.val(),
            BaseImageId: imgInfoId.val(),
            Account: account.val(),
            PassWord: passWord.val(),
            NickName: nickName.val(),
            MobilePhone: mobilePhone.val(),
            Remarks: remarks.val()
        };
        window.parent.showModal();
        //提交
        $.post('/SysAccount/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/SysAccount/List';
                }, 1500);
            }
            if (data.Status == 202) {
                swal("提示", "操作失败");
            }
            if (data.Status == 203) {
                swal("提示", "该账号已存在，请勿重复添加。");
            }
        }).complete(function () { window.parent.hideModal(); });
    });
});