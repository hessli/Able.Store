/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function(){
	var loading = false;
	$(".now-tel").html("当前号码 +"+ window.localStorage.prefix +" " + spaceTel(window.localStorage.tel) );
	$(document).on("submit",".form",function(event){
		event.preventDefault();
		if ( loading ){
			return;
		}
		var code = document.getElementById("code").value;
		if ( code == "" ){
			windowAlert("请输入收到的验证码");
			return
		}
		loading = true;
		$(".submit").find("input").val("正在注册...");
		if ( typeof(window.localStorage.base64) == "undefined" ){
			window.base64 = "";
		}
		else{
			window.base64 = window.localStorage.base64.substring(22,window.localStorage.base64.length);
		}
		//验证成功后注册
		var postData = {};
		postData.areacode = window.localStorage.prefix;
		postData.phone = window.localStorage.tel;
		postData.pwd = window.localStorage.password;
		postData.nickname = window.localStorage.nickname;
		postData.verifycode = code;
		postData.opentype = 1;
		//postData.openid = strEnc(window.localStorage.token,"stbl.com");
		postData.openid = window.localStorage.token;
		postData.unionid = window.localStorage.unionid;
		postData.sex = window.localStorage.sex;
		postData.headimgurl = window.localStorage.headimgurl;
		postData.country = window.localStorage.area;
		postData.city = window.localStorage.city;
		postData.province = window.localStorage.province;
		postData.base64 = base64,
		postData.language = "简体中文";
		$.ajax({
			type: "post",
	        url: baseapi_url + "/h5/web/user/register",
//			contentType: "application/json; charset=utf-8",
			data:postData,
			dataType: "json",
			success: function(data){
				if ( !detectionResult(data,1) ){
					$(".submit").find("input").val("开始师徒部落之旅");
					loading = false;
					return;
				}
				window.localStorage.headimgurl = data.result.imgurl;
				window.localStorage.removeItem("base64");
				$(".submit").find("input").val("注册成功");
				window.localStorage.accesstoken = data.result.accesstoken;
				window.location.href = "select-master.html";
			},
			error:function(){
				windowAlert("网络错误，请重试");
				$(".submit").find("input").val("开始师徒部落之旅");
				loading = false;
			},
			headers:{
				"x-stbl-token": window.token,
			}
		});
	})
	var send = true;
	var time = 90;
	var waitTime = 0;
	var first = true;
	function jiTime(){
		waitTime = time;
		autoTime = setInterval(function(){
			waitTime --;
			if ( waitTime == 0 ){
				clearInterval(autoTime);
				$("#again").html("重发验证码");
				send = false;
				waitTime = time;
			}
			else{
				$("#again").html( waitTime + "秒可再次发送" );
			}
		},1000)
	}
	function sendMessage(){
		var postData = {};
		postData.areacode = window.localStorage.prefix;
		postData.phone = window.localStorage.tel;
		$.ajax({
			type: "post",
	        url: baseapi_url + "/h5/web/auth/phonecode/get",
//			contentType: "application/json; charset=utf-8",
			data:postData,
			dataType: "json",
			beforeSend:function(){
				$("#again").html("正在发送");
			},
			success: function(data){
				if ( !detectionResult(data,1) ){
					$("#again").html("重发验证码");
					send = false;
					return;
				}
				this.innerHTML = "验证码发送成功";
				if ( first ){
					first = false;
					setTimeout(function(){
						$("#again").html("重发验证码");
					},1000)
					send = false;
				}
				else{
					setTimeout(function(){
						jiTime();
					},1000)
				}
			},
			error:function(){
				windowAlert("网络错误，请重试");
				$("#again").html("重发验证码");
				send = false;
			},
			headers:{
				"x-stbl-token": window.token,
			}
		});
	}
	$(document).on("tap","#again",function(event){
		event.preventDefault();
		if ( send ){
			return;
		}
		send = true;
		sendMessage();
	})
	sendMessage();
})