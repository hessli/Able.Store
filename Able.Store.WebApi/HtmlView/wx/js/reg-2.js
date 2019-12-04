/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function(){
//	$(document).on("change","#select-country",function(){
//		var value = this.value;
//		var option = document.getElementById("select-country").getElementsByTagName("option");
//		for ( var i=0;i<option.length;i++ ){
//			if ( value == option[i].value ){
//				document.getElementById("country").value = option[i].innerHTML;
//				window.prefix = option[i].getAttribute("data-prefix");
//				break;
//			}
//		}
//	})
	var loading = false;
	function getCounty(){
		$.ajax({
			type: "post",
	        url: baseapi_url + "/h5/web/common/mobileprefix/query",
//			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function(data){
				if ( !detectionResult(data,1) ){
					return;
				}
				var datas = data.result;
				var html = "";
				for ( var i=0;i<datas.length;i++ ){
					html += "<option value='"+ datas[i].id +"' data-prefix='"+ datas[i].prefix +"'>"+ datas[i].country +"</option>";
					if ( i == 0 ){
						window.prefix = datas[i].prefix;
					}
				}
				document.getElementById("country").innerHTML = html;
				loading = false;
			},
			error:function(){
				windowAlert("网络错误，请重试");
				getCounty();
			},
			headers:{
				"x-stbl-token": window.token,
			}
		});
	}
	getCounty();
	
	$(document).on("submit",".form",function(event){
		event.preventDefault();
		if ( loading ){
			return;
		}
		var country = document.getElementById("country").value;
		var tel = document.getElementById("tel").value;
		var password = document.getElementById("password").value;
		if ( !checkTel(tel) && country == 1 ){
			windowAlert("请输入正确的手机号码");
			return
		}
		if ( password.length < 6 ){
			windowAlert("密码长度不少得于6位");
			return
		}
		var option = document.getElementById("country").getElementsByTagName("option");
		for ( var i=0;i<option.length;i++ ){
			if ( option[i].value == country ){
				window.countryText = "";
				if ( option[i].value != 0 ){
					window.countryText = option[i].innerHTML;
					window.prefix = option[i].getAttribute("data-prefix");
				}
				break;
			}
		}
		$.ajax({
			type: "post",
	        url: baseapi_url + "/h5/web/user/check/phone",
//			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data:{"phone":tel,"areacode":prefix},
			success: function(data){
				if ( !detectionResult(data,1) ){
					loading = false;
					return;
				}
				window.localStorage.country = country;
				window.localStorage.countryText = countryText;
				window.localStorage.prefix = prefix;
				window.localStorage.tel = tel;
				window.localStorage.password = md5(password);
				window.location.href = "reg-3.html";
			},
			error:function(){
				windowAlert("网络错误，请重试");
			},
			headers:{
				"x-stbl-token": window.token,
			}
		});
	})
})