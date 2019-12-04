$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			var page_index = 1;
			var page_size = 10;
			function ajax(){
				var postdata = {};
				postdata.page_index = page_index;
				postdata.page_size = page_size;
				$.ajax({
					type: "post",
			        url: "api/Order/GetOrderTransction",
			        data: postdata,
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
							html += "<li><a href='order-detail.html?shopid="+window.shopid+"&orderId="+datas[i].id+"'><time><em>"+datas[i].day+"</em>11-11</time><h2>"+datas[i].money+"</h2><p>订单号"+datas[i].orderId+"</p><div class='trade-state'>"+datas[i].state+"</div></a></li>";
						}
						$("#list").append(html);
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
						$("#loading").html("网络错误,点击重新加载...");
						loading = false;
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			}
			//加载更多
				$(document).on("tap","#loading",function(event){
					event.preventDefault();
					if ( !window.noresult ){
						ajax();
					}
				})
			//一切就绪后拿取数据
				ajax();
			//累计消费金额
				$.ajax({
					type: "post",
			        url: "api/SaleStatistics/GetSaleStatistics",
					dataType: "json",
					beforeSend: function(){
						$("#loading").show().html("正在加载...");
					},
					success: function(data){
						if ( !detectionResult(data,0) ){
							loading = false;
							return;
						}
						$(".coin-text").html("￥"+data.result.saleAmount.toFixed(2));
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
		})
})
