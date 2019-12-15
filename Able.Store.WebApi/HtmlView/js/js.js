/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
//jssdk
 window.baseurl = "http://192.168.1.103:7689/";

//window.baseurl = "http://localhost:4876/";
//显示alert
	function windowAlert(text,alertTime){
		if ( typeof(autoTip) != "undefined" ){
			clearTimeout(autoTip);
		}
		$("#tip").remove();
		$("body").append("<div id='tip'><span></span></div>");
		var $tip = $("#tip");
		$tip.find("span").html(text);
		$tip.show();
		var height = $("#tip").height();
		$tip.css({bottom:-height}).transition({y:-height-68,complete:function(){
			if ( alertTime != 0 ){
				autoTip = setTimeout(function(){
					$tip.transition({y:0,complete:function(){
						$tip.hide();
					}},300);
				},1000)
			}
		}},300);
	}
	function windowAlertClose(text){
		if ( typeof(autoTip) != "undefined" ){
			clearTimeout(autoTip);
		}
		var $tip = $("#tip");
		$tip.find("span").html(text);
		autoTip = setTimeout(function(){
			$tip.transition({y:0,complete:function(){
				$tip.hide();
			}},300);
		},1000)
	}
//显示confirm
	function windowConfirm(text1,text2,text3){
		if ( !document.getElementById("confirm") ){
			$("body").append("<div class='confirm' id='confirm'><div class='confirm-box'><div class='confirm-title'><div class='td'></div></div><div class='confirm-button'><a class='false'></a><a class='true'></a></div></div></div>");
			$("#confirm").on("touchstart mousedown touchmove mousemove",function(event){
				event.preventDefault();
			})
			$("#confirm .false").on("tap",function(event){
				event.preventDefault();
				$("#confirm").hide();
			})
			$("#confirm .true").on("tap",function(event){
				event.preventDefault();
				trueFunction();
				$("#confirm").hide();
			})
		}
		var $confirm = $("#confirm");
		$confirm.find(".td").html(text1);
		$confirm.find(".false").html(text2);
		$confirm.find(".true").html(text3);
		$confirm.show();
	}
	//立即购买
//		$(document).on("tap",".buy",function(event){
//			event.preventDefault();
//			var href = this.href;
//			trueFunction = function(){
//				window.location.href = href;
//			}
//			windowConfirm("立即购买？","否","购买");
//		})
//弹窗业务
	function windowOpen(type,scroll,callBack){
		if ( openAni ){
			return;
		}
		openAni = true;
		if ( !document.getElementById("window") ){
			$("body").append("<div id='window'></div>");
		}
		var $window = $("#window");
		$window.show();
		if ( !document.getElementById(type) ){
			var defer = $.Deferred();
			$.ajax({
				type:"get",
				url:"ajax.html",
				dataType:"html",
				async:true,
				success:function(data){
					$window.append(data);
					$window.children().hide();
					windowOpenAni(type,scroll,callBack);
				},
				error:function(){
					windowAlert("网络错误，请重试！");
					$window.hide();
					openAni = false;
				},
			});
		}
		else{
			windowOpenAni(type,scroll,callBack);
		}
	}
	var scrollTop;
	var openAni = false;
	function windowOpenAni(type,scroll,callBack){
		$("#"+type).show().transition({x:0,complete:function(){
			if ( $("html").attr("id") != "full-page" ){
				scrollTop = document.documentElement.scrollTop+document.body.scrollTop;
				$('html,body').scrollTop(0);
				$("html").attr("id","full-page");
				$("#window").css({"position":"absolute"});
			}
			if ( typeof(scroll) != "undefined" ){
				if ( scroll == "scroll" ){
					$("#"+type).css({"overflow":"hidden"});
					window.scroll = new IScroll("#"+type, { probeType: 3, scrollX: false, freeScroll: true,fadeScrollbars:false,resizeScrollbars:false,shrinkScrollbars:'clip',scrollbars: true, scrollbars: 'custom' });
				}
			}
			if ( typeof(callBack) != "undefined" ){
				callBack();
			}
			openAni = false;
		}},500);
	}
	function windowClose(){
		if ( openAni ){
			return;
		}
		$("html").attr("id","");
		$('html,body').scrollTop(scrollTop);
		$("#window").css({"position":"fixed"}).find(">div:visible").transition({x:"100%",complete:function(){
			$(this).hide();
			$("#window").hide();
		}},500);
	}
	function windowCloseSingle(type){
		if ( openAni ){
			return;
		}
		$("#"+type).transition({x:"100%",complete:function(){
			$(this).hide();
			openAni = false;
		}},500);
	}
	function goHref(obj){
		window.location.href = obj.getAttribute("data-href");
	}
//图片延迟加载
	function loadsrc(){
		var scrollTop = document.documentElement.scrollTop + document.body.scrollTop + document.documentElement.clientHeight;
		$("body img[loadsrc]:visible").each(function(){
			if ( scrollTop > $(this).offset().top){
				$(this).attr("src",$(this).attr("loadsrc")).removeAttr("loadsrc");
			}
		})
	}
	document.addEventListener("scroll",function(){
		loadsrc();
	})
//检测返回的结果状态
	function detectionResult(data,alertType){
        if (data.issuccess == 0) {
            if (data.errcode == "-200000" || data.errcode == "-200001" || data.errcode == "-200002" || data.errcode == "-200003" || data.errcode == "-2000015" || data.errcode == "-200007")
            {
                window.location.href = window.localStorage.jumpUrl;
                return false
            }
            else if ( data.errcode == "1000") {
                window.location.href = "500.html";
                return false
            }
            windowAlert(data.message,alertType);
			return false
		}
		return true;
	}
