$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
				$.ajax({
					type: "post",
			        url: "api/SaleStatistics/GetSaleStatistics",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							loading = false;
							return;
						}
						$(".jifen").html(data.result.consumptionpoint);
						if ( data.result.consumptionpoint > 0 ){
							$(".jifen-img").find("p:first").remove();
						}
					},
					error:function(){
						$(".jifen").html("网络错误");
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
		})
})
