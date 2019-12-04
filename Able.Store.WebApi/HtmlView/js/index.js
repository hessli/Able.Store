$(function () {
    //授权成功&&店铺状态正常 执行
    $.when(window.shopState).done(function () {
        //搜索
        $(document).on("submit", ".form-search", function (event) {
            event.preventDefault();
            keyword = document.getElementById("keyword").value;
            var url = $.query.set("keyword", encodeURIComponent(keyword));
            window.location.href = "product-list.html" + url;
        });

        //	特约姓名
        //var postdata = {};
        //postdata.shopid = window.shopid;
        //$.ajax({
        //	type: "get",
        //       url: "api/DealerInfo/GetShopUserName",
        //       data: postdata,
        //	dataType: "json",
        //	success: function(data){
        //		if ( !detectionResult(data,1) ){
        //			return;
        //		}
        //		$("title").html("欢迎光临茅台特约经销商"+data.result+"的商城");
        //		function is_weixn(){
        //		    var ua = navigator.userAgent.toLowerCase();  
        //		    if(ua.match(/MicroMessenger/i)=="micromessenger") {  
        //		        return true;  
        //		    }
        //		    else {
        //		        return false;  
        //		    }  
        //		}  
        //		if ( is_weixn() ){
        //			var $iframe = $("<iframe src='/favicon.ico' style='display:none'></iframe>").on('load', function() {
        //				setTimeout(function() {
        //					$iframe.off('load').remove();
        //				}, 0)
        //			}).appendTo($("body"));
        //		}
        //	},
        //	headers:{
        //		"x-moutai-token": window.token,
        //	}
        //});
        //banner

        $.ajax({
            type: "get",
            url: window.baseurl + "api/index/getbanner?size=" + 5,
            //data:JSON.stringify(postdata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                var datas = data.result;
                if (datas.length > 0) {
                    var html = "";
                    for (var i = 0; i < datas.length; i++) {
                        html += "<li><a href='" + datas[i].link + "?shopid=" + window.shopid + "'><img src='" + datas[i].img + "' /></a></li>";
                    }
                    $(".banner").prepend("<ul>" + html + "</ul>").show();
                    if (datas.length == 1) {
                        return;
                    }
                    $(".banner").addClass("swiper-container").append("<div class='num'></div>");
                    $(".banner>ul:first").addClass("swiper-wrapper").find("li").addClass("swiper-slide");
                    var auto_width = $(".banner").width();
                    var auto_height = 274;
                    var auto_num = $(".banner>ul:first img").length;
                    function flash() {
                        //获取图片高度
                        if (document.documentElement.clientWidth >= 640) {
                            auto_width = 640;
                            auto_height = 274;
                        }
                        else {
                            auto_width = document.documentElement.clientWidth;
                            auto_height = 274 * auto_width / 640;
                        }
                        $(".banner").width(auto_width).height(auto_height);
                        $(".banner>ul:first li").width(auto_width);
                        $(".banner>ul:first").width(auto_width * auto_num).height(auto_height);
                    }
                    flash();
                    $(window).resize(function () { flash() });
                    $('.banner').swiper({
                        pagination: '.num',
                        mode: 'horizontal',
                        loop: true,
                        autoplay: 5000,
                        autoplayDisableOnInteraction: false
                    })
                }
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        ////分类

        $.ajax({
            type: "get",
            url: window.baseurl + "api/index/getcategoris?size=" + 4,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    html += "<li><a href='product-class.html?shopid=" + window.shopid + "&classId=" + datas[i].id + "'><img src='images/class-" + (i + 1) + ".png' />" + datas[i].title + "</a></li>";
                }
                $(".class").prepend(html).show();
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        ////推荐产品

        $.ajax({
            type: "get",
            url: window.baseurl + "api/index/getrecommend?size=" + 3,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    html += "<li><a href='" + datas[i].link + "?shopid=" + window.shopid + "'><img src='" + datas[i].img + "' /></a></li>";
                }
                $(".hot-product").append(html).show();
                loadsrc();
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        ////新品上架

        $.ajax({
            type: "get",
            url: window.baseurl + "api/index/getnew?size=" + 10,
            success: function (data) {

                if (!detectionResult(data, 1)) {
                    return;
                }
                var w = document.documentElement.clientWidth - 178;
                var length = Math.floor(w / 11) * 2;
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    html += "<li><a href='product-detail.html?shopid=" + window.shopid + "&skuid=" + datas[i].id+"&productId=" + datas[i].productid + "'><div class='ul-new-img'><img src='' loadsrc='" + datas[i].img + "' /></div><h3>" + datas[i].title + "</h3><p class='attr'>销量：" + datas[i].saleqty + "</p><div class='price'>￥" + datas[i].price.toFixed(2) + "</div><p>" + datas[i].text.substring(0, length) + "</p><p>" + datas[i].text.substring(length, datas[i].text.length) + "</p></a></li>";
                }
                $(".ul-new").append(html);
                loadsrc();
                $(".new-product").show();
                $("body").show();  
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        ////读取客服消息
        //	$.ajax({
        //		type: "get",
        //        url: "api/IM/GetLatestMessage",
        //		dataType: "json",
        //		data:{"shopid":window.shopid},
        //		success: function(data){
        //			if ( !detectionResult(data,0) ){
        //				return;
        //			}
        //			if ( data.result.number > 0 ){
        //				$(".msg").addClass("have-msg");
        //			}
        //		},
        //		headers:{
        //			"x-moutai-token": window.token,
        //		}
        //	});
        ////读取订单及系统消息
        //	$.ajax({
        //		type: "get",
        //        url: "api/SystemMsg/GetMsgSummarize",
        //		dataType: "json",
        //		success: function(data){
        //			if ( !detectionResult(data,0) ){
        //				return;
        //			}
        //			var datas = data.result;
        //			for ( var i=0;i<datas.length;i++ ){
        //				if ( datas[i].number > 0 ){
        //					$(".msg").addClass("have-msg");
        //					break;
        //				}
        //			}
        //		},
        //		headers:{
        //			"x-moutai-token": window.token,
        //		}
        //	});
    });

    
});
