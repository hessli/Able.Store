/**
 * 
 */
var JSSDK = function (title, desc, image) {
    this._share_title = title;
    this._share_desc = desc;
    this._share_image = image;
    this._share_url = location.href;
}
JSSDK.prototype.init = function (call_back) {
    $that = this;
    $.ajax({
        url: "http://www.shifugroup.net:3821/home/getsignpackage",
        data: { url: this._share_url },
        dataType: "json",
        success: function (rsp) {
            wx.config({
                debug: false,
                appId: rsp.appId,
                timestamp: rsp.timestamp,
                nonceStr: rsp.nonceStr,
                signature: rsp.signature,
                jsApiList: [
                  'onMenuShareAppMessage', 'onMenuShareTimeline', 'onMenuShareQQ', 'onMenuShareWeibo'
                ]
            });
            wx.ready(function () {
                $that.change_share({});
                if (typeof (call_back) != "undefined") {
                    call_back();
                }
            });

        }
    });
}
/**
 * 分享
 * @param {title,desc,url,image} change 
 * @returns {} 
 */
JSSDK.prototype.change_share = function (change) {
    //window.alert(JSON.stringify(change));
    if (change.title != undefined && change.title != '') {
        this._share_title = change.title;
    }
    if (change.desc != undefined && change.desc != '') {
        this._share_desc = change.desc;
    }
    if (change.url != undefined && change.url != '') {
        this._share_url = change.url;
    }
    if (change.image != undefined && change.image != '') {
        this._share_image = change.image;
    }

    var share_data = {
        title: this._share_title,
        desc: this._share_desc,
        link: this._share_url,
        imgUrl: this._share_image,
        type: 'link',
        dataUrl: '',
        success: function () {
            if (typeof (shareSuccess) != "undefined") {
                shareSuccess();
            }
        },
        cancel: function () {
        }
    };

    var share_data_timeline = {
        title: this._share_desc,
        desc: this._share_desc,
        link: this._share_url,
        imgUrl: this._share_image,
        type: 'link',
        dataUrl: '',
        success: function () {
            if (typeof (shareSuccess) != "undefined") {
                shareSuccess();
            }
        },
        cancel: function () {
        }
    };

    wx.onMenuShareAppMessage(share_data);
    wx.onMenuShareTimeline(share_data_timeline);
    wx.onMenuShareQQ(share_data);
    wx.onMenuShareWeibo(share_data);
}

JSSDK.prototype.shareDefault = function () {
    var title = getMetaContent("sharetitle", "师徒部落官方");
    var desc = getMetaContent("sharedesc", "师徒部落，让你马上玩转师徒部落，玩转人脉");
    var defaultImg = $("img").first().attr("src");
    if (!defaultImg) {
        defaultImg = "http://www.shifugroup.net/app/qa/images/img_suoluetu.jpg";
    }
    var img = getMetaContent("shareimgurl", defaultImg);

    console.log({
        title: title,
        desc: desc,
        defaultImg: defaultImg
    });

    this.init();
    var change = {
        title: title,
        desc: desc,
        image: img,
        url: window.location.href
    };
    this.change_share(change);
}


function getMetaContent(name, defaultValue) {
    var content = $("meta[name=" + name + "]").first().attr("content");
    if (!content)
        return defaultValue;
    return content;
}



