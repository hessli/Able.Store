$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			window.orderId = $.query.get("orderId");
			var postData = {};
			postData.orderId = orderId;
			$.ajax({
				type: "post",
		        url: "api/ReturnOrder/GetExpressList",
				data:JSON.stringify(postData),
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(data){
					if ( !detectionResult(data,0) ){
						return;
					}
					$(".wuliu-head").html("<div class='wuliu-img'><img src='"+ data.result.img +"' /></div><h2>物流状态 <em>"+ data.result.state +"</em></h2><p>承运快递："+ data.result.company +"</p><p>运单编号："+ data.result.waybill +"</p><p>官方电话：<em>"+ data.result.tel +"</em></p>");
					var datas = data.result.detail;
					var html = "";
					for ( var i=0;i<datas.length;i++ ){
						html += "<li><p>"+ datas[i].message +"</p><time>"+ datas[i].date +"</time></li>";
					}
					$(".ul-genzong").html(html);
					$(".none").show();
				},
				error: function(){
					windowAlert("网络错误，刷新重试",0);
				},
				headers:{
					"x-moutai-token": window.token,
				}
			})
		})
})
