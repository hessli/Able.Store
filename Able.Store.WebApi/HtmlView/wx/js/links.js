/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function(){
	var userid = $.query.get("userid");
	if ( userid == "" ){
		$("html,body").css({"visibility":"hidden","height":"100%","overflow":"hidden"});
		windowAlert("用户不存在",0);
		return
	}
	//获取动态
		var page = 1;
		function getLinks(){
			var postData = {};
			postData.objuserid = userid;
			postData.page = page;
			//postData.count = 10;
			$.ajax({
				type: "post",
		        url: baseapi_url + "/h5/web/user/links/show",
	//			contentType: "application/json; charset=utf-8",
				data:postData,
				dataType: "json",
				success: function(data){
					if ( !detectionResult(data,0) ){
						return;
					}
					var user = data.result.user;
					var sex = user.gender;
					if (sex == 0) {
						sex = "♂";
					} else if ( sex == 1 ) {
						sex = "♀";
					}
					else{
						sex = "";
					}
					var html = "<div class='user-box info'><div class='user-photo open' data-id='"+ user.userid +"'><img src='"+ user.imgurl +"' /></div><h3>"+ user.nickname +"</h3><div class='xb'>"+ sex +" "+ user.age +"</div><div class='areas'>"+ user.cityname +"</div><div class='info-tit regs'>精彩链接</div><div class='link-number reg'>"+ data.result.linkscount +"</div></div>";
					$(".main").prepend(html);
					var sex = user.gender;
					if (sex == 0) {
						$(".main .xb").addClass("man");
					}
					if (sex == -1) {
						$(".main .xb").addClass("yao");
					}
					html = "";
					var links = data.result.links;
					for ( var i=0;i<links.length;i++ ){
						html += "<li class='to-link' data-link='"+ data.result.links[i].linkurl +"''><a><div class='link-detail'><div class='link-img'><img src='"+ links[i].picminurl +"' /></div><h3>"+ links[i].linktitle +"</h3></div></a></li>";
					}
					$(".ul-link").append(html).next().hide();
					$(".ul-link").find(".link-img").each((function(){
						var img = new Image();
						var imgs = $(this).find("img");
						img.src = imgs.attr("src");
						if ( img.complete ){
							small(imgs);
						}
						else{
							img.onload = function(){
								small(imgs);
							}
						}
					}))
				},
				error:function(){
					windowAlert("网络错误，请重试");
				},
				headers:{
					"x-stbl-token": window.token,
				}
			});
		}
		getLinks();
	//日期处理
		function setTimes(m){
			m = m*1000;
			var d = new Date();
			var ds = d.setTime(m);
			var nd = new Date(ds);
			return nd.getFullYear() + "-" + (nd.getMonth()+1) + "-" + nd.getDate() + " " + nd.getHours()  + ":" + nd.getMinutes() + ":" + nd.getSeconds()
		}
	//注册提示
		$(document).on("tap",".regs",function(event){
			event.preventDefault();
			$("#reg-tip").show();
		})
		$(document).on("tap",".reg-tip-close",function(event){
			event.preventDefault();
			$("#reg-tip").hide();
		})
	//进入个人部落
		$(document).on("tap",".open",function(event){
			event.preventDefault();
			var link = this.getAttribute("data-id");
			window.location.href = "tribe.html?userid=" + link;
		})
	//查看链接详细
		$(document).on("tap",".to-link",function(event){
			event.preventDefault();
			var link = $(this).attr("data-link");
			window.location.href = link;
		})
})