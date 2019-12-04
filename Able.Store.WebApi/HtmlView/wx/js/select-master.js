/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
 */
var oldInvitation = "";
 var flag_check = true;
function set() {
	if (!flag_check) {
		$(".code-tit").addClass("wrong");
		// var value = Invitation ? Invitation : "请输入师傅邀请码";
		$("#yard").val("请输入师傅邀请码");
	} else {
		$(".code-tit").removeClass("wrong");
		$("#yard").val(Invitation);
	}
}

function getMater() {
	var postData = {};
	postData.invitecode = Invitation;
	$.ajax({
		type: "post",
		url: baseapi_url + "/h5/web/user/getwithinvitecode",
		//		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: postData,
		//		beforeSend:function(){
		//			$(".photo").html("<img src='images/nophoto.png' />").next().html(" ");
		//		},
		success: function(data) {
			if (!detectionResult(data, 1)) {
				// Invitation = "";
				
				check = false;
				flag_check = false;
				set();

				var value = oldInvitation ? oldInvitation : "请输入师傅邀请码";
				$("#yard").val(value);
				return;
			}

			// 绑定师傅信息
			$(".photo").html("<img src='" + data.result.user.imgurl + "' />").next().html(data.result.user.nickname);
			
			check = true;
			flag_check = true;
			set();
			window.groupid = data.result.groupinfo.groupid;
			window.groupname = data.result.groupinfo.groupname;
			window.groupdesc = data.result.groupinfo.groupdesc;
			window.groupimg = data.result.groupinfo.groupimg;
		},
		error: function() {
			Invitation = "";
			check = false;
			flag_check = false;
			set();
			windowAlert("网络错误，请重试");
			$(".photo").html("<img src='images/nophoto.png' />").next().html(" ");
			check = false;
		},
		headers: {
			"x-stbl-token": window.localStorage.accesstoken,
		}
	});
}

function getFind() {
	if (ajaxing) {
		return;
	}
	ajaxing = true;
	$("#find-list").next().show();
	var postData = {};
	postData.keyword = document.getElementById("keyword").value;
	postData.page = page;
	postData.count = counts;
	$.ajax({
		type: "post",
		url: baseapi_url + "/h5/web/user/search",
		//		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: postData,
		beforeSend: function() {
			$("#find-list").html("").next().html("正在加载...").show();
		},
		success: function(data) {
			if (!detectionResult(data, 1)) {
				return;
			}
			if (data.result.length == 0) {
				ajaxing = false;
				$("#find-list").html("").next().html("无相关结果").show();
				return
			}
			var datas = data.result;
			var html = "";
			for (var i = 0; i < datas.length; i++) {
				var sex;
				if (datas[i].userview.gender == 0) {
					sex = "♂";
				} else {
					sex = "♀";
				}
				html += "<li data-id='" + datas[i].userview.invitecode + "' data-id='" + datas[i].userview.userid + "' data-name='" + datas[i].userview.nickname + "' data-img='" + datas[i].userview.imgurl + "'><div class='master-photo'><img src='" + datas[i].userview.imgurl + "' /></div><h3>" + datas[i].userview.nickname + "</h3><div class='xb'>" + sex + " " + datas[i].userview.age + "</div><div class='areas'>" + datas[i].userview.cityname + "</div><div class='add'>选择</div></li>";
			}
			$("#find-list").html(html).next().hide();
			//page ++;
			ajaxing = false;
			iscroll.refresh();
		},
		error: function() {
			windowAlert("网络错误，请重试");
			ajaxing = false;
		},
		headers: {
			"x-stbl-token": window.localStorage.accesstoken,
		}
	});
}
var have = false;

