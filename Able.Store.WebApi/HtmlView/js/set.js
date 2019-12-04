$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			//读取头像及姓名
				$.ajax({
					type: "get",
			        url: "api/CustomerUser/GetUserInfo",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						$(".name").html(data.result.name);
						$(".photo").prepend("<img src='"+data.result.headPhoto+"' />");
						$("#name").val(data.result.name);
					},
					error: function(){
						windowAlert("网络错误,刷新重试",0)
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//读取详细资料
				$.ajax({
					type: "get",
			        url: "api/CustomerUser/GetAccountInfo",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						$("#IDcard").val(data.result.cardsId);
						if ( data.result.cardsId != "" ){
							$("#IDcard").attr("disabled","disabled").after("<span>不可修改</span>");
						}
						$("#tel").val(data.result.tel);
						$("#weixin").val(data.result.weixin);
						window.weixin = data.result.weixin;
						if ( data.result.sex == 1 ){
							$("#sex").find("li").eq(0).addClass("on");
						}
						else{
							$("#sex").find("li").eq(1).addClass("on");
						}
						$(".none").show();
					},
					error: function(){
						windowAlert("网络错误,刷新重试",0)
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//修改资料
				var loading = false;
				$(document).on("submit",".info-save",function(event){
					event.preventDefault();
					if ( loading ){
						return;
					}
					var name = document.getElementById("name").value;
					var sex = $("#sex").find(".on").index() + 1;
					var cardsId = document.getElementById("IDcard").value;
					var tel = document.getElementById("tel").value;
					if ( name == "" ){
						windowAlert("请填写姓名");
						return;
					}
					if ( !$("#sex").find("li.on").length ){
						windowAlert("请选择性别");
						return;
					}
					if ( !checkNumber(cardsId) ){
						windowAlert("请填写正确的身份证号码");
						return;
					}
					if ( !checkTel(tel) ){
						windowAlert("请填写正确的手机号码");
						return;
					}
					loading = true;
					var postData = {};
					postData.name = name;
					postData.sex = sex;
					postData.cardsId = cardsId;
					postData.tel = tel;
					postData.weixin = window.weixin;
					windowAlert("正在修改",0);
					$.ajax({
						type: "post",
				        url: "api/CustomerUser/EditUser",
						dataType: "json",
						data:JSON.stringify(postData),
						contentType: "application/json; charset=utf-8",
						success: function(data){
							if ( !detectionResult(data,1) ){
								loading = false;
								return;
							}
							windowAlertClose("修改成功");
							$(".name").html(name);
							loading = false;
						},
						error: function(){
							windowAlertClose("网络错误,刷新重试")
							loading = false;
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
				})
		    //图片缩放定位
		    	function imgPosition()
				{
				    $("#img img").each(function(){
				    	$(this).removeAttr("style");
				        var imgwidth = parseInt($(this).width());
				        var imgheight = parseInt($(this).height());
				        width = imgwidth;
				        height = imgheight;
				        var parent = $(this).parent();
				        var parentwidth = parseInt(parent.width());
				        var parentheight = parseInt(parent.height());
				        if ( imgwidth <= parentwidth) 
				        {
				            if ( parentwidth / imgwidth * imgheight >= parentheight ) {
				                imgheight = imgheight / (imgwidth / parentwidth);
				                imgwidth = parentwidth;
				            }
				            else
				            {
				                imgwidth = imgwidth / (imgheight / parentheight);
				                imgheight = parentheight;
				            }
				        }
				        else 
				        {
				            if ( parentwidth / imgwidth * imgheight <= parentheight ) {
				                imgwidth = imgwidth / (imgheight / parentheight);
				                imgheight = parentheight;
				            }
				            else {
				                imgheight = imgheight / (imgwidth / parentwidth);
				                imgwidth = parentwidth;
				            }
				        }
				        $(this).css({"width":imgwidth,"height":imgheight,"marginLeft":(parentwidth - imgwidth) / 2,"marginTop":(parentheight - imgheight) / 2,"visibility":"visible"});
				        scaleWidth = parseInt(imgwidth);
				        scaleHeight = parseInt(imgheight);
				        newleft = parseInt((parentwidth - imgwidth) / 2);
				        newtop = parseInt((parentheight - imgheight) / 2);
				        scale = width/scaleWidth;
				        imgnwidth = imgwidth;
				        imgnheight = imgheight;
				    })
				}
			if ( $(".upload").length ){
			    //图片选择
		    	var imgData = "";
		    	var imgData120 = "";
		    	var imgData640 = "";
		    	function file1(){
		 			var div = $(".upload").parent();
		 			div.find("input").remove();
		 			div.append("<input type='file' capture='camera' accept='image/*' class='upload' />");
				    $(".upload").makeThumb({
				        width: 640,
				        height: 640,
				        success: function(dataURL, tSize, file, sSize, fEvt) {
				 			imgData = dataURL;
				 			var imgDiv = $("#img");
				 			imgDiv.empty().append("<img />");
				 			imgDiv.find("img").attr("src",dataURL).load(function(){
					 			var imgDiv = $("#img");
					 			imgDiv.empty().append("<img />");
					 			imgDiv.find("img").attr("src",imgData).load(function(){
									document.getElementById("photo").style.display = "block";
									$(".photo-photo").height(document.documentElement.clientWidth);
									imgPosition();
									windowAlertClose("图片处理完成");
					 			});
				 			});
		    				file1();
				        }
				    });
		    	}
		    	file1();
			    //图片操作
					var oneX1,oneX2,oneY1,oneY2,twoX1,twoY1,twoX2,twoY2,imgLeft,imgTop,newleft,newtop,a,b,c,imgnwidth,imgnheight,width,height;		
			        var container = document.getElementById('move'),
			        shouts = {}
			        shouts['hold'] = 'You are holding me.';
			        shouts['tap'] = 'You just tapped me.';
			        shouts['doubletap'] = 'You just tapped me. Twice.';
			        shouts['transformstart'] = 'You started transforming me.';
			        shouts['transform'] = 'You are transforming me.';
			        shouts['transformend'] = 'You just transformed me.';
			        shouts['dragstart'] = 'You started dragging me.';
			        shouts['drag'] = 'You are dragging me.';
			        shouts['dragend'] = 'You just dragged me.';
			        shouts['swipe'] = 'You just swiped me.';
			
			        var hammer = new Hammer(container, {
			            tap_max_interval: 700 // seems to bee needed for IE8
			        });
			        
			        function shout(e){
			            switch ( e.type )
			            {
			            	case "dragstart":
								oneX1 = e.touches[0].x;
								oneY1 = e.touches[0].y;
								var imgTag = $("#img img");
								imgLeft = parseInt(imgTag.css("marginLeft"));
								imgTop = parseInt(imgTag.css("marginTop"));
								imgnwidth = parseInt(imgTag.width());
								imgnheight = parseInt(imgTag.height());
			            		break;
			            	case "drag":
								oneX2 = e.touches[0].x;
								oneY2 = e.touches[0].y;
								var imgTag = $("#img img");
								newleft = imgLeft+(oneX2-oneX1);
								newtop = imgTop+(oneY2-oneY1);
								if ( newleft >= 0 )
								{
									newleft = 0;
								}
								if ( newleft <= document.documentElement.clientWidth - 0 - imgnwidth )
								{
									newleft = document.documentElement.clientWidth - 0 - imgnwidth;
								}
								if ( newtop >= 0 )
								{
									newtop = 0;
								}
								if ( newtop <= document.documentElement.clientWidth - 0 - imgnheight )
								{
									newtop = document.documentElement.clientWidth - 0 - imgnheight;
								}
								imgTag.css({marginLeft:newleft,marginTop:newtop});
			            		break;
			            	case "transformstart":
								oneX1 = e.touches[0].x;
								oneY1 = e.touches[0].y;
								oneX2 = e.touches[0].x;
								oneY2 = e.touches[0].y;
								twoX1 = e.touches[1].x;
								twoY1 = e.touches[1].y;
								twoX2 = e.touches[1].x;
								twoY2 = e.touches[1].y;
								a = Math.pow((Math.pow((twoX1-oneX1),2)+Math.pow((twoY1-oneY1),2)),0.5);//缩放前距离
								xc = (oneX1 + twoX1)/2-$("#move").offset().left;
								yc = (oneY1 + twoY1)/2-$("#move").offset().top;
								var imgTag = $("#img img");
								scaleWidth = imgTag.width();
								scaleHeight = imgTag.height();
								imgLeft = parseInt(imgTag.css("marginLeft"));
								imgTop = parseInt(imgTag.css("marginTop"));
								imgTag[0].style.transformOrigin = parseInt(xc - (imgTag.offset().left-$("#move").offset().left)) + "px "+ parseInt(yc - (imgTag.offset().top-$("#move").offset().top)) +"px";
								imgTag[0].style.webkitTransformOrigin = parseInt(xc - (imgTag.offset().left-$("#move").offset().left)) + "px "+ parseInt(yc - (imgTag.offset().top-$("#move").offset().top)) +"px";
			            		break;
			            	case "transform":
								var imgTag = $("#img img");
								oneX2 = e.touches[0].x;
								oneY2 = e.touches[0].y;
								twoX2 = e.touches[1].x;
								twoY2 = e.touches[1].y;
								b = Math.pow((Math.pow((twoX2-oneX2),2)+Math.pow((twoY2-oneY2),2)),0.5);//变化中距离
								c = (b/a);//当前缩放后的比例
								imgTag[0].style.transform = "scale("+c+")";
								imgTag[0].style.webkitTransform = "scale("+c+")";
			            		break;
			            	case "transformend":
								var imgTag = $("#img img");
								scaleWidth = scaleWidth*c;
								scaleHeight = scaleHeight*c;
								newleft = imgTag.offset().left-$("#move").offset().left;
								newtop = imgTag.offset().top-$("#move").offset().top;
								imgTag.css({"width":scaleWidth,"height":scaleHeight});
								imgTag[0].style.transform = "scale(1)";
								imgTag[0].style.webkitTransform = "scale(1)";
								imgTag.css({"marginLeft":newleft,"marginTop":newtop});
						        scale = width/scaleWidth;
								imgnwidth = parseInt(imgTag.width());
								imgnheight = parseInt(imgTag.height());
			            		break;
			            }
			        }
			        var type;
			        for(type in shouts) {
			            hammer['on'+ type] = shout;
			        }
			        $("#move").on("touchstart",function(event){
			        	event.preventDefault();
			        })
			        var r1=false,r2=false;
					$(document).on("tap",".photo-submit",function(event){
						event.preventDefault();
						if ( loading ){
							return;
						}
						if ( newleft > 0 || newleft < document.documentElement.clientWidth - 0 - imgnwidth || newtop > 0 || newtop < document.documentElement.clientWidth - 0 - imgnheight )
						{
							windowAlert("图片必须布满整个白色线框！");
						}
						else{
							windowAlert("正在上传...",0);
							loading = true;
							r1 = false;
							r2 = false;
							var data = {};
							var canvas = document.createElement("canvas");
							var ctx = canvas.getContext("2d");
							var img = new Image();
							img.src = imgData;
							img.onload = function(){
								newleft -= 0;
								newtop -= 0;
								var w = parseInt(( 0 - newleft + document.documentElement.clientWidth - 0 - 0)*scale) - parseInt(( 0 - newleft )*scale);
								var h = parseInt(( 0 - newtop + document.documentElement.clientWidth - 0 - 0)*scale) - parseInt(( 0 - newtop )*scale);
								canvas.width = w;
								canvas.height = h;
								ctx.drawImage(img,parseInt(newleft*scale),parseInt(newtop*scale),width,height);
								imgData = canvas.toDataURL();
								ctx.clearRect(0,0,w,h);
								var img1 = new Image();
								img1.src = imgData;
								img1.onload = function(){
									canvas.width = 120;
									canvas.height = 120;
									ctx.drawImage(img1,0,0,w,h,0,0,canvas.width,canvas.height);
									imgData120 = canvas.toDataURL();
									ctx.clearRect(0,0,canvas.width,canvas.height);
									data.imgData120 = imgData120;//图片
									r1 = true;
									if ( r1 && r2 ){
										ajaxForm(data);
									}
								}
								var img2 = new Image();
								img2.src = imgData;
								img2.onload = function(){
									canvas.width = 640;
									canvas.height = 640;
									ctx.drawImage(img2,0,0,w,h,0,0,canvas.width,canvas.height);
									imgData640 = canvas.toDataURL();
									ctx.clearRect(0,0,canvas.width,canvas.height);
									data.imgData640 = imgData640;//图片
									r2 = true;
									if ( r1 && r2 ){
										ajaxForm(data);
									}
								}
							}
						}
					})
					function ajaxForm(datas){
						datas.imgData640 = datas.imgData640.substring(22,datas.imgData640.length)
						var postData = {};
						postData.base64 = datas.imgData640;
						$.ajax({
							type: "post",
					        url: "http://moutai-img.stbl.cc/mall/userhead/upload",
							dataType: "json",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							success: function(data){
								if ( !detectionResult(data,0) ){
									return;
								}
								var img = data.result.image_domain + "120x120/" + data.result.img;
								$(".photo").find("img:first").attr("src",img);
								windowAlertClose("上传成功!");
								document.getElementById("photo").style.display = "none";
								loading = false;
							},
							error: function(){
								windowAlertClose("网络错误,刷新重试!");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						});
					}
					$(document).on("tap",".photo-false",function(event){
						event.preventDefault();
						$("#photo").hide();
					})
			}
		})
})
