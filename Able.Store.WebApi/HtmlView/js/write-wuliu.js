$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var loading = false;
			var orderId = $.query.get("orderId");
			var postData = {};
			postData.orderId = orderId;
			//填写物流
				$(document).on("submit",".address-form",function(event){
					event.preventDefault();
					if ( loading ){
						return
					}
					postData.company = document.getElementById("company").value;
					postData.waybill = document.getElementById("waybill").value;
					if ( postData.company == "" ){
						windowAlert("请输入物流公司",1);
						return
					}
					if ( postData.waybill == "" ){
						windowAlert("请输入运单号",1);
						return
					}
					trueFunction = function(){
						loading = true;
						windowAlert("正在提交物流信息",0);
						$.ajax({
							type: "post",
					        url: "api/ReturnOrder/WriteExpressInfo",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							success: function(data){
								if ( !detectionResult(data,1) ){
									loading = false;
									return;
								}
								windowAlertClose("填写物流成功");
								setTimeout(function(){
									window.location.href = "order-return-detail.html?shopid=" + window.shopid + "&orderId=" + orderId;
								},1000)
							},
							error: function(){
								windowAlertClose("网络错误，刷新重试");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						})
					}
					windowConfirm("确认运单号正确了吗？","取消","确定");
				})
		})
})
