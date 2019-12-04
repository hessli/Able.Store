$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var loading = false;
			var orderId = $.query.get("orderId");
			var postData = {};
			postData.orderId = orderId;
			//获取退货理由列表
				$.ajax({
					type: "post",
			        url: "api/CustomerSystem/GetReturnReason",
					data:JSON.stringify(postData),
					contentType: "application/json; charset=utf-8",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						var html = "";
						for (var i=0;i<data.result.length;i++){
							html += "<option value='"+data.result[i].id+"'>"+data.result[i].name+"</option>";
						}
						document.getElementById("select").innerHTML = html;
						//获取订单详情
							$.ajax({
								type: "post",
						        url: "api/Order/GetOrderDetail",
								data:JSON.stringify(postData),
								contentType: "application/json; charset=utf-8",
								dataType: "json",
								success: function(data){
									if ( !detectionResult(data,0) ){
										return;
									}
									var datas = data.result;
									var html = "";
									for (var i=0;i<datas.product.length;i++){
										html += "<div class='order-list-item'><div class='order-img'><img src='' loadsrc='"+datas.product[i].img+"' /></div><h3>"+datas.product[i].title+"</h3><div class='sx'><p>"+datas.product[i].net+" "+datas.product[i].strength+"</p><p>"+datas.product[i].size+"</p></div><div class='shu'><strong>￥"+datas.product[i].price.toFixed(2)+"</strong>× <em>"+datas.product[i].number+"</em></div></div><div class='return-number' data-size='"+datas.product[i].sizeId+"' data-id='"+datas.product[i].productId+"' data-price='"+datas.product[i].price.toFixed(2)+"'><span>退货数量</span><div class='shuliang'><span class='cut'>－</span><input type='tel' value='0' max='"+datas.product[i].number+"' /><span class='add'>+</span></div></div>";
									}
									$("#list").html("<li><div class='orders'><h2>退货清单</h2><div class='order-num'>订单号："+datas.orderId+"</div><div class='order-list'>"+html+"</div><div class='heji heji-full'><span>共退货<span id='number'>0</span>件商品</span>　合计 <strong id='total'>￥0.00</strong></div></div></li>");
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
					},
					error: function(){
						windowAlert("网络错误，刷新重试",0);
					},
					headers:{
						"x-moutai-token": window.token,
					}
				})
			//修改数量时计算价格
				$(document).on("tap",".cut,.add",function(event){
					event.preventDefault();
					priceTotal();
				})
			//计算价格
				function priceTotal(){
					window.price = 0;
					var number = 0;
					$(".return-number").each(function(){
						price += $(this).attr("data-price") * $(this).find("input").val();
						number += parseInt($(this).find("input").val());
					})
					$("#number").html(number);
					$("#total").html("￥"+price.toFixed(2));
				}
			//手动修改数量时计算
				$(document).on("change",".shuliang input",function(event){
					event.preventDefault();
					priceTotal();
				})
			//退货
				$(document).on("tap",".order-return",function(event){
					event.preventDefault();
					if ( loading ){
						return
					}
					var each = false;
					for ( var i=0;i<$("#list .return-number").length;i++ ){
						$("#list .return-number").eq(i).find("input").blur();
						if ( parseInt($("#list .return-number").eq(i).find("input").val()) != 0 ){
							each = true;
							break;
						}
					}
					if ( !each ){
						windowAlert("请先选择需要退货的商品");
						return
					}
					priceTotal();
					trueFunction = function(){
						loading = true;
						windowAlert("正在提交退货申请",0);
						var postData = {};
						postData.returnReason = document.getElementById("select").value;
						postData.orderId = orderId;
						postData.total = price.toFixed(2);
						postData.product = [];
						for ( var i=0;i<$("#list .return-number").length;i++ ){
							var d = {};
							d.productId = $("#list .return-number").eq(i).attr("data-id");
							d.sizeId = $("#list .return-number").eq(i).attr("data-size");
							d.number = $("#list .return-number").eq(i).find("input").val();
							postData.product.push(d);
						}
						$.ajax({
							type: "post",
					        url: "api/ReturnOrder/GenerateOrder",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							success: function(data){
								if ( !detectionResult(data,1) ){
									loading = false;
									return;
								}
								windowAlertClose("退货申请已提交");
								setTimeout(function(){
									window.location.href = "order-return-detail.html?shopid=" + window.shopid + "&orderId=" + data.result;
								},1000)
								loading = false;
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
					windowConfirm("确定要退货吗？","取消","确定");
				})
		})
})
