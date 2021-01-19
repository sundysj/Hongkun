<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamic_resource.aspx.cs" Inherits="Service.HongKun.dynamic_resource" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <meta name="viewport" content="width=width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0">
    <meta charset="utf-8">
    <title>资源动态</title>
    <!-- 引入 jquery 文件 -->
    <script src="./js/jquery.min.js"></script>
    <!-- 引入 ECharts 文件 -->
    <script src="./js/echarts.js"></script>
    <script src="./js/map/china.js"></script>
    <!-- 引入 bootstrap 文件 -->
    <script src="./js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="./css/bootstrap.min.css">
    <link rel="stylesheet" href="./css/desktop_new.css">
</head>
<body>
    <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
    <div id="main" class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="manage_area" style="background: #008a70; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">管理区域</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="manage_pro" style="background: #ef8f04; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">管理项目</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="house_pro" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">住宅项目</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="public_pro" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">公建项目</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="build_area" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0万平方米</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">建筑面积</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="jacket_area" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0万平方米</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">套内面积</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="build_sum" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">楼宇数量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="room_sum" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">房屋数量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="park_sum" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0个</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">车位数量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="customer_sum" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0户</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">客户数量</div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-12 col-xs-12" style="padding: 10px;">
                        <span class="span_tips_blue" onclick="initMapChart(1);">按项目</span>
                        <span class="span_tips_org" onclick="initMapChart(0);">按面积</span>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">公司管理项目</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="map-content" class="row" style="height: 400px;"></div>
                    </div>
                </div>
            </div>


            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-12 col-xs-12" style="padding: 10px;">
                        <span class="span_tips_blue" onclick="initPieChart(1);">按户数</span>
                        <span class="span_tips_org" onclick="initPieChart(0);">按面积</span>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">管理业态</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="pie-content" class="row" style="height: 400px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <form action="/" runat="server">
        <asp:HiddenField ID="OrganCode" runat="server" />
        <asp:HiddenField ID="OrganName" runat="server" />
        <asp:HiddenField ID="UserCode" runat="server" />
    </form>
    <script type="text/javascript">
        var mMapChart;
        var mPieChart;
        $(function () {
            document.title = $("#OrganName").val() + '-' + '资源动态';
            mMapChart = echarts.init(document.getElementById('map-content'));
            mPieChart = echarts.init(document.getElementById('pie-content'));
            initResourceKPI();
            initMapChart(1);
            initPieChart(1);

        });
        function initResourceKPI() {
            var url = getResourceKPIUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                if (!json.Result) {
                    alert(json.data);
                    return;
                }
                $('#manage_area').html(json.data.manage_area + "个");
                $('#manage_pro').html(json.data.manage_pro + "个");
                $('#house_pro').html(json.data.house_pro + "个");
                $('#public_pro').html(json.data.public_pro + "个");
                $('#build_area').html(json.data.build_area + "万平方米");
                $('#jacket_area').html(json.data.jacket_area + "万平方米");
                $('#build_sum').html(json.data.build_sum + "个");
                $('#room_sum').html(json.data.room_sum + "个");
                $('#park_sum').html(json.data.park_sum + "个");
                $('#customer_sum').html(json.data.customer_sum + "户");
            });
        }
        function initMapChart(mType) {
            mMapChart.showLoading();
            var url = getMapUrl(mType);
            $.getJSON(url, {}, function (json, textStatus) {
                mMapChart.hideLoading();
                var data = [];
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    tooltip: {
                        trigger: 'item'
                    },
                    visualMap: {
                        min: 0,
                        max: 1000,
                        left: 'left',
                        top: 'center',
                        text: ['高', '低'],
                        calculable: true,
                        inRange: {
                            color: ['#e0ffff', '#006edd']
                        },
                        textStyle: {
                            color: '#ffffff'
                        },
                        show: true
                    },
                    series: [
                        {
                            name: '项目',
                            type: 'map',
                            mapType: 'china',
                            roam: true,
                            scaleLimit: {
                                min: 0.5,
                                max: 5.0
                            },
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true
                                    }
                                },
                                emphasis: {
                                    label: {
                                        show: true
                                    }
                                }
                            },
                            data: data
                        }
                    ]
                };
                mMapChart.setOption(option);
            });
        }

        function initPieChart(mType) {
            mPieChart.showLoading();
            var url = getManageformatUrl(mType);
            $.getJSON(url, {}, function (json, textStatus) {
                mPieChart.hideLoading();
                var data = null;
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    tooltip: {
                        trigger: 'item',
                        formatter: (function () {
                            var res = "{a} <br/>{b} : {c}万㎡ ({d}%)";
                            if (mType == 1) {
                                res = "{a} <br/>{b} : {c}户 ({d}%)";
                            }
                            return res;
                        })()
                    },
                    legend: {
                        orient: 'vertical',
                        left: 'left',
                        show: false
                    },
                    calculable: false,
                    series: [
                        {
                            name: '管理业态',
                            type: 'pie',
                            radius: '65%',
                            center: ['50%', '50%'],
                            itemStyle: {
                                normal: {
                                    label: {
                                        position: 'outer',
                                        formatter: '{b} \n ({d}%)'
                                    }
                                }
                            },
                            data: (function () {
                                var arr = [];
                                if (mType == 1) {
                                    if (data != null && data.length > 0) {
                                        for (var i = 0; i < data.length; i++) {
                                            arr.push({
                                                value: data[i].value,
                                                name: data[i].name
                                            });
                                        }
                                    }
                                    else {
                                        arr.push({
                                            value: 100,
                                            name: '暂无数据'
                                        });
                                    }
                                }
                                else {
                                    if (data != null && data.length > 0) {
                                        for (var i = 0; i < data.length; i++) {
                                            arr.push({
                                                value: (data[i].value / 10000).toFixed(2),
                                                name: data[i].name
                                            });
                                        }
                                    } else {
                                        arr.push({
                                            value: 100,
                                            name: '暂无数据'
                                        });
                                    }
                                }
                                return arr;
                            })()
                        }
                    ]
                };
                mPieChart.setOption(option);
            });

        }

        function getResourceKPIUrl() {
            return "../HongKun/DataRequest.ashx?Command=getresourcekpi&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getMapUrl(type) {
            return "../HongKun/DataRequest.ashx?Command=getproject&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val() + "&type=" + type;
        }
        function getManageformatUrl(type) {
            return "../HongKun/DataRequest.ashx?Command=getmanageformat&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val()+ "&type=" + type;;
        }
    </script>
</body>
</html>
