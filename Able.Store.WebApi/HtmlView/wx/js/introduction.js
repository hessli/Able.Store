/*
 * 全站公共脚本,基于jquery-2.1.1脚本库
*/
$(function(){
	var sex = window.localStorage.sex;
	if ( sex == 0 ){
		sex = "♂";
	}
	else{
		sex = "♀";
	}
	var area = "";
	if ( window.localStorage.cityText == "" ){
		area = window.localStorage.areaText;
	}
	else{
		area = window.localStorage.cityText;
	}
	var dl = $(".introduction").find("dl").eq(1).find(".user-box");
	var dl_head=window.localStorage.headimgurl;
	var dl_images="images/icon_user.jpg";
	if(dl_head){
		if(dl_head == "undefined"){
			var dl_top = '<img src="'+ dl_images +'"/>';
		}
		else{
        	var dl_top="<img src='"+ dl_head +"' />";
		}
	}else{
        var dl_top='<img src="'+ dl_images +'"/>';
	}
    // $(".user-photo").css({"border":"none","border-radius":"0"})
	dl.find(".user-photo").html(dl_top);
	var mini=window.localStorage.nickname;
	if(mini){
		d_mini=mini;
	}else{
		d_mini="MINI";
	}
	dl.find("h3").html(d_mini);
	dl.find(".areas").html(area);
	dl.find(".xb").html(sex);
	var sex = window.localStorage.sex;
	if (sex == 0) {
		$(".xb").addClass("man");
	}
	var dl = $(".introduction").find("dl").eq(0).find(".user-box");
	var di_img=window.localStorage.groupimg;
	var di_images="images/icon_bangqun_moren.png";
	if(di_img){
		var di_find='<img src="'+ di_img +'"/><img src="images/icon_group_bg.png" class="user-photo-img"/>'
	}else{
        var di_find='<img src="'+ di_images +'"/>';
	};
	dl.find(".user-photo").html(di_find);
	var omak=window.localStorage.groupname;
	if(omak){
		var otext=omak;
	}else{
        var otext="师傅的群";
	};
	dl.find("h3").html(otext).next().html(window.localStorage.groupdesc);
})

