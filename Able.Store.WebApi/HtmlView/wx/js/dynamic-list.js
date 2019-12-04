/*
 * 动态列表脚本,基于jquery-2.1.1脚本库
 */
$(function() {
	var objuserid = $.query.get("objuserid"); //1458395695414723
	var postData = {};
	postData.objuserid = objuserid;
	postData.lastid = "";
	postData.count = "";

	//调出APP		
	getDataList(postData);

	//获取动态列表
	function getDataList(postData){
		$.ajax({
			type: "get",
			url: baseapi_url + "/h5/web/statuses/myshow",
			data: postData,
			dataType: "json",
			success: function(data) {
				if (!detectionResult(data, 0)) {
					return;
				}
				//生成列表
				var html = '';
				if(!data.result){
					html = "<div><span>暂无动态</span></div>";
					$(".product-tug").html(html);
					return;
				}

				for (var i = 0; i < data.result.length; i++) {
					var zhaopian = data.result[i].statusespic;
					var pichtml = '';
					if(data.result[i].statusespic){
						if(data.result[i].statusestype == 0){
							for(var j=0; j<zhaopian.pics.length; j++){
								pichtml += "<li><div><img src='"+ zhaopian.thumbpic + zhaopian.pics[j] +"' /></div></li>";
							}
						}
					}

					var user = data.result[i].user;
					var sex = user.gender;
					var ahtml = '<li class="product_s" data-id="' + data.result[i].statusesid + '"><div class="users users_s"><div class="user-photo open" data-id="' + user.userid + '"><img src="' + user.imgurl + '"></div><h3 class="user-name">' + user.nickname + '</h3><div class="clear"></div>';
					if (sex == 0) {
						sex = "♂";
					} else if ( sex == 1 ) {
						sex = "♀";
					}
					else{
						sex = "";
					}
					if (user.age == 0 || user.age == null) {
						var age = "0";
					} else {
						var age = user.age;
					}
					//长短动态判断
					var chtml = "";
					if(data.result[i].statusestype == 0){ //短文章
						chtml = '<div class="product_nei"><div class="say"><span>' + data.result[i].content + '</span></div>';
					}else{  //长文章
						chtml = '<div class="product_nei"><div class="card-link card-text say"><a class="card-link-a"><div class="card-link-img"><img src="'+data.result[i].statusespic.thumbpic+data.result[i].statusespic.pics[0]+'"></div><div class="card-link-p"><h3>'+data.result[i].title+'</h3><p>'+data.result[i].content+'</p></div></a></div>';
					}
//					var bhtml = '<div class="xb">' + sex + " " + age + '</div><div class="pub-time">' + setTimes(data.result[i].createtime) + '</div><div class="report regs report_t"></div><div class="clear"></div>'+ chtml +'</div>';
					
					var sexs = user.gender;
					if (sexs == 0) {
						var bhtml = '<div class="xb man">' + sex + " " + age + '</div><div class="pub-time">' + setTimes(data.result[i].createtime) + '</div><div class="report regs report_t"></div><div class="clear"></div>'+ chtml +'</div>';
					} else if ( sexs == 1 ) {
						var bhtml = '<div class="xb">' + sex + " " + age + '</div><div class="pub-time">' + setTimes(data.result[i].createtime) + '</div><div class="report regs report_t"></div><div class="clear"></div>'+ chtml +'</div>';
					}
					else{
						var bhtml = '<div class="xb yao">' + sex + " " + age + '</div><div class="pub-time">' + setTimes(data.result[i].createtime) + '</div><div class="report regs report_t"></div><div class="clear"></div>'+ chtml +'</div>';
					}
					
					var dhtml = '';
					if(data.result[i].statusespic){
						if(data.result[i].statusestype == 0){						
							dhtml = '<ul class="ul-photo product_text photo-text">' + pichtml + '</ul>';						
							if(data.result[i].links && data.result[i].links.linktype == 5){
								dhtml = '';
							}
						}
					}
					
					var ehtml = '';
					if (data.result[i].links) {
						var link = data.result[i].links;
						switch (link.linktype) {
							case 1: //名片
								var sex = link.userinfo.gender;
								if (sex == 0) {
									sex = "♂";
								} else if ( sex == 1 ) {
									sex = "♀";
								}
								else{
									sex = "";
								}
								if (link.userinfo.age == 0 || link.userinfo.age == null) {
									var age = "0";
								}
//								ehtml = '<div class="card-link card-text"><a data-id="' + link.linkid + '"><div class="card-img card_img"><img src="' + link.userinfo.imgurl + '"></div><h3 class="user-name">' + link.userinfo.nickname + '</h3><div class="clear"></div><div class="xb">' + sex + ' ' + age + '</div><span class="areas">' + link.userinfo.cityname + '</span></a></div>';
								
								var sexs = link.userinfo.gender;
								if (sexs == 0) {
									ehtml = '<div class="card-link card-text"><a data-id="' + link.linkid + '"><div class="card-img card_img"><img src="' + link.userinfo.imgurl + '"></div><h3 class="user-name">' + link.userinfo.nickname + '</h3><div class="clear"></div><div class="xb">' + sex + ' ' + age + '</div><span class="areas">' + link.userinfo.cityname + '</span></a></div>';
								} else if ( sexs == 1 ) {
									ehtml = '<div class="card-link card-text"><a data-id="' + link.linkid + '"><div class="card-img card_img"><img src="' + link.userinfo.imgurl + '"></div><h3 class="user-name">' + link.userinfo.nickname + '</h3><div class="clear"></div><div class="xb">' + sex + ' ' + age + '</div><span class="areas">' + link.userinfo.cityname + '</span></a></div>';
								}
								else{
									ehtml = '<div class="card-link card-text"><a data-id="' + link.linkid + '"><div class="card-img card_img"><img src="' + link.userinfo.imgurl + '"></div><h3 class="user-name">' + link.userinfo.nickname + '</h3><div class="clear"></div><div class="xb">' + sex + ' ' + age + '</div><span class="areas">' + link.userinfo.cityname + '</span></a></div>';
								}
								
								break;
							case 3: //动态
								var defaultImg = "";
								if(link.statusesinfo.statusespic.pics.length > 0){
									defaultImg = link.statusesinfo.statusespic.originalpic + link.statusesinfo.statusespic.defaultpic;
								}else{
									defaultImg = link.statusesinfo.defaultimg;
								}
								ehtml = '<div class="card-link card-text"><a data-id="'+link.linkid+'"><div class="card-img card_img"><img src="'+defaultImg+'"></div><h3>'+link.statusesinfo.content+'</h3></a></div>';
								break;
							case 4: //商品
								ehtml = '<div class="card-link card_text"><a data-id="'+link.linkid+'"><div class="card-img card_img"><img src="'+link.productinfo.imgurl+'"></div><h3>'+link.productinfo.goodsname+'</h3><div class="pix">'+link.productinfo.minprice+'</div><div class="sale">销量：'+link.productinfo.salecount+'</div></a></div>';
								break;
							case 5: // 直播
								ehtml = '<div class="card-link card_text"><a data-id="'+link.linkid+'"><div class="card-img card_img"><img src="'+link.livedefaultimg+'"></div><h3>'+link.linktitle+'</h3></a></div>';
						}
					}
					var fhtml = '<div class="product_zambia"><ul class="ul-ctrl"><li class="c-1 regs">'+data.result[i].forwardcount+'</li><li class="c-2 regs">'+data.result[i].commentcount+'</li><li class="c-3 regs">'+data.result[i].praisecount+'</li><li class="c-4 regs">'+data.result[i].favorcount+'</li></ul><div class="clear"></div></div></div></li>';
					html = ahtml + bhtml + dhtml + ehtml + fhtml;
					$(".product-tug").append(html);

					if ( zhaopian.pics.length == 1 ){
						// var ww = $(".ul-photo").eq(0).width();
						// $(".ul-photo").width(ww);
						// $(".ul-photo").find("li").each((function(){
						// 	  $(this).width(ww).height(ww);
						// 	  var img = new Image();
						// 	  var imgs = $(this).find("img");
						// 	  img.src = imgs.attr("src");
						// 	  if ( img.complete ){
						// 	     small(imgs);
						// 	  }
						// 	  else{
						// 		 img.onload = function(){
						// 			small(imgs);
						// 		}
						// 	  }
						// }))

						var ww = parseInt($(".ul-photo").eq(0).width());
						$(".ul-photo").find("li").each((function(){
							$(this).width(parseInt(ww/3)-14).height(parseInt(ww/3)-14);
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
						$(".ul-photo").width(ww+10);
					}
					else if ( zhaopian.pics.length > 1 && zhaopian.pics.length < 5 ){
						var ww = parseInt($(".ul-photo").eq(0).width());
						$(".ul-photo").find("li").each((function(){
							$(this).width(parseInt(ww/2)-2).height(parseInt(ww/2)-2);
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
						$(".ul-photo").width(ww+10);
					}
					else{
						var ww = parseInt($(".ul-photo").eq(0).width());
						$(".ul-photo").find("li").each((function(){
							$(this).width(parseInt(ww/3)-14).height(parseInt(ww/3)-14);
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
						$(".ul-photo").width(ww+10);
					}

				}
				// $(".product-tug").html(html);

			},
			error: function(err) {
				windowAlert("网络错误，请重试");
			},
			headers: {
				"x-stbl-token": window.token,
			}
		});
	}	

	//注册提示
	$(document).on("tap", ".regs", function(event) {
		event.preventDefault();
		$("#reg-tip").show();
	})
	$(document).on("tap", ".reg-tip-close", function(event) {
		event.preventDefault();
		$("#reg-tip").hide();
	})
		//动态详细
	$(document).on("tap", ".product_text", function(event) {
		event.preventDefault();
		var link = $(this).parents("li").attr("data-id");
		window.location.href = "dynamic.html?statusesid=" + link;
	})
		//跳转到部落
	$(document).on("tap", ".user-photo", function(event) {
		event.preventDefault();
		window.location.href = "tribe.html?userid=" + objuserid;
	})
	$(document).on("tap", ".product_nei", function(event) {
		event.preventDefault();
		var link = $(this).parents("li").attr("data-id");
		window.location.href = "dynamic.html?statusesid=" + link;
	})


})

//日期处理
function setTimes(m) {
	var d = new Date();
	var ds = d.setTime(m * 1000);
	var nd = new Date(ds);
	return nd.getFullYear() + "-" + (nd.getMonth() + 1) + "-" + nd.getDate() + " " + nd.getHours() + ":" + nd.getMinutes() + ":" + nd.getSeconds()
}



