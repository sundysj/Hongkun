<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamic_personal.aspx.cs" Inherits="Service.HongKun.dynamic_personal" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <meta name="viewport" content="width=width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0">
    <meta charset="utf-8">
    <title>人事动态</title>
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
                            <span id="kpi_1" style="background: #008a70; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">当前定编人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_2" style="background: #ef8f04; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">当前在职人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_3" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">当前缺编率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_4" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">当前签订合同人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_5" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月合同到期人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_6" style="background: #00a285; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">当前持证人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_7" style="background: #ef8e06; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">持证上岗率</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_8" style="background: #0091ff; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月入职人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_9" style="background: #23abe2; padding: 10px 10px 10px 20px; border-radius: 4px;">0人</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月离职人数</div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="smalldiv blue_smalldiv">
                            <span id="kpi_10" style="background: #fb6a68; padding: 10px 10px 10px 20px; border-radius: 4px;">0%</span>
                        </div>
                        <div class="smalldiv gray_smalldiv">本月离职率</div>
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
                        <span class="span_tips_blue" onclick="initPersonelOneCharts(1,1);">按区域</span>
                        <span class="span_tips_org" onclick="initPersonelOneCharts(1,0);">按岗位</span>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">公司人员编制</strong>
                    </div>
                    <div class="col-md-12 col-xs-12" style="padding: 10px;">
                        <img style="height: 20px; width: 20px;" src="./img/ic_first.png" onclick="first();">
                        <img style="height: 20px; width: 20px;" src="./img/ic_previous.png" onclick="previous();">
                        <span id="page_num" style="padding: 4px 8px; border: 2px solid #00B4E3; border-radius: 50% 50%;">1</span>
                        <img style="height: 20px; width: 20px;" src="./img/ic_next.png" onclick="next();">
                        <img style="height: 20px; width: 20px;" src="./img/ic_last.png" onclick="last();">
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="personel-one-content" class="row" style="height: 400px;"></div>
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
                        <div id="personel-two-content" class="row" style="height: 400px;"></div>
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
        var mPersonelOneChart;
        var mPersonelTwoChart;
        var gType = 1;
        var gPage = 1;
        var projectCount = 0;
        var color = ['#ff7f50', '#87cefa', '#da70d6', '#32cd32', '#6495ed',
            '#ff69b4', '#ba55d3', '#cd5c5c', '#ffa500', '#40e0d0',
            '#1e90ff', '#ff6347', '#7b68ee', '#00fa9a', '#ffd700',
            '#6b8e23', '#ff00ff', '#3cb371', '#b8860b', '#30e0e0'];
        $(function () {
            document.title = $("#OrganName").val()  + '-' + '人事动态';
            mPersonelOneChart = echarts.init(document.getElementById('personel-one-content'));
            mPersonelTwoChart = echarts.init(document.getElementById('personel-two-content'));
            initPersonelKPI();
            initPersonelOneCharts(1, gType);
            initPersonelTwoCharts();

        });
        function first() {
            initPersonelOneCharts(1, gType);
        }
        function previous() {
            initPersonelOneCharts(gPage - 1, gType);
        }
        function next() {
            initPersonelOneCharts(gPage + 1, gType);
        }
        function last() {
            initPersonelOneCharts(projectCount % 6 > 0 ? parseInt(projectCount / 6) + 1 : projectCount / 6, gType);
        }

        function initPersonelKPI() {
            var url = getPersonelKPIUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                if (!json.Result) {
                    alert(json.data);
                    return;
                }
                $('#kpi_1').html(json.data.PersonNum + "人");
                $('#kpi_2').html(json.data.PersonState + "人");
                $('#kpi_3').html(json.data.Vacancies + "%");
                $('#kpi_4').html(json.data.ContractNum + "人");
                $('#kpi_5').html(json.data.ContractExpire + "人");
                $('#kpi_6').html(json.data.CertificateNum + "人");
                $('#kpi_7').html(json.data.Certificate + "%");
                $('#kpi_8').html(json.data.EntryNum + "人");
                $('#kpi_9').html(json.data.WorkDimissionNum + "人");
                $('#kpi_10').html(json.data.Quit + "%");
            });
        }

        function initPersonelOneCharts(page, mType) {
            var allPage = projectCount % 6 > 0 ? parseInt(projectCount / 6) + 1 : projectCount / 6;
            if (page > allPage) {
                page = allPage;
            }
            if (page < 1) {
                page = 1;
            }
            $('#page_num').html(page);
            var startIndex = (page - 1) * 6;
            var endIndex = page * 6;
            gType = mType;
            gPage = page;
            mPersonelOneChart.showLoading();
            var url = getPersonelOneUrl(startIndex, endIndex, mType);
            $.getJSON(url, {}, function (json, textStatus) {
                mPersonelOneChart.hideLoading();
                var data = [];
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data.list;
                    projectCount = json.data.count;
                }
                var option = {
                    color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        },
                        formatter: '{b}<br/>{a0}:{c0}<br/>{a2}:{c2}<br/>{a4}:{c4}<br/>{a6}:{c6}'
                    },
                    legend: {
                        top: 'bottom',
                        data: ['定编人数', '在岗人数', '缺编人数', '缺编率'],
                        textStyle: {
                            color: '#ffffff'
                        }
                    },
                    grid: {
                        y: 0,
                        y2: 30,
                        x: 60,
                        x2: 0
                    },
                    xAxis: [
                        {
                            type: 'value',
                            position: 'bottom',
                            splitLine: { show: false },
                            axisLabel: { show: false }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'category',
                            splitLine: { show: false },
                            axisLabel: {
                                rotate: 0,
                                textStyle: {
                                    color: '#ffffff'
                                }
                            },
                            data: (function () {
                                var arr = [0, 0, 0];
                                if (data != null && data.length > 0) {
                                    arr = [];
                                    for (var i = 0; i < data.length; i++) {
                                        arr.push(data[i].DepName);
                                    }
                                }
                                return arr;
                            })()
                        }
                    ],
                    series: [
                        {
                            name: '定编人数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        position: 'inside',
                                        formatter: function (val) {
                                            if (data != null && data.length > 0) {
                                                var str = "";
                                                for (var i = 0; i < data.length; i++) {
                                                    if (val.name == data[i].DepName) {
                                                        str = data[i].PersonNum
                                                    }
                                                }
                                                return str;
                                            }
                                        }
                                    }
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '定编人数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                },
                                emphasis: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '在岗人数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        position: 'inside',
                                        formatter: function (val) {
                                            if (data != null && data.length > 0) {
                                                var str = "";
                                                for (var i = 0; i < data.length; i++) {
                                                    if (val.name == data[i].DepName) {
                                                        str = data[i].RealNum
                                                    }
                                                }
                                                return str;
                                            }
                                        }
                                    }
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '在岗人数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                },
                                emphasis: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '缺编人数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        position: 'inside',
                                        formatter: function (val) {
                                            if (data != null && data.length > 0) {
                                                var str = "";
                                                for (var i = 0; i < data.length; i++) {
                                                    if (val.name == data[i].DepName) {
                                                        str = data[i].LostNum
                                                    }
                                                }
                                                return str;
                                            }
                                        }
                                    }
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '缺编人数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                },
                                emphasis: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '缺编率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        position: 'inside',
                                        formatter: function (val) {
                                            if (data != null && data.length > 0) {
                                                var str = "";
                                                for (var i = 0; i < data.length; i++) {
                                                    if (val.name == data[i].DepName) {
                                                        str = data[i].Point
                                                    }
                                                }
                                                return str;
                                            }
                                        }
                                    }
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        },
                        {
                            name: '缺编率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                },
                                emphasis: {
                                    barBorderColor: 'rgba(0,0,0,0)',
                                    color: 'rgba(0,0,0,0)'
                                }
                            },
                            data: [50, 50, 50, 50, 50, 50, 50, 50]
                        }
                    ]
                };
                mPersonelOneChart.setOption(option);
            });
        }

        function initPersonelTwoCharts() {
            mPersonelTwoChart.showLoading();
            var url = getPersonelTwoUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                mPersonelTwoChart.hideLoading();
                var data = null;
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data;
                }
                var option = {
                    title: {
                        text: '公司人员结构',
                        x: 'center',
                        textStyle: {
                            fontSize: 15,
                            color: '#ffffff'
                        }
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a}"
                    },
                    legend: {
                        orient: 'vertical',
                        data: (function () {
                            var arr = [];
                            for (var i = 0; i < data.length; i++) {
                                arr.push({
                                    name: data[i].name + data[i].y + '%',
                                    color: color[i]
                                });
                            }
                            return arr;
                        })(),
                        show: true,
                        x: 'right',
                        y: 'center',
                        textStyle: {
                            color: '#ffffff'
                        }
                    },
                    calculable: false,
                    series: (function () {
                        var arr = [];
                        var radius_lag = [['70%', '80%'], ['60%', '70%'], ['50%', '60%'], ['40%', '50%'], ['30%', '40%'], ['20%', '30%'], ['10%', '20%']];
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].y < 2) {
                                data[i].y = 2;
                            }
                            arr.push({
                                name: data[i].name + data[i].PersonNum,
                                type: 'pie',
                                center: ['50%', '60%'],
                                radius: radius_lag[i],
                                itemStyle: {
                                    normal: {
                                        label: { show: false },
                                        labelLine: { show: false },
                                        color: color[i]
                                    }
                                },
                                clockWise: false,
                                data: [{
                                    value: data[i].y,
                                    name: data[i].name + data[i].y + '%'
                                },
                                {
                                    value: 140 - data[i].y,
                                    name: 'invisible',
                                    itemStyle: {
                                        normal: {
                                            color: 'rgba(0,0,0,0)',
                                            label: { show: false },
                                            labelLine: { show: false }
                                        },
                                        emphasis: {
                                            color: 'rgba(0,0,0,0)'
                                        }
                                    }
                                }]
                            });
                        }
                        return arr;
                    })()
                };
                mPersonelTwoChart.setOption(option);
            });

        }

        function getPersonelKPIUrl() {
            return "../HongKun/DataRequest.ashx?Command=getPersonelKPI&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getPersonelOneUrl(startIndex, endIndex, mType) {
            return "../HongKun/DataRequest.ashx?Command=getPersonelDynamicCompany&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getPersonelTwoUrl() {
            return "../HongKun/DataRequest.ashx?Command=getPersonelDynamicStructure&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }

    </script>
</body>
</html>
