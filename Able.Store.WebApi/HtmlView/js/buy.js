$(function () {
    //授权成功&&店铺状态正常 执行
    $.when(window.shopState).done(function () {
        var skuId = $.query.get("skuids").toString();
        var postData = {};
        postData.skuId = [];
        skuId = skuId.split(",");
        for (var i = 0; i < skuId.length; i++) {
            postData.skuId.push(skuId[i]);
        }
        window.skuId = postData.skuId;
        $.ajax({
            type: "post",
            url: window.baseurl + "api/shopping/getbasketbysku",
            dataType: "json",
            data: postData,
            traditional: true,
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                var datas = data.result;
                if (datas.length == 0) {
                    windowAlert("您所指定的订单不存在", 0);
                    return
                }
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    html += "<li class='order-list-item' data-price='" + datas[i].price.toFixed(2) + "' data-number='" + datas[i].qty + "'><div class='order-img'><img src='' loadsrc='" + datas[i].img + "' /></div><h3>" + datas[i].title + "</h3><div class='sx'><p>" + datas[i].propertyname + "" + datas[i].propertyvalue + "</p></div><div class='shu'><strong>￥" + datas[i].price.toFixed(2) + "</strong>× <em>" + datas[i].qty + "</em></div></li>";
                }
                $("#list").html(html).show();
                loadsrc();
                $(".liuyan").css({ "visibility": "visible" });
                $(".ul-attr").show();
                window.price = 0;
                $("#list li").each(function () {
                    price += $(this).attr("data-price") * $(this).attr("data-number");
                })
                $("#price").html("￥" + price.toFixed(2));
                window.yunfei = 0;
                $("#yunfei,#yunfeis").html("￥" + yunfei.toFixed(2));
                window.total = (window.price + window.yunfei).toFixed(2);
                $("#total,#totals").html("￥" + total);
                $(".buy-jine,.footer-bar").show();
                $.ajax({
                    type: "get",
                    url: window.baseurl + "api/user/getdefault",
                    dataType: "json",
                    success: function (data) {
                        if (!detectionResult(data, 0)) {
                            return;
                        }
                        if (typeof (data.result.name) != "undefined") {
                            $(".liuyan").after("<div class='address buy-address' data-id='" + data.result.receiverId + "'><h2>" + data.result.name + "</h2><div class='mob'>" + data.result.tel + "</div><p>" + data.result.province + data.result.city + data.result.area + data.result.detailed + "</p></div>");
                            $(".js-address").hide();
                        }
                    },
                    error: function () {
                        windowAlert("网络错误,刷新重试", 0)
                    },
                    headers: {
                        "x-moutai-token": window.token,
                    }
                });
            },
            error: function () {
                windowAlert("网络错误,请刷新重试");
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        window.getAddress = function () {
            $.ajax({
                type: "get",
                url: window.baseurl + "api/user/getreceiver",
                dataType: "json",
                success: function (data) {
                    if (!detectionResult(data, 0)) {
                        return;
                    }
                    var datas = data.result;
                    window.addressData = datas;
                    if (datas.length == 0) {
                        $("#address-list-open").html("<div class='get-more-button'>暂无地址，请添加</div>");
                        return
                    }
                    var html = "";
                    for (var i = 0; i < datas.length; i++) {
                        if (datas[i].isDefault == 1) {
                            html += "<li class='active on' data-id='" + datas[i].receiverId + "'><h2>" + datas[i].name + "<em>[默认]</em></h2><div class='mob'>" + datas[i].tel + "</div><p>" + datas[i].province + datas[i].city + datas[i].area + datas[i].detailed + "</p></li>";
                        }
                        else {
                            html += "<li data-id='" + datas[i].receiverId + "'><h2>" + datas[i].name + "</h2><div class='mob'>" + datas[i].tel + "</div><p>" + datas[i].province + datas[i].city + datas[i].area + datas[i].detailed + "</p></li>";
                        }
                    }
                    $("#address-list-open").html(html);
                    if (typeof (window.scroll) != "undefined") {
                        window.scroll.refresh();
                    }
                },
                error: function () {
                    windowAlert("网络错误,刷新重试", 1)
                },
                headers: {
                    "x-moutai-token": window.token,
                }
            });
        }
    })
    //发票信息
    $(document).on("tap", ".js-fapiao", function (event) {
        event.preventDefault();
        windowOpen("fapiao");
    })
    $(document).on("tap", "#fapiao .go-pay", function (event) {
        event.preventDefault();
        if ($("#fapiao").find("li.on").index() == 0) {
            $(".js-fapiao").find("p").html($("#fapiao").find("li.on").html());
            windowClose();
        }
        else {
            var textarea = $("#fapiao").find("textarea");
            if (textarea.val() == "") {
                $("#fapiao").find("li").removeClass("on").eq(0).addClass("on");
                $(".js-fapiao").find("p").html($("#fapiao").find("li.on").html());
                windowClose();
            }
            else {
                $(".js-fapiao").find("p").html(textarea.val());
                windowClose();
            }
        }
        $("#fapiao").find("textarea").blur();
    })
    //地址
    $(document).on("tap", ".js-address,.buy-address", function (event) {
        event.preventDefault();
        var callBack = window.getAddress;
        windowOpen("address", "scroll", callBack);
    })
    $(document).on("tap", "#select-address", function (event) {
        event.preventDefault();
        if ($("#address").find(".on").length) {
            $(".buy-address").remove();
            $(".liuyan").after("<div class='address buy-address' data-id='" + $("#address-list-open li.on").attr("data-id") + "'>" + $("#address").find(".on").html() + "</div>")
            $(".buy-address").find("h2 em").remove();
            $(".buy-address").find(".edit").remove();
            $(".js-address").hide();
            windowClose();
        }
        else {
            windowAlert("请先选择一条地址信息！");
        }
    });


    //设置默认地址
    var loading = false;
    $(document).on("tap", "#set-address", function (event) {
        event.preventDefault();
        if ($("#address-list-open").find("li.on").hasClass("active")) {
            windowAlert("已是默认收货地址");
            return;
        }
        if (!$("#address-list-open").find("li.on").length) {
            windowAlert("请先选择一条地址信息！");
            return;
        }
        if (loading) {
            return;
        }
        loading = true;
        windowAlert("正在设置默认收货地址", 0);
        var postData = {};
        postData.receiverid = $("#address-list-open").find("li.on").attr("data-id");
        $.ajax({
            type: "post",
            url: window.baseurl + "api/user/setdefault",
            dataType: "json",
            data: postData,
            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                windowAlertClose("设置成功");
                var active = $("#address-list-open").find(".active");
                var em = active.find("h2 em");
                var on = $("#address-list-open").find("li.on");
                if (!active.hasClass("on")) {
                    on.addClass("active").find("h2").append(em);
                    active.removeClass("active");
                }
                loading = false;
            },
            error: function () {
                windowAlert("网络错误,请重试")
                loading = false;
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
    })
    //新增地址
    $(document).on("tap", ".js-add-address", function (event) {
        event.preventDefault();
        window.type = "add";
        if (document.getElementById("add-dizhi")) {
            document.getElementById("province").innerHTML = "<option>请选择</option>";
            document.getElementById("city").innerHTML = "<option>请选择</option>";
            document.getElementById("county").innerHTML = "<option>请选择</option>";
        }
        var callBack = window.getProvince;
        windowOpen("add-dizhi", "scroll", callBack);
    })
    //修改地址
    $(document).on("tap", ".js-edit-address", function (event) {
        event.preventDefault();
        if (!$("#address-list-open").find("li.on").length) {
            windowAlert("请先选择一条地址信息！");
            return;
        }
        window.type = "update";
        window.receiverId = $("#address-list-open").find("li.on").attr("data-id");
        for (var i = 0; i < window.addressData.length; i++) {
            if (window.addressData[i].receiverId == window.receiverId) {
                window.index = i;
                break;
            }
        }
        var callBack = window.getProvince;
        windowOpen("add-dizhi", "scroll", callBack);
    })
    //取消
    $(document).on("tap", ".js-quxiao", function (event) {
        event.preventDefault();
        windowCloseSingle("add-dizhi");
    })
    //获取省市列表
    window.getProvince = function () {
        $.ajax({
            type: "get",
            url: window.baseurl + "api/adtrative/getprovince",
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {

                    if (window.type == "update" && window.addressData[window.index].province == datas[i].name) {
                        html += "<option value='" + datas[i].code + "' selected>" + datas[i].name + "</option>";
                    }
                    else {
                        html += "<option value='" + datas[i].code + "'>" + datas[i].name + "</option>";
                    }
                }
                document.getElementById("province").innerHTML = html;
                getCity();
            },
            error: function () {
                windowAlert("网络错误,请重试")
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        if (window.type == "update") {
            $("#name").val(window.addressData[window.index].name);
            $("#tel").val(window.addressData[window.index].tel);
            $("#detailed").val(window.addressData[window.index].detailed);
            $("#postNumber").val(window.addressData[window.index].postal);
        }
        else {
            $("#name").val("");
            $("#tel").val("");
            $("#detailed").val("");
            $("#postNumber").val("");
        }
    }
    function getCity() {

        var provinceId = document.getElementById("province").value;
        $.ajax({
            type: "get",
            url: window.baseurl + "api/adtrative/getcities?code=" + provinceId,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    if (window.type == "update" && window.addressData[window.index].city == datas[i].name) {
                        html += "<option value='" + datas[i].code + "' selected>" + datas[i].name + "</option>";
                    }
                    else {
                        html += "<option value='" + datas[i].code + "'>" + datas[i].name + "</option>";
                    }
                }
                document.getElementById("city").innerHTML = html;
                if (data.result.isUnder == 1) {
                    $("#city").closest(".return-attr").hide();
                    getCounty();
                }
                else {
                    $("#city").closest(".return-attr").show();
                    getCounty();
                }
            },
            error: function () {
                windowAlert("网络错误,请重试")
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
    }
    function getCounty() {

        var cityId = document.getElementById("city").value;
        $.ajax({
            type: "get",
            url: window.baseurl + "api/adtrative/getareas?code=" + cityId,
            dataType: "json",

            success: function (data) {
                if (!detectionResult(data, 1)) {
                    return;
                }
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    if (window.type == "update" && window.addressData[window.index].area == datas[i].name) {
                        html += "<option value='" + datas[i].code + "' selected>" + datas[i].name + "</option>";
                    }
                    else {
                        html += "<option value='" + datas[i].code + "'>" + datas[i].name + "</option>";
                    }
                }
                document.getElementById("county").innerHTML = html;
            },
            error: function () {
                windowAlert("网络错误,请重试")
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
    }
    $(document).on("change", "#province", function () {
        getCity(this.value);
    })
    $(document).on("change", "#city", function () {
        getCounty(this.value);
    })
    //提交表单
    var loading = false;
    $(document).on("submit", ".address-form", function (event) {
        event.preventDefault();
        if (loading) {
            return
        }
        var name = document.getElementById("name").value;
        var tel = document.getElementById("tel").value;
        var province = $('#province option:selected').text(); //document.getElementById("province").text;
        var provincecode = $('#province option:selected').val();
        var citycode = $('#city option:selected').val();
        var city = $('#city option:selected').text();      // document.getElementById("city").text;
        var county = $('#county option:selected').text();  //document.getElementById("county").text;
        var countycode = $('#county option:selected').val();
        var detailed = document.getElementById("detailed").value;
        var postNumber = document.getElementById("postNumber").value;
        if (name == "") {
            windowAlert("请填写收货人姓名");
            return;
        }
        if (!checkTel(tel)) {
            windowAlert("请填写正确的手机号码");
            return;
        }
        if (province == "") {
            windowAlert("请选择省份");
            return;
        }
        if (city == "") {
            windowAlert("请选择城市");
            return;
        }
        if (county == "") {
            windowAlert("请选择区域");
            return;
        }
        if (detailed == "") {
            windowAlert("请填写详细地址");
            return;
        }
        if (postNumber == "") {
            windowAlert("请填写邮政邮编");
            return;
        }
        loading = true;
        var postData = {};
        if (window.type == "add") {
            postData.receiverId = "";
        }
        else {
            postData.receiverId = window.receiverId;
        }
        postData.name = name;
        postData.tel = tel;
        postData.province = province;
        postData.provincecode = provincecode;
        postData.city = city;
        postData.citycode = citycode;
        postData.areacode = countycode;
        postData.area = county;
        postData.detailed = detailed;
        postData.postal = postNumber;
        $.ajax({
            type: "post",
            url: window.baseurl + "api/user/createreceiver",
            dataType: "json",
            data: postData,
            beforeSend: function () {
                if (window.type == "add") {
                    windowAlert("正在添加", 0);
                }
                else {
                    windowAlert("正在修改", 0);
                }
            },
            success: function (data) {
                loading = false;
                if (!detectionResult(data, 1)) {
                    return;
                }
                if (window.type == "add") {
                    windowAlertClose("添加成功");
                    windowCloseSingle("add-dizhi");
                    getAddress();
                }
                else {
                    windowAlertClose("修改成功");
                    windowCloseSingle("add-dizhi");
                    getAddress();
                }
            },
            error: function () {
                if (window.type == "add") {
                    windowAlertClose("网络错误，请重试");
                }
                else {
                    windowAlertClose("网络错误，请重试");
                }
                loading = false;
            },
            headers: {
                "x-moutai-token": window.token,
            }
        })
    });


    //快递信息
    $(document).on("tap", ".js-kuaidi", function (event) {
        event.preventDefault();
        windowOpen("kuaidi");
    })
    $(document).on("tap", "#kuaidi .go-pay", function (event) {
        event.preventDefault();
        $(".js-kuaidi").find("p").html($("#kuaidi").find("li.on").html());
        windowClose();
    })
    //确认支付,创建订单
    $(document).on("tap", "#buy", function (event) {
        event.preventDefault();
        if (loading) {
            return;
        }
        if (!$(".buy-address").length) {
            windowAlert("请填写收货地址！");
            return
        }
        loading = true;
        var postData = {};

        postData.skuId = window.skuId;
        postData.message = document.getElementById("message").value;
        postData.receiverid = $(".buy-address").attr("data-id");
        postData.mode = $(".js-kuaidi").find("p").html();
        var invoicetitle = $(".js-fapiao").find("p").html();
        if (invoicetitle == "不需要发票") {
            postData.needinvoice = 0;
        }
        else {
            postData.needinvoice = 1;
            postData.invoicetitle = invoicetitle;
        }
        //postData.commodity = window.price.toFixed(2);
        //postData.freight = window.yunfei.toFixed(2);
        //postData.total = window.total;
        $.ajax({
            type: "post",
            url: window.baseurl+"api/order/create",
            dataType: "json",
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                loading = false;
                if (!detectionResult(data, 1)) {
                    return;
                }
                window.location.href = "pay.html?shopid=1&orderId=" + data.result;
            },
            error: function () {
                windowAlert("网络错误，请重试");
                loading = false;
            },
            headers: {
                "x-moutai-token": window.token,
            }
        })
    })
})