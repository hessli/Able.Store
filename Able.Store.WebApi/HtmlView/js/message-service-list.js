$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			//读取客服消息
				$.ajax({
					type: "get",
			        url: "api/IM/GetCustomerService",
					dataType: "json",
					data:{"shopid":window.shopid},
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						var datas = data.result[0];
						var li = $(".ul-message").find("li").eq(0);
						if ( typeof(datas.number) == "undefined" ){
							li.html("<a href='message-service.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服</h2><p>暂无消息</p></a>");
							return;
						}
						if ( datas.number == 0 ){
							if ( datas.date == "" ){
								li.html("<a href='message-service.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服</h2><p>暂无消息</p></a>");
							}
							else{
								li.html("<a href='message-service.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服</h2><p>"+getText(datas.last)+"</p><time>"+datas.date+"</time></a>");
							}
						}
						else{
							if ( datas.number > 99 ){
								li.html("<a href='message-service.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服</h2><p>"+getText(datas.last)+"</p><time>"+datas.date+"</time><span class='new'>N</span></a>");
							}
							else{
								li.html("<a href='message-service.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>客服</h2><p>"+getText(datas.last)+"</p><time>"+datas.date+"</time><span class='new'>"+datas.number+"</span></a>");
							}
						}
						var datas = data.result[1];
						var li = $(".ul-message").find("li").eq(1);
						if ( typeof(datas.number) == "undefined" ){
							li.html("<a href='message-feitian.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>飞天专属客服</h2><p>暂无消息</p></a>");
							return;
						}
						if ( datas.number == 0 ){
							if ( datas.date == "" ){
								li.html("<a href='message-feitian.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>飞天专属客服</h2><p>暂无消息</p></a>");
							}
							else{
								li.html("<a href='message-feitian.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>飞天专属客服</h2><p>"+getText(datas.last)+"</p><time>"+datas.date+"</time></a>");
							}
						}
						else{
							if ( datas.number > 99 ){
								li.html("<a href='message-feitian.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>飞天专属客服</h2><p>"+getText(datas.last)+"</p><time>"+datas.date+"</time><span class='new'>N</span></a>");
							}
							else{
								li.html("<a href='message-feitian.html?shopid="+window.shopid+"'><div class='message-icon'><img src='images/message-1.png' /></div><h2>飞天专属客服</h2><p>"+getText(datas.last)+"</p><time>"+datas.date+"</time><span class='new'>"+datas.number+"</span></a>");
							}
						}
					},
					error: function(){
						var li = $(".ul-message").find("li");
						li.find("p").html("网络异常");
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//表情
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
