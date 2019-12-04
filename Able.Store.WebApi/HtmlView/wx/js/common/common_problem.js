$(function() {
    seingten()
})

function seingten() {
    $.ajax({
        type: "post",
        url: "http://www.stbl.cc/Login/GetShopHelpList",
        dataType: "JSON",
        data: {
            "pageindex": 1,
            "pagesize": 10,
            "word": ""
        },

        success: function(rete) {
            // parameter(data);
            var nav_c = '<div class="n help_ulp" id="nav_id1"><span class="icon1 i1"></span>&nbsp;' + rete.data[0].TypeTitle + '</div>\
                         <div class="n" id="nav_id2"><span class="icon1 i2"></span>&nbsp;' + rete.data[1].TypeTitle + ' </div>\
                         <div class="n" id="nav_id3"><span class="icon1 i3 "></span>&nbsp;' + rete.data[2].TypeTitle + ' </div>';
            $("#nav").html(nav_c);




            //标题选项切换
            $(".n").click(function(event) {
                $(this).addClass('help_ulp').siblings().removeClass('help_ulp');
                $(".dec").eq($(this).index()).addClass('decoct').siblings().removeClass('decoct');
            });
            var old = $("#divtitle p");
            old.attr("cityname", rete.data[0].Title);
            old.html(rete.data[0].Title);
            var nav_id = $("#nav_id1");
            nav_id.click(function(a) {
                old.attr("cityname", rete.data[0].Title);
                old.html(rete.data[0].Title);
            });

            var nav_id = $("#nav_id2");
            nav_id.click(function(a) {
                old.attr("cityname", rete.data[1].Title);
                old.html(rete.data[1].Title);
            });

            var nav_id = $("#nav_id3");
            nav_id.click(function(a) {
                old.attr("cityname", rete.data[2].Title);
                old.html(rete.data[2].Title);
            });



            //文章内容标题
            var nav_tc = '<li>' + rete.data[0].TypeTitle + '</li>\
                          <li>' + rete.data[1].TypeTitle + '</li>\
                          <li>' + rete.data[2].TypeTitle + '</li>'
            $(".dec .dec_moc ul").html(nav_tc);
            var nav_ac = $(".dec .dec_moc ul li");
            // nav_ac.each(function(i) {
            //     $(this).attr("cont", rete.data[i].TypeTitle);
            // })

            nav_ac.each(function(i) {
                $(this).attr("cont", rete.data[i].TypeTitle);
            })



            //搜索按钮
            $("#txt_search").bind('input propertychange', function() {
                var searchCityName = $("#txt_search").val();
                if (searchCityName == "") {
                    nav_ac.show();
                } else {
                    $("#btnsearch").click(function() {
                        nav_ac.each(function(i) {
                            $(this).attr("cont", rete.data[i].TypeTitle);
                            if (rete.data[i].TypeTitle.indexOf(searchCityName) != -1) {
                                $(this).show();
                            } else {
                                $(this).hide();
                            }
                        });
                    })

                }
            })




            // var searchCityName = $("#txt_search").val();
            // if (searchCityName == "") {
            //     alert("搜索结果为空")
            // } else {
            //     searchCityName==old.html(rete.data[0].Title)||old.html(rete.data[1].Title)||old.html(rete.data[2].Title);
            // }


            // alert("1111");
        },
        error: function(err) {
            alert("2222");
        }

    })
}
