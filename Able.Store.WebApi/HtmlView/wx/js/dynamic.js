/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function () {
    var statusesid = $.query.get("statusesid");
    if (statusesid == "") {
        $("html,body").css({ "visibility": "hidden", "height": "100%", "overflow": "hidden" });
        windowAlert("动态不存在", 0);
        return
    }
    //获取动态
    var postData = {};
    postData.statusesid = statusesid;

    //调出APP		
    var loadDateTime = new Date();
    window.setTimeout(function () {
        var timeOutDateTime = new Date();
        if (timeOutDateTime - loadDateTime < 500) {
            getDataList(postData);
        }
    }, 400);

    var url = "stbl://stbl/?m=3&v=" + statusesid;
    $("#open_app").attr("href", url);
    document.getElementById("open_app").click();

    function getDataList(postData) {
        $.ajax({
            type: "post",
            url: baseapi_url + "/h5/web/statuses/detail_get",
            // contentType: "application/json; charset=utf-8",
            data: postData,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                var shareContent = "";
                if (data.result.statusestype == 1) {
                    $("#short").hide();
                    var datas = data.result;

                    if (data.result.statusespic) {
                        $(".full-img").html("<img src='" + data.result.statusespic.middlepic + data.result.statusespic.defaultpic + "' />");
                    }

                    var html = "";
                    var zhaopian = data.result.statusespic;
                    for (var i = 0; i < zhaopian.pics.length; i++) {
                        html += "<li data-img='" + zhaopian.originalpic + zhaopian.pics[i] + "'><div><img src='" + zhaopian.thumbpic + zhaopian.pics[i] + "' /></div></li>";
                    }
                    var user = data.result.user;
                    var sex = user.gender;
                    if (sex == 0) {
                        sex = "♂";
                    } else if (sex == 1) {
                        sex = "♀";
                    }
                    else {
                        sex = "";
                    }
                    if (user.age == 0 || user.age == null) {
                        var age = "0"
                    }
                    else {
                        var age = user.age;
                    }
                    var htmls = "<div class='user-photo open' data-id='" + user.userid + "'><img src='" + user.imgurl + "' /></div><h3 class='user-name'>" + user.nickname + "</h3><div class='clear'></div><div class='xb'>" + sex + " " + age + "</div><div class='pub-time'>" + setTimes(data.result.createtime) + "</div><div class='report regs'>举报</div>";
                    $(".users").eq(0).html(htmls);
                    var sex = user.gender;
                    if (sex == 0) {
                        $(".users .xb").addClass("man");
                    }
                    if (sex == -1) {
                        $(".users .xb").addClass("yao");
                    }
                    var content = data.result.content;
                    var n = content.split("{img}");
                    var contents = "";
                    var zhaopian = data.result.statusespic;
                    if (n.length > 0) {
                        for (var i = 0; i < n.length; i++) {
                            if (zhaopian.pics[i]) {
                                contents += "<p>" + n[i] + "</p><img src='" + zhaopian.originalpic + zhaopian.pics[i] + "' />";
                            }
                        }
                        shareContent = n[0];
                    }
                    else {
                        contents = content;
                        shareContent = content;
                    }
                    $(".long").html("<h1 class='long-title'>" + data.result.title + "</h1>" + contents);
                    var htmls = "<div class='photo-box'><div class='reward regs'><img src='images/reward.png' /></div><p>已有" + data.result.rewardcount + "人打赏</p></div><ul class='ul-ctrl'><li class='c-1 regs'>" + data.result.forwardcount + "</li><li class='c-2 regs'>" + data.result.commentcount + "</li><li class='c-3 regs'>" + data.result.praisecount + "</li><li class='c-4 regs'>" + data.result.favorcount + "</li></ul>";
                    $(".users").eq(1).html(htmls);
                    var w = $(".ul-photo").find("li").width();
                    $(".ul-photo").find("li").each((function () {
                        $(this).width(w).height(w);
                        $(this).parent().width($(this).outerWidth(true) * 3);
                        var img = new Image();
                        var imgs = $(this).find("img");
                        img.src = imgs.attr("src");
                        if (img.complete) {
                            small(imgs);
                        }
                        else {
                            img.onload = function () {
                                small(imgs);
                            }
                        }
                    }))
                    var bbs = data.result.comments;
                    var bbsHtml = "";
                    if (data.result.commentcount == 0) {
                        //$(".bbs").html("暂无评论");
                        $(".bbs").closest("dl").hide();
                    }
                    else {
                        for (var i = 0; i < bbs.length; i++) {
                            if (bbs.praisecount == 0) {
                                bbsHtml += "<li><div class='bbs-photo open' data-id='" + bbs[i].user.userid + "'><img src='" + bbs[i].user.imgurl + "' /></div><h4><strong>" + bbs[i].user.nickname + "</strong><time>" + setTimes(bbs[i].createtime) + "</time></h4><p>" + bbs[i].content + "</p><div class='diggs regs'>" + bbs[i].praisecount + "</div></li>";
                            }
                            else {
                                bbsHtml += "<li><div class='bbs-photo open' data-id='" + bbs[i].user.userid + "'><img src='" + bbs[i].user.imgurl + "' /></div><h4><strong>" + bbs[i].user.nickname + "</strong><time>" + setTimes(bbs[i].createtime) + "</time></h4><p>" + bbs[i].content + "</p><div class='diggs regs have-digg'>" + bbs[i].praisecount + "</div></li>";
                            }
                        }
                        $(".ul-bbs").html(bbsHtml);
                    }
                    var digg = data.result.praises;
                    var diggHtml = "";
                    /*
                    if ( data.result.ispraised == 0 ){
                        //$(".digg-list").html("暂无点赞");
                        $(".digg-list").hide();
                    }*/
                    if (data.result.praisecount == 0) {
                        //$(".digg-list").html("暂无点赞");
                        $(".digg-list").hide();
                    }
                    else {
                        for (var i = 0; i < digg.length; i++) {
                            diggHtml += "<li class='open' data-id='" + digg[i].user.userid + "'><img src='" + digg[i].user.imgurl + "' /></li>";
                        }
                        diggHtml += "<li class='regs'><img src='images/data/more.png' /></li>";
                        $(".digg-list").html(diggHtml);
                    }
                    if (data.result.links) {
                        switch (data.result.links.linktype) {
                            case 1:
                                var sex = data.result.links.userinfo.gender;
                                if (sex == 0) {
                                    sex = "♂";
                                } else if (sex == 1) {
                                    sex = "♀";
                                }
                                else {
                                    sex = "";
                                }
                                if (data.result.links.userinfo.age == 0 || data.result.links.userinfo.age == null) {
                                    var age = "0"
                                }
                                else {
                                    var age = data.result.links.userinfo.age;
                                }
                                $(".card-link").html("<li class='open-1' data-id='" + data.result.links.linkid + "'><div class='user'><div class='user-photo'><img src='" + data.result.links.userinfo.imgurl + "'></div><h3 class='user-name'>" + data.result.links.userinfo.nickname + "</h3><div class='xb'>" + sex + " " + age + "</div><span class='areas'>" + data.result.links.userinfo.cityname + "</span></div></li>");
                                var sex = data.result.links.userinfo.gender;
                                if (sex == 0) {
                                    $(".card-link .xb").addClass("man");
                                }
                                if (sex == -1) {
                                    $(".card-link .xb").addClass("yao");
                                }
                                break;
                            case 3:
                                $(".card-link").html("<li class='open-2' data-id='" + data.result.links.linkid + "'><a><div class='card-img'><img src='" + data.result.links.statusesinfo.defaultimg + "' /></div><h3>" + data.result.links.statusesinfo.content + "</h3><p>" + data.result.links.statusesinfo.user.nickname + "</p></a></li>");
                                break;
                            case 4:
                                $(".card-link").html("<li class='open-3' data-id='" + data.result.links.linkid + "'><a><div class='card-img'><img src='" + data.result.links.productinfo.imgurl + "' /></div><h3>" + data.result.links.productinfo.goodsname + "</h3><div class='pix'>" + data.result.links.productinfo.minprice + "</div><div class='sale'>销量：" + data.result.links.productinfo.salecount + "</div></a></li>");
                                break;
                            case 5:  //直播
                                $(".card-link").html("<li class='open-4' data-id='" + data.result.links.linkid + "'><a><div class='card-img'><img src='" + data.result.links.livedefaultimg + "' /></div><h3>" + data.result.user.nickname + "的语音直播室</h3><span>话题：" + data.result.links.linktitle + "</span></a></div>");
                                break;
                        }
                    }
                }
                else {
                    $("#long").hide();
                    var datas = data.result;

                    var html = "";
                    var zhaopian = data.result.statusespic; data.result.statusespic.pics.length
                    for (var i = 0; i < zhaopian.pics.length; i++) {
                        html += "<li data-img='" + zhaopian.originalpic + zhaopian.pics[i] + "'><div><img src='" + zhaopian.originalpic + zhaopian.pics[i] + "' /></div></li>";
                    }
                    var user = data.result.user;
                    var sex = user.gender;
                    if (sex == 0) {
                        sex = "♂";
                    } else if (sex == 1) {
                        sex = "♀";
                    }
                    else {
                        sex = "";
                    }
                    if (user.age == 0 || user.age == null) {
                        var age = "0"
                    }
                    else {
                        var age = user.age;
                    }
                    var sayhtml = '<div class="say"><span>' + datas.content + '</span></div>';
                    var htmls = "<div class='user-photo open' data-id='" + user.userid + "'><img src='" + user.imgurl + "' /></div><h3 class='user-name'>" + user.nickname + "</h3><div class='clear'></div><div class='xb'>" + sex + " " + age + "</div><div class='pub-time'>" + setTimes(data.result.createtime) + "</div><div class='report regs'>举报</div><div class='clear'></div><div class='product_nei'>" + sayhtml + "</div><ul class='ul-photo hidden'>" + html + "</ul><ul class='card-link'></ul><div class='photo-box'><div class='reward regs'><img src='images/reward.png' /></div><p>已有" + data.result.rewardcount + "人打赏</p></div><ul class='ul-ctrl'><li class='c-1 regs'>" + data.result.forwardcount + "</li><li class='c-2 regs'>" + data.result.commentcount + "</li><li class='c-3 regs'>" + data.result.praisecount + "</li><li class='c-4 regs'>" + data.result.favorcount + "</li></ul>";
                    if (datas.content) {
                        shareContent = datas.content;
                    } else {
                        shareContent = user.nickname + "发布的动态";
                    }
                    $(".users").html(htmls);
                    var sex = user.gender;
                    if (sex == 0) {
                        $(".users .xb").addClass("man");
                    }
                    if (sex == -1) {
                        $(".users .xb").addClass("yao");
                    }
                    var bbs = data.result.comments;
                    var bbsHtml = "";
                    if (data.result.commentcount == 0) {
                        //$(".bbs").html("暂无评论");
                        $(".bbs").closest("dl").hide();
                    }
                    else {
                        for (var i = 0; i < bbs.length; i++) {
                            if (bbs.praisecount == 0) {
                                bbsHtml += "<li><div class='bbs-photo open' data-id='" + bbs[i].user.userid + "'><img src='" + bbs[i].user.imgurl + "' /></div><h4><strong>" + bbs[i].user.nickname + "</strong><time>" + setTimes(bbs[i].createtime) + "</time></h4><p>" + bbs[i].content + "</p><div class='diggs regs'>" + bbs[i].praisecount + "</div></li>";
                            }
                            else {
                                bbsHtml += "<li><div class='bbs-photo open' data-id='" + bbs[i].user.userid + "'><img src='" + bbs[i].user.imgurl + "' /></div><h4><strong>" + bbs[i].user.nickname + "</strong><time>" + setTimes(bbs[i].createtime) + "</time></h4><p>" + bbs[i].content + "</p><div class='diggs regs have-digg'>" + bbs[i].praisecount + "</div></li>";
                            }
                        }
                        $(".ul-bbs").html(bbsHtml);
                    }
                    var digg = data.result.praises;
                    var diggHtml = "";
                    if (data.result.praisecount == 0) {
                        //$(".digg-list").html("暂无点赞");
                        $(".digg-list").hide();
                    }
                    else {
                        for (var i = 0; i < digg.length; i++) {
                            diggHtml += "<li class='open' data-id='" + digg[i].user.userid + "'><img src='" + digg[i].user.imgurl + "' /></li>";
                        }
                        diggHtml += "<li class='regs'><img src='images/data/more.png' /></li>";
                        $(".digg-list").html(diggHtml);
                    }
                    if (data.result.links) {
                        switch (data.result.links.linktype) {
                            case 1:
                                var sex = data.result.links.userinfo.gender;
                                if (sex == 0) {
                                    sex = "♂";
                                } else if (sex == 1) {
                                    sex = "♀";
                                }
                                else {
                                    sex = "";
                                }
                                if (data.result.links.userinfo.age == 0 || data.result.links.userinfo.age == null) {
                                    var age = "未知"
                                }
                                else {
                                    var age = data.result.links.userinfo.age;
                                }
                                $(".card-link").html("<li class='open-1' data-id='" + data.result.links.linkid + "'><div class='user'><div class='user-photo'><img src='" + data.result.links.userinfo.imgurl + "'></div><h3 class='user-name'>" + data.result.links.userinfo.nickname + "</h3><div class='xb'>" + sex + " " + age + "</div><span class='areas'>" + data.result.links.userinfo.cityname + "</span></div></li>");
                                var sex = data.result.links.userinfo.gender;
                                if (sex == 0) {
                                    $(".card-link .xb").addClass("man");
                                }
                                if (sex == -1) {
                                    $(".card-link .xb").addClass("yao");
                                }
                                break;
                            case 3:
                                $(".card-link").html("<li class='open-2' data-id='" + data.result.links.linkid + "'><a><div class='card-img'><img src='" + data.result.links.statusesinfo.defaultimg + "' /></div><h3>" + data.result.links.statusesinfo.content + "</h3><p>" + data.result.links.statusesinfo.user.nickname + "</p></a></li>");
                                break;
                            case 4:
                                $(".card-link").html("<li class='open-3' data-id='" + data.result.links.linkid + "'><a><div class='card-img'><img src='" + data.result.links.productinfo.imgurl + "' /></div><h3>" + data.result.links.productinfo.goodsname + "</h3><div class='pix'>" + data.result.links.productinfo.minprice + "</div><div class='sale'>销量：" + data.result.links.productinfo.salecount + "</div></a></li>");
                                break;
                            case 5:  //直播
                                $(".ul-photo").css("display", "none");
                                $(".card-link").html("<div class='open-4 submit_top' data-id='" + data.result.links.linkid + "'><a><div class='card-img'><img src='" + data.result.links.livedefaultimg + "' /></div><h3>" + data.result.user.nickname + "的语音直播室</h3><span>话题：" + data.result.links.linktitle + "</span></a></div>");
                                break;
                        }
                    }
                    if (zhaopian.pics.length == 1) {
                        var ww = $(".ul-photo").eq(0).width();
                        $(".ul-photo").width(ww);
                        $(".ul-photo").find("li").each((function () {
                            $(this).width(ww).height(ww);
                            var img = new Image();
                            var imgs = $(this).find("img");
                            img.src = imgs.attr("src");
                            if (img.complete) {
                                small(imgs);
                            }
                            else {
                                img.onload = function () {
                                    small(imgs);
                                }
                            }
                        }))
                    }
                    else if (zhaopian.pics.length > 1 && zhaopian.pics.length < 5) {
                        var ww = parseInt($(".ul-photo").eq(0).width());
                        $(".ul-photo").find("li").each((function () {
                            $(this).width(parseInt(ww / 2) - 2).height(parseInt(ww / 2) - 2);
                            var img = new Image();
                            var imgs = $(this).find("img");
                            img.src = imgs.attr("src");
                            if (img.complete) {
                                small(imgs);
                            }
                            else {
                                img.onload = function () {
                                    small(imgs);
                                }
                            }
                        }))
                        $(".ul-photo").width(ww + 10);
                    }
                    else {
                        var ww = parseInt($(".ul-photo").eq(0).width());
                        $(".ul-photo").find("li").each((function () {
                            $(this).width(parseInt(ww / 3) - 2).height(parseInt(ww / 3) - 2);
                            var img = new Image();
                            var imgs = $(this).find("img");
                            img.src = imgs.attr("src");
                            if (img.complete) {
                                small(imgs);
                            }
                            else {
                                img.onload = function () {
                                    small(imgs);
                                }
                            }
                        }))
                        $(".ul-photo").width(ww + 10);
                    }
                }
                $(".hidden").css({ "visibility": "visible" });

                var shareImg = null;
                var zp = data.result.statusespic;
                if (zp.pics.length > 0) {
                    shareImg = zp.originalpic + zp.pics[0];
                } else {
                    shareImg = data.result.user.imgurl;
                }

                var sdk = new JSSDK();
                sdk.init();
                sdk.change_share({
                    "title": "动态分享",
                    "desc": shareContent,
                    "image": shareImg,
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
        return nd.getFullYear() + "-" + (nd.getMonth() + 1) + "-" + nd.getDate() + " " + nd.getHours() + ":" + nd.getMinutes() + ":" + nd.getSeconds();
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
    $(document).on("tap", ".open,.open-1", function (event) {
        event.preventDefault();
        var link = this.getAttribute("data-id");
        window.location.href = "tribe.html?userid=" + link;
    })
    //进入动态
    $(document).on("tap", ".open-2", function (event) {
        event.preventDefault();
        var link = this.getAttribute("data-id");
        window.location.href = "dynamic.html?statusesid=" + link;
    })
    //进入商品
    $(document).on("tap", ".open-3", function (event) {
        event.preventDefault();
        var link = this.getAttribute("data-id");
        window.location.href = "product.html?goodsid=" + link;
    })
    //大图预览
    window.onload = function () {
        var flashContent = "<div class='fullFlash'><ul></ul></div>";
        $("body").append(flashContent);
        $(".fullFlash").addClass("swiper-container").append("<div class='num'></div>");
        $(".fullFlash>ul:first").addClass("swiper-wrapper");
        $(".fullFlash").on("tap", function () {
            $(this).transition({
                y: 50, opacity: 0, complete: function () {
                    $(this).hide();
                }
            });
        })
        var flashload = false;
        $(document).on("tap", ".ul-photo li", function (evnet) {
            event.preventDefault();
            var index = $(this).index();
            var html = "";
            var li = $(this).parent().children();
            for (var i = 0; i < li.length; i++) {
                html += "<li class='swiper-slide'><img src='" + li[i].getAttribute("data-img") + "' /></li>";
            }
            $(".fullFlash>ul:first").html(html);
            if (typeof (flash) != "undefined") {
                flash.destroy();
            }
            flash = $(".fullFlash").swiper({
                pagination: ".num",
                paginationActiveClass: 'on',
            })
            $(".numm").width($(".numm span").outerWidth(true) * $(".fullFlash li").length);
            $(".fullFlash").show().css({ opacity: 0, y: -50 }).transition({ y: 0, opacity: 1 });
            setTimeout(function () {
                flash.swipeTo(index, 100);
            }, 100)
        })
    }
})