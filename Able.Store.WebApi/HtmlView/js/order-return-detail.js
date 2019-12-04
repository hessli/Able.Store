$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var loading = false;
			var orderId = $.query.get("orderId");
			var postData = {};
			postData.orderId = orderId;
			$.ajax({
				type: "post",
		        url: "api/returnorder/getReturnOrderDetail",
				data:JSON.stringify(postData),
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(data){
					if ( !detectionResult(data,0) ){
						return;
					}
					$(".address").html("<h2>"+data.result.name+"</h2><div class='mob'>"+data.result.tel+"</div><p>"+data.result.province+data.result.city+data.result.county+data.result.detailed+"</p>");
					var html = "";
					for ( var i=0;i<data.result.product.length;i++ ){
						html += "<a href='product-detail.html?shopid="+window.shopid+"&productId="+data.result.product[i].productId+"' class='order-list-item'><div class='order-img'><img src='' loadsrc='"+data.result.product[i].image_domain+"110x110/"+data.result.product[i].img+"' /></div><h3>"+data.result.product[i].title+"</h3><div class='sx'><p>"+data.result.product[i].net+" "+data.result.product[i].size+"</p></div><div class='shu'><strong>￥"+data.result.product[i].price.toFixed(2)+"</strong>× <em>"+data.result.product[i].number+"</em></div></a>";
					}
					$("#list").html("<li><div class='orders'><h2>商品信息</h2><div class='order-num'>订单号："+data.result.orderId+"</div><div class='order-list'>"+html+"</div><div class='s-total'><div class='yunfei'>+运费<strong>￥"+data.result.freight.toFixed(2)+"</strong></div><div class='je'>交易金额<strong>￥"+data.result.orderTotal.toFixed(2)+"</strong></div></div><div class='heji'>退款金额 <strong>￥"+data.result.returnTotal.toFixed(2)+"</strong></div></div></li>");
					switch ( data.result.state ){
						case 2:
							$(".order-sx").html("<h1>待审核</h1><p>您已申请退货，请联系卖家审核哦~</p>");
							$(".order-info").html("<p><em>退货单号：</em>"+data.result.returnId+"</p><p><em>退货类型：</em>"+data.result.returnType+"</p><p><em>退货原因：</em>"+data.result.returnReasons+"</p><p><em>退货金额：</em><strong>￥"+data.result.returnTotal.toFixed(2)+"</strong></p><div class='line'></div><p><em>申请时间：</em>"+data.result.returnDateApply+"</p><p><em>审核时间：</em>"+data.result.returnDateReview+"</p><p><em>发货时间：</em>"+data.result.returnDateDelivery+"</p><p><em>收货时间：</em>"+data.result.returnDateReceipt+"</p><p><em>退款时间：</em>"+data.result.returnDateRefund+"</p>");
							$(".order-ctrl").html("<a class='order-false close-return'>关闭退货</a>");
							break;
						case 4:
							$(".order-sx").html("<h1>待退货</h1><p>卖家已审核通过，剩余"+data.result.nextState+"自动关闭</p>");
							$(".order-info").html("<p><em>退货单号：</em>"+data.result.returnId+"</p><p><em>退货类型：</em>"+data.result.returnType+"</p><p><em>退货原因：</em>"+data.result.returnReasons+"</p><p><em>退货金额：</em><strong>￥"+data.result.returnTotal.toFixed(2)+"</strong></p><div class='line'></div><p><em>申请时间：</em>"+data.result.returnDateApply+"</p><p><em>审核时间：</em>"+data.result.returnDateReview+"</p><p><em>发货时间：</em>"+data.result.returnDateDelivery+"</p><p><em>收货时间：</em>"+data.result.returnDateReceipt+"</p><p><em>退款时间：</em>"+data.result.returnDateRefund+"</p>");
							$(".order-ctrl").html("<a href='write-wuliu.html?shopid="+window.shopid+"&orderId="+orderId+"' class='order-false'>填写物流</a><a class='order-false close-return'>关闭退货</a>");
							break;
						case 5:
							$(".order-sx").html("<h1>等待卖家验货</h1>");
							$(".order-info").html("<p><em>退货单号：</em>"+data.result.returnId+"</p><p><em>退货类型：</em>"+data.result.returnType+"</p><p><em>退货原因：</em>"+data.result.returnReasons+"</p><p><em>退货金额：</em><strong>￥"+data.result.returnTotal.toFixed(2)+"</strong></p><div class='line'></div><p><em>申请时间：</em>"+data.result.returnDateApply+"</p><p><em>审核时间：</em>"+data.result.returnDateReview+"</p><p><em>发货时间：</em>"+data.result.returnDateDelivery+"</p><p><em>收货时间：</em>"+data.result.returnDateReceipt+"</p><p><em>退款时间：</em>"+data.result.returnDateRefund+"</p>");
							break;
						case 6:
							$(".order-sx").addClass("complete").html("<h1>退货完成</h1><p>卖家验货完成，款项已退回支付账户</p>");
							$(".order-info").html("<p><em>退货单号：</em>"+data.result.returnId+"</p><p><em>退货类型：</em>"+data.result.returnType+"</p><p><em>退货原因：</em>"+data.result.returnReasons+"</p><p><em>退货金额：</em><strong>￥"+data.result.returnTotal.toFixed(2)+"</strong></p><div class='line'></div><p><em>申请时间：</em>"+data.result.returnDateApply+"</p><p><em>审核时间：</em>"+data.result.returnDateReview+"</p><p><em>发货时间：</em>"+data.result.returnDateDelivery+"</p><p><em>收货时间：</em>"+data.result.returnDateReceipt+"</p><p><em>退款时间：</em>"+data.result.returnDateRefund+"</p>");
							break;
						case 7:
							$(".order-sx").addClass("close").html("<h1>退货关闭</h1><p>买家取消了退货申请</p>");
							$(".order-info").html("<p><em>退货单号：</em>"+data.result.returnId+"</p><p><em>退货类型：</em>"+data.result.returnType+"</p><p><em>退货原因：</em>"+data.result.returnReasons+"</p><p><em>退货金额：</em><strong>￥"+data.result.returnTotal.toFixed(2)+"</strong></p><div class='line'></div><p><em>申请时间：</em>"+data.result.returnDateApply+"</p><p><em>关闭时间：</em>"+data.result.returnDateClose+"</p>");
							break;
					}
					if ( data.result.state == 5 || data.result.state == 6 ){
						//最新物流
						$.ajax({
							type: "post",
					        url: "api/ReturnOrder/GetLastExpress",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							success: function(data){
								if ( !detectionResult(data,0) ){
									return;
								}
								$(".order-sx").after("<a href='wuliu-return.html?shopid="+ shopid +"&orderId="+ orderId +"' class='wuliu'><h2>"+ data.result.message +"</h2><p>"+ data.result.date +"</p></a>");
							},
							error: function(){
								windowAlert("网络错误,刷新重试",0)
							},
							headers:{
								"x-moutai-token": window.token,
							}
						});
					}
					$(".none").show();
					loadsrc();
				},
				error: function(){
					windowAlertClose("网络错误，刷新重试");
				},
				headers:{
					"x-moutai-token": window.token,
				}
			})
			//关闭退货
				$(document).on("tap",".close-return",function(event){
					event.preventDefault();
					trueFunction = function(){
						if ( loading ){
							return;
						}
						loading = true;
						windowAlert("正在关闭",0);
						var postData = {};
						postData.orderId = orderId;
						$.ajax({
							type: "post",
					        url: "api/ReturnOrder/Close",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							success: function(data){
								if ( !detectionResult(data,0) ){
									loading = false;
									return;
								}
								window.location.reload();
								loading = false;
							},
							error: function(){
								windowAlertClose("网络错误，请重试");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						})
					}
					windowConfirm("确定要关闭退货吗？","取消","确定");
				})
		})
})
