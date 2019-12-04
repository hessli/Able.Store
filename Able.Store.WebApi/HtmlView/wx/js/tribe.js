/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
 */
$(document).ready(function () {
    var userid = $.query.get("userid");
    if (userid == "") {
        $("html,body").css({
            "visibility": "hidden",
            "height": "100%",
            "overflow": "hidden"
        });
        windowAlert("用户不存在", 0);
        return
    }
    //调出APP		
    var loadDateTime = new Date();
    window.setTimeout(function () {
        var timeOutDateTime = new Date();
        if (timeOutDateTime - loadDateTime < 500) {
            getData(userid);
        }
    }, 400);

    var url = "stbl://stbl/?m=5&v=" + userid;
    $("#open_app").attr("href", url);
    document.getElementById("open_app").click();

    //获取动态
    function getData(userid) {
        var postData = {};
        postData.userid = userid;
        $.ajax({
            type: "post",
            url: baseapi_url + "/h5/web/user/tribe/get",
            // contentType: "application/json; charset=utf-8",
            data: postData,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                var user = data.result.userview.user;
                //console.log(user);
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
                } else {
                    var age = user.age;
                }

                if (user.signature) {
                    var signature = user.signature;
                } else {
                    var signature = "";
                }
                window.photo = user.imgurl;
                //edit 20160502 by:kain
                if (user.bigchiefuserid == 0) {
                    //follow_start_img = "<img src='images/chief_sub_b.png' style='width: .35rem;position: absolute;top: -0.13rem;left: 0.05rem;'/>";
                    if (user.roleflag == 4 || user.roleflag == 5 || user.roleflag == 7) {
                        chief_style = "style='background: url(images/chief_icon.png) top center no-repeat;background-size: 88%;padding: 0.2rem;top:0.14rem;'";
                    } else {
                        chief_style = "";
                    }

                    follow_start_img = "";
                    if (typeof (user.cbigchiefinfo) != "undefined") {
                        follow_start_img = "<img src='" + user.cbigchiefinfo.imgurl + "' style='width: .35rem;position: absolute;top: -0.13rem;left: 0.05rem;'/>";;
                    }

                    big_chief_img = "";
                    if (typeof (user.cbigchiefinfo.imgurl) != "undefined") {
                        big_chief_img = "<img src='" + user.cbigchiefinfo.imgurl + "' />";
                        show = ' open';
                        bigCid = " data-id='" + user.cbigchiefuserid + "'";
                    } else {
                        show = ' none';
                    }
                    var html = "<div class='star none'" + bigCid + ">" + big_chief_img + "</div><div class='ziliao-photo' " + chief_style + "><img src='" + user.imgurl + "' /></div><div class='ziliao-info' style='padding-top:0.1rem;'><h3>" + user.nickname + "</h3><div class='xb'>" + sex + " " + age + "</div><div class='areas'>" + user.cityname + "</div></div><div class='distance'>距离  0.01km</div><div class='ziliao-bar'><div class='ziliao-short'>" + follow_start_img + signature + "</div><div class='play regs'></div></div>";
                }
                else {
                    if (user.roleflag == 4 || user.roleflag == 5 || user.roleflag == 7) {
                        chief_style = "style='background: url(images/chief_icon.png) top center no-repeat;background-size: 88%;padding: 0.2rem;top:0.14rem;'";
                    } else {
                        chief_style = "";
                    }
                    follow_start_img = "";
                    if (user.roleflag != 1) {
                        if (typeof (user.cbigchiefinfo) != "undefined") {
                            follow_start_img = "<img src='" + user.cbigchiefinfo.imgurl + "' style='width: .35rem;position: absolute;top: -0.13rem;left: 0.05rem;'/>";
                        }
                    }

                    big_chief_img = "";
                    if (typeof (user.bigchiefinfo.imgurl) != "undefined") {
                        big_chief_img = "<img src='" + user.bigchiefinfo.imgurl + "' />";
                        show = ' open';
                        bigCid = " data-id='" + user.bigchiefuserid + "'";
                    } else {
                        show = ' none';
                    }
                    var html = "<div class='star" + show + "'" + bigCid + "'>" + big_chief_img + "</div><div class='ziliao-photo' " + chief_style + "><img src='" + user.imgurl + "' /></div><div class='ziliao-info'><h3>" + user.nickname + "</h3><div class='xb'>" + sex + " " + age + "</div><div class='areas'>" + user.cityname + "</div></div><div class='distance'>距离  0.01km</div><div class='ziliao-bar'><div class='ziliao-short'>" + follow_start_img + signature + "</div><div class='play regs'></div></div>";
                }
                $(".ziliao").html(html);
                var sex = user.gender;
                if (sex == 0) {
                    $(".ziliao .xb").addClass("man");
                }
                if (sex == -1) {
                    $(".ziliao .xb").addClass("yao");
                }
                html = "<li><h3>" + addCommas(data.result.countview.tudi_count) + "</h3><p>徒弟</p></li><li><h3>" + addCommas(data.result.countview.attention_count) + "</h3><p>关注</p></li><li><h3>" + addCommas(data.result.countview.fans_count) + "</h3><p>粉丝</p></li>";
                $(".follow").html(html);
                var level = data.result.userview.level;
                html = "<li><h3>" + level.levelrichtitle + "</h3><p>财富等级 LV ." + level.levelrichname + "</p><div class='percent'><span style='width:" + level.levelrichcur / (level.levelrichcur + level.levelrichnext) + "%'></span></div></li><li><h3>" + level.levelcontacttitle + "</h3><p>人脉等级 LV." + level.levelcontactname + "</p><div class='percent'><span style='width:" + level.levelcontactcur / (level.levelcontactcur + level.levelcontactnext) + "%'></span></div></li>";
                $(".wealth").html(html);
                html = "";
                var liwu = data.result.giftsview;
                if (liwu.length == 0) {
                    html += "暂无";
                } else {
                    for (var i = 0; i < liwu.length; i++) {
                        html += "<li><div class='liwu-img'><img src='" + liwu[i].giftinfo.giftimg + "' /></div><p>" + liwu[i].giftinfo.giftname + "</p></li>";
                    }
                }
                $(".liwu").html(html);
                html = "";
                var qiandao = data.result.proxysigninview;
                if (qiandao.length == 0) {
                    html += "暂无";
                } else {
                    for (var i = 0; i < qiandao.length; i++) {
                        html += "<li class='open' data-id='" + qiandao[i].userid + "'><img src='" + qiandao[i].imgurl + "' /></li>";
                    }
                }
                $(".ul-qiandao").html(html);
                $(".other").show();
                html = "";
                var zhaopian = data.result.userphotoview;
                if (zhaopian.length == 0) {
                    html += "暂无";
                    $(".ul-img").html(html);
                } else {
                    for (var i = 0; i < zhaopian.length; i++) {
                        html += "<li data-img='" + zhaopian[i].originalurl + "'><div><img src='" + zhaopian[i].thumburl + "' /></div></li>";
                    }
                    $(".ul-img").html(html);
                    $(".ul-img").find("li").each((function () {
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
                    $(".ul-img").width(($(".ul-img").find("li").length + 1) * $(".ul-img").find("li").outerWidth(true));
                    var myScroll = new IScroll("#srcolls", {
                        probeType: 3,
                        scrollX: true,
                        freeScroll: true,
                        fadeScrollbars: false,
                        resizeScrollbars: false,
                        shrinkScrollbars: 'clip',
                        scrollbars: true,
                        scrollbars: 'custom'
                    });
                }
                $(".zhaopianshu").html("Ta的照片(" + data.result.userphotocount + ")");
                html = "";
                var link = data.result.linksview;
                if (link.length >= 5) {
                    var shuliang = 5;
                } else {
                    var shuliang = link.length;
                }
                for (var i = 0; i < shuliang; i++) {
                    html += "<li><img src='" + link[i].piclarurl + "' /></li>";
                }
                $(".ul-links").html(html);
                $(".ul-links").find("li").each((function () {
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
                $(".links-tit").eq(0).find("strong").html(data.result.userlinksnum);
                html = "";

                var dongtai = data.result.lateststatusesview;
                if (dongtai) {

                    var defaultface = dongtai.statusespic.thumbpic + dongtai.statusespic.defaultpic;

                    if (dongtai.statusespic.defaultpic) {
                        if (dongtai.statusestype == 1) {
                            html += "<div class='dt-img'><img src='" + defaultface + "' /></div><h3>" + dongtai.title + "</h3><div class='pub-time'>" + setTimes(dongtai.createtime) + "</div>";
                        } else {
                            html += "<div class='dt-img'><img src='" + defaultface + "' /></div><h3>" + dongtai.content + "</h3><div class='pub-time'>" + setTimes(dongtai.createtime) + "</div>";
                        }
                    } else {
                        html += "<div class='dt-img'><img src='" + window.photo + "' /></div><h3>" + dongtai.content + "</h3><div class='pub-time'>" + setTimes(dongtai.createtime) + "</div>";
                    }

                    $(".dt").html(html);

                    $(".links-tit").eq(1).find("strong").html(data.result.userstatusesnum);
                    small($(".dt-img img"));
                    //window.statusesid = dongtai.statusesid;
                }
                else {
                    $(".dt").parent().hide();
                }
                $(".hidden").css({
                    "visibility": "visible"
                });

                var sdk = new JSSDK();
                sdk.init();
                sdk.change_share({
                    title: "部落分享",
                    desc: user.nickname,
                    image: user.imgurl,
                    url: window.location.href
                });
            },
            error: function () {
                windowAlert("网络错误，请重试");
            },
            headers: {
                "x-stbl-token": window.token,
            }
        });
        var postData = {};
        postData.objuserid = userid;
        $.ajax({
            type: "post",
            url: baseapi_url + "/h5/web/user/info/relation",
            // contentType: "application/json; charset=utf-8",
            data: postData,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                var html = "";
                if (data.result.headmenview) {
                    html += "<li><a class='open' data-id='" + data.result.headmenview.userid + "'>酋长</a></li>";
                } else {
                    html += "<li><a class='gray'>酋长</a></li>";
                }
                if (data.result.elderview) {
                    html += "<li><a class='open' data-id='" + data.result.elderview.userid + "'>长老</a></li>";
                } else {
                    html += "<li><a class='gray'>长老</a></li>";
                }
                if (data.result.masterview) {
                    html += "<li><a class='open' data-id='" + data.result.masterview.userid + "'>师傅</a></li>";
                } else {
                    html += "<li><a class='gray'>师傅</a></li>";
                }
                $(".line").html(html + "<li><a class='regs'>排行</a></li><li><a class='regs'>社区</a></li>").show();
            },
            error: function () {
                windowAlert("网络错误，请重试");
            },
            headers: {
                "x-stbl-token": window.token,
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
    //进入个人动态
    $(document).on("tap", ".dynamic-link", function (event) {
        event.preventDefault();
        window.location.href = "dynamic-list.html?objuserid=" + userid;
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
    window.onload = function () {
        var flashContent = "<div class='fullFlash'><ul></ul></div>";
        $("body").append(flashContent);
        $(".fullFlash").addClass("swiper-container").append("<div class='num'></div>");
        $(".fullFlash>ul:first").addClass("swiper-wrapper");
        $(".fullFlash").on("tap", function () {
            $(this).transition({
                y: 50,
                opacity: 0,
                complete: function () {
                    $(this).hide();
                }
            });
        })
        var flashload = false;
        $(document).on("tap", ".ul-img li", function (evnet) {
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
            $(".fullFlash").show().css({
                opacity: 0,
                y: -50
            }).transition({
                y: 0,
                opacity: 1
            });
            setTimeout(function () {
                flash.swipeTo(index, 100);
            }, 100)
        })
    }
})