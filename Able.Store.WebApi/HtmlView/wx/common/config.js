//js的全局变量

var baseapi_url = "http://test-api.shifugroup.net";//数据总接口
var baseimg_url = "http://test-upload.shifugroup.net";//图像
var company_url = "http://www.stbl.cc"; //公司的域名接口
var weixinauth_url = "http://wx-auth.stbl.cc";//微信授权



//app下载 20160502
//ios -> appstore
//安卓 -》 微信 -》应用宝 
//其他浏览器 -> 服务器下载

var weixin_link = 'http://a.app.qq.com/o/simple.jsp?pkgname=com.stbl.stbl';
var official_link = company_url + '/download/stbl_release.apk';
var itunes_link = 'https://itunes.apple.com/us/app/shi-tu-bu-luo/id1099608943?l=zh&ls=1&mt=8';

function appdownload() {
    if (/MicroMessenger/i.test(navigator.userAgent)) {
        //if(/(iPhone|iPad|iPod|iOS)/i.test(navigator.userAgent)){
        window.location.href = weixin_link;
        //}
        //else{
        //	alert("请使用系统浏览器打开本页面进行下载");
        //alert("即将上线,敬请期待");
        //	return;
        //}
    }
    else {
        if (/(iPhone|iPad|iPod|iOS)/i.test(navigator.userAgent)) {
            window.open(itunes_link);
        }
        else {
            window.open(official_link);
            //alert("即将上线,敬请期待");
            //return;
        }
    }
};
