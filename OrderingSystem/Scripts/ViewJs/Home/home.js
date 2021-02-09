$(document).ready(function () {

    //获取新订单 
    var newCount = 0;
    var newOrders = function () {
        var businessId = $("#businessInfo_id").val();
        $.ajax({
            type: "get",
            url: "/home/GetNewOrders",
            data: {},
            dataType: "json",
            success: function (data) {
                if (data.Data == null) {
                    console.log("网络错误")
                    return;
                }
                if (data.Data > 0) {
                    newCount += data.Data;

                    //todo:提醒产生新订单操作
                    $("#page-wrapper .count-info span").text(newCount);
                    $("#page-wrapper .new-order strong").text(newCount);

                    //if ($.browser.msie && $.browser.version == '8.0') {
                    //    //本来这里用的是<bgsound src="system.wav"/>,结果IE8不播放声音,于是换成了embed 
                    //    $('#newMessageDIV').html('<embed src="Meadia/system.wav"/>');
                    //} else {
                    //IE9+,Firefox,Chrome均支持<audio/> 
                    $('#newMessageDIV').html('<audio autoplay="autoplay"><source src="/Meadia/system.wav"'
                    + 'type="audio/wav"/><source src="/Meadia/system.mp3" type="audio/mpeg"/></audio>');
                    // }
                    //} 

                } else {
                    $('#newMessageDIV').html('');

                    //$("#page-wrapper .count-info span").text(newCount);
                   // $("#page-wrapper .new-order strong").text(newCount); 
                }
            },
            error: function (x, h, r) {
                //console.log(x);
            }
        });
    };

    var bid = $("#businessInfo_id").val();
    if (bid > 0) {
        setInterval(newOrders, 5000);
    }

})