<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamic_charge.aspx.cs" Inherits="Service.HongKun.dynamic_charge" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <meta name="viewport" content="width=width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0">
    <meta charset="utf-8">
    <title>收费动态</title>
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
                            <span id="kpi_1" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">年初往年欠费</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_2" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本年应收收入</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_3" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本年实际收入</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_4" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本年收缴率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_5" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">往年欠费追缴率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_6" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本年前期欠费</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_7" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月应收收入</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_8" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月实际收入</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_9" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0.00万元</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月收缴率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_10" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本年前期追缴率</div>
                    </div>

                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_11" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月房屋转让办理</div>
                    </div>

                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_12" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月房屋租赁办理</div>
                    </div>

                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_13" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月房屋装修办理</div>
                    </div>

                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_14" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月房屋状态变更</div>
                    </div>

                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_15" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月合同时间到期</div>
                    </div>

                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_16" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本年合同费用到期</div>
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
                        <span class="span_tips_blue" onclick="initMapChart(1);">按区域</span>
                        <span class="span_tips_org" onclick="initMapChart(0);">按项目</span>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">收费排行榜</strong>
                    </div>
                    <div class="col-md-12 col-xs-12" style="padding: 10px;">
                        <img style="height: 20px; width: 20px;" src="./img/ic_first.png" onclick="first();">
                        <img style="height: 20px; width: 20px;" src="./img/ic_previous.png" onclick="previous();">
                        <span id="page_num" style="padding: 4px 8px; border: 2px solid #00B4E3; border-radius: 50% 50%;">1</span>
                        <img style="height: 20px; width: 20px;" src="./img/ic_next.png" onclick="next();">
                        <img style="height: 20px; width: 20px;" src="./img/ic_last.png" onclick="last();">
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="charge-one-content" class="row" style="height: 400px;"></div>
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
                        <strong style="line-height: 40px;" class="strong_tip">收费率同比</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="charge-two-content" class="row" style="height: 400px;"></div>
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
        var mChargeOneChart;
        var mChargeTwoChart;
        var gPage = 1;
        var allCount = 0;
        var gPageSize = 3;
        var mType = 1;
        var months = ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'];
        $(function () {
            document.title = $("#OrganName").val() + '-' + '收费动态';
            mChargeOneChart = echarts.init(document.getElementById('charge-one-content'));
            mChargeTwoChart = echarts.init(document.getElementById('charge-two-content'));
            initChargeKPI();
            initChargeOneCharts(1);
            initChargeTwoCharts();

        });
        function initMapChart(type) {
            mType = type;
            initChargeOneCharts(1);
        }
        function first() {
            initChargeOneCharts(1);
        }
        function previous() {
            initChargeOneCharts(gPage - 1);
        }
        function next() {
            initChargeOneCharts(gPage + 1);
        }
        function last() {
            initChargeOneCharts(allCount % gPageSize > 0 ? parseInt(allCount / gPageSize) + 1 : allCount / gPageSize);
        }

        function initChargeKPI() {
            var url = initChargeKPIUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                if (!json.Result) {
                    alert(json.data);
                    return;
                }
                $('#kpi_1').html((parseFloat(json.data.kpi_1) / 10000).toFixed(2) + "万元");
                $('#kpi_2').html((parseFloat(json.data.kpi_2) / 10000).toFixed(2) + "万元");
                $('#kpi_3').html((parseFloat(json.data.kpi_3) / 10000).toFixed(2) + "万元");
                $('#kpi_4').html(json.data.kpi_4 + "%");
                $('#kpi_5').html(json.data.kpi_5 + "%");
                $('#kpi_6').html((parseFloat(json.data.kpi_6) / 10000).toFixed(2) + "万元");
                $('#kpi_7').html((parseFloat(json.data.kpi_7) / 10000).toFixed(2) + "万元");
                $('#kpi_8').html((parseFloat(json.data.kpi_8) / 10000).toFixed(2) + "万元");
                $('#kpi_9').html(json.data.kpi_9 + "%");
                $('#kpi_10').html(json.data.kpi_10 + "%");
                $('#kpi_11').html(json.data.kpi_11);
                $('#kpi_12').html(json.data.kpi_12);
                $('#kpi_13').html(json.data.kpi_13);
                $('#kpi_14').html(json.data.kpi_14);
                $('#kpi_15').html(json.data.kpi_15);
                $('#kpi_16').html(json.data.kpi_16);
            });
        }

        function initChargeOneCharts(page) {
            var allPage = allCount % gPageSize > 0 ? parseInt(allCount / gPageSize) + 1 : allCount / gPageSize;
            if (page > allPage) {
                page = allPage;
            }
            if (page < 1) {
                page = 1;
            }
            $('#page_num').html(page);
            gPage = page;
            mChargeOneChart.showLoading();
            var url = getChargeOneUrl(page, gPageSize, mType);
            $.ajax({
                url: url, success: function (json, status, xhr) {
                    mChargeOneChart.hideLoading();
                    json = eval('(' + json + ')');
                    var CommList = '[]';
                    var MonthChargeData = '[]';
                    var YearChargeData = '[]';
                    if (!json.Result) {
                        alert(json.data);
                    } else {
                        CommList = json.data.CommList;
                        MonthChargeData = json.data.MonthChargeData;
                        YearChargeData = json.data.YearChargeData;
                        allCount = json.data.count;
                    }
                    var option = {
                        color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                        tooltip: {
                            trigger: 'axis'
                        },
                        legend: {
                            data: ['本月累计收缴率', '本年累计收缴率'],
                            x: 'center',
                            y: 'top',
                            textStyle: {
                                color: '#ffffff'
                            }
                        },
                        grid: {
                            y: 30,
                            y2: 100,
                            x: 50,
                            x2: 20
                        },
                        calculable: false,
                        xAxis: [
                            {
                                type: 'category',
                                data: (function () {
                                    if (CommList != '[]') {
                                        return eval(CommList);
                                    } else {
                                        return ['0', '0', '0'];
                                    }
                                })(),
                                axisLabel: {
                                    rotate: 20,
                                    textStyle: {
                                        color: '#ffffff'
                                    }
                                }
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value',
                                axisLabel: {
                                    textStyle: {
                                        color: '#ffffff'
                                    }
                                }
                            }
                        ],
                        series: [
                            {
                                name: '本月累计收缴率',
                                type: 'bar',
                                data: (function () {
                                    if (CommList != '[]') {
                                        return eval(MonthChargeData);
                                    } else {
                                        return [0, 0, 0];
                                    }
                                })(),
                            },
                            {
                                name: '本年累计收缴率',
                                type: 'bar',
                                data: (function () {
                                    if (CommList != '[]') {
                                        return eval(YearChargeData);
                                    } else {
                                        return [0, 0, 0];
                                    }
                                })(),
                            }
                        ]
                    };
                    mChargeOneChart.setOption(option);
                },
                error: function (xhr) {
                    alert("错误提示： " + xhr.status + " " + xhr.statusText);
                },
                complete: function (xhr, status) {
                    mChargeOneChart.hideLoading();
                }
            }
            );
        }

        function initChargeTwoCharts() {
            mChargeTwoChart.showLoading();
            var url = getChargeTwoUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                mChargeTwoChart.hideLoading();
                var data = null;
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    title: {
                        text: '',
                        x: 'center',
                        textStyle: {
                            fontSize: 15,
                            color: '#ffffff'
                        }
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data: ['去年收费率', '今年收费率'],
                        x: 'center',
                        y: 'top',
                        textStyle: {
                            color: '#ffffff'
                        }
                    },
                    //color: ['#287CD7', '#000000'],
                    grid: {
                        y: 60,
                        y2: 20,
                        x: 50,
                        x2: 20
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            boundaryGap: false,
                            data: months,
                            axisLabel: {
                                textStyle: {
                                    color: '#ffffff'
                                }
                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            axisLabel: {
                                formatter: '{value}',
                                textStyle: {
                                    color: '#ffffff'
                                }
                            }
                        }
                    ],
                    series: [
                        {
                            name: '去年收费率',
                            type: 'line',
                            data: (function () {
                                var arr = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                                if (data != null && data.length > 0) {
                                    arr = [];
                                    for (var i = 0; i < months.length; i++) {
                                        var values = months[i] + "收费率";
                                        arr.push(data[0][values]);
                                    }
                                }
                                return arr;
                            })()
                        },
                        {
                            name: '今年收费率',
                            type: 'line',
                            data: (function () {
                                var arr = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                                if (data != null && data.length > 0) {
                                    arr = [];
                                    for (var i = 0; i < months.length; i++) {
                                        var values = months[i] + "收费率";
                                        arr.push(data[1][values]);
                                    }
                                }
                                return arr;
                            })()
                        }
                    ]
                };
                mChargeTwoChart.setOption(option);
            });

        }

        function initChargeKPIUrl() {
            return "../HongKun/DataRequest.ashx?Command=getchargekpi&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getChargeOneUrl(page, size, mType) {
            return "../HongKun/DataRequest.ashx?Command=getchargelist&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val() + "&page=" + page + "&size=" + size + "&type=" + mType;
        }
        function getChargeTwoUrl() {
            return "../HongKun/DataRequest.ashx?Command=getchargerate&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
    </script>
</body>
</html>

