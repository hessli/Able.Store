$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var loading = false;
			var orderId = $.query.get("orderId");
			$.ajax({
                type: "get",
                url: window.baseurl+ "api/order/get?orderId=" + orderId,
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function(data){
					if ( !detectionResult(data,0) ){
						return;
					}
					$(".address").html("<h2>"+data.result.name+"</h2><div class='mob'>"+data.result.tel+"</div><p>"+data.result.province+data.result.city+data.result.county+data.result.detailed+"</p>");
					var html = "";
					for ( var i=0;i<data.result.product.length;i++ ){
                        html += "<a href='product-detail.html?shopid=1&productId=" + data.result.product[i].skuId+"' class='order-list-item'><div class='order-img'><img src='' loadsrc='"+data.result.product[i].img+"' /></div><h3>"+data.result.product[i].title+"</h3><div class='sx'><p>"+data.result.product[i].property+" "+data.result.product[i].propertyValue+"</p></div><div class='shu'><strong>￥"+data.result.product[i].price.toFixed(2)+"</strong>× <em>"+data.result.product[i].qty+"</em></div></a>";
					}
					$("#list").html("<li><div class='orders'><h2>商品信息</h2><div class='order-num'>订单号："+data.result.orderNo+"</div><div class='order-list'>"+html+"</div><div class='s-total'><div class='yunfei'>+运费<strong>￥"+data.result.freight.toFixed(2)+"</strong></div><div class='je'>商品总额<strong>￥"+data.result.commodity.toFixed(2)+"</strong></div></div><div class='heji'>合计 <strong>￥"+data.result.total.toFixed(2)+"</strong></div></div></li>");
					switch ( data.result.state ){
						case 2:
							$(".order-sx").html("<h1>等待我付款</h1><p>剩余"+data.result.nextState+"自动取消</p>");
                            $(".order-info").html("<p><em>配送方式：</em>" + data.result.wuliu + "</p><p><em>发票信息：</em>" + data.result.invoicetitle + "</p><div class='line'></div><p><em>创建时间：</em>" + data.result.dateAdd + "</p><p><em>付款时间：</em>" + data.result.datePay+"</p><p><em>发货时间：</em>"+data.result.dateDelivery+"</p><p><em>收货时间：</em>"+data.result.dateReceipt+"</p>");
							$(".order-ctrl").html("<a class='order-false close-order'>取消订单</a><a href='pay.html?shopid="+window.shopid+"&orderId="+orderId+"' class='order-pay'>立即支付</a>");
							break;
						case 3:
							$(".order-sx").html("<h1>等待卖家发货</h1><p>如有问题请联系客服</p>");
							$(".order-info").html("<p><em>配送方式：</em>"+data.result.wuliu+"</p><p><em>发票信息：</em>"+data.result.invoicetitle+"</p><div class='line'></div><p><em>创建时间：</em>"+data.result.dateAdd+"</p><p><em>付款时间：</em>"+data.result.datePay+"</p><p><em>发货时间：</em>"+data.result.dateDelivery+"</p><p><em>收货时间：</em>"+data.result.dateReceipt+"</p>");
							$(".order-ctrl").html("<a class='order-pay tixingfahuo'>提醒卖家发货</a>");
							break;
						case 4:
							$(".order-sx").html("<h1>等待我收货</h1><p>剩余"+data.result.nextState+"自动收货</p>");
                            $(".order-info").html("<p><em>配送方式：</em>" + data.result.wuliu + "</p><p><em>发票信息：</em>" + data.result.invoicetitle + "</p><div class='line'></div><p><em>创建时间：</em>" + data.result.dateAdd + "</p><p><em>付款时间：</em>" + data.result.datePay+"</p><p><em>发货时间：</em>"+data.result.dateDelivery+"</p><p><em>收货时间：</em>"+data.result.dateReceipt+"</p>");
							$(".order-ctrl").html("<a class='order-pay shou-order'>确定收货</a>");
							break;
						case 5:
							$(".order-sx").addClass("complete").html("<h1>交易完成</h1><p>如有问题请联系客服</p>");
                            $(".order-info").html("<p><em>配送方式：</em>" + data.result.wuliu + "</p><p><em>发票信息：</em>" + data.result.invoicetitle + "</p><div class='line'></div><p><em>创建时间：</em>" + data.result.dateAdd + "</p><p><em>付款时间：</em>" + data.result.datePay+"</p><p><em>发货时间：</em>"+data.result.dateDelivery+"</p><p><em>收货时间：</em>"+data.result.dateReceipt+"</p>");
							if ( data.result.complete_status != 1 ){
								$(".order-ctrl").html("<a href='return.html?shopid="+window.shopid+"&orderId="+orderId+"' class='order-false'>申请退货</a>");
							}
							break;
						case 6:
							$(".order-sx").addClass("close").html("<h1>交易取消</h1><p>如有问题请联系客服</p>");
							$(".order-info").html("<p><em>配送方式：</em>"+data.result.wuliu+"</p><p><em>发票信息：</em>"+data.result.invoicetitle+"</p><div class='line'></div><p><em>创建时间：</em>"+data.result.dateAdd+"</p><p><em>取消时间：</em>"+data.result.dateClose+"</p>");
							break;
						case 7:
							$(".order-sx").addClass("close").html("<h1>交易取消</h1><p>如有问题请联系客服</p>");
							$(".order-info").html("<p><em>配送方式：</em>"+data.result.wuliu+"</p><p><em>发票信息：</em>"+data.result.invoicetitle+"</p><div class='line'></div><p><em>创建时间：</em>"+data.result.dateAdd+"</p><p><em>取消时间：</em>"+data.result.dateClose+"</p>");
							break;
					}
					if ( data.result.state == 4 || data.result.state == 5 ){
						//最新物流
						$.ajax({
							type: "post",
					        url: "api/Order/GetLastExpress",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							success: function(data){
								if ( !detectionResult(data,0) ){
									return;
								}
								$(".order-sx").after("<a href='wuliu.html?shopid="+ shopid +"&orderId="+ orderId +"' class='wuliu'><h2>"+ data.result.message +"</h2><p>"+ data.result.date +"</p></a>");
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
					windowAlert("网络错误，刷新重试",0);
				},
				headers:{
					"x-moutai-token": window.token,
				}
			})
			//取消订单
				$(document).on("tap",".close-order",function(event){
					event.preventDefault();
					trueFunction = function(){
						if ( loading ){
							return;
						}
						loading = true;
						windowAlert("正在取消",0);
						var postData = {};
						postData.orderId = orderId;
						$.ajax({
							type: "post",
					        url: "api/Order/Close",
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
					windowConfirm("确定要取消此订单吗？","取消","确定");
				})
			//提醒发货
				$(document).on("tap",".tixingfahuo",function(event){
					event.preventDefault();
					if ( loading ){
						return;
					}
					loading = true;
					var $this = $(this);
					var postData = {};
					postData.orderId = orderId;
					$.ajax({
						type: "post",
				        url: "api/Order/NoticSend",
						data:JSON.stringify(postData),
						contentType: "application/json; charset=utf-8",
						dataType: "json",
						success: function(data){
							if ( !detectionResult(data,1) ){
								loading = false;
								return;
							}
							$this.css({"borderColor":"#7f7f7f","background":"#7f7f7f"}).html("已提醒卖家发货").removeClass("tixingfahuo");
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
				})
			//确认收货
				$(document).on("tap",".shou-order",function(event){
					event.preventDefault();
					trueFunction = function(){
						if ( loading ){
							return;
						}
						loading = true;
						windowAlert("正在确认收货",0);
						var postData = {};
						postData.orderId = orderId;
						$.ajax({
							type: "post",
					        url: "api/Order/ConfrimReciver",
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
					windowConfirm("确定已收到货物了吗？","取消","确定");
				})
		})
})
