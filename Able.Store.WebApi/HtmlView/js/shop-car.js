$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			//读取购物车
				$.ajax({
                    type: "get",
                    url: window.baseurl+ "api/shopping/getbasket",
					dataType: "json",
					success: function(data){
						if ( !detectionResult(data,0) ){
							return;
						}
						var datas = data.result;
						if ( datas.length < 1 ){
							$(".empty").show();
							return;
						}
						$(".car-number").html("购物车（"+datas.length+"）");
						$(".car-header").show();
						var html = "";
						for ( var i=0;i<datas.length;i++ ){
							html += "<li data-id='"+datas[i].skuid+"' data-price='"+datas[i].price.toFixed(2)+"'><a href='javascript:void(0)'><div class='car-img'><img src='' loadsrc='"+datas[i].img+"' /></div><h2>"+datas[i].title+"</h2><p>"+datas[i].propertyname+":"+datas[i].propertyvalue+"</p><div class='price'>￥"+datas[i].price.toFixed(2)+"</div></a><div class='select'></div><div class='shuliang'><span class='cut'>－</span><input type='tel' value='"+datas[i].qty+"' /><span class='add'>+</span></div></li>";
						}
						$(".ul-car").append(html).show();
						loadsrc();//加载可视区域内的图片
						$(".car-ctrl").show();
					},
					error: function(){
						windowAlert("网络错误,刷新重试",0)
					},
					headers:{
						"x-moutai-token": window.token,
					}
				});
			//选择
				$(document).on("tap",".select",function(event){
					event.preventDefault();
					var li = $(this).parent();
					if ( li.hasClass("on") ){
						li.removeClass("on");
					}
					else{
						li.addClass("on");
					}
					priceTotal();
				})
			//全选
				$(document).on("tap",".select-all",function(event){
					event.preventDefault();
					if ( $(this).hasClass("select-all-on") ){
						$(this).removeClass("select-all-on");
						$(".ul-car").find("li").removeClass("on");
					}
					else{
						$(this).addClass("select-all-on");
						$(".ul-car").find("li").addClass("on");
					}
					priceTotal();
				})
			//从购物车中删除
				var loading = false;
				$(document).on("tap",".car-del",function(event){
					event.preventDefault();
					if ( loading ){
						return;
					}
					if ( !$(".ul-car").find("li.on").length ){
						windowAlert("请选择需要删除的购物车商品");
						return
					}
					loading = true;
                    var postData = {};
                    postData.skuId = [];
					$(".ul-car").find("li.on").each(function(){
                        postData.skuId.push($(this).attr("data-id"));
					})
					$.ajax({
                        type: "Post",
                        url: window.baseurl+"api/shopping/remove",
						dataType: "json",
						data:postData,
      					traditional:true,
						beforeSend: function(){
							windowAlert("正在从购物车中删除",0);
						},
						success: function(data){
							if ( !detectionResult(data,0) ){
								loading = false;
								return;
							}
							windowAlertClose("删除成功");
							$(".ul-car").find("li.on").transition({height:0,complete:function(){
								$(this).remove();
								$(".select-all").removeClass("select-all-on");
								$(".car-number").html("购物车（"+$(".ul-car").find("li").length+"）");
								if ( !$(".ul-car").find("li").length ){
									$(".ul-car,.car-header,.car-ctrl").hide();
									$(".empty").show();
								}
								priceTotal();
							}},500);
							loading = false;
						},
						error: function(){
							windowAlertClose("删除购物车时发生网络错误,请重试");
							loading = false;
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
				})
			//修改数量时计算价格
				$(document).on("tap",".cut,.add",function(event){
					event.preventDefault();
					priceTotal();
				})
			//计算价格
				function priceTotal(){
					var price = 0;
					$(".ul-car li.on").each(function(){
						price += $(this).attr("data-price") * $(this).find("input").val();
					})
					$("#price").html("￥"+price.toFixed(2));
					var postData = {};
					
                    $(".ul-car").find("li").each(function () {
                       
                        postData.skuid = $(this).attr("data-id");
                        postData.qty = $(this).find("input").val();
						
                    })

                    if (!postData.skuid)
                        return false;

					$.ajax({
                        type: "post",
                        url: window.baseurl +"api/shopping/changenumber",
						dataType: "json",
						data:JSON.stringify(postData),
						contentType: "application/json; charset=utf-8",
						success: function(data){
							if ( !detectionResult(data,0) ){
								return;
							}
						},
						headers:{
							"x-moutai-token": window.token,
						}
				    })
				}
			//手动修改数量时计算
				$(document).on("change",".shuliang input",function(event){
					event.preventDefault();
					priceTotal();
				})
			//结算
				$(document).on("tap",".go",function(event){
					event.preventDefault();
					if ( loading ){
						return;
					}
					if ( !$(".ul-car").find("li.on").length ){
						windowAlert("请选择需要结算的商品");
						return
					}
					var skuId = "";
					$(".ul-car").find("li.on").each(function(){
                        skuId += $(this).attr("data-id") + ",";
					})
                    skuId = skuId.substring(0, skuId.length-1);
                    window.location.href = "buy.html?shopid=" + window.shopid + "&skuids=" + skuId;
				})
		})
})
