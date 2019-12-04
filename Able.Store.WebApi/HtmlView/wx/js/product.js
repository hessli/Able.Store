/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function () {
    var goodsid = $.query.get("goodsid");
    if (goodsid == "") {
        $("html,body").css({ "visibility": "hidden", "height": "100%", "overflow": "hidden" });
        windowAlert("商品不存在", 0);
        return
    }
    var postData = {};
    postData.goodsid = goodsid;
    //调出APP		
    var loadDateTime = new Date();
    window.setTimeout(function () {
        var timeOutDateTime = new Date();
        if (timeOutDateTime - loadDateTime < 500) {
            getDataList(postData);
        }
    }, 400);

    var url = "stbl://stbl/?m=1&v=" + goodsid;
    $("#open_app").attr("href", url);
    document.getElementById("open_app").click();

    // 获取动态
    function getDataList(postData) {
        $.ajax({
            type: "post",
            url: baseapi_url + "/h5/web/goods/get",
            // contentType: "application/json; charset=utf-8",
            data: postData,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                var html = "";
                var imgs = data.result.proddetail.bannerimgurls;
                for (var i = 0; i < imgs.length; i++) {
                    html += "<li><img src='" + imgs[i] + "' /></li>";
                }
                $(".ul-flash").html(html);
                $(".flash").addClass("swiper-container").append("<div class='num'></div>");
                $(".flash>ul:first").addClass("swiper-wrapper").find("li").addClass("swiper-slide");
                var auto_width = $(".flash").width();
                var auto_height = 620;
                var auto_num = $(".flash>ul:first img").length;

                function flashs() {
                    //获取图片高度
                    if (document.documentElement.clientWidth >= 640) {
                        auto_width = 640;
                        auto_height = 620;
                    } else {
                        auto_width = document.documentElement.clientWidth;
                        auto_height = 620 * auto_width / 640;
                    }
                    $(".flash").width(auto_width).height(auto_height);
                    $(".flash>ul:first li").width(auto_width);
                    $(".flash>ul:first").width(auto_width * auto_num).height(auto_height);
                }

                flashs();
                $(window).resize(function () { flash() });
                $('.flash').swiper({
                    pagination: '.flash .num',
                    mode: 'horizontal',
                    loop: true,
                    autoplay: 5000,
                    autoplayDisableOnInteraction: false
                });
                var html = "<h1 class='product-tit'>" + data.result.proddetail.goodsname + "</h1><div class='sales'>销量：" + data.result.proddetail.account.salecount + "</div><div class='product-tag'><div class='baoyou'>包邮</div><div class='tuihuan'>7天无条件退换货</div></div><div class='price'>¥" + data.result.proddetail.minprice + "</div>";
                $(".product-info").html(html).after("<div class='who open' data-id='" + data.result.proddetail.shopid + "'><div class='who-img'><img src='" + data.result.proddetail.shopinfoview.shopimgurl + "' /></div><h3>" + data.result.proddetail.shopinfoview.shopname + "</h3></div>");
                $(".talk-number").html(data.result.proddetail.account.commentcount + "条");
                $("#shouchang").html(data.result.proddetail.account.collectcount);

                //商品详情
                var content = data.result.proddetail.goodsinfo;
                var contents = "";
                var zhaopian = data.result.proddetail.imginfos;
                if (zhaopian.length > 0) {
                    contents = "<p>" + content + "</p>";
                    for (var i = 0; i < zhaopian.length; i++) {
                        if (zhaopian[i]) {
                            contents += "<img src='" + zhaopian[i].imgurl + "/>";
                            if (zhaopian[i].description != undefined) {
                                contents += "<p>" + zhaopian[i].description + "</p>";
                            }
                        }
                    }
                } else {
                    contents = content;
                }
                $(".goods-contnet").html(contents);

                var html = "";
                var rec = data.result.recommendgoods;
                for (var i = 0; i < rec.length; i++) {
                    html += "<li data-img='" + rec[i].imgurl + "' data-id='" + rec[i].goodsid + "'><div class='tj-img'><img src='" + rec[i].imgurl + "' /></div><p>" + rec[i].goodsname + "</p><div class='pixs'>" + rec[i].minprice + "</div></li>";
                }
                $(".tjs").html(html);
                $(".tjs").width(($(".tjs").find("li").length + 0.3) * $(".tjs").find("li").outerWidth(true));
                var myScroll = new IScroll("#srcolls", { probeType: 3, scrollX: true, freeScroll: true, fadeScrollbars: false, resizeScrollbars: false, shrinkScrollbars: 'clip', scrollbars: true, scrollbars: 'custom' });
                $(".tjs").find("li").each((function () {
                    var img = new Image();
                    var imgs = $(this).find("img");
                    img.src = imgs.attr("src");
                    if (img.complete) {
                        small(imgs);
                    } else {
                        img.onload = function () {
                            small(imgs);
                        }
                    }
                }))
                $(".hidden").css({ "visibility": "visible" });

                var sdk = new JSSDK();
                sdk.init();
                sdk.change_share({
                    "title": "商品分享",
                    "desc": data.result.proddetail.goodsname,
                    "image": imgs && imgs.length > 0 ? imgs[0] : "",
                    "url": window.location.href
                });
            },
            error: function () {
                windowAlert("网络错误，请重试");
            },
            headers: {
                "x-stbl-token": window.token
            }
        });
    }

    //日期处理
    function setTimes(m) {
        var d = new Date();
        var ds = d.setTime(m * 1000);
        var nd = new Date(ds);
        return nd.getFullYear() + "-" + (nd.getMonth() + 1) + "-" + nd.getDate() + " " + nd.getHours() + ":" + nd.getMinutes() + ":" + nd.getSeconds()
    }
    //注册提示
    $(document).on("tap", ".regs", function (event) {
        event.preventDefault();
        $("#reg-tip").show();
    })
    $(document).on("tap", ".reg-tip-close", function (event) {
        event.preventDefault();
        $("#reg-tip").hide();
    })
    //进入个人部落
    $(document).on("tap", ".open", function (event) {
        event.preventDefault();
        var link = this.getAttribute("data-id");
        window.location.href = "tribe.html?userid=" + link;
    })
    //进入个人链接
    $(document).on("tap", ".ul-links", function (event) {
        event.preventDefault();
        window.location.href = "links.html?userid=" + userid;
    })
    //大图预览
    $(document).on("tap", ".tjs li", function (evnet) {
        event.preventDefault();
        window.location.href = "product.html?goodsid=" + this.getAttribute("data-id");
    })
    $(".go-top").click(function () { $('html,body').animate({ scrollTop: '0px' }, 800); })
})