$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var music = new Audio();
			music.src = "js/message.mp3";
			music.load();
			window.imAuth = $.Deferred();//通讯授权
			//获取即时通讯授权
				$.ajax({
					type:"get",
					url:"api/IM/GetRongInfo",
					dataType:"json",
					success:function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						window.app_key = data.result.app_key;
						window.access_token = data.result.access_token;
						window.imAuth.resolve();
					},
					error:function(){
						windowAlert("网络错误，刷新重试",0);
						window.imAuth.reject();
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//获取通讯授权后执行
				$.when(window.imAuth).done(function(){
					var messageId = "";//最末一条消息id
					var loading = false;
					var havemessage = true;
					//var title = "";
					//前置事件
						if ( $(".send-button").length ){
							$(document).on("keyup","#textarea",function(event){
								event.preventDefault();
								this.style.height = ""
								var height = this.scrollHeight;
								if ( height >= 65 ){
									height = 65;
								}
								this.style.height = height + 'px';
								$(this).parent().height(height);
								$(".send-form").height(height+12);
							})
						}
						window.listScroll = new IScroll("#scroll", { probeType: 3, scrollX: false, freeScroll: true,fadeScrollbars:false,resizeScrollbars:false,shrinkScrollbars:'clip',scrollbars: true, scrollbars: 'custom' });
						listScroll.on("scroll", onscrolls);
						listScroll.on("scrollEnd", onscrolls);
						function onscrolls()
						{
							if ( havemessage ){
								if ( this.y >= 20 )
								{
									$("#loading").html("松开后加载");
									window.touch = true;
								}
								else{
									$("#loading").html("下拉加载更多");
									window.touch = false;
								}
							}
						}
						$(document).on("touchend mouseup","#scroll",function(){
							if ( window.touch ){
								messageId = parseInt($("#list").find("li:first").attr("data-id"));
								window.listScroll.disable();
								$("#loading").html("正在加载...");
								ajax();
								window.touch = false;
							}
						})
					//提取新消息
						function getNewMessage(){
							var postdata = {};
							postdata.shopId = "";
							$.ajax({
								type:"get",
								url:"api/IM/GetNewMessage",
								dataType:"json",
								data:postdata,
								success:function(data){
									if ( !detectionResult(data,1) ){
										return;
									}
									//插入新消息
									var html = "";
									for (var i=0;i<data.result.data.length;i++ ){
										if ( i == 0 ){
											html += "<li data-id='"+data.result.data[i].messageId+"'><div class='msg-time'><time>"+data.result.data[i].time.split(" ")[1]+"</time></div><div class='ul-msg-img'><img src='images/data/3.png' /></div><div class='text'>"+getText(data.result.data[i].content)+"</div></li>";
										}
										else{
											html += "<li data-id='"+data.result.data[i].messageId+"'><div class='ul-msg-img'><img src='images/data/3.png' /></div><div class='text'>"+getText(data.result.data[i].content)+"</div></li>";
										}
									}
									$("#list").append(html);
									window.listScroll.refresh();
									window.listScroll.scrollTo(0,window.listScroll.maxScrollY);
									music.play();
								},
								headers:{
									"x-moutai-token": window.token,
								}
							});
						}
					//获取历史消息
						var first = true;
						function ajax(){
							if ( loading ){
								return;
							}
							loading = true;
							var postdata = {};
							postdata.length = 10;
							postdata.lastMessageId = messageId;
							postdata.shopId = "";
							$.ajax({
								type:"get",
								url:"api/IM/GetHistoryMessage",
								dataType:"json",
								data:postdata,
								beforeSend:function(){
									$("#loading").html("正在加载...");
								},
								success:function(data){
									//绑定监听
									if ( first ){
										new MessageClient(app_key, access_token).init(function () {
											getNewMessage();
										});
										first = false;
									}
									if ( !detectionResult(data,1) ){
										loading = false;
										return;
									}
									//获取到聊天记录设置人名
//									if ( title == "" ){
//										title = $(".title").html() + "-" + data.result.targetusername;
//										$(".title").html(title);
//									}
									//无任何聊天记录
									if ( data.result.data.length == 0 ){
										$("#loading").html("暂无历史聊天记录");
										havemessage = false;
										return;
									}
									if ( data.result.havemessage == 0 ){
										$("#loading").hide();
										havemessage = false;
									}
									else{
										$("#loading").html("下拉读取更多");
									}
									var datas = data.result.data;
									var html = "";
									for (var i=0;i<datas.length;i++){
										if ( datas[i].self == 1 ){
											html += "<li class='self' data-id='"+datas[i].messageId+"'><div class='msg-time'><time>"+datas[i].time+"</time></div><div class='ul-msg-img'><img src='images/data/3.png' /></div><div class='text'>"+getText(datas[i].content)+"</div></li>";
										}
										else{
											html += "<li data-id='"+datas[i].messageId+"'><div class='msg-time'><time>"+datas[i].time+"</time></div><div class='ul-msg-img'><img src='images/data/3.png' /></div><div class='text'>"+getText(datas[i].content)+"</div></li>";
										}
									}
									var height1 = $("#scroll").find("div:first").outerHeight(true);
									$("#list").prepend(html);
									var height2 = $("#scroll").find("div:first").outerHeight(true);
									window.listScroll.refresh();
									if ( messageId == "" ){
										window.listScroll.scrollTo(0,window.listScroll.maxScrollY);
									}
									else{
										if ( !havemessage ){
											window.listScroll.scrollTo(0,height1-height2 + 45);
										}
										else{
											window.listScroll.scrollTo(0,height1-height2);
										}
										window.listScroll.enable();
									}
									$("#list").find("li:first").addClass("first");
									loading = false;
								},
								error:function(){
									$("#loading").html("网络错误，请重试...");
									loading = false;
									window.listScroll.enable();
								},
								headers:{
									"x-moutai-token": window.token,
								}
							});
						}
						ajax();
					//发布消息
						var sending = false;
						$(document).on("submit",".send-form",function(event){
							event.preventDefault();
							if ( sending ){
								return;
							}
							var text = document.getElementById("textarea").value;
							if ( text == "" ){
								return;
							}
							sending = true;
							var postdata = {};
							postdata.message = getValue(text);
							postdata.shopId = "";
							$.ajax({
								type:"post",
								url:"api/IM/SendMessage",
								dataType:"json",
								data:postdata,
								success:function(data){
									sending = false;
									if ( !detectionResult(data,1) ){
										return;
									}
									//textarea复原
									$("#textarea").val("");
									document.getElementById("textarea").style.height = ""
									var height = document.getElementById("textarea").scrollHeight;
									if ( height >= 65 ){
										height = 65;
									}
									document.getElementById("textarea").style.height = height + 'px';
									$("#textarea").parent().height(height);
									$(".send-form").height(height+12);
									//插入新消息
									$("#list").append("<li class='self' data-id='"+data.result.messageId+"'><div class='msg-time'><time>"+data.result.time.split(" ")[1]+"</time></div><div class='ul-msg-img'><img src='images/data/3.png' /></div><div class='text'>"+getText(data.result.content)+"</div></li>");
									window.listScroll.refresh();
									window.listScroll.scrollTo(0,window.listScroll.maxScrollY);
								},
								error:function(){
									windowAlert("网络错误，刷新重试");
									sending = false;
								},
								headers:{
									"x-moutai-token": window.token,
								}
							});
						})
					//表情
						$(".biaoqing").addClass("swiper-container").append("<div class='num'></div>");
						$('.biaoqing').swiper({
							pagination: '.num',
							mode:'horizontal',
						})
						var open = false;
						$(document).on("tap",".bq",function(event){
							event.preventDefault();
							if ( !open ){
								$("#textarea").blur();
								$(".biaoqing").animate({"height":175},300,function(){
									open = true;
								});
							}
							else{
								$(".biaoqing").animate({"height":0},300,function(){
									open = false;
								});
							}
						})
						$(document).on("focus","#textarea",function(event){
							event.preventDefault();
							if ( open ){
								$(".biaoqing").animate({"height":0},300,function(){
									open = false;
								});
							}
						})
						$(document).on("tap",".ul-biaoqing li",function(event){
							event.preventDefault();
							var id = this.getAttribute("data-id");
							var value = document.getElementById("textarea").value;
							value += "[表情"+ id +"]";
							document.getElementById("textarea").value = value;
						})
						$(document).on("keyup","#textarea",function(event){
							event.preventDefault();
							var value = document.getElementById("textarea").value;
							if ( value[value.length-5] == "[" && value[value.length-4] == "表" && value[value.length-3] == "情" ){
								value = value.substring(0,value.length-5);
							}
							document.getElementById("textarea").value = value;
						})
						function getValue(value){
							var newValue = "";
							value = value.split("[表情");
							for ( var i=0;i<value.length;i++ ){
								if ( value[i] != "" && value[i][2] == "]" && i != 0 ){
									var k = value[i].substring(0,2);
									var kk =  value[i].substring(3,value[i].length);
									newValue += "*#emo_"+ k +"#*";
								}
								else{
									var k = value[i];
									var kk = "";
									newValue += k;
								}
								newValue += kk;
							}
							return newValue;
						}
						function getText(value){
							var newValue = "";
							value = value.split("*#emo_");
							for ( var i=0;i<value.length;i++ ){
								if ( value[i] != "" && value[i][2] == "#" && value[i][3] == "*" && i != 0 ){
									var k = value[i].substring(0,2);
									var kk =  value[i].substring(4,value[i].length);
									newValue += "<img src='images/emo/emo_"+ k +".gif'>";
								}
								else{
									var k = value[i];
									var kk = "";
									newValue += k;
								}
								newValue += kk;
							}
							return newValue;
						}
				})
		})
})
