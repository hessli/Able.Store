$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var loading = false;
			var page_size = 10;
			var page_index = 1;
			function ajax(){
				var postData = {};
				postData.page_index = page_index;
				postData.page_size = page_size;
				$.ajax({
					type: "post",
			        url: "api/Order/GetOrderMessage",
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
							for ( var j=0;j<datas[i].imgs.length;j++ ){
								htmls += "<div class='msg-order-img'><img src='"+datas[i].imgs[j]+"' /></div>";
							}
							switch ( datas[i].state ){
								case 2:
									html += "<li><div class='msg-time'><time>"+datas[i].date+"</time></div><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2>茅台官方商城</h2><div class='state'>待支付</div><div class='msg-order-list'>"+htmls+"</div><div class='msg-order-total'>... 共"+datas[i].number+"款</div><div class='jine'>合计：￥"+datas[i].total.toFixed(2)+"</div><div class='msg-order-foot'><div class='order-number'>订单号："+datas[i].orderId+"</div><span>查看详情 ></span></div></a></li>";
									break;
								case 3:
									html += "<li><div class='msg-time'><time>"+datas[i].date+"</time></div><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2>茅台官方商城</h2><div class='state'>待发货</div><div class='msg-order-list'>"+htmls+"</div><div class='msg-order-total'>... 共"+datas[i].number+"款</div><div class='jine'>合计：￥"+datas[i].total.toFixed(2)+"</div><div class='msg-order-foot'><div class='order-number'>订单号："+datas[i].orderId+"</div><span>查看详情 ></span></div></a></li>";
									break;
								case 4:
									html += "<li><div class='msg-time'><time>"+datas[i].date+"</time></div><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2>茅台官方商城</h2><div class='state'>待收货</div><div class='msg-order-list'>"+htmls+"</div><div class='msg-order-total'>... 共"+datas[i].number+"款</div><div class='jine'>合计：￥"+datas[i].total.toFixed(2)+"</div><div class='msg-order-foot'><div class='order-number'>订单号："+datas[i].orderId+"</div><span>查看详情 ></span></div></a></li>";
									break;
								case 5:
									html += "<li><div class='msg-time'><time>"+datas[i].date+"</time></div><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2>茅台官方商城</h2><div class='state'>交易完成</div><div class='msg-order-list'>"+htmls+"</div><div class='msg-order-total'>... 共"+datas[i].number+"款</div><div class='jine'>合计：￥"+datas[i].total.toFixed(2)+"</div><div class='msg-order-foot'><div class='order-number'>订单号："+datas[i].orderId+"</div><span>查看详情 ></span></div></a></li>";
									break;
								case 6:
									html += "<li><div class='msg-time'><time>"+datas[i].date+"</time></div><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2>茅台官方商城</h2><div class='state'>交易关闭</div><div class='msg-order-list'>"+htmls+"</div><div class='msg-order-total'>... 共"+datas[i].number+"款</div><div class='jine'>合计：￥"+datas[i].total.toFixed(2)+"</div><div class='msg-order-foot'><div class='order-number'>订单号："+datas[i].orderId+"</div><span>查看详情 ></span></div></a></li>";
									break;
								case 7:
									html += "<li><div class='msg-time'><time>"+datas[i].date+"</time></div><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><h2>茅台官方商城</h2><div class='state'>交易关闭</div><div class='msg-order-list'>"+htmls+"</div><div class='msg-order-total'>... 共"+datas[i].number+"款</div><div class='jine'>合计：￥"+datas[i].total.toFixed(2)+"</div><div class='msg-order-foot'><div class='order-number'>订单号："+datas[i].orderId+"</div><span>查看详情 ></span></div></a></li>";
									break;
							}
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
			ajax();
			//加载更多
				$(document).on("tap","#loading",function(event){
					event.preventDefault();
					if ( !window.noresult ){
						ajax();
					}
				})
		})
})
