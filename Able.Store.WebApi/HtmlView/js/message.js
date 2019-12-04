$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			//读取客服消息
				$.ajax({
					type: "get",
			        url: "api/IM/GetLatestMessage",
					dataType: "json",
					data:{"shopid":window.shopid},
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						var li = $(".ul-message").find("li").eq(0);
						if ( typeof(data.result.number) == "undefined" ){
							li.html("<a href='message-service-list.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服中心</h2><p>暂无消息</p></a>");
							return;
						}
						if ( data.result.number == 0 ){
							if ( data.result.date == "" ){
								li.html("<a href='message-service-list.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服中心</h2><p>暂无消息</p></a>");
							}
							else{
								li.html("<a href='message-service-list.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服中心</h2><p>"+data.result.last+"</p><time>"+data.result.date+"</time></a>");
							}
						}
						else{
							if ( data.result.number > 99 ){
								li.html("<a href='message-service-list.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服中心</h2><p>"+data.result.last+"</p><time>"+data.result.date+"</time><span class='new'>N</span></a>");
							}
							else{
								li.html("<a href='message-service-list.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服中心</h2><p>"+data.result.last+"</p><time>"+data.result.date+"</time><span class='new'>"+data.result.number+"</span></a>");
							}
						}
					},
					error: function(){
						var li = $(".ul-message").find("li").eq(0);
						li.find("p").html("网络异常");
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//读取订单及系统消息
				$.ajax({
					type: "get",
			        url: "api/SystemMsg/GetMsgSummarize",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						var datas = data.result;
						for ( var i=0;i<datas.length;i++ ){
							var li = $(".ul-message").find("li").eq(datas[i].type);
							if ( datas[i].last == "" ){
								li.find("p").html("暂无消息");
							}
							else{
								li.find("p").html(datas[i].last);
								li.find("time").html(datas[i].date);
								if ( datas[i].number > 0 ){
									if ( datas[i].number > 99 ){
										li.find("a").append("<span class='new'>N</span>");
									}
									else{
										li.find("a").append("<span class='new'>"+datas[i].number+"</span>");
									}
								}
							}
						}
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
		})
})