//手机号码正则判断
	function checkTel(value){
		var myreg = /(((13[0-9]{1})|(18[0-9]{1})|(17[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
		if( myreg.test(value) ){
			return true;
		}
		else{
			return false;
		}
	}
//身份证号码正则判断
	function checkNumber(value){
		var myreg = /^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/;
		if( myreg.test(value) ){
			return true;
		}
		else{
			return false;
		}
	}
//依赖dom加载完毕的绑定事件,可能重复应用于多个页面的脚本
$(function(){
	//window.parameter = $.Deferred();//参数
	//window.shopState = $.Deferred();//店铺状态
	////url必带参数判断
	//	if ( typeof(must) != "undefined" ){
	//		for ( var i=0;i<must.length;i++ ){
	//			if ( $.query.get(must[i].title) == "" ){
	//				$("body").children(":not(script)").remove();
	//				windowAlert(must[i].alert,0);
	//				window.parameter.reject();
	//				return
	//			}
	//		}
	//		window.shopid = $.query.get("shopid");
	//		window.localStorage.url = window.location.href;
 //           window.localStorage.jumpUrl = "http://localhost:61873/api/mall/auth?shopid=" + window.shopid;

 //           //window.localStorage.jumpUrl="http://moutai-mall.stbl.cc/api/mall/auth?shopid="+window.shopid;
	//		window.parameter.resolve();
	//	}
	//	else{
	//		window.parameter.resolve();
	//		window.shopid = $.query.get("shopid");
	//	}
	////参数正常登录拿取授权
	//	$.when(window.parameter).done(function(){
	//		window.token = window.localStorage.token;
	//		if ( typeof(token) == "undefined" ){
	//			window.location.href = window.localStorage.jumpUrl;
	//			window.shopState.reject();
	//			return;
	//		}
	//		//window.shopState.resolve();
	//		//jssdk
	//		$.ajax({
	//			type: "get",
	//	        url: "api/shareconfig/get",
	//			dataType: "json",
	//			data:{"shopid":window.shopid},
	//			success: function(data){
	//				if ( !detectionResult(data,1) ){
	//					return;
	//				}
	//				jssdkCallback(data.result);
	//				window.shopState.resolve();
	//			},
	//			headers:{
	//				"x-moutai-token": window.token,
	//			}
	//		});
	//	})
	////不缺少参数&&授权成功 绑定前置事件
	//	$.when(window.shopState).done(function(){
	//		document.body.style.visibility = "visible";
	//		//页面中所有默认存在的链接重定义url
	//			$("body a[href]").each(function(){
	//				var href = this.href;
	//				if ( href.charAt(0) != "j" ){//排除含脚本属性的链接，如后退按钮
	//					if ( href.indexOf("?") > 0 ){
	//						this.href = href + "&shopid=" + window.shopid;
	//					}
	//					else{
	//						this.href = href + "?shopid=" + window.shopid;
	//					}
	//				}
	//			})
	//		//数量控制
				$(document).on("tap",".cut",function(event){
					event.preventDefault();
					var input = $(this).next();
					var num = parseInt(input.val());
					if ( input.attr("max") ){
						if ( num > 0 ){
							input.val(num-1);
						}
					}
					else if ( num > 1 ){
						input.val(num-1);
					}
					input.blur();
				})
				$(document).on("tap",".add",function(event){
					event.preventDefault();
					var input = $(this).prev();
					var num = parseInt(input.val());
					if ( input.attr("max") ){
						if ( num < input.attr("max") ){
							input.val(num+1);
						}
					}
					else{
						input.val(num+1);
					}
					input.blur();
				})
	//		//手动修改数量时判断类型
				$(document).on("change",".shuliang input",function(event){
					event.preventDefault();
					var input = $(this);
					var num = input.val();
					if ( input.attr("max") ){
						if ( num <= 0 || isNaN(num) ){
							input.val("0");
						}
						if ( num > input.attr("max") ){
							input.val(input.attr("max"));
						}
					}
					else{
						if ( num <= 0 || isNaN(num) ){
							input.val("1");
						}
					}
					input.val(parseInt(input.val()));
				})
	 		//tab切换
				$(document).on("tap",".tab>li",function(event){
					event.preventDefault();
					var tab_index = $(this).parent().index("body .tab");
					var index = $(this).index();
					$(this).siblings().removeClass("on").end().addClass("on");
					$("body .tab-div").eq(tab_index).children().hide().eq(index).show();
				})
			//单选
				$(document).on("tap",".single>li",function(event){
					event.preventDefault();
					$(this).siblings().removeClass("on").end().addClass("on");
				})
			//文本区域高度自动
				$("textarea").each(function(){
					this.style.height = this.scrollHeight + 'px';
				})
				$(document).on("input","textarea",function(event){
					event.preventDefault();
					this.style.height = ""
					var height = this.scrollHeight;
					this.style.height = height + 'px';
				})
	//		//订单列表进入聊天
	//			$(document).on("tap",".say",function(event){
	//				event.preventDefault();
	//				window.location.href = "message-service.html";
	//			})
	//		//需要发票
	//			$(document).on("tap",".fapiao textarea",function(){
	//				$(this).parent().siblings().filter(".ul-k").find("li:last").addClass("on").siblings().removeClass("on");
	//			})
	//    });
})