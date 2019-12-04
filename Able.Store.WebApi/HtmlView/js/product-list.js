$(function () {
    //授权成功&&店铺状态正常 执行
    $.when(window.shopState).done(function () {
        //相关参数
        var page_size = 10;
        var keyword, timeSort, saleSort, priceSort;
        var loading = false;
        var noresult = false;
        //拿取url参数后读取数据
        function ajax() {
            if (loading) {
                return;
            }
            loading = true;
            keyword = $.query.get("keyword");
            timeSort = $.query.get("timeSort");
            saleSort = $.query.get("saleSort");
            priceSort = $.query.get("priceSort");
            //关键字
            if (keyword === true) {
                keyword = "";
            }
            $("#keyword").val(decodeURIComponent(keyword));
            //时间排序
            if (timeSort == "") {
                timeSort = 1;
            }
            if (timeSort == 1) {
                $("#timeSort").addClass("order-1");
                var url = $.query.set("timeSort", "2");
                $("#timeSort").find("a").attr("href", url);
            }
            if (timeSort == 2) {
                $("#timeSort").addClass("order-2");
                var url = $.query.set("timeSort", "1");
                $("#timeSort").find("a").attr("href", url);
            }
            //销量排序
            if (saleSort == "") {
                saleSort = 0;
            }
            if (saleSort == 0) {
                var url = $.query.set("saleSort", "1");
                $("#saleSort").find("a").attr("href", url);
            }
            if (saleSort == 1) {
                $("#saleSort").addClass("order-1");
                var url = $.query.set("saleSort", "2");
                $("#saleSort").find("a").attr("href", url);
            }
            if (saleSort == 2) {
                $("#saleSort").addClass("order-2");
                var url = $.query.set("saleSort", "1");
                $("#saleSort").find("a").attr("href", url);
            }
            //价格排序
            if (priceSort == "") {
                priceSort = 0;
            }
            if (priceSort == 0) {
                var url = $.query.set("priceSort", "1");
                $("#priceSort").find("a").attr("href", url);
            }
            if (priceSort == 1) {
                $("#priceSort").addClass("order-1");
                var url = $.query.set("priceSort", "2");
                $("#priceSort").find("a").attr("href", url);
            }
            if (priceSort == 2) {
                $("#priceSort").addClass("order-2");
                var url = $.query.set("priceSort", "1");
                $("#priceSort").find("a").attr("href", url);
            }
            //当前页数
            if (typeof (page_index) == "undefined") {
                window.page_index = 1;
            }
            //读取列表
            var postdata = {};
            postdata.keyword = decodeURIComponent(keyword);
            postdata.page_index = page_index;
            postdata.page_size = page_size;
            postdata.order = [];
            if (postdata.timeSort != "") {
                  
                postdata.order.push({ filedname: "PublishTime", isdesc: timeSort== 1 ? true : false });
            }
            if (postdata.saleSort != "") {
                postdata.order.push({ filedname: "SaleQty", isdesc: saleSort == 1 ? true : false });
            }
            if (postdata.priceSort != "") {
                postdata.order.push({ filedname: "Price", isdesc: priceSort == 1 ? true : false });
            }
            $.ajax({
                type: "post",
                url: window.baseurl+"api/products/pages",
                data: JSON.stringify(postdata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#loading").show().html("正在加载...");
                },
                success: function (data) {
                    if (!detectionResult(data, 0)) {
                        loading = false;
                        return;
                    }
                    if (data.result.count == 0) {
                        $("#loading").html("暂无结果");
                        noresult = true;
                        return;
                    }
                    var datas = data.result.data;
                    var html = "";
                    for (var i = 0; i < datas.length; i++) {
                        html += "<li><a href='product-detail.html?shopid=" + window.shopid + "&skuid=" + datas[i].id+ "&productId=" + datas[i].productid + "'><div class='product-img'><img src='" + datas[i].img + "' /></div><h3>" + datas[i].title + "</h3><p>销量：" + datas[i].saleqty + "</p><div class='price'>￥" + datas[i].price.toFixed(2) + "</div></a></li>";
                    }
                    if (page_index == 1) {
                        $("#list").empty();
                    }
                    $("#list").append(html);
                    loadsrc();//加载可视区域内的图片
                    var pageCount = Math.ceil(data.result.count / page_size);
                    if (page_index == pageCount) {
                        $("#loading").hide();
                    }
                    else {
                        $("#loading").html("点击加载更多");
                        page_index++;
                    }
                    loading = false;
                },
                error: function () {
                    $("#loading").html("网络错误,点击重新加载...");
                    loading = false;
                },
                headers: {
                    "x-moutai-token": window.token,
                }
            });
        }
        //显示方式
        if (window.localStorage.show_style == "undefined") {
            window.localStorage.show_style = "small";
        }
        if (window.localStorage.show_style == "small") {
            $("#show-style").addClass("style-big");
        }
        if (window.localStorage.show_style == "big") {
            $("#list").addClass("product-list-big");
        }
        $(document).on("tap", "#show-style", function (event) {
            event.preventDefault();
            showStyle();
        })
        function showStyle() {
            if (window.localStorage.show_style == "small") {
                $("#show-style").removeClass("style-big");
                $("#list").addClass("product-list-big");
                window.localStorage.show_style = "big";
            }
            else {
                $("#show-style").addClass("style-big");
                $("#list").removeClass("product-list-big");
                window.localStorage.show_style = "small";
            }
        }
        //加载更多
        $("#loading").on("click", function () {
            if (!noresult) {
                ajax();
            }
        })
        //搜索
        $(document).on("submit", ".form-search", function (event) {
            event.preventDefault();
            keyword = document.getElementById("keyword").value;
            var url = $.query.set("keyword", encodeURIComponent(keyword)).set("timeSort", "1").set("saleSort", "0").set("priceSort", "0");
            window.location.href = url;
        })
        //一切就绪后拿取数据
        ajax();
    })
})
