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
						$(".photo").html("<img src='"+data.result.headPhoto+"' />");
						$(".none").show();
					},
					error: function(){
						windowAlert("网络错误,刷新重试",0)
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//消费积分
				var number = 0;
				$.ajax({
					type: "post",
			        url: "api/SaleStatistics/GetSaleStatistics",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							loading = false;
							return;
						}
						$("#jifen").find("a").append("<em>"+data.result.consumptionpoint+"分</em>");
						if ( data.result.times == 0 ){
							$(".times").html("尚无任何交易");
						}
						else{
							$(".times").html("我已累计"+data.result.times+"次交易");
						}
						number += data.result.msgcount;
						dingdan = true;
						setFen();
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
				var postData = {};
				postData.shopid = window.shopid;
				$.ajax({
					type: "post",
			        url: "api/IM/GetLatestMessage",
					dataType: "json",
					data:postData,
					success: function(data){
						if ( !detectionResult(data,0) ){
							loading = false;
							return;
						}
						number += data.result.number;
						kefu = true;
						setFen();
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
				var kefu = false;
				var dingdan = false;
				function setFen(){
					if ( kefu && dingdan ){
						if ( number > 0 ){
							$("#xiaoxi").find("a").append("<em>"+number+"</em>");
						}
					}
				}
		})
})
