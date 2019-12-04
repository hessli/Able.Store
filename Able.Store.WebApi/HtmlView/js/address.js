$(function(){
	//授权成功&&店铺状态正常 执行
		$.when(window.shopState).done(function(){
			//读取收货地址列表
				function getAddress(){
					$.ajax({
						type: "get",
				        url: "api/CustomerAddress/FindListByUserId",
						dataType: "json",
						success: function(data){
							if ( !detectionResult(data,0) ){
								return;
							}
							var datas = data.result;
							window.addressData = datas;
							if ( datas.length == 0 ){
								$("#loading").html("暂无地址，请添加");
								return
							}
							var html = "";
							for ( var i=0;i<datas.length;i++ ){
								if ( datas[i].isDefault == 1 ){
									html += "<li class='active' data-id='"+datas[i].addressId+"'><h2>"+datas[i].name+"<em>[默认]</em></h2><div class='mob'>"+datas[i].tel+"</div><p>"+datas[i].province+datas[i].city+datas[i].county+datas[i].detailed+"</p></li>";
								}
								else{
									html += "<li data-id='"+datas[i].addressId+"'><h2>"+datas[i].name+"</h2><div class='mob'>"+datas[i].tel+"</div><p>"+datas[i].province+datas[i].city+datas[i].county+datas[i].detailed+"</p></li>";
								}
							}
							$("#address-list").html(html);
							$(".hidden").remove();
						},
						error: function(){
							windowAlert("加载收货地址时发生网络错误,刷新重试",0)
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
				}
				getAddress();
			//设置默认地址
				var loading = false;
				$(document).on("tap","#set-default",function(event){
					event.preventDefault();
					if ( $("#address-list").find("li.on").hasClass("active") ){
						windowAlert("已是默认收货地址");
						return;
					}
					if ( !$("#address-list").find("li.on").length ){
						windowAlert("请先选择一条地址信息！");
						return;
					}
					if ( loading ){
						return;
					}
					loading = true;
					windowAlert("正在设置默认收货地址",0);
					var postData = {};
					postData.addressId = $("#address-list").find("li.on").attr("data-id");
					$.ajax({
						type: "get",
				        url: "api/CustomerAddress/SetDefalut",
						dataType: "json",
						data:postData,
						success: function(data){
							if ( !detectionResult(data,1) ){
								return;
							}
							windowAlertClose("设置成功");
							var active = $("#address-list").find(".active");
							var em = active.find("h2 em");
							var on = $("#address-list").find("li.on");
							if ( !active.hasClass("on") ){
								on.addClass("active").find("h2").append(em);
								active.removeClass("active");
							}
							loading = false;
						},
						error: function(){
							windowAlert("设置默认收货地址时发生网络错误,请重试")
							loading = false;
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
				})
			//获取省市列表
				window.getProvince = function(){
					$.ajax({
						type: "get",
				        url: "api/MoutaiArea/FindProvince",
						dataType: "json",
						success: function(data){
							if ( !detectionResult(data,1) ){
								return;
							}
							var datas = data.result;
							var html = "";
							for ( var i=0;i<datas.length;i++ ){
								if ( window.type == "update" && window.addressData[window.index].province == datas[i].title ){
									html += "<option value='"+datas[i].provinceId+"' selected>"+datas[i].title+"</option>";
								}
								else{
									html += "<option value='"+datas[i].provinceId+"'>"+datas[i].title+"</option>";
								}
							}
							document.getElementById("province").innerHTML = html;
							getCity();
						},
						error: function(){
							windowAlert("获取省份列表时发生网络错误,请重试")
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
					if ( window.type == "update" ){
						$("#name").val(window.addressData[window.index].name);
						$("#tel").val(window.addressData[window.index].tel);
						$("#detailed").val(window.addressData[window.index].detailed);
						$("#postNumber").val(window.addressData[window.index].postal);
					}
					else{
						$("#name").val("");
						$("#tel").val("");
						$("#detailed").val("");
						$("#postNumber").val("");
					}
				}
				function getCity(){
					var postData = {};
					postData.provinceId = document.getElementById("province").value;
					$.ajax({
						type: "get",
				        url: "api/MoutaiArea/FindCity",
						dataType: "json",
						data:postData,
						success: function(data){
							if ( !detectionResult(data,1) ){
								return;
							}
							var datas = data.result.data;
							var html = "";
							for ( var i=0;i<datas.length;i++ ){
								if ( window.type == "update" && window.addressData[window.index].city == datas[i].title ){
									html += "<option value='"+datas[i].cityId+"' selected>"+datas[i].title+"</option>";
								}
								else{
									html += "<option value='"+datas[i].cityId+"'>"+datas[i].title+"</option>";
								}
							}
							document.getElementById("city").innerHTML = html;
							if ( data.result.isUnder == 1 ){
								$("#city").closest(".return-attr").hide();
								getCounty();
							}
							else{
								$("#city").closest(".return-attr").show();
								getCounty();
							}
						},
						error: function(){
							windowAlert("获取城市列表时发生网络错误,请重试")
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
				}
				function getCounty(){
					var postData = {};
					postData.cityId = document.getElementById("city").value;
					$.ajax({
						type: "get",
				        url: "api/MoutaiArea/FindCountry",
						dataType: "json",
						data:postData,
						success: function(data){
							if ( !detectionResult(data,1) ){
								return;
							}
							var datas = data.result;
							var html = "";
							for ( var i=0;i<datas.length;i++ ){
								if ( window.type == "update" && window.addressData[window.index].county == datas[i].title ){
									html += "<option value='"+datas[i].countyId+"' selected>"+datas[i].title+"</option>";
								}
								else{
									html += "<option value='"+datas[i].countyId+"'>"+datas[i].title+"</option>";
								}
							}
							document.getElementById("county").innerHTML = html;
						},
						error: function(){
							windowAlert("获取区县列表时发生网络错误,请重试")
						},
						headers:{
							"x-moutai-token": window.token,
						}
					});
				}
				$(document).on("change","#province",function(){
					getCity(this.value);
				})
				$(document).on("change","#city",function(){
					getCounty(this.value);
				})
			//提交表单
				var loading = false;
				$(document).on("submit",".address-form",function(event){
					event.preventDefault();
					if ( loading ){
						return
					}
					var name = document.getElementById("name").value;
					var tel = document.getElementById("tel").value;
					var province = document.getElementById("province").value;
					var city = document.getElementById("city").value;
					var county = document.getElementById("county").value;
					var detailed = document.getElementById("detailed").value;
					var postNumber = document.getElementById("postNumber").value;
					if ( name == "" ){
						windowAlert("请填写收货人姓名");
						return;
					}
					if ( !checkTel(tel) ){
						windowAlert("请填写正确的手机号码");
						return;
					}
					if ( province == "" ){
						windowAlert("请选择省份");
						return;
					}
					if ( city == "" ){
						windowAlert("请选择城市");
						return;
					}
					if ( county == "" ){
						windowAlert("请选择区域");
						return;
					}
					if ( detailed == "" ){
						windowAlert("请填写详细地址");
						return;
					}
					if ( postNumber == "" ){
						windowAlert("请填写邮政邮编");
						return;
					}
					loading = true;
					var postData = {};
					if ( window.type == "add" ){
						postData.addressId = "";
					}
					else{
						postData.addressId = window.addressId;
					}
					postData.name = name;
					postData.tel = tel;
					postData.province = province;
					postData.city = city;
					postData.county = county;
					postData.detailed = detailed;
					postData.postal = postNumber;
					$.ajax({
						type: "post",
				        url: "api/CustomerAddress/EditAddress",
						dataType: "json",
						data:postData,
						beforeSend:function(){
							if ( window.type == "add" ){
								windowAlert("正在添加",0);
							}
							else{
								windowAlert("正在修改",0);
							}
						},
						success: function(data){
							loading = false;
							if ( !detectionResult(data,1) ){
								return;
							}
							if ( window.type == "add" ){
								windowAlertClose("添加成功");
								windowClose();
								getAddress();
							}
							else{
								windowAlertClose("修改成功");
								windowClose();
								getAddress();
							}
						},
						error: function(){
							if ( window.type == "add" ){
								windowAlertClose("添加收货地址时发生网络错误");
							}
							else{
								windowAlertClose("修改收货地址时发生网络错误");
							}
							loading = false;
						},
						headers:{
							"x-moutai-token": window.token,
						}
					})
				})
			//取消
				$(document).on("tap",".js-quxiao",function(event){
					event.preventDefault();
					windowClose();
				})
			//新增地址
				$(document).on("tap","#add-address",function(event){
					event.preventDefault();
					window.type = "add";
					if ( document.getElementById("add-dizhi") ){
						document.getElementById("province").innerHTML = "<option>请选择</option>";
						document.getElementById("city").innerHTML = "<option>请选择</option>";
						document.getElementById("county").innerHTML = "<option>请选择</option>";
					}
					var callBack = window.getProvince;
					windowOpen("add-dizhi","scroll",callBack);
				})
			//修改地址
				$(document).on("tap","#edit-address",function(event){
					event.preventDefault();
					if ( !$("#address-list").find("li.on").length ){
						windowAlert("请先选择一条地址信息！");
						return;
					}
					window.type = "update";
					window.addressId = $("#address-list").find("li.on").attr("data-id");
					for( var i=0;i<window.addressData.length;i++ ){
						if ( window.addressData[i].addressId == window.addressId ){
							window.index = i;
							break;
						}
					}
					var callBack = window.getProvince;
					windowOpen("add-dizhi","scroll",callBack);
				})
			//删除地址
				$(document).on("tap","#del-address",function(event){
					event.preventDefault();
					if ( loading ){
						return;
					}
					if ( !$("#address-list").find("li.on").length ){
						windowAlert("请先选择一条地址信息！");
						return;
					}
					if ( $("#address-list").find("li.on").hasClass("active") ){
						windowAlert("默认收货地址不能删除！");
						return;
					}
					trueFunction = function(){
						loading = true;
						var postData = {};
						postData.addressId = $("#address-list").find("li.on").attr("data-id");
						$.ajax({
							type: "get",
					        url: "api/CustomerAddress/Remove",
							dataType: "json",
							data:postData,
							beforeSend:function(){
								windowAlert("正在删除");
							},
							success: function(data){
								loading = false;
								if ( !detectionResult(data,1) ){
									return;
								}
								windowAlertClose("删除成功");
								windowClose();
								getAddress();
							},
							error: function(){
								windowAlert("删除收货地址时发生网络错误");
								loading = false;
							},
							headers:{
								"x-moutai-token": window.token,
							}
						})
					}
					windowConfirm("确认删除？","否","删除");
				})
		})
})