function getRecommend() {
	if (ajaxing) {
		return;
	}
	ajaxing = true;
	var postData = {};
	postData.page = pages;
	postData.count = count;
	$.ajax({
		type: "post",
		url: baseapi_url + "/h5/web/user/recommendmaster/get",
		//		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: postData,
		success: function(data) {
			if (!detectionResult(data, 1)) {
				ajaxing = false;
				return;
			}
			if (data.result.length == 0) {
				pages = 1;
				ajaxing = false;
				getRecommend();
				return;
			}
			var datas = data.result;
			var html = "";
			for (var i = 0; i < datas.length; i++) {
				var imgdata = datas[i].photoview;
				var htmls = "";
				for (var j = 0; j < imgdata.length; j++) {
					htmls += "<li data-big='" + imgdata[j].originalurl + "'><img src='" + imgdata[j].thumburl + "' /></li>";
				}
				var sex;
				if (datas[i].sex == 0) {
					sex = "♂";
				} else {
					sex = "♀";
				}
				html += "<li data-id='" + datas[i].userview.invitecode + "' data-name='" + datas[i].userview.nickname + "' data-img='" + datas[i].userview.imgurl + "'><div class='master-photo'><img src='" + datas[i].userview.imgurl + "' /></div><h3>" + datas[i].userview.nickname + "</h3><div class='xb'>" + sex + " " + datas[i].userview.age + "</div><div class='areas'>" + datas[i].userview.cityname + "</div><div class='add'>选择</div><div class='he'><h3>Ta的照片</h3><ul class='ul-he'>" + htmls + "</ul></div><ul class='follow'><li><h3>" + addCommas(datas[i].countview.tudi_count) + "</h3><p>徒弟</p></li><li><h3>" + addCommas(datas[i].countview.fans_count) + "</h3><p>粉丝</p></li></ul></li>";
			}
			$("#recommend-list").html(html).next().hide();
			var w = $("#recommend-list").find(".ul-he li").width() - 5;
			$("#recommend-list").find(".ul-he li").each((function() {
				$(this).width(w).height(w);
				$(this).parent().width($(this).outerWidth(true) * 5);
				var img = new Image();
				var imgs = $(this).find("img");
				img.src = imgs.attr("src");
				if (img.complete) {
					small(imgs);
				} else {
					img.onload = function() {
						small(imgs);
					}
				}
			}))
			pages++;
			ajaxing = false;
			iscroll.refresh();
			have = true;
		},
		error: function() {
			windowAlert("网络错误，请重试");
			ajaxing = false;
		},
		headers: {
			"x-stbl-token": window.localStorage.accesstoken,
		}
	});
}
$(function() {
	$(document).on("tap", ".add", function(event) {
		event.preventDefault();
		Invitation = $(this).closest("li").attr("data-id");
		oldInvitation = Invitation;
		$(".photo").html("<img src='" + $(this).closest("li").attr("data-img") + "' />").next().html($(this).closest("li").attr("data-name"));
		getMater();
		windowClose();
	})
	if (typeof(window.localStorage.ui) != "undefined") {
		window.Invitation = window.localStorage.ui;
		getMater();
	} else {
		window.Invitation = "";
	}
	var closeFlag = false;
	$(document).on("tap", "#yard", function(event) {
		event.preventDefault();
		$("html").attr("id", "full-page");
		$("#shade").show();
		closeFlag = true;
		$("#shade-code").focus();
	})

	var changes = false;
	$(document).on("tap", "#shade-ok", function(event) {

		var value = document.getElementById("shade-code").value;

		if (value != "" && value != Invitation) {
			changes = true;
			Invitation = value;
			check = true;
			flag_check = true;
			set();
			document.getElementById("shade-code").value = "";
		}
		$("html").attr("id", "");
		$("#shade").hide();
		closeFlag = false;
		$("#shade-code").blur();

		Invitation = document.getElementById("yard").value;
		getMater();


	})
	
	$(document).on("tap",".reg-tip-close",function(event){
		event.preventDefault();
		$("#shade").hide();
	})

	// $(document).on("tap", ".main", function(event) {
	// 	event.preventDefault();
	// 	if (!closeFlag) return;
	// 	$("#shade").hide();
	// 	closeFlag = false;
	// })
	$(document).on("tap", ".change", function(event) {

		//弹出输入邀请码框
		event.preventDefault();
		$("html").attr("id", "full-page");
		$("#shade").show();
		closeFlag = true;
		$("#shade-code").focus();


		//		原型确定邀请码按钮
		//		if ( !changes ){
		//			windowAlert("点击邀请码处重新输入后才能更改");
		//			return
		//		}
		//		Invitation = document.getElementById("yard").value;
		//		getMater();
	})
	window.check = false;
	var loading = false;
	$(document).on("submit", ".form", function(event) {
			event.preventDefault();
			if (loading) {
				return;
			}
			if (Invitation == "" || !check) {
				windowAlert("请根据师傅邀请码选择师傅");
				return
			}
			loading = true;
			var postData = {};
			postData.toinvitecode = Invitation;
			$.ajax({
				type: "post",
				url: baseapi_url + "/h5/web/user/baishi",
				//		contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: postData,
				beforeSend: function() {
					$(".submit").find("input").val("正在拜师...");
				},
				success: function(data) {
					if (!detectionResult(data, 1)) {
						loading = false;
						$(".submit").find("input").val("确认完成");
						return;
					}
					$(".submit").find("input").val("成功拜师");
					window.localStorage.groupid = window.groupid;
					window.localStorage.groupname = window.groupname;
					window.localStorage.groupdesc = window.groupdesc;
					window.localStorage.groupimg = window.groupimg;
					window.location.href = "introduction.html";
				},
				error: function() {
					windowAlert("网络错误，请重试");
					$(".submit").find("input").val("正在拜师...");
					loading = false;
				},
				headers: {
					"x-stbl-token": window.localStorage.accesstoken,
				}
			});
		})
		//推荐师傅
	window.page = 1;
	window.count = 5;
	window.ajaxing = false;
	$(document).on("tap", ".recommend", function(event) {
		event.preventDefault();
		window.callBack = function() {
			window.iscroll = new IScroll($("#recommend .main")[0], {
				probeType: 3,
				scrollX: false,
				freeScroll: true,
				fadeScrollbars: false,
				resizeScrollbars: false,
				shrinkScrollbars: 'clip',
				scrollbars: true,
				scrollbars: 'custom'
			});
			if (!have) {
				getRecommend();
			}
		}
		windowOpen("recommend", null, callBack);
	})
	$(document).on("tap", ".changes", function(event) {
			event.preventDefault();
			$("#recommend-list").empty().next().show();
			iscroll.refresh();
			getRecommend();
		})
		//精确查找
	window.pages = 1;
	window.counts = 5;
	$(document).on("tap", ".search", function(event) {
		event.preventDefault();
		window.callBack = function() {
			window.iscroll = new IScroll($("#find .main")[0], {
				probeType: 3,
				scrollX: false,
				freeScroll: true,
				fadeScrollbars: false,
				resizeScrollbars: false,
				shrinkScrollbars: 'clip',
				scrollbars: true,
				scrollbars: 'custom'
			});
			setTimeout(function() {
				$("#keyword").click();
				document.getElementById("keyword").focus();
			}, 100)
		}
		windowOpen("find", null, callBack);
	})
	$(document).on("submit", ".header", function(event) {
			event.preventDefault();
			var value = document.getElementById("keyword").value;
			if (value == "") {
				windowAlert("先输入他的昵称吧");
				return
			}
			$("#keyword").blur();
			getFind();
		})
		//大图预览
	window.onload = function() {
		var flashContent = "<div class='fullFlash'><ul></ul></div>";
		$("body").append(flashContent);
		$(".fullFlash").addClass("swiper-container").append("<div class='num'></div>");
		$(".fullFlash>ul:first").addClass("swiper-wrapper");
		$(".fullFlash").on("tap", function() {
			$(this).transition({
				y: 50,
				opacity: 0,
				complete: function() {
					$(this).hide();
				}
			});
		})
		var flashload = false;
		$(document).on("tap", ".ul-he li", function(evnet) {
			event.preventDefault();
			var index = $(this).index();
			var html = "";
			var li = $(this).parent().children();
			for (var i = 0; i < li.length; i++) {
				html += "<li class='swiper-slide'><img src='" + li[i].getAttribute("data-big") + "' /></li>";
			}
			$(".fullFlash>ul:first").html(html);
			if (typeof(flash) != "undefined") {
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
			setTimeout(function() {
				flash.swipeTo(index, 100);
			}, 100)
		})
	}
})