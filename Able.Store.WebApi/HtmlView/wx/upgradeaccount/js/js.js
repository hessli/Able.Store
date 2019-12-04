/*
 * 全站公共脚本,基于jquery-2.1.1脚本库，库中已嵌入tap,transition,url
*/

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
		$tip.css({bottom:-height}).transition({y:-height-36,complete:function(){
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
//弹窗业务
	function windowOpen(type,scroll){
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
			$window.children().hide();
			windowOpenAni(type,scroll);
		}
		else{
			windowOpenAni(type,scroll);
		}
	}
	var scrollTop;
	var openAni = false;
	function windowOpenAni(type,scroll){
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
			if ( callBack != null ){
				callBack();
				callBack = null;
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
//图片定位
	function big(bigsrc){
        var imgwidth = bigsrc.width();
        var imgheight = bigsrc.height();
        var parent = bigsrc.parent();
        var parentwidth = parent.width();
        var parentheight = parent.height();
        if ( imgwidth <= parentwidth) 
        {
            if ( imgheight >= parentheight ) {
                imgwidth = imgwidth / (imgheight / parentheight);
                imgheight = parentheight;
            }
        }
        else 
        {
            if ( imgheight / (imgwidth / parentwidth) <= parentheight ) {
                imgheight = imgheight / (imgwidth / parentwidth);
                imgwidth = parentwidth;
            }
            else {
                imgwidth = imgwidth / (imgheight / parentheight);
                imgheight = parentheight;
            }
        }
        bigsrc.css({"visibility":"visible","position":"absolute","width" : imgwidth, "height" : imgheight, "left" : (parentwidth - imgwidth) / 2, "top" : (parentheight - imgheight) / 2});
	}
//检测返回的结果状态
	function detectionResult(data,alertType){
		if ( data.issuccess == 0 ){
//			if ( data.err.errcode == "-200000" || data.err.errcode == "-200001" || data.err.errcode == "-200002" || data.err.errcode == "-200003" || data.err.errcode == "-2000015" || data.err.errcode == "-200007" ){
//				window.location.href = window.localStorage.jumpUrl;
//				return false
//			}
//			else if ( data.err.errcode == "1000" ){
//				window.location.href = "500.html";
//				return false
//			}
			windowAlert(data.err.msg,alertType);
			return false
		}
		return true;
	}
//正则
	function checkTel(value){
		var myreg = /(((13[0-9]{1})|(18[0-9]{1})|(17[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
		if( myreg.test(value) ){
			return true;
		}
		else{
			return false;
		}
	}
	function checkMail(value){
		var myreg = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
		if( myreg.test(value) ){
			return true;
		}
		else{
			return false;
		}
	}
$(function(){
	//token
		var token = decodeURIComponent($.query.get("token"));
		if ( token == "" ){
			windowAlert("token不正确",0);
			return
		}
		if ( document.getElementById("warchief") ){
			var type = 0;
		}
		else if ( document.getElementById("seller") ){
			var type = 1;
		}
		if ( $(".ul-list").length ){
			$(".ul-list").find("a").each(function(){
				var encodeToken = encodeURIComponent(token);

				var href = this.href;
				if ( href.charAt(0) != "j" ){//排除含脚本属性的链接，如后退按钮
					if ( href.indexOf("?") > 0 ){
						this.href = href + "&token=" + encodeToken;
					}
					else{
						this.href = href + "?token=" + encodeToken;
					}
				}
			})
			$(".ul-list").show();
		}
		else{
			$.ajax({
				type: "post",
		        url: baseapi_url + "/h5/web/user/apply/issend",
				dataType: "json",
				data:{"applytype":type},
				success: function(data){
					if ( data.issuccess == 0 ){
						$("#step-1").show();
						$("textarea").each(function(){
							this.style.height = this.scrollHeight + 'px';
						})		
						//展示图片定位
							if ( $("#show-img").length ){
								var img = new Image();
								var showImg = $("#show-img").find("img");
								img.src = showImg.attr("src");
								img.onload = function(){
									big(showImg);
								}
							}
						return false;
					}
					if ( data.issuccess == 1 ){
						$.ajax({
							type: "post",
					        url: baseapi_url + "/h5/web/user/apply/check",
							dataType: "json",
							data:{"applytype":type},
							success: function(data){
								if ( !detectionResult(data,1) ){
									return;
								}
								var state = data.result.checkstate;
								switch ( state ){
									case 0:
										$("#step-2").show();
										break;
									case 1:
										$("#step-4").show();
										break;
									case 2:
										$("#step-3").show();
										break;
								}
							},
							error:function(){
								windowAlert("您的网络不稳定喔，请刷新试试。^_^",0);
							},
							headers:{
								"x-stbl-token": token,
							}
						});
						return false;
					}
				},
				error:function(){
					windowAlert("您的网络不稳定喔，请刷新试试。^_^",0);
				},
				headers:{
					"x-stbl-token": token,
				}
			});
		}
	//返回查看优势
		$(document).on("tap",".back-youshi",function(event){
			event.preventDefault();
			$("#step-1").show().find("form").hide().prev().hide();
			$("#step-2").hide();
		})
	//再次申请
		$(document).on("tap",".rightnow",function(event){
			event.preventDefault();
			$("#step-1").show();
			$("#step-4").hide();
		})
	//文本区域高度自动
		$(document).on("input","textarea",function(event){
			event.preventDefault();
			this.style.height = ""
			var height = this.scrollHeight;
			this.style.height = height + 'px';
		})
	//打开协议
		$(document).on("tap",".open-xieyi-1",function(event){
			event.preventDefault();
			window.callBack = function(){
				new IScroll($("#explain .main")[0], { probeType: 3, scrollX: false, freeScroll: true,fadeScrollbars:false,resizeScrollbars:false,shrinkScrollbars:'clip',scrollbars: true, scrollbars: 'custom' });
			}
			windowOpen("explain",null);
		})
		$(document).on("tap",".open-xieyi-2",function(event){
			event.preventDefault();
			window.callBack = function(){
				new IScroll($("#explains .main")[0], { probeType: 3, scrollX: false, freeScroll: true,fadeScrollbars:false,resizeScrollbars:false,shrinkScrollbars:'clip',scrollbars: true, scrollbars: 'custom' });
			}
			windowOpen("explains",null);
		})
	//关闭
		$(document).on("tap",".go-back",function(event){
			event.preventDefault();
			windowClose();
			$("input").blur();
		})
	//联动
		$(document).on("touchstart mousedown",".shade",function(event){
			event.preventDefault();
		})
		if ( document.getElementById("select") ){
			$.ajax({
				type: "post",
		        url: baseapi_url + "/h5/web/common/citytree/show",
				dataType: "json",
				data:{"filter":3},
				success: function(data){
					if ( !detectionResult(data,1) ){
						return;
					}
					window.selectData = data.result;
					var html = "";
					for ( var i=0;i<selectData.length;i++ ){
						html += "<li data-code='"+ selectData[i].adcode +"'>"+ selectData[i].provincename +"</li>";
					}
					$("#province").find("ul").html(html);
					$("#province").find("ul").each(function(){
						var ons = $(this).find("li:first").addClass("ons");
						ons.next().addClass("on-4").next().addClass("on-5").next().addClass("on-6");
						ons.prev().addClass("on-3").prev().addClass("on-2").prev().addClass("on-1");
					})
					getCity();
				},
				error:function(){
					windowAlert("您的网络不稳定喔，请刷新试试。^_^",0);
				},
				headers:{
					"x-stbl-token": token,
				}
			});
			function getCity(){
				var province = $("#province").find("li.ons").attr("data-code");
				var html = "";
				for ( var i=0;i<selectData.length;i++ ){
					if ( selectData[i].adcode == province ){
						for ( var j=0;j<selectData[i].citys.length;j++ ){
							html += "<li data-code='"+ selectData[i].citys[j].adcode +"'>"+ selectData[i].citys[j].cityname +"</li>";
						}
						$("#city").find("ul").html(html);
						$("#city").find("ul").each(function(){
							var ons = $(this).find("li:first").addClass("ons");
							ons.next().addClass("on-4").next().addClass("on-5").next().addClass("on-6");
							ons.prev().addClass("on-3").prev().addClass("on-2").prev().addClass("on-1");
						})
						getCountry();
						break;
					}
				}
			}
			var first = false;
			function getCountry(){
				var province = $("#province").find("li.ons").attr("data-code");
				var city = $("#city").find("li.ons").attr("data-code");
				var html = "";
				for ( var i=0;i<selectData.length;i++ ){
					if ( selectData[i].adcode == province ){
						for ( var j=0;j<selectData[i].citys.length;j++ ){
							if ( selectData[i].citys[j].adcode == city ){
								for ( var k=0;k<selectData[i].citys[j].districts.length;k++ ){
									html += "<li data-code='"+ selectData[i].citys[j].districts[k].adcode +"'>"+ selectData[i].citys[j].districts[k].districtname +"</li>";
								}
								$("#country").find("ul").html(html);
								$("#country").find("ul").each(function(){
									var ons = $(this).find("li:first").addClass("ons");
									ons.next().addClass("on-4").next().addClass("on-5").next().addClass("on-6");
									ons.prev().addClass("on-3").prev().addClass("on-2").prev().addClass("on-1");
								})
								if ( !first ){
									first = true;
									var html = "";
									html += $("#province").find("li.ons").html();
									html += " " + $("#city").find("li.ons").html();
									if ( $("#country").find("li.ons").length ){
										html += " " + $("#country").find("li.ons").html();
									}
									$(".select").find("p").html(html);
								}
								break;
							}
						}
						break;
					}
				}
			}
			var provinceScroll = new IScroll('#province', { probeType:3,mouseWheel:true,bounce:false,momentum: false,snap:false,snapSpeed:30,keyBindings:true});
			provinceScroll.on('scroll', updatePosition);
			provinceScroll.on('scrollEnd', updatePositionEnd);
			function updatePosition(){
				this.maxScrollY = -($(this.wrapper).find("li").length-1)*$(this.wrapper).find("li").height();
				var y = Math.abs(this.y);
				var number = y/$(this.wrapper).find("li").height();
				$(this.wrapper).find("li").removeAttr("class");
				var ons = $(this.wrapper).find("li").eq(parseInt(number)).addClass("ons");
				ons.next().addClass("on-4").next().addClass("on-5").next().addClass("on-6");
				ons.prev().addClass("on-3").prev().addClass("on-2").prev().addClass("on-1");
				getCity();
				cityScroll.scrollToElement($("#city").find("li:first")[0],null,null,true);
			}
			function updatePositionEnd(){
				this.scrollToElement($(this.wrapper).find("ul:visible").find("li.ons")[0], null, null, true)
			}
			var cityScroll = new IScroll('#city', { probeType:3,mouseWheel:true,bounce:false,momentum: false,snap:false,snapSpeed:30,keyBindings:true});
			cityScroll.on('scroll', updatePositions);
			cityScroll.on('scrollEnd', updatePositionEnd);
			function updatePositions(){
				this.maxScrollY = -($(this.wrapper).find("ul:visible").find("li").length-1)*$(this.wrapper).find("ul:visible").find("li").height();
				var y = Math.abs(this.y);
				var number = y/$(this.wrapper).find("ul:visible").find("li").height();
				$(this.wrapper).find("ul:visible").find("li").removeAttr("class");
				var ons = $(this.wrapper).find("ul:visible").find("li").eq(parseInt(number)).addClass("ons");
				ons.next().addClass("on-4").next().addClass("on-5").next().addClass("on-6");
				ons.prev().addClass("on-3").prev().addClass("on-2").prev().addClass("on-1");
				getCountry();
				countryScroll.scrollToElement($("#country").find("li:first")[0],null,null,true);
			}
			var countryScroll = new IScroll('#country', { probeType:3,mouseWheel:true,bounce:false,momentum: false,snap:false,snapSpeed:30,keyBindings:true});
			countryScroll.on('scroll', updatePositionss);
			countryScroll.on('scrollEnd', updatePositionEnd);
			function updatePositionss(){
				this.maxScrollY = -($(this.wrapper).find("ul:visible").find("li").length-1)*$(this.wrapper).find("ul:visible").find("li").height();
				var y = Math.abs(this.y);
				var number = y/$(this.wrapper).find("ul:visible").find("li").height();
				$(this.wrapper).find("ul:visible").find("li").removeAttr("class");
				var ons = $(this.wrapper).find("ul:visible").find("li").eq(parseInt(number)).addClass("ons");
				ons.next().addClass("on-4").next().addClass("on-5").next().addClass("on-6");
				ons.prev().addClass("on-3").prev().addClass("on-2").prev().addClass("on-1");
			}
			$(document).on("tap","#ok-select",function(event){
				event.preventDefault();
				var html = "";
				html += $("#province").find("ul:visible").find("li.ons").html();
				html += " " + $("#city").find("ul:visible").find("li.ons").html();
				if ( $("#country").find("ul:visible").find("li.ons").length ){
					html += " " + $("#country").find("ul:visible").find("li.ons").html();
				}
				$(".select").find("p").html(html);
				$("#select").hide();
			})
			$(document).on("tap",".select",function(event){
				event.preventDefault();
				$("#select").show();
				provinceScroll.refresh();
				cityScroll.refresh();
				countryScroll.refresh();
			})
	    	function file1(){
	 			var div = $(".upload").parent();
	 			div.find("input").remove();
	 			div.append("<input type='file' capture='camera' accept='image/*' class='upload' />");
			    $(".upload").makeThumb({
			        width: 1200,
			        height: 1200,
			        success: function(dataURL, tSize, file, sSize, fEvt) {
	    				file1();
	    				$(".upload").hide();
			 			var imgDiv = $("#show-img");
			 			imgDiv.empty().append("<img />");
			 			imgDiv.find("img").attr("src",dataURL).load(function(){
			 				big($(this));
							$.ajax({
								type: "post",
						        url: baseapi_url + "/user/auth/seller/uploadbase64",
								data:{"imgstring":dataURL.substring(23,dataURL.length)},
								success: function(data){
									if ( !detectionResult(data,1) ){
										return;
									}
									windowAlertClose("图片上传成功",1);
	    							$(".upload").show();
	    							document.getElementById("img").value = data.result.filename;
								},
								error:function(){
									windowAlertClose("您的网络不稳定喔，请稍后再试试。^_^",1);
	    							$(".upload").show();
								},
								headers:{
									"x-stbl-token": token,
								}
							});
			 			});
			        }
			    });
	    	}
	    	file1();
		}
	//协议同意
		$(document).on("tap",".agree",function(event){
			event.preventDefault();
			if ( $(this).hasClass("agreed") ){
				$(this).removeClass("agreed");
			}
			else{
				$(this).addClass("agreed");
			}
		})
	//大酋长申请
		var submit = false;
		$(document).on("submit","#warchief .form",function(event){
			event.preventDefault();
			if ( submit ){
				return false;
			}
			if ( !$(".agree").hasClass("agreed") ){
				windowAlert("请先阅读相关协议后同意条款后继续",1);
				return false;
			}
			var postData = {};
			postData.contactname = document.getElementById("name").value;
			postData.contactphone = document.getElementById("tel").value;
			postData.email = document.getElementById("mail").value;
			postData.applyreason = document.getElementById("identity").value;
			if ( postData.contactname == "" ){
				windowAlert("请输入联系人姓名",1);
				return false;
			}
			if ( !checkTel(postData.contactphone) ){
				windowAlert("请输入正确的手机号码",1);
				return false;
			}
			if ( !checkMail(postData.email) ){
				windowAlert("请输入正确的邮箱地址",1);
				return false;
			}
			submit = true;
			var button = $(this).find("input[type=submit]");
			button.val("正在提交...");
			$.ajax({
				type: "post",
		        url: baseapi_url + "/h5/web/user/apply/big",
				data:JSON.stringify(postData),
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(data){
					if ( !detectionResult(data,1) ){
						submit = false;
						button.val("我要申请");
						return;
					}
					$("#step-1").hide().next().show();
					$('html,body').animate({scrollTop: '0px'},1);
				},
				error:function(){
					windowAlert("您的网络不稳定喔，请稍后再试试。^_^",1);
					submit = false;
					button.val("我要申请");
				},
				headers:{
					"x-stbl-token": token,
				}
			});
		})
	//商家申请
		$(document).on("submit","#seller .form",function(event){
			event.preventDefault();
			if ( submit ){
				return false;
			}
			if ( !$(".agree").hasClass("agreed") ){
				windowAlert("请先阅读相关协议后同意条款后继续",1);
				return false;
			}
			var postData = {};
			postData.contactname = document.getElementById("name").value;
			postData.contactphone = document.getElementById("tel").value;
			postData.email = document.getElementById("mail").value;
			postData.applyreason = document.getElementById("identity").value;
			postData.provinceid = $("#province").find("li.ons").attr("data-code");
			postData.cityid = $("#city").find("li.ons").attr("data-code");
			if ( $("#province").find("li.ons").length ){
				postData.districtid = $("#province").find("li.ons").attr("data-code");
			}
			else{
				postData.districtid = "";
			}
			postData.contactaddr = document.getElementById("address").value;
			//暂时屏蔽上传图片
			//postData.authurl = document.getElementById("img").value;
			if ( postData.contactname == "" ){
				windowAlert("请输入联系人姓名",1);
				return false;
			}
			if ( !checkTel(postData.contactphone) ){
				windowAlert("请输入正确的手机号码",1);
				return false;
			}
			if ( !checkMail(postData.email) ){
				windowAlert("请输入正确的邮箱地址",1);
				return false;
			}
			if ( postData.contactaddr == "" ){
				windowAlert("请输入详情地址",1);
				return false;
			}
			//暂时屏蔽上传图片
//			if ( postData.authurl == "" ){
//				windowAlert("请先上传认证照片",1);
//				return false;
//			}
			submit = true;
			var button = $(this).find("input[type=submit]");
			button.val("正在提交...");
			$.ajax({
				type: "post",
		        url: baseapi_url + "/h5/web/user/apply/seller",
				data:JSON.stringify(postData),
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(data){
					if ( !detectionResult(data,1) ){
						submit = false;
						button.val("我要申请");
						return;
					}
					$("#step-1").hide().next().show();
					$('html,body').animate({scrollTop: '0px'},1);
				},
				error:function(){
					windowAlert("您的网络不稳定喔，请稍后再试试。^_^",1);
					submit = false;
					button.val("我要申请");
				},
				headers:{
					"x-stbl-token": token,
				}
			});
		})
})