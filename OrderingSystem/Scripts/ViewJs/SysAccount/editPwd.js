$(function () {
    $('#btnSave').click(function () {
        var _accountId = $('#accountId'),
            _account = $('#account'),
            _oldPassword = $('#oldPassword'),
            _newPassword = $('#newPassword'),
            _confirmPassword = $('#confirmPassword');
        if (_oldPassword.val() == '') {
            _oldPassword.focus();
            return false;
        }
        if (_newPassword.val() == '') {
            _newPassword.focus();
            return false;
        }
        if (_confirmPassword.val() == '') {
            _confirmPassword.focus();
            return false;
        }
        if (_confirmPassword.val().length < 6 || _confirmPassword.val().length > 18) {
            swal("提示", "密码长度必须为6-18位");
            return false;
        }
        if (_newPassword.val() != _confirmPassword.val()) {
            swal("提示", "两次密码输入不一致");
            return false;
        }
        $.ajax({
            type: "post",
            url: "/SysAccount/EditPwd",
            data: {
                AccountId: _accountId.val(), Account: _account.val().trim(), OldPassword: _oldPassword.val().trim(), NewPassword: _newPassword.val().trim()
            },
            dataType: "json",
            beforeSend: function () {
                window.parent.showModal();
            },
            success: function (data) {
                if (data.Status == 201) {
                    swal("提示", "原密码错误");
                } 
                if (data.Status == 200) {
                    swal("提示", "操作成功");
                    setTimeout(function () {
                        window.location.href = '/Login/Login';
                    }, 1500);

                }
            },
            complete: function () {
                window.parent.hideModal();
            },
        });
    });
});