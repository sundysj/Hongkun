<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamic_service.aspx.cs" Inherits="Service.HongKun.dynamic_service" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <meta name="viewport" content="width=width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0">
    <meta charset="utf-8">
    <title>客服动态</title>
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
                            <span id="kpi_1" style="background: #008a70; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月报事发生量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_2" style="background: #ef8f04; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月报事分派量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_3" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">报事分派及时率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_4" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">逾期未分派报事</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_5" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月报事处理量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_6" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">报事处理及时率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_7" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">逾期未处理报事</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_8" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月报事回访量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_9" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">报事回访及时率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_10" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">逾期未回访报事</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_11" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0件</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月投诉发生量</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_12" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月客户满意度</div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">本月报事来源</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="service-source-content" class="row" style="height: 400px;"></div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">本月报事分析</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="service-trends-content" class="row" style="height: 400px;"></div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">本月投诉分析</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="service-complaint-content" class="row" style="height: 400px;"></div>
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
        var mServiceTrendsChart;
        var mServiceComplaintChart;
        var mServiceSourceChart;
        $(function () {
            document.title = $("#OrganName").val() + '-' + '客服动态';
            mServiceTrendsChart = echarts.init(document.getElementById('service-trends-content'));
            mServiceComplaintChart = echarts.init(document.getElementById('service-complaint-content'));
            mServiceSourceChart = echarts.init(document.getElementById('service-source-content'));
            initServiceKPI();
            initServiceTrendsCharts();
            initServiceComplaintsCharts();
            initServiceSourceCharts()

        });
        function initServiceKPI() {
            var url = getServiceKPIUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                if (!json.Result) {
                    alert(json.data);
                    return;
                }
                $('#kpi_1').html(json.data.kpi_1 + "件");
                $('#kpi_2').html(json.data.kpi_2 + "件");
                $('#kpi_3').html(json.data.kpi_3 + "%");
                $('#kpi_4').html(json.data.kpi_4 + "件");
                $('#kpi_5').html(json.data.kpi_5 + "件");
                $('#kpi_6').html(json.data.kpi_6 + "%");
                $('#kpi_7').html(json.data.kpi_7 + "件");
                $('#kpi_8').html(json.data.kpi_8 + "件");
                $('#kpi_9').html(json.data.kpi_9 + "%");
                $('#kpi_10').html(json.data.kpi_10 + "件");
                $('#kpi_11').html(json.data.kpi_11 + "件");
                $('#kpi_12').html(json.data.kpi_12 + "%");
            });
        }
        function initServiceTrendsCharts() {
            mServiceTrendsChart.showLoading();
            var url = getServiceTrendsUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                mServiceTrendsChart.hideLoading();
                var data = null;
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    title: {
                        text: '本月报事分析',
                        x: 'center',
                        textStyle: {
                            fontSize: 15,
                            color: '#ffffff'
                        },
                        show: false
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{b} : {c} ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: (function () {
                            var arr = [];
                            if (data != null) {
                                for (var i = 0; i < data.length; i++) {
                                    arr.push(data[i].TypeName);
                                }
                            }
                            else {
                                arr.push('暂无数据');
                            }
                            return arr;
                        })(),
                        show: false
                    },
                    calculable: true,
                    series: [
                        {
                            name: '本月报事',
                            type: 'pie',
                            radius: [30, 100],
                            center: ['50%', 200],
                            roseType: 'area',
                            x: '50%',
                            itemStyle: {
                                normal: {
                                    label: {
                                        position: 'outer',
                                        formatter: '{b} \n {c} ({d}%)'
                                    }
                                }
                            },
                            data: (function () {
                                var arr = [];
                                if (data != null && data.length > 0) {
                                    for (var i = 0; i < data.length; i++) {
                                        arr.push({
                                            value: data[i].AllCount,
                                            name: data[i].TypeName
                                        });
                                    }
                                }
                                else {
                                    arr.push({
                                        value: 100,
                                        name: '暂无数据'
                                    })
                                }
                                return arr;
                            })()
                        }
                    ]
                };
                mServiceTrendsChart.setOption(option);
            });
        }

        function initServiceComplaintsCharts() {
            mServiceComplaintChart.showLoading();
            var url = getServiceComplaintsUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                mServiceComplaintChart.hideLoading();
                var data = null;
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    title: {
                        text: '本月投诉分析',
                        x: 'center',
                        textStyle: {
                            fontSize: 15,
                            color: '#ffffff'
                        },
                        show: false
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: (function () {
                            var arr = [];
                            if (data != null) {
                                for (var i = 0; i < data.length; i++) {
                                    arr.push(data[i].TypeName);
                                }
                            }
                            else {
                                arr.push('暂无数据');
                            }
                            return arr;
                        })(),
                        show: false
                    },
                    calculable: true,
                    series: [
                        {
                            name: '本月报事',
                            type: 'pie',
                            radius: [30, 100],
                            center: ['50%', 200],
                            roseType: 'area',
                            x: '50%',
                            itemStyle: {
                                normal: {
                                    label: {
                                        position: 'outer',
                                        formatter: '{b} \n {c} ({d}%)'
                                    }
                                }
                            },
                            data: (function () {
                                var arr = [];
                                if (data != null && data.length > 0) {
                                    for (var i = 0; i < data.length; i++) {
                                        arr.push({
                                            value: data[i].AllCount,
                                            name: data[i].TypeName
                                        });
                                    }
                                }
                                else {
                                    arr.push({
                                        value: 100,
                                        name: '暂无数据'
                                    })
                                }
                                return arr;
                            })(),
                        }
                    ]
                };
                mServiceComplaintChart.setOption(option);
            });

        }

        function initServiceSourceCharts() {
            mServiceSourceChart.showLoading();
            var url = getServiceSourceUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                mServiceSourceChart.hideLoading();
                var data = [];
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    title: {
                        text: '本月报事来源',
                        x: 'center',
                        textStyle: {
                            fontSize: 15,
                            color: '#ffffff'
                        },
                        show: false
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{b} : {c} ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: (function () {
                            var arr = [];
                            if (data != null && data.length > 0) {
                                for (var i = 0; i < data.length; i++) {
                                    arr.push(data[i].IncidentMode);
                                }
                            }
                            else {
                                arr.push('暂无数据');
                            }
                            return arr;
                        })(),
                        show: false
                    },
                    calculable: true,
                    series: [
                        {
                            name: '本月报事',
                            type: 'pie',
                            radius: [30, 100],
                            center: ['50%', 200],
                            roseType: 'area',
                            x: '50%',
                            itemStyle: {
                                normal: {
                                    label: {
                                        position: 'outer',
                                        formatter: '{b} \n {c} ({d}%)'
                                    }
                                }
                            },
                            data: (function () {
                                var arr = [];
                                if (data != null && data.length > 0) {
                                    for (var i = 0; i < data.length; i++) {
                                        arr.push({
                                            value: data[i].AllAcount,
                                            name: data[i].IncidentMode
                                        });
                                    }
                                }
                                else {
                                    arr.push({
                                        value: 100,
                                        name: '暂无数据'
                                    })
                                }
                                return arr;
                            })()
                        }
                    ]
                };
                mServiceSourceChart.setOption(option);
            });
        }

        function getServiceKPIUrl() {
            return "../HongKun/DataRequest.ashx?Command=getservicekpi&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getServiceTrendsUrl() {
            return "../HongKun/DataRequest.ashx?Command=getservicetrends&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getServiceComplaintsUrl() {
            return "../HongKun/DataRequest.ashx?Command=getservicecomplaints&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }

        function getServiceSourceUrl() {
            return "../HongKun/DataRequest.ashx?Command=getservicesource&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
    </script>
</body>
</html>
