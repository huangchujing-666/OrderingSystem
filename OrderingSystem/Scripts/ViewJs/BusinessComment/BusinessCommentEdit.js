$(function () {
    $('#btnSave').click(function () {
        var BusinessCommentId = $('#BusinessCommentId'),
           UserId = $('#UserId'),
           BusinessInfoId = $('#BusinessInfoId'),
           Contents = $('#Contents'),
           IsAnonymous = $("input[name='IsAnonymous']:checked"),
           LevelId = $('#LevelId'),
           RecommendDishes = $('#RecommendDishes');
        //校验
        if (Contents.val() == null || Contents.val() == '') {
            swal("提示", "请输入商家评论信息");
            Contents.focus();
            return false;
        }
        if (LevelId.val() ==0) {
            swal("提示", "请输入选择评论等级");
            Contents.focus();
            return false;
        }
        if (RecommendDishes.val() == null || RecommendDishes.val() == '') {
            swal("提示", "请输入推荐商家菜品");
            RecommendDishes.focus();
            return false;
        }
        var dataArr = {
            BusinessCommentId: BusinessCommentId.val(),
            UserId: UserId.val(),
            BusinessInfoId: BusinessInfoId.val(),
            Contents: Contents.val(),
            IsAnonymous: IsAnonymous.val(),
            LevelId: LevelId.val(),
            RecommendDishes:RecommendDishes.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BusinessComment/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BusinessComment/List';
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