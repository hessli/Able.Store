var MessageClient = function (app_key,access_token) {
    this._app_key = app_key;
    this._access_token = access_token;
    this._new_message_listeners = new Array();
}
MessageClient.prototype.set_app_key = function (key) {
    this._app_key = key;
    return this;
}
MessageClient.prototype.get_app_key = function () {
    return this._app_key;
}
MessageClient.prototype.get_access_token = function () {
    return this._access_token;
}
MessageClient.prototype.set_access_token = function (token) {
    this._access_token = token;
    return this;
}
MessageClient.prototype.init = function (new_message_call_back) {
    var _that_client = this;
    initRongYun(_that_client.get_app_key(), _that_client.get_access_token(), new_message_call_back);
    return this;
}

///不需要再这里抓，从服务器获取
MessageClient.prototype.get_history_message = function (targetUserid, limit, call_back) {//从融云获取,考虑是从服务器获取
    RongIMClient.getInstance().getHistoryMessages(RongIMLib.ConversationType.PRIVATE, targetUserid, null, limit, {
        onSuccess: function (list, hasMsg) {
            // hasMsg为boolean值，如果为true则表示还有剩余历史消息可拉取，为false的话表示没有剩余历史消息可供拉取。
            // list 为拉取到的历史消息列表
            console.log("Get History messages.");
            if (call_back != undefined) {
                call_back(list, hasMsg);
            }
            
        },
        onError: function (error) {
            // APP未开启消息漫游或处理异常
            // throw new ERROR ......
        }
    });
}


///不需要再这里发，提交到服务器用server api发
MessageClient.prototype.sendMessage = function (to, content,call_back) {
    var msg = new RongIMLib.TextMessage({ content: content, extra: "" });//extra 可以保存数据库id
    //或者使用RongIMLib.TextMessage.obtain 方法.具体使用请参见文档
    //var msg = RongIMLib.TextMessage.obtain("hello");
    var conversationtype = RongIMLib.ConversationType.PRIVATE; // 私聊
    var targetId = to; // 目标 Id
    RongIMClient.getInstance().sendMessage(conversationtype, targetId, msg, {
        // 发送消息成功
        onSuccess: function (message) {
            //message 为发送的消息对象并且包含服务器返回的消息唯一Id和发送消息时间戳
            console.log(message);
            console.log("Send successfully");
            if (call_back != undefined) {
                call_back(message);
            }
        },
        onError: function (errorCode, message) {
            var info = '';
            switch (errorCode) {
                case RongIMLib.ErrorCode.TIMEOUT:
                    info = '超时';
                    break;
                case RongIMLib.ErrorCode.UNKNOWN_ERROR:
                    info = '未知错误';
                    break;
                case RongIMLib.ErrorCode.REJECTED_BY_BLACKLIST:
                    info = '在黑名单中，无法向对方发送消息';
                    break;
                case RongIMLib.ErrorCode.NOT_IN_DISCUSSION:
                    info = '不在讨论组中';
                    break;
                case RongIMLib.ErrorCode.NOT_IN_GROUP:
                    info = '不在群组中';
                    break;
                case RongIMLib.ErrorCode.NOT_IN_CHATROOM:
                    info = '不在聊天室中';
                    break;
                default:
                    info = x;
                    break;
            }
            console.log('发送失败:' + info);
        }
    }
    );
}


function initRongYun(app_key, access_token, new_message_call_back) {
    RongIMClient.init(app_key);
    RongIMClient.setConnectionStatusListener({
        onChanged: function (status) {
            switch (status) {
                //链接成功
                case RongIMLib.ConnectionStatus.CONNECTED:
                    console.log('链接成功');
                    break;
                    //正在链接
                case RongIMLib.ConnectionStatus.CONNECTING:
                    console.log('正在链接');
                    break;
                    //重新链接
                case RongIMLib.ConnectionStatus.DISCONNECTED:
                    console.log('断开连接');
                    break;
                    //其他设备登陆
                case RongIMLib.ConnectionStatus.KICKED_OFFLINE_BY_OTHER_CLIENT:
                    console.log('其他设备登陆');
                    break;
                    //网络不可用
                case RongIMLib.ConnectionStatus.NETWORK_UNAVAILABLE:
                    console.log('网络不可用');
                    break;
            }
        }
    });

    // 消息监听器
    RongIMClient.setOnReceiveMessageListener({
        // 接收到的消息
        onReceived: function (message) {
            console.log("new message ");
            console.log(message);            
            if (typeof (new_message_call_back) != "undefined") {
                new_message_call_back(message);
            }             
        }
    });
    console.log("connect by token:" + access_token);
    RongIMClient.connect(access_token, {
        onSuccess: function (userId) {
            console.log("Login successfully." + userId);
        },
        onTokenIncorrect: function () {
            console.log('token无效');
        },
        onError: function (errorCode) {
            var info = '';
            switch (errorCode) {
                case RongIMLib.ErrorCode.TIMEOUT:
                    info = '超时';
                    break;
                case RongIMLib.ErrorCode.UNKNOWN_ERROR:
                    info = '未知错误';
                    break;
                case RongIMLib.ErrorCode.UNACCEPTABLE_PaROTOCOL_VERSION:
                    info = '不可接受的协议版本';
                    break;
                case RongIMLib.ErrorCode.IDENTIFIER_REJECTED:
                    info = 'appkey不正确';
                    break;
                case RongIMLib.ErrorCode.SERVER_UNAVAILABLE:
                    info = '服务器不可用';
                    break;
            }
            console.log(errorCode);
        }
    });
}
