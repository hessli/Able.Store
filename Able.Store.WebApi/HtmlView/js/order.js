$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var loading = false;
			var type = $.query.get("type");
			var typeId;
			var keyword = $.query.get("keyword");
			if ( keyword === true ){
				keyword = "";
			}
			$("#keyword").val(decodeURIComponent(keyword));
			var page_size = 10;
			var page_index = 1;
			//当前页处理
				switch ( type ){
					case "":
						var index = 0;
						typeId = 0;
						break;
					case "pay":
						var index = 1;
						typeId = 2;
						break;
					case "fa":
						var index = 2;
						typeId = 3;
						break;
					case "shou":
						var index = 3;
						typeId = 4;
						break;
					case "return":
						var index = 4;
						typeId = 0;
						break;
				}
				$(".order-class").find("li").eq(index).addClass("on");
				$(".none").show();
			//读取列表
				function ajax(){
					if ( loading ){
						return;
					}
					loading = true;
					var postData = {};
					postData.keyword = decodeURIComponent(keyword);
					postData.page_index = page_index;
					postData.page_size = page_size;
					postData.type = typeId;
					if ( type != "return" ){
						$.ajax({
							type: "post",
					        url: "api/Order/Paging",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							beforeSend: function(){
								$("#loading").show().html("正在加载...");
							},
							success: function(data){
								if ( !detectionResult(data,0) ){
									loading = false;
									return;
								}
								if ( data.result.count == 0 ){
									$("#loading").html("暂无结果");
									window.noresult = true;
									return;
								}
								var datas = data.result.data;
								var html = "";
								for ( var i=0;i<datas.length;i++ ){
									var htmls = "";
									for ( var j=0;j<datas[i].product.length;j++ ){
										htmls += "<div class='order-list-item'><div class='order-img'><img src='' loadsrc='"+datas[i].product[j].image_domain+"110x110/"+datas[i].product[j].img+"' /></div><h3>"+datas[i].product[j].title+"</h3><div class='sx'><p>"+datas[i].product[j].net+" "+datas[i].product[j].size+"</p></div><div class='shu'><strong>￥"+datas[i].product[j].price.toFixed(2)+"</strong>× <em>"+datas[i].product[j].number+"</em></div></div>";
									}
									switch ( datas[i].state ){
										case 2:
											html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>待支付</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a class='button-white close-order'>取消订单</a><a href='pay.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'>马上支付</a></div></li>";
											break;
										case 3:
											html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>待发货</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a class='button-white tixingfahuo'>提醒发货</a></div></li>";
											break;
										case 4:
											html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>待收货</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a href='wuliu.html?shopid="+window.shopid+"&orderId="+datas[i].id+"' class='button-white'>查看物流</a><a class='shou-order'>确认收货</a></div></li>";
											break;
										case 5:
											if ( datas[i].complete_status == 1 ){
												html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>交易完成</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'></div></li>";
											}
											else{
												html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>交易完成</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a href='return.html?shopid="+window.shopid+"&orderId="+datas[i].id+"' class='button-white'>申请退货</a><a href='wuliu.html?shopid="+window.shopid+"&orderId="+datas[i].id+"' class='button-white'>查看物流</a></div></li>";
											}
											break;
										case 6:
											html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>交易关闭</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'></div></li>";
											break;
										case 7:
											html += "<li data-id='"+datas[i].id+"'><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>交易关闭</div><div class='order-list'>"+htmls+"</div><div class='heji'>实付 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'></div></li>";
											break;
									}
									html += "";
								}
								$("#list").append(html);
								loadsrc();//加载可视区域内的图片
								var pageCount = Math.ceil(data.result.count/page_size);
								if ( page_index == pageCount ){
									$("#loading").hide();
								}
								else{
									$("#loading").html("点击加载更多");
									page_index ++;
								}
								loading = false;
							},
							error: function(){
								$("#loading").html("加载失败,点击重新加载...");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						});
					}
					else{
						$.ajax({
							type: "post",
					        url: "api/ReturnOrder/Paging",
							data:JSON.stringify(postData),
							contentType: "application/json; charset=utf-8",
							dataType: "json",
							beforeSend: function(){
								$("#loading").show().html("正在加载...");
							},
							success: function(data){
								if ( !detectionResult(data,0) ){
									loading = false;
									return;
								}
								if ( data.result.count == 0 ){
									$("#loading").html("暂无结果");
									window.noresult = true;
									return;
								}
								var datas = data.result.data;
								var html = "";
								for ( var i=0;i<datas.length;i++ ){
									var htmls = "";
									for ( var j=0;j<datas[i].product.length;j++ ){
										htmls += "<div class='order-list-item'><div class='order-img'><img src='' loadsrc='"+datas[i].product[j].image_domain+"110x110/"+datas[i].product[j].img+"' /></div><h3>"+datas[i].product[j].title+"</h3><div class='sx'><p>"+datas[i].product[j].net+"ml "+datas[i].product[j].size+"</p></div><div class='shu'><strong>￥"+datas[i].product[j].price.toFixed(2)+"</strong>× <em>"+datas[i].product[j].number+"</em></div></div>";
									}
									switch ( datas[i].state ){
										case 2:
											html += "<li data-id='"+datas[i].id+"'><a href='order-return-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>待审核</div><div class='order-list'>"+htmls+"</div><div class='heji'>退款金额 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a class='button-white close-return'>关闭退货</a></div></li>";
											break;
										case 4:
											html += "<li data-id='"+datas[i].id+"'><a href='order-return-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>待退货</div><div class='order-list'>"+htmls+"</div><div class='heji'>退款金额 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a href='write-wuliu.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'>填写物流</a><a class='button-white close-return'>关闭退货</a></div></li>";
											break;
										case 5:
											html += "<li data-id='"+datas[i].id+"'><a href='order-return-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>待收货</div><div class='order-list'>"+htmls+"</div><div class='heji'>退款金额 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a href='wuliu-return.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'>查看物流</a></div></li>";
											break;
										case 6:
											html += "<li data-id='"+datas[i].id+"'><a href='order-return-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>退货完成</div><div class='order-list'>"+htmls+"</div><div class='heji'>退款金额 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a><div class='button'><a href='wuliu-return.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'>查看物流</a></div></li>";
											break;
										case 7:
											html += "<li data-id='"+datas[i].id+"'><a href='order-return-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2 class='say'>茅台官方商城</h2><div class='states'>退货关闭</div><div class='order-list'>"+htmls+"</div><div class='heji'>退款金额 <strong>￥"+datas[i].total.toFixed(2)+"</strong></div></a></li>";
											break;
									}
									html += "";
								}
								$("#list").append(html);
								loadsrc();//加载可视区域内的图片
								var pageCount = Math.ceil(data.result.count/page_size);
								if ( page_index == pageCount ){
									$("#loading").hide();
								}
								else{
									$("#loading").html("点击加载更多");
									page_index ++;
								}
								loading = false;
							},
							error: function(){
								$("#loading").html("加载失败,点击重新加载...");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						});
					}
				}
			//搜索
				$(document).on("submit",".order-search",function(event){
					event.preventDefault();
					keyword = document.getElementById("keyword").value;
					var url = $.query.set("keyword", encodeURIComponent(keyword));
					window.location.href = url;
				})
			//加载更多
				$(document).on("tap","#loading",function(event){
					event.preventDefault();
					if ( !window.noresult ){
						ajax();
					}
				})
			//一切就绪后拿取数据
				ajax();
			//进入聊天
				$(document).on("tap",".say",function(event){
					event.preventDefault();
					window.location.href = "message-service-list.html?shopid=" + window.shopid;
				})
			//取消订单
				$(document).on("tap",".close-order",function(event){
					event.preventDefault();
					var li = $(this).closest("li");
					var orderId = li.attr("data-id");
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
								if ( type == "" ){
									windowAlertClose("订单已取消");
									li.find(".states").html("交易取消");
									li.find(".button").empty();
								}
								else{
									windowAlertClose("订单已取消");
									li.height(li.height()).css("minHeight","inherit");
									li.find(">a").height(li.find(">a").height());
									li.css({"overflow":"hidden"}).transition({height:0,complete:function(){
										li.remove();
									}},300)
								}
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
					postData.orderId = $(this).closest("li").attr("data-id");
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
							$this.css({"width":"auto","padding":"0 5px","color":"#7f7f7f","borderColor":"#7f7f7f"}).html("已提醒卖家发货").removeClass("tixingfahuo");
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
					var li = $(this).closest("li");
					var orderId = li.attr("data-id");
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
								if ( type == "" ){
									windowAlertClose("确认收货成功");
									li.find(".states").html("交易完成");
									li.find(".button").html("<a href='return.html?shopid="+window.shopid+"&orderId="+orderId+"' class='button-white'>申请退货</a><a href='order-wuliu.html?shopid="+window.shopid+"&orderId="+orderId+"' class='button-white'>查看物流</a>");
								}
								else{
									windowAlertClose("确认收货成功");
									li.height(li.height()).css("minHeight","inherit");
									li.find(">a").height(li.find(">a").height());
									li.css({"overflow":"hidden"}).transition({height:0,complete:function(){
										li.remove();
									}},300)
								}
								loading = false;
							},
							error: function(){
								windowAlertClose("确认收货时发生网络错误，请重试");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						})
					}
					windowConfirm("确定已收到货物了吗？","取消","确定");
				})
			//关闭退货
				$(document).on("tap",".close-return",function(event){
					event.preventDefault();
					var li = $(this).closest("li");
					var orderId = li.attr("data-id");
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
								if ( type == "" ){
									windowAlertClose("退货已关闭");
									li.find(".states").html("退货关闭");
									li.find(".button").empty();
								}
								else{
									windowAlertClose("退货已关闭");
									li.height(li.height()).css("minHeight","inherit");
									li.find(">a").height(li.find(">a").height());
									li.css({"overflow":"hidden"}).transition({height:0,complete:function(){
										li.remove();
									}},300)
								}
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
