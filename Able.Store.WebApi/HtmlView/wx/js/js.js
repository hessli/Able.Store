﻿/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
//客户端判断
var browser = {
    versions: function () {
        var u = navigator.userAgent, app = navigator.appVersion;
        return {         //移动终端浏览器版本信息
            trident: u.indexOf('Trident') > -1, //IE内核
            presto: u.indexOf('Presto') > -1, //opera内核
            webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
            mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
            iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器
            iPad: u.indexOf('iPad') > -1, //是否iPad
            webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
        };
    }(),
    language: (navigator.browserLanguage || navigator.language).toLowerCase()
}
//发送请求前
$(document).ajaxStart(function () {
    showLoading();
})
$(document).ajaxComplete(function () {
    hideLoading();
})
function showLoading() {
    var str = '<div class="show_loading"> \
                  <div id="over_load" class="over_load"></div> \
                  <div id="layout_load" class="layout_load"><div class="load_content"><img src="images/loading_list.gif"/></div> \
                  </div>';
    $("body").append(str);
    $("#over_load,#layout_load").css("display", "block");
}
function hideLoading() {
    $("#showLoading").remove();
    $("#over_load,#layout_load").css("display", "none");

}

//显示alert
function windowAlert(text, alertTime) {
    if (typeof (autoTip) != "undefined") {
        clearTimeout(autoTip);
    }
    $("#tip").remove();
    $("body").append("<div id='tip'><span></span></div>");
    var $tip = $("#tip");
    $tip.find("span").html(text);
    $tip.show();
    var height = $("#tip").height();
    $tip.css({ bottom: -height }).transition({
        y: -height - 36, complete: function () {
            if (alertTime != 0) {
                autoTip = setTimeout(function () {
                    $tip.transition({
                        y: 0, complete: function () {
                            $tip.hide();
                        }
                    }, 300);
                }, 1000)
            }
        }
    }, 300);
}
function windowAlertClose(text) {
    if (typeof (autoTip) != "undefined") {
        clearTimeout(autoTip);
    }
    var $tip = $("#tip");
    $tip.find("span").html(text);
    autoTip = setTimeout(function () {
        $tip.transition({
            y: 0, complete: function () {
                $tip.hide();
            }
        }, 300);
    }, 1000)
}
//弹窗业务
function windowOpen(type, scroll, callBack) {
    if (openAni) {
        return;
    }
    openAni = true;
    if (!document.getElementById("window")) {
        $("body").append("<div id='window'></div>");
    }
    var $window = $("#window");
    $window.show();
    if (!document.getElementById(type)) {
        $window.children().hide();
        windowOpenAni(type, scroll, callBack);
    }
    else {
        windowOpenAni(type, scroll, callBack);
    }
}
var scrollTop;
var openAni = false;
function windowOpenAni(type, scroll, callBack) {
    $("#" + type).show().transition({
        x: 0, complete: function () {
            if ($("html").attr("id") != "full-page") {
                scrollTop = document.documentElement.scrollTop + document.body.scrollTop;
                $('html,body').scrollTop(0);
                $("html").attr("id", "full-page");
                $("#window").css({ "position": "absolute" });
            }
            if (typeof (scroll) != "undefined") {
                if (scroll == "scroll") {
                    $("#" + type).css({ "overflow": "hidden" });
                    window.scroll = new IScroll("#" + type, { probeType: 3, scrollX: false, freeScroll: true, fadeScrollbars: false, resizeScrollbars: false, shrinkScrollbars: 'clip', scrollbars: true, scrollbars: 'custom' });
                }
            }
            if (typeof (callBack) != "undefined") {
                callBack();
            }
            openAni = false;
        }
    }, 500);
}
function windowClose() {
    if (openAni) {
        return;
    }
    $("html").attr("id", "");
    $('html,body').scrollTop(scrollTop);
    $("#window").css({ "position": "fixed" }).find(">div:visible").transition({
        x: "100%", complete: function () {
            $(this).hide();
            $("#window").hide();
        }
    }, 500);
}
function windowCloseSingle(type) {
    if (openAni) {
        return;
    }
    $("#" + type).transition({
        x: "100%", complete: function () {
            $(this).hide();
            openAni = false;
        }
    }, 500);
}
//检测返回的结果状态
function detectionResult(data, alertType) {
    if (data.issuccess == 0) {
        //			if ( data.err.errcode == "-200000" || data.err.errcode == "-200001" || data.err.errcode == "-200002" || data.err.errcode == "-200003" || data.err.errcode == "-2000015" || data.err.errcode == "-200007" ){
        //				window.location.href = window.localStorage.jumpUrl;
        //				return false
        //			}
        //			else if ( data.err.errcode == "1000" ){
        //				window.location.href = "500.html";
        //				return false
        //			}
        windowAlert(data.err.msg, alertType);
        return false;
    }
    return true;
}
//手机号码正则判断
function checkTel(value) {
    var myreg = /(((13[0-9]{1})|(18[0-9]{1})|(17[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    if (myreg.test(value)) {
        return true;
    }
    else {
        return false;
    }
}

// 做判断。微信则授权，其他直接打开！！！！

//跳转授权
var jump = window.location.search;
jump = jump.substring(1, jump.length).split("&");
for (var i = 0; i < jump.length; i++) {
    var jumps = jump[i].split("=");
    if (jumps[0] == "openid") {
        window.openid = decodeURIComponent(jumps[1]);
    }
    if (jumps[0] == "nickname") {
        window.nickname = decodeURIComponent(jumps[1]);
    }
    if (jumps[0] == "headimgurl") {
        window.headimgurl = decodeURIComponent(jumps[1]);
    }
    if (jumps[0] == "ui") {
        window.ui = decodeURIComponent(jumps[1]);
    }
}
if (typeof (ui) != "undefined") {
    window.localStorage.ui = ui;
}
if (typeof (openid) != "undefined") {
    window.localStorage.token = window.openid;
    window.localStorage.nickname = window.nickname;
    window.localStorage.headimgurl = window.headimgurl;
    //测试
    //alert("js:" + window.openid);
    //alert("js:" + window.unionid);
}
else {
    window.nickname = window.localStorage.nickname;
    window.headimgurl = window.localStorage.headimgurl;
}

//localStorage.removeItem("token");

window.token = window.localStorage.token;
window.localStorage.url = window.location.href;

//判断是否微信
if (browser.versions.mobile) {
    var ua = navigator.userAgent.toLowerCase();//获取判断用的对象
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        //在微信中打开
        if (typeof (token) == "undefined") {
            window.location.href = "http://wx-auth.stbl.cc/oauthwx/usershare?stblbackurl=" + encodeURIComponent(window.location.href);
        }
        else {
            document.body.style.visibility = "visible";
        }
    } else {
        document.body.style.visibility = "visible";
        $(".welcome").addClass("mar_tp");
        $("#headerImg").remove();
        $("#headerImg").css("border", "0");
    }
}

//if ( typeof(token) == "undefined" ){
//	window.location.href = "http://wx-auth.stbl.cc/oauthwx/usershare?stblbackurl=" + encodeURIComponent("http://dev-wap.stbl.cc/web/wx/");
//}
//else{
//	document.body.style.visibility = "visible";
//}


//图片定位
function small(smallsrc) {
    var imgwidths = smallsrc.width();
    var imgheights = smallsrc.height();
    var parents = smallsrc.parent();
    var parentwidths = parents.width();
    var parentheights = parents.height();
    if (imgwidths <= parentwidths) {
        if (parentwidths / imgwidths * imgheights >= parentheights) {
            imgheights = imgheights / (imgwidths / parentwidths);
            imgwidths = parentwidths;
        }
        else {
            imgwidths = imgwidths / (imgheights / parentheights);
            imgheights = parentheights;
        }
    }
    else {
        if (parentwidths / imgwidths * imgheights <= parentheights) {
            imgwidths = imgwidths / (imgheights / parentheights);
            imgheights = parentheights;
        }
        else {
            imgheights = imgheights / (imgwidths / parentwidths);
            imgwidths = parentwidths;
        }
    }
    smallsrc.css({ "position": "absolute", "width": imgwidths, "height": imgheights, "left": (parentwidths - imgwidths) / 2, "top": (parentheights - imgheights) / 2 });
}
//科学计数
function addCommas(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
//隔开手机号
function spaceTel(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{4})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ' ' + '$2');
    }
    return x1 + x2;
}
//系统识别
function isIos() {
    var userAgentInfo = navigator.userAgent;
    var Agents = new Array("iPhone");
    var flag = false;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) { flag = true; break; }
    }
    return flag;
}
$(function () {

    //下载按钮
    var downImg = $(".down");
    if (!isIos()) {
        for (var i = 0; i < downImg.length; i++) {
            downImg[i].innerHTML = "<img src='images/down-android.png' />";
        }
    }
    else {
        for (var i = 0; i < downImg.length; i++) {
            downImg[i].innerHTML = "<img src='images/down-iphone.png' />";
        }
    }
    //底部提示防错位
    if ($(".bar").length) {
        function setElements() {
            var clientHeight = document.documentElement.clientHeight;
            var clientWidth = document.documentElement.clientWidth;
            if (clientHeight / clientWidth < 1.3) { $(".bar").css({ "position": "absolute" }) }
            else { $(".bar").css({ "position": "fixed" }) }
        }
        $(window).on("resize", function () { setElements() })
        setElements();
    }
    //注册
    $(document).on("tap", ".ul-radio li", function (event) {
        event.preventDefault();
        $(this).siblings().removeClass("on").end().addClass("on");
        var value = this.getAttribute("data-value");
        document.getElementById("sex").value = value;
    })
    //语言切换
    var langOpen = false;
    $(document).on("tap", ".lang>h3", function (event) {
        event.preventDefault();
        if ($(this).next().is(":visible")) {
            langOpen = true;
        }
        else {
            $(this).next().show();
            langOpen = false;
        }
    })
    $(document).on("tap", ".lang-list", function () {
        langOpen = false;
    })
    $(document).on("tap", function (event) {
        if (langOpen) {
            $(".lang-list").hide();
        }
        langOpen = true;
    })
    //照片翻动
    if ($(".img").length) {
        $(".ul-img").width(($(".ul-img").find("li").length + 1) * $(".ul-img").find("li").outerWidth(true));
        var myScroll = new IScroll("#srcoll", { probeType: 3, scrollX: true, freeScroll: true, fadeScrollbars: false, resizeScrollbars: false, shrinkScrollbars: 'clip', scrollbars: true, scrollbars: 'custom' });
    }
    //关闭
    $(document).on("tap", ".go-back", function (event) {
        event.preventDefault();
        windowClose();
        $("input").blur();
    })
    //打开协议
    $(document).on("tap", ".xieyi", function (event) {
        event.preventDefault();
        window.callBack = function () {
            new IScroll($("#explain .main")[0], { probeType: 3, scrollX: false, freeScroll: true, fadeScrollbars: false, resizeScrollbars: false, shrinkScrollbars: 'clip', scrollbars: true, scrollbars: 'custom' });
        }
        windowOpen("explain", null, callBack);
    })
    //邀请码说明
    $(document).on("tap", ".open-code", function (event) {
        event.preventDefault();
        window.callBack = function () {
            window.iscroll = new IScroll($("#explain .main")[0], { probeType: 3, scrollX: false, freeScroll: true, fadeScrollbars: false, resizeScrollbars: false, shrinkScrollbars: 'clip', scrollbars: true, scrollbars: 'custom' });
        }
        windowOpen("explain", null, callBack);
    })
    //产品页
    if ($(".flash").length) {
        $(".flash").addClass("swiper-container").append("<div class='num'></div>");
        $(".flash>ul:first").addClass("swiper-wrapper").find("li").addClass("swiper-slide");
        var auto_width = $(".flash").width();
        var auto_height = 620;
        var auto_num = $(".flash>ul:first img").length;
        function flash() {
            //获取图片高度
            if (document.documentElement.clientWidth >= 640) {
                auto_width = 640;
                auto_height = 620;
            }
            else {
                auto_width = document.documentElement.clientWidth;
                auto_height = 620 * auto_width / 640;
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
            loop: true,
            autoplay: 5000,
            autoplayDisableOnInteraction: false
        })
    }
    //推荐产品翻动
    if ($(".tj").length) {
        $(".tjs").width(($(".tjs").find("li").length + 0.3) * $(".tjs").find("li").outerWidth(true));
        var myScroll = new IScroll("#srcoll", { probeType: 3, scrollX: true, freeScroll: true, fadeScrollbars: false, resizeScrollbars: false, shrinkScrollbars: 'clip', scrollbars: true, scrollbars: 'custom' });
    }
})
//密码        
function md5(string) {
    function md5_RotateLeft(lValue, iShiftBits) {
        return (lValue << iShiftBits) | (lValue >>> (32 - iShiftBits));
    }
    function md5_AddUnsigned(lX, lY) {
        var lX4, lY4, lX8, lY8, lResult;
        lX8 = (lX & 0x80000000);
        lY8 = (lY & 0x80000000);
        lX4 = (lX & 0x40000000);
        lY4 = (lY & 0x40000000);
        lResult = (lX & 0x3FFFFFFF) + (lY & 0x3FFFFFFF);
        if (lX4 & lY4) {
            return (lResult ^ 0x80000000 ^ lX8 ^ lY8);
        }
        if (lX4 | lY4) {
            if (lResult & 0x40000000) {
                return (lResult ^ 0xC0000000 ^ lX8 ^ lY8);
            } else {
                return (lResult ^ 0x40000000 ^ lX8 ^ lY8);
            }
        } else {
            return (lResult ^ lX8 ^ lY8);
        }
    }
    function md5_F(x, y, z) {
        return (x & y) | ((~x) & z);
    }
    function md5_G(x, y, z) {
        return (x & z) | (y & (~z));
    }
    function md5_H(x, y, z) {
        return (x ^ y ^ z);
    }
    function md5_I(x, y, z) {
        return (y ^ (x | (~z)));
    }
    function md5_FF(a, b, c, d, x, s, ac) {
        a = md5_AddUnsigned(a, md5_AddUnsigned(md5_AddUnsigned(md5_F(b, c, d), x), ac));
        return md5_AddUnsigned(md5_RotateLeft(a, s), b);
    };
    function md5_GG(a, b, c, d, x, s, ac) {
        a = md5_AddUnsigned(a, md5_AddUnsigned(md5_AddUnsigned(md5_G(b, c, d), x), ac));
        return md5_AddUnsigned(md5_RotateLeft(a, s), b);
    };
    function md5_HH(a, b, c, d, x, s, ac) {
        a = md5_AddUnsigned(a, md5_AddUnsigned(md5_AddUnsigned(md5_H(b, c, d), x), ac));
        return md5_AddUnsigned(md5_RotateLeft(a, s), b);
    };
    function md5_II(a, b, c, d, x, s, ac) {
        a = md5_AddUnsigned(a, md5_AddUnsigned(md5_AddUnsigned(md5_I(b, c, d), x), ac));
        return md5_AddUnsigned(md5_RotateLeft(a, s), b);
    };
    function md5_ConvertToWordArray(string) {
        var lWordCount;
        var lMessageLength = string.length;
        var lNumberOfWords_temp1 = lMessageLength + 8;
        var lNumberOfWords_temp2 = (lNumberOfWords_temp1 - (lNumberOfWords_temp1 % 64)) / 64;
        var lNumberOfWords = (lNumberOfWords_temp2 + 1) * 16;
        var lWordArray = Array(lNumberOfWords - 1);
        var lBytePosition = 0;
        var lByteCount = 0;
        while (lByteCount < lMessageLength) {
            lWordCount = (lByteCount - (lByteCount % 4)) / 4;
            lBytePosition = (lByteCount % 4) * 8;
            lWordArray[lWordCount] = (lWordArray[lWordCount] | (string.charCodeAt(lByteCount) << lBytePosition));
            lByteCount++;
        }
        lWordCount = (lByteCount - (lByteCount % 4)) / 4;
        lBytePosition = (lByteCount % 4) * 8;
        lWordArray[lWordCount] = lWordArray[lWordCount] | (0x80 << lBytePosition);
        lWordArray[lNumberOfWords - 2] = lMessageLength << 3;
        lWordArray[lNumberOfWords - 1] = lMessageLength >>> 29;
        return lWordArray;
    };
    function md5_WordToHex(lValue) {
        var WordToHexValue = "", WordToHexValue_temp = "", lByte, lCount;
        for (lCount = 0; lCount <= 3; lCount++) {
            lByte = (lValue >>> (lCount * 8)) & 255;
            WordToHexValue_temp = "0" + lByte.toString(16);
            WordToHexValue = WordToHexValue + WordToHexValue_temp.substr(WordToHexValue_temp.length - 2, 2);
        }
        return WordToHexValue;
    };
    function md5_Utf8Encode(string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            } else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            } else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }
        return utftext;
    };
    var x = Array();
    var k, AA, BB, CC, DD, a, b, c, d;
    var S11 = 7, S12 = 12, S13 = 17, S14 = 22;
    var S21 = 5, S22 = 9, S23 = 14, S24 = 20;
    var S31 = 4, S32 = 11, S33 = 16, S34 = 23;
    var S41 = 6, S42 = 10, S43 = 15, S44 = 21;
    string = md5_Utf8Encode(string);
    x = md5_ConvertToWordArray(string);
    a = 0x67452301; b = 0xEFCDAB89; c = 0x98BADCFE; d = 0x10325476;
    for (k = 0; k < x.length; k += 16) {
        AA = a; BB = b; CC = c; DD = d;
        a = md5_FF(a, b, c, d, x[k + 0], S11, 0xD76AA478);
        d = md5_FF(d, a, b, c, x[k + 1], S12, 0xE8C7B756);
        c = md5_FF(c, d, a, b, x[k + 2], S13, 0x242070DB);
        b = md5_FF(b, c, d, a, x[k + 3], S14, 0xC1BDCEEE);
        a = md5_FF(a, b, c, d, x[k + 4], S11, 0xF57C0FAF);
        d = md5_FF(d, a, b, c, x[k + 5], S12, 0x4787C62A);
        c = md5_FF(c, d, a, b, x[k + 6], S13, 0xA8304613);
        b = md5_FF(b, c, d, a, x[k + 7], S14, 0xFD469501);
        a = md5_FF(a, b, c, d, x[k + 8], S11, 0x698098D8);
        d = md5_FF(d, a, b, c, x[k + 9], S12, 0x8B44F7AF);
        c = md5_FF(c, d, a, b, x[k + 10], S13, 0xFFFF5BB1);
        b = md5_FF(b, c, d, a, x[k + 11], S14, 0x895CD7BE);
        a = md5_FF(a, b, c, d, x[k + 12], S11, 0x6B901122);
        d = md5_FF(d, a, b, c, x[k + 13], S12, 0xFD987193);
        c = md5_FF(c, d, a, b, x[k + 14], S13, 0xA679438E);
        b = md5_FF(b, c, d, a, x[k + 15], S14, 0x49B40821);
        a = md5_GG(a, b, c, d, x[k + 1], S21, 0xF61E2562);
        d = md5_GG(d, a, b, c, x[k + 6], S22, 0xC040B340);
        c = md5_GG(c, d, a, b, x[k + 11], S23, 0x265E5A51);
        b = md5_GG(b, c, d, a, x[k + 0], S24, 0xE9B6C7AA);
        a = md5_GG(a, b, c, d, x[k + 5], S21, 0xD62F105D);
        d = md5_GG(d, a, b, c, x[k + 10], S22, 0x2441453);
        c = md5_GG(c, d, a, b, x[k + 15], S23, 0xD8A1E681);
        b = md5_GG(b, c, d, a, x[k + 4], S24, 0xE7D3FBC8);
        a = md5_GG(a, b, c, d, x[k + 9], S21, 0x21E1CDE6);
        d = md5_GG(d, a, b, c, x[k + 14], S22, 0xC33707D6);
        c = md5_GG(c, d, a, b, x[k + 3], S23, 0xF4D50D87);
        b = md5_GG(b, c, d, a, x[k + 8], S24, 0x455A14ED);
        a = md5_GG(a, b, c, d, x[k + 13], S21, 0xA9E3E905);
        d = md5_GG(d, a, b, c, x[k + 2], S22, 0xFCEFA3F8);
        c = md5_GG(c, d, a, b, x[k + 7], S23, 0x676F02D9);
        b = md5_GG(b, c, d, a, x[k + 12], S24, 0x8D2A4C8A);
        a = md5_HH(a, b, c, d, x[k + 5], S31, 0xFFFA3942);
        d = md5_HH(d, a, b, c, x[k + 8], S32, 0x8771F681);
        c = md5_HH(c, d, a, b, x[k + 11], S33, 0x6D9D6122);
        b = md5_HH(b, c, d, a, x[k + 14], S34, 0xFDE5380C);
        a = md5_HH(a, b, c, d, x[k + 1], S31, 0xA4BEEA44);
        d = md5_HH(d, a, b, c, x[k + 4], S32, 0x4BDECFA9);
        c = md5_HH(c, d, a, b, x[k + 7], S33, 0xF6BB4B60);
        b = md5_HH(b, c, d, a, x[k + 10], S34, 0xBEBFBC70);
        a = md5_HH(a, b, c, d, x[k + 13], S31, 0x289B7EC6);
        d = md5_HH(d, a, b, c, x[k + 0], S32, 0xEAA127FA);
        c = md5_HH(c, d, a, b, x[k + 3], S33, 0xD4EF3085);
        b = md5_HH(b, c, d, a, x[k + 6], S34, 0x4881D05);
        a = md5_HH(a, b, c, d, x[k + 9], S31, 0xD9D4D039);
        d = md5_HH(d, a, b, c, x[k + 12], S32, 0xE6DB99E5);
        c = md5_HH(c, d, a, b, x[k + 15], S33, 0x1FA27CF8);
        b = md5_HH(b, c, d, a, x[k + 2], S34, 0xC4AC5665);
        a = md5_II(a, b, c, d, x[k + 0], S41, 0xF4292244);
        d = md5_II(d, a, b, c, x[k + 7], S42, 0x432AFF97);
        c = md5_II(c, d, a, b, x[k + 14], S43, 0xAB9423A7);
        b = md5_II(b, c, d, a, x[k + 5], S44, 0xFC93A039);
        a = md5_II(a, b, c, d, x[k + 12], S41, 0x655B59C3);
        d = md5_II(d, a, b, c, x[k + 3], S42, 0x8F0CCC92);
        c = md5_II(c, d, a, b, x[k + 10], S43, 0xFFEFF47D);
        b = md5_II(b, c, d, a, x[k + 1], S44, 0x85845DD1);
        a = md5_II(a, b, c, d, x[k + 8], S41, 0x6FA87E4F);
        d = md5_II(d, a, b, c, x[k + 15], S42, 0xFE2CE6E0);
        c = md5_II(c, d, a, b, x[k + 6], S43, 0xA3014314);
        b = md5_II(b, c, d, a, x[k + 13], S44, 0x4E0811A1);
        a = md5_II(a, b, c, d, x[k + 4], S41, 0xF7537E82);
        d = md5_II(d, a, b, c, x[k + 11], S42, 0xBD3AF235);
        c = md5_II(c, d, a, b, x[k + 2], S43, 0x2AD7D2BB);
        b = md5_II(b, c, d, a, x[k + 9], S44, 0xEB86D391);
        a = md5_AddUnsigned(a, AA);
        b = md5_AddUnsigned(b, BB);
        c = md5_AddUnsigned(c, CC);
        d = md5_AddUnsigned(d, DD);
    }
    return (md5_WordToHex(a) + md5_WordToHex(b) + md5_WordToHex(c) + md5_WordToHex(d)).toLowerCase();
}



/*判断是否滑动到底部，是的话弹出footer*/
$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        $(".footer").css("display", "block");
    } else {
        $(".footer").css("display", "none");
    }
});