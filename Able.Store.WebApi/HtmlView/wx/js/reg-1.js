/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function(){
	var provinceData = "";
	var username = nickname ? nickname + "" : "";
	document.getElementById("name").value = username;
	//省份下拉框
//	$(document).on("change","#area",function(){
//		var value = this.value;
//		if ( value == 1 ){
//			$(".select[data-type=province]").show();
//			if ( provinceData == "" ){
//			//获取省份
////				getProvince();
//			}
//		}
//		else{
//			$(".select[data-type=province]").hide();
//			$(".select[data-type=city]").hide();
//		}
//	})

    //地区下拉框
//	$(document).on("change","#province",function(){
//		var value = this.value;
//		if ( value == 0 ){
//			$(".select[data-type=city]").hide();
//		}
//		else{
//			var zhixiashi = false;
//			if ( value == "110000" || value == "120000" || value == "310000" || value == "500000" ){
//				zhixiashi = true;
//				$(".select[data-type=city]").hide();
//			}
//			else{
//				$(".select[data-type=city]").show();
//			}
//			var datas;
//			for ( var i=0;i<provinceData.length;i++ ){
//				if ( value == provinceData[i].adcode ){
//					datas = provinceData[i].citys;
//				}
//			}
//			var html = "<option value='0'>点击选择城市</option>";
//			for ( var i=0;i<datas.length;i++ ){
//				if ( zhixiashi ){
//					html += "<option value='"+ datas[i].citycode +"' selected='selected'>"+ datas[i].cityname +"</option>";
//				}
//				else{
//					html += "<option value='"+ datas[i].citycode +"'>"+ datas[i].cityname +"</option>";
//				}
//			}
//			document.getElementById("city").innerHTML = html;
//		}
//	})
	var loading = false;
	$(document).on("submit",".form",function(event){
		event.preventDefault();
		if ( loading ){
			return
		}
		var name = document.getElementById("name").value;
		var sex = document.getElementById("sex").value;
		//选择地区赋值
//		var area = document.getElementById("area").value;
//		var province = document.getElementById("province").value;
//		var city = document.getElementById("city").value;
		if ( name == "" ){
			windowAlert("请输入昵称");
			return
		}
		//选择地区判断
//		if ( area == 0 ){
//			windowAlert("请选择地区");
//			return
//		}
//		if ( area == 1 ){
//			if ( province == 0 ){
//				windowAlert("请选择省份");
//				return
//			}
//			if ( city == 0 ){
//				windowAlert("请选择城市");
//				return
//			}
//		}
		loading = true;
		$.ajax({
			type: "post",
	        url: baseapi_url + "/h5/web/user/check/nickname",
//			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data:{"nickname":document.getElementById("name").value},
			success: function(data){
				if ( !detectionResult(data,1) ){
					loading = false;
					return;
				}
				window.localStorage.nickname = name;
				window.localStorage.sex = sex;
				
				//本地保存地址等
//				var option = document.getElementById("area").getElementsByTagName("option");
//				for ( var i=0;i<option.length;i++ ){
//					if ( option[i].value == area ){
//						window.areaText = "";
//						window.areaText = option[i].innerHTML;
//						break;
//					}
//				}
//				window.localStorage.areaText = areaText;
//				window.localStorage.area = area;
//				var option = document.getElementById("province").getElementsByTagName("option");
//				for ( var i=0;i<option.length;i++ ){
//					if ( option[i].value == province ){
//						window.provinceText = "";
//						if ( option[i].value != 0 ){
//							window.provinceText = option[i].innerHTML;
//						}
//						break;
//					}
//				}
//				window.localStorage.provinceText = provinceText;
//				window.localStorage.province = province;
//				var option = document.getElementById("city").getElementsByTagName("option");
//				for ( var i=0;i<option.length;i++ ){
//					if ( option[i].value == city ){
//						window.cityText = "";
//						if ( option[i].value != 0 ){
//							window.cityText = option[i].innerHTML;
//						}
//						break;
//					}
//				}
//				window.localStorage.cityText = cityText;
//				window.localStorage.city = city;
				window.location.href = "reg-2.html";
			},
			error:function(){
				windowAlert("网络错误，请重试");
			},
			headers:{
				"x-stbl-token": window.token,
			}
		});
	})
	function getProvince(){
		$.ajax({
			type: "post",
	        url: baseapi_url + "/h5/web/common/citytree/show",
//			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data:{"filter":"2"},
			success: function(data){
				if ( !detectionResult(data,1) ){
					return;
				}
				provinceData = data.result;
				var html = "<option value='0'>点击选择省份</option>";
				for ( var i=0;i<provinceData.length;i++ ){
					html += "<option value='"+ provinceData[i].adcode +"'>"+ provinceData[i].provincename +"</option>";
				}
				document.getElementById("province").innerHTML = html;
			},
			error:function(){
				windowAlert("网络错误，请重试");
			},
			headers:{
				"x-stbl-token": window.token,
			}
		});
	}
})