$(function() {
    //授权成功&&店铺状态正常 执行
    $.when(window.shopState).done(function() {
        //读取订单
        var loading = false;
        var orderId = $.query.get("orderId");
        var postData = {};
        postData.orderId = orderId;
        $.ajax({
            type: "get",
            url: window.baseurl+"api/order/get?orderId=" + orderId,
            dataType: "json",
            success: function(data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                if (data.result.state != 2) {
                    window.location.href = "order-detail.html?shopid=" + window.shopid + "&orderId=" + orderId;
                    return
                }
                $("#total").html("￥" + data.result.total.toFixed(2));
                $("#yunfei").html("￥" + data.result.freight.toFixed(2));
                //读取支付方式
                $.ajax({
                    type: "get",
                    url: window.baseurl+ "api/order/getPayWay",
                    dataType: "json",
                    success: function(data) {
                        if (!detectionResult(data, 0)) {
                            return;
                        }
                        var html = "";
                        for (var i = 0; i < data.result.length; i++) {
                            html += "<li data-id='" + data.result[i].payType + "'>" + data.result[i].title + "</li>";
                        }
                        $("#list").html(html).find("li:first").addClass("on");
                        $(".pay").show();
                    },
                    error: function() {
                        windowAlert("读取支付方式时发生网络错误,刷新重试", 0)
                    },
                    headers: {
                        "x-moutai-token": window.token,
                    }
                });
            },
            error: function() {
                windowAlert("网络错误,刷新重试", 0)
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        //支付
        //当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
        //document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            //var url = "http://moutai-mall.stbl.cc/api/mall/payconfig/get";
            //$.ajax({
            //    type: "post",
            //    url: url,
            //    dataType: "json",
            //    data: { "orderid": orderId, "shopid": shopid },
            //    success: function(data) {
            //        if (!detectionResult(data, 0)) {
            //            return;
            //        }
            //        if (!data.issuccess) {
            //            windowAlert(JSON.parse(data));
            //        }
            //        $(".none").show();
            //        $(document).on("tap", "#pay", function(event) {
            //            event.preventDefault();
            //            if (loading) {
            //                return;
            //            }
            //            loading = true;  
            //            WeixinJSBridge.invoke('getBrandWCPayRequest', {
            //                "appId": data.result.appId,              //公众号名称，由商户传入
            //                "timeStamp": data.result.timeStamp,      //时间戳
            //                "nonceStr": data.result.nonceStr,        //随机串
            //                "package": data.result.package,          //扩展包
            //                "signType": "MD5",                          //微信签名方式:MD5
            //                "paySign": data.result.paySign//微信签名
            //            }, function(res) {
            //                //使用以下方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
            //                //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
            //                if (res.err_msg == "get_brand_wcpay_request:ok") {
            //                    queryOrder(data.result.out_trade_no);
            //                    window.location.href = "pay-success.html?shopid=" + window.shopid + "&orderId=" + orderId;
            //                }
            //                else {
            //                    loading = false;
            //                    window.location.href = "pay-fail.html?shopid=" + window.shopid + "&orderId=" + orderId;
            //                }
            //            });
            //        });
            //    },
            //    error: function() {
            //        windowAlert("网络错误,刷新重试", 0)
            //    },
            //    headers: {
            //        "x-moutai-token": window.token,
            //    }
            //});

            //查询订单是否支付成功
            function queryOrder(out_trade_no) {
                var ourl = "http://moutai-mall.stbl.cc/api/mall/orderpay/check";
                // var ourl = "http://wx-auth.stbl.cc/moutai/mall/pay/query";
                $.post(ourl, { "orderid": orderId, "shopid": shopid, "out_trade_no": out_trade_no }, function(data) {
                    if (data.issuccess == 1) {
                        loading = false;
                        window.location.href = "pay-success.html?shopid=" + window.shopid + "&orderId=" + orderId;
                    }
                    else {
                        loading = false;
                        window.location.href = "pay-fail.html?shopid=" + window.shopid + "&orderId=" + orderId;
                    }
                }, "json");
            }
        //}, false);
    })
})
