<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessDetails.aspx.cs" Inherits="Service.BusinessDetailsHtml.BusinessDetails" %>

<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../BusinessDetailsCss/BusinessDetails.css" rel="stylesheet" />
    <link href="../BusinessDetailsCss/flickerplate.css" rel="stylesheet" />

    <script src="../Jsscipt-UI/jquery-1.9.1.js"></script>
    <script src="../Jsscipt-UI/flickerplate.min.js"></script>
    <script src="../Jsscipt-UI/jquery-finger-v0.1.0.min.js"></script>
    <script src="../Jsscipt-UI/modernizr-custom-v2.7.1.min.js"></script>

    <title>商品详情</title>
</head>
<body>
    <div id="Business">
        <div id="BusinessLogo" runat="server" class="flicker">
            <ul id="BusinessLogoUl" runat="server">
            </ul>
            <div id="DivBorderBut"></div>
        </div>
        <div id="BusinessIntroduce">
            <ul>
                <li class="F2" id="li4">
                    <span id="AdContent" runat="server"></span>
                </li>
                <li class="F1" id="li2">
                    <span id="Nature" runat="server"></span>
                </li>
                <li class="F1" id="li3">
                    <span id="liDisCountPrice" runat="server"></span>
                </li>
                <li class="F1" id="li1">
                    <span id="Name" runat="server"></span>
                </li>
                <li class="F1" id="li6">
                    <span id="IsSupportCoupon" runat="server"></span>
                </li>
                <li class="F2" id="li5">
                    <span id="liSalePrice" runat="server"></span>
                </li>
                <li style="clear:both;"></li>
                <li class="F2" id="li7" runat="server">
                    下单数量：<span id="OrderNum" runat="server"></span>
                </li>
            </ul>
        </div>

        <div id="BusinessTitle">

            <div id="BusinessIntroduceBorder"></div>

            <div id="button">
                <a href="javascript:;" id="buttonBD" onclick="change_div('BusinessDetail')">图文详情</a>
                <a href="javascript:;" id="buttonBE" onclick="change_div('BuyerEvaluation')">买家评价</a>
            </div>

            <div id="BusinessDetailBorber"></div>
            <div id="BuyerEvaluationBorber"></div>

            <div id="BusinessTitleContent">
                <div id="BusinessDetail">
                    <!--商品详情-->
                    <div id="BusinessDetaildivImg" runat="server">
                    </div>
                    <div id="BusinessDetailBack">
                        <hr id="hr1" />
                        <hr id="hr2" />
                        <p>同店商品</p>
                    </div>
                    <div id="BusinessDetaildivTable" runat="server">
                    </div>
                </div>
                <div id="BuyerEvaluation" runat="server">

                </div>
            </div>
        </div>
    </div>
    <input type="hidden" name="HidLength" id="HidLength" value="" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('.flicker').flicker({
                auto_flick: true,
                auto_flick_delay: 2.5,
            });
        });


        function change_div(id) {
            if (id == 'BuyerEvaluation') {
                document.getElementById("BusinessDetail").style.display = 'none';
                document.getElementById("BuyerEvaluation").style.display = 'block';

                document.getElementById("BusinessDetailBorber").style.display = 'none';
                document.getElementById("BuyerEvaluationBorber").style.display = 'block';
            }
            else {
                document.getElementById("BuyerEvaluation").style.display = 'none';
                document.getElementById("BusinessDetail").style.display = 'block';

                document.getElementById("BuyerEvaluationBorber").style.display = 'none';
                document.getElementById("BusinessDetailBorber").style.display = 'block';
            }
        }

        window.onload = function () {
            //获取手机宽度
            var width = window.screen.width;
            $("#BusinessDetaildivImg img").css("width", width - 10);

            //加载判断商品描述长度限定
            var length = $("#HidLength").val();
            for (var i = 0; i < length; i++) {
                var str = $("#ResourcesName" + i + "").text();
                if (str.length > 20) {
                    str = str.substr(0, 20);
                    $("#ResourcesName" + i + "").text(str + '..');
                }
            }
            var name = $("#Name").val();
            for (var i = 0; i < name.length; i++) {
                if (name.length > 9) {
                    name = name.substr(0, 8);
                    $("#Name" + i + "").text(name + '..');
                }
            }
        };
    </script>
</body>
</html>
