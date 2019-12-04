$(function () {
    //授权成功&&店铺状态正常 执行
    $.when(window.shopState).done(function () {
        //搜索
        $(document).on("submit", ".form-search", function (event) {
            event.preventDefault();
            var keyword = document.getElementById("keyword").value;
            var url = $.query.set("keyword", encodeURIComponent(keyword));
            window.location.href = "product-list.html" + url;
        })
        //分类列表
        window.classState = $.Deferred();//分类状态
        var classId = $.query.get("classId");
        var host = "api/products/getcategoris";
      

        $.ajax({
            type: "get",
            url: window.baseurl + host,
            dataType: "json",
            success: function (data) {
                if (!detectionResult(data, 0)) {
                    return;
                }
                var datas = data.result;
                var html = "";
                for (var i = 0; i < datas.length; i++) {
                    if (classId == datas[i].id) {
                        html += "<li class='on' data-id='" + datas[i].id + "'><a href='?shopid=" + window.shopid + "&classId=" + datas[i].id + "'>" + datas[i].title + "</a></li>";
                    }
                    else {
                        html += "<li data-id='" + datas[i].id + "'><a href='?shopid=" + window.shopid + "&classId=" + datas[i].id + "'>" + datas[i].title + "</a></li>";
                    }
                }
                $("#class-list").prepend(html);
                if (classId == "") {
                    $("#class-list").find("li:first").addClass("on");
                    classId = parseInt($("#class-list").find("li:first").attr("data-id"));
                }
                $(".class-list").show();
                window.classState.resolve();
            },
            error: function () {
                windowAlert("网络错误，刷新重试", 0);
                window.classState.reject();
            },
            headers: {
                "x-moutai-token": window.token,
            }
        });
        $.when(window.classState).done(function () {
            var page_size = 10;
            var page_index = 1;
            var loading = false;
            function ajax() {
                if (loading) {
                    return;
                }
                loading = true;
                var postdata = {};
                postdata.categoryId = classId;
                postdata.page_index = page_index;
                postdata.page_size = page_size;
                $.ajax({
                    type: "post",
                    url: window.baseurl + "api/products/getgategoryproducts",
                    data: postdata,
                    success: function (data) {
                        if (!detectionResult(data, 1)) {
                            loading = false;
                            return;
                        }
                        $(".class-product").show();
                        if (data.result.count == 0) {
                            $("#loading").html("暂无产品").show();
                            loading = false;
                            return
                        }
                        var datas = data.result.data;
                        var html = "";
                        for (var i = 0; i < datas.length; i++) {
                            html += "<li><a href='product-detail.html?shopid=" + window.shopid + "&skuid=" + datas[i].skuid + "'><img src='' loadsrc='" + datas[i].img + "' /></a>" + datas[i].title + "</li>";
                        }
                        $("#list").append(html);
                        var pageCount = Math.ceil(data.result.count / page_size);
                        if (page_index == pageCount) {
                            $("#loading").hide();
                        }
                        else {
                            $("#loading").show();
                            page_index++;
                        }
                        loadsrc();
                        loading = false;
                    },
                    error: function () {
                        windowAlert("网络错误，刷新重试");
                        loading = false;
                    },
                    headers: {
                        "x-moutai-token": window.token,
                    }
                })
            }
            //加载更多
            $(document).on("tap", "#loading", function (event) {
                event.preventDefault();
                ajax();
            })
            //更换分类
            $(document).on("tap", "#class-list li", function (event) {
                event.preventDefault();
                if (loading) {
                    return
                }
                page_index = 1;
                classId = parseInt($(this).attr("data-id"));
                $(this).siblings().removeClass("on").end().addClass("on");
                $("#list").empty();
                ajax();
            })
            //初始读取
            ajax();
        })
    })
})
