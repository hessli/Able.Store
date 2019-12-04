$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var orderId = $.query.get("orderId");
			$(".empty-go").attr("href","pay.html?shopid="+window.shopid+"&orderId="+orderId);
			$(".none").show();
		})
})
