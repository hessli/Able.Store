$(function () {
    //授权成功&&店铺状态正常 执行
    $.when(window.shopState).done(function () {
        //商品详情页图片轮播
        function flash() {
            $(".flash").addClass("swiper-container").append("<div class='num'></div>");
            $(".flash>ul:first").addClass("swiper-wrapper").find("li").addClass("swiper-slide");
            var auto_width = $(".flash").width();
            var auto_height = 640;
            var auto_num = $(".flash>ul:first img").length;
            function flash() {
                if (document.documentElement.clientWidth >= 640) {
                    auto_width = 640;
                    auto_height = 640;
                }
                else {
                    auto_width = document.documentElement.clientWidth;
                    auto_height = 640 * auto_width / 640;
                }
                $(".flash").width(auto_width).height(auto_height);
                $(".flash>ul:first li").width(auto_width);
                $(".flash>ul:first").width(auto_width * auto_num).height(auto_height);
            }
            flash();
            $(window).resize(function () { flash() });
            $('.flash').swiper({
                pagination: '.num',
                mode: 'horizontal',
            })
        }
        //获取商品信息
        window.productId = $.query.get("productId");
        window.skuId = $.query.get("skuid");

        $.ajax({
            type: "get",
            url: window.baseurl + "api/products/detail?productid=" + productId + "&skuid=" + skuId,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                //轮播图
                var html = "";

                var sku = data.result.sku;
                for (var i = 0; i < sku.imgs.length; i++) {
                    html += "<li><img src='" + sku.imgs[i].img + "' /></li>";
                }
                $("#flash").html(html);
                if (sku.imgs.length > 1) {
                    flash();
                }
                //简介
                $(".detail-content").html("<h1 class='detail-name'>" + sku.title + "</h1><p>销量：" + sku.saleqty + "</p><div class='price'>￥" + sku.price.toFixed(2) + "</div>");
                //型号
                $(".ul-key").text(data.result.propertyname);
                var html = "";
                for (var i = 0; i < data.result.properties.length; i++) {
                    if (data.result.properties[i].id == skuId) {
                        html += "<li data-id='" + data.result.properties[i].id + "' data-price='0' class='on'>" + data.result.properties[i].value + "</li>";
                    } else {
                        html += "<li data-id='" + data.result.properties[i].id + "' data-price='0' >" + data.result.properties[i].value + "</li>";
                    }
                }
                $(".ul-sx").html(html);
                //图文详情
                var html = "";

                //for (var i=0;i<data.result.content.length;i++){
                //	html += "<p><img src='"+data.result.image_domain+"ori/"+data.result.content[i]+"' /></p>";
                //}
                $(".tab-div").children().first().html("<div class='detail-text'>" + sku.content + "</div>" + html);
                //产品参数
                //$(".sxs").html("<dl><dt>香型</dt><dd>"+data.result.odor+"</dd></dl><dl><dt>酒精度</dt><dd>"+data.result.strength+"</dd></dl><dl><dt>净含量</dt><dd>"+data.result.net+"</dd></dl><dl><dt>规格</dt><dd>"+data.result.size+"</dd></dl><dl><dt>箱规</dt><dd>"+data.result.carton+"</dd></dl><dl><dt>贮存条件</dt><dd>"+data.result.tiaojian+"</dd></dl>");

                //产品参数
                var html = "";
                for (var i = 0; i < sku.skuattributes.length; i++) {

                    html += "<dl><dt>" + sku.skuattributes[i].name + "</dt><dd>" + sku.skuattributes[i].value + "</dd></dl>";
                }
                $(".sxs").html(html);
                $(".none").show();
                //分享
                //  wx.ready(function(){
                //   shareTitle = data.result.title + "-茅台总代理授权的正品微信商城，原厂发货价格实惠！";//朋友标题
                //shareTimeline= data.result.title + "-茅台总代理授权的正品微信商城，原厂发货价格实惠！";//朋友圈标题
                //shareDescContent= data.result.title + "-茅台总代理授权的正品微信商城，原厂发货价格实惠！";//朋友圈描述
                //shareUrl = window.location.href;//链接
                //shareImgUrl = data.result.image_domain+"640x640/"+data.result.imgs[0];//图标
                //  	wxShare();
                //  });
            },
            error: function () {
                windowAlert("网络错误，刷新重试", 0);
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });


        //选择型号
        $(document).on("tap", ".single li", function (event) {
            event.preventDefault();

            var skuid = $(this).attr("data-id");
            window.location.href = "product-detail.html?productId=" + productId + "&skuid=" + skuid;

            //$(".detail-content").find(".price").html("￥"+price);
        })
        //检测是否选择相关属性
        function testSelect() {
            if (!$(".ul-sx").find("li.on").length) {
                windowAlert("请先选择型号");
                return false;
            }
            //window.num = $(".shuliang").find("input").val();
            //window.pack = $(".ul-sx").find("li.on").attr("data-id");

            var num = $(".shuliang").find("input").val();
            var key = $(".ul-sx").find("li.on").attr("data-id");
            window.pack = {
                skuid: key,
                qty: num
            };
            if (pack.qty <= 0 || isNaN(pack.qty)) {
                windowAlert("商品数量不正确");
                return false;
            }
            return true;
        }
        //加入购物车
        var loading = false;
        $(document).on("tap", ".add-car", function (event) {
            event.preventDefault();
            if (loading) {
                return;
            }
            if (!testSelect()) {
                return;
            }
            loading = true;
            var postData = {};
            postData.pack = [pack];
            $.ajax({
                type: "post",
                url: window.baseurl + "api/shopping/tobasket",
                dataType: "json",
                data: postData,
                success: function (data) {
                    if (!detectionResult(data, 1)) {
                        loading = false;
                        return;
                    }
                    windowAlert("已成功加入购物车");
                    window.skuids = data.result;
                    loading = false;
                },
                error: function () {
                    windowAlert("加入购物车时发生网络错误，请重试");
                    loading = false;
                },
                headers: {
                    "x-moutai-token": window.token,
                }
            });
        })
        //立即购买
        $(document).on("tap", ".buy", function (event) {
            event.preventDefault();
            if (loading) {
                return;
            }
            if (!testSelect()) {
                return;
            }
            loading = true;
            if (typeof (window.skuids) != "undefined") {
                window.location.href = "buy.html?shopid=" + window.shopid + "&skuids=" + window.skuids;
            }
            else {
                var postData = {};
                postData.productId = productId;
                postData.pack = [pack];
                $.ajax({
                    type: "post",
                    url: window.baseurl + "api/shopping/tobasket",
                    dataType: "json",
                    data: postData,
                    beforeSend: function () {
                        windowAlert("即将跳转，请稍候...", 0);
                    },
                    success: function (data) {
                        if (!detectionResult(data, 1)) {
                            loading = false;
                            return;
                        }
                        window.skuids = data.result;
                        loading = false;
                        window.location.href = "buy.html?shopid=" + window.shopid + "&skuids=" + window.skuids;
                    },
                    error: function () {
                        windowAlert("网络错误，请重试");
                        loading = false;
                    },
                    headers: {
                        "x-moutai-token": window.token,
                    }
                });
            }
        })
        //滑动
        var mousedown = false;
        var x1, x2;
        var distance = 0;
        $("#detail-move").on("touchstart mousedown", function (e) {
            mousedown = true;
            switch (e.type) {
                case "mousedown":
                    x1 = e.pageX;
                    break;
                case "touchstart":
                    x1 = e.originalEvent.targetTouches[0].pageX;
                    break;
            }
        })
        $("#detail-move").on("touchmove mousemove", function (e) {
            if (mousedown) {
                switch (e.type) {
                    case "mousemove":
                        x2 = e.pageX;
                        break;
                    case "touchmove":
                        x2 = e.originalEvent.targetTouches[0].pageX;
                        break;
                }
                distance = Math.abs(x2 - x1);
                if (distance > 80) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (x2 < x1) {
                        $(".detail-tab").find("li").removeClass("on").eq(1).addClass("on");
                        $(".tab-div").children().hide().eq(1).show();
                    }
                    else {
                        $(".detail-tab").find("li").removeClass("on").eq(0).addClass("on");
                        $(".tab-div").children().hide().eq(0).show();
                    }
                }
            }
        })
        $("#detail-move").on("touchend mouseup", function (event) {
            mousedown = false;
        })
    })
})