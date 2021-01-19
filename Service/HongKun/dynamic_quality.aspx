<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamic_quality.aspx.cs" Inherits="Service.HongKun.dynamic_quality" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <meta name="viewport" content="width=width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0">
    <meta charset="utf-8">
    <title>品质动态</title>
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
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">本月核查情况</strong>
                    </div>
                    <div class="col-md-12 col-xs-12" style="padding: 10px;">
                        <img style="height: 20px; width: 20px;" src="./img/ic_first.png" onclick="first();">
                        <img style="height: 20px; width: 20px;" src="./img/ic_previous.png" onclick="previous();">
                        <span id="page_num" style="padding: 4px 8px; border: 2px solid #00B4E3; border-radius: 50% 50%;">1</span>
                        <img style="height: 20px; width: 20px;" src="./img/ic_next.png" onclick="next();">
                        <img style="height: 20px; width: 20px;" src="./img/ic_last.png" onclick="last();">
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="quality-one-content" class="row" style="height: 400px;"></div>
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
                        <div id="quality-two-content" class="row" style="height: 400px;"></div>
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
        var color = ['#ff7f50', '#87cefa', '#da70d6', '#32cd32', '#6495ed',
            '#ff69b4', '#ba55d3', '#cd5c5c', '#ffa500', '#40e0d0',
            '#1e90ff', '#ff6347', '#7b68ee', '#00fa9a', '#ffd700',
            '#6b8e23', '#ff00ff', '#3cb371', '#b8860b', '#30e0e0'];
        var mQualityOneChart;
        var mQualityTwoChart;
        var gPage = 1;
        var allCount = 0;
        $(function () {
            document.title = $("#OrganName").val() + '-' + '品质动态';
            mQualityOneChart = echarts.init(document.getElementById('quality-one-content'));
            mQualityTwoChart = echarts.init(document.getElementById('quality-two-content'));
            initQualityOneCharts(1);
            initQualityTwoCharts();

        });
        function first() {
            initQualityOneCharts(1);
        }
        function previous() {
            initQualityOneCharts(gPage - 1);
        }
        function next() {
            initQualityOneCharts(gPage + 1);
        }
        function last() {
            initQualityOneCharts(allCount % 6 > 0 ? parseInt(allCount / 6) + 1 : allCount / 6);
        }

        function initQualityOneCharts(page) {
            var allPage = allCount % 6 > 0 ? parseInt(allCount / 6) + 1 : allCount / 6;
            if (page > allPage) {
                page = allPage;
            }
            if (page < 1) {
                page = 1;
            }
            $('#page_num').html(page);
            gPage = page;
            mQualityOneChart.showLoading();
            var url = getQualityOneUrl(page, 6);
            $.getJSON(url, {}, function (json, textStatus) {
                mQualityOneChart.hideLoading();
                var data = [];
                if (!json.Result) {
                    alert(json.data);
                } else {
                    data = json.data.list;
                    allCount = json.data.count;
                }
                var QmArry = [];
                var QmArry1 = [];
                var QmArry2 = [];
                var QmArry3 = [];
                var QmArry4 = [];
                var QmArry5 = [];
                var QmArry6 = [];
                var QmArryName = [];
                var QmArryCountName = [];
                $.each(data, function (i, item) {
                    QmArry.push(item.RoleName);
                    QmArry1.push(item.TaskRate);
                    QmArry2.push(item.ProblemRate);
                    QmArry3.push(item.CompleteRate);
                    QmArry4.push(item.TaskRateB);
                    QmArry5.push(item.ProblemRateB);
                    QmArry6.push(item.CompleteRateB);
                    QmArryName.push(item.RoleName);
                    QmArryCountName.push(item.TaskCount);
                });
                if (data.length < 1) {
                    for (var i = 0; i < 6; i++) {
                        QmArry.push("无");
                        QmArry1.push(0);
                        QmArry2.push(0);
                        QmArry3.push(0);
                        QmArry4.push(100);
                        QmArry5.push(100);
                        QmArry6.push(100);
                        QmArryName.push('无');
                        QmArryCountName.push(0);
                    }
                }
                var QmCounts = data.length;
                var QmCountsStr = "[";
                var QmCountsStrMin = "[";
                if (QmCounts == 0) {
                    QmCounts = 6;
                }

                for (var i = 0; i < QmCounts; i++) {
                    QmCountsStrMin += "50,";
                    QmCountsStr += "50,";
                }
                if (QmCountsStr != "[") {
                    QmCountsStr = QmCountsStr.substring(0, QmCountsStr.length - 1) + "]";
                    QmCountsStrMin = QmCountsStrMin.substring(0, QmCountsStrMin.length - 1) + "]";
                }
                else {
                    QmCountsStr = QmCountsStr + "]";
                    QmCountsStrMin = QmCountsStrMin + "]";
                }
                var placeHoledStyle = {
                    normal: {
                        color: 'rgba(0,0,0,0)',
                        label: { show: false },
                        labelLine: { show: false }
                    },
                    emphasis: {
                        color: 'rgba(0,0,0,0)'
                    }
                };
                var dataStyle = {
                    normal: {
                        label: { show: false },
                        labelLine: { show: false }
                    }
                };
                var dataStyle2 = {
                    normal: {
                        label: {
                            show: true,
                            position: 'insideLeft',
                            formatter: function (val) {
                                var str = "";
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == val.name) {
                                        str = QmArryCountName[i];
                                    }
                                }
                                return str;
                            }
                        }
                    }
                };
                var dataStyleTaskRate = {
                    normal: {
                        label: {
                            show: true,
                            position: 'insideLeft',
                            formatter: function (val) {
                                var str = "";
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == val.name) {
                                        str = QmArry1[i] + "%";
                                        break;
                                    }
                                }
                                return str;
                            }
                        }
                    }
                };
                var dataStyleProblemRate = {
                    normal: {
                        label: {
                            show: true,
                            position: 'insideLeft',
                            formatter: function (val) {
                                var str = "";
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == val.name) {
                                        str = QmArry2[i] + "%";
                                        break;
                                    }
                                }
                                return str;
                            }
                        }
                    }
                };
                var dataStyleCompleteRate = {
                    normal: {
                        label: {
                            show: true,
                            position: 'insideLeft',
                            formatter: function (val) {
                                var str = "";
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == val.name) {
                                        str = QmArry3[i] + "%";
                                        break;
                                    }
                                }
                                return str;
                            }
                        }
                    }
                };
                var option = {
                    color: color,
                    tooltip: {
                        trigger: 'item',
                        formatter: function (params) {
                            var str = "";
                            if (params.seriesName == '核查任务数') {
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == params.name) {
                                        str = params.name + ' : ' + QmArryCountName[i] + "条";
                                        break;
                                    }
                                }
                                return str;
                            }
                            else if (params.seriesName == '任务完成率') {
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == params.name) {
                                        str = params.name + ' : ' + QmArry1[i] + '%';
                                        break;
                                    }
                                }
                                return str;
                            }
                            else if (params.seriesName == '问题发现率') {
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == params.name) {
                                        str = params.name + ' : ' + QmArry2[i] + '%';
                                        break;
                                    }
                                }
                                return str;
                            }
                            else if (params.seriesName == '复查合格率') {
                                for (var i = 0; i < QmArryName.length; i++) {
                                    if (QmArryName[i] == params.name) {
                                        str = params.name + ' : ' + QmArry3[i] + '%';
                                        break;
                                    }
                                }
                                return str;
                            }
                        }
                    },
                    legend: {
                        y: 10,
                        x: 'center',
                        itemGap: document.getElementById('quality-one-content').offsetWidth / 20,
                        data: ['核查任务数', '任务完成率', '问题发现率', '复查合格率'],
                        textStyle: {
                            color: '#ffffff'
                        }
                    },
                    grid: {
                        borderColor: '#ffffff',
                        y: 80,
                        y2: 30,
                        x: 60,
                        x2: 20
                    },
                    xAxis: [
                        {
                            type: 'value',
                            position: 'top',
                            splitLine: { show: false },
                            axisLabel: { show: false }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'category',
                            splitLine: { show: false },
                            data: QmArry,
                            axisLabel: {
                                textStyle: {
                                    color: '#ffffff'
                                }
                            }
                        }
                    ],
                    series: [
                        {
                            name: '核查任务数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: dataStyle2,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '核查任务数',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: placeHoledStyle,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '任务完成率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: dataStyleTaskRate,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '任务完成率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: placeHoledStyle,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '问题发现率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: dataStyleProblemRate,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '问题发现率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: placeHoledStyle,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '复查合格率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: dataStyleCompleteRate,
                            data: eval(QmCountsStr)
                        },
                        {
                            name: '复查合格率',
                            type: 'bar',
                            stack: '总量',
                            itemStyle: placeHoledStyle,
                            data: eval(QmCountsStr)
                        }
                    ]
                };
                mQualityOneChart.setOption(option);
            });
        }

        function initQualityTwoCharts() {
            mQualityTwoChart.showLoading();
            var url = getQualityQuestionUrl();
            $.getJSON(url, {}, function (json, textStatus) {
                mQualityTwoChart.hideLoading();
                var obj = [];
                if (!json.Result) {
                    alert(json.data);
                } else {
                    obj = eval(json.data);
                }
                var placeHolderStyle = {
                    normal: {
                        color: 'rgba(0,0,0,0)',
                        label: { show: false },
                        labelLine: { show: false }
                    },
                    emphasis: {
                        color: 'rgba(0,0,0,0)'
                    }
                };

                var option = {
                    title: {
                        text: '本月核查问题分布',
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
                            for (var i = 0; i < obj.length; i++) {
                                arr.push({
                                    name: obj[i].name + ':' + obj[i].Proportion + '%',
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
                        for (var i = 0; i < obj.length; i++) {
                            if (obj[i].y < 2) {
                                obj[i].y = 2;
                            }
                            arr.push({
                                name: obj[i].name + '-查见问题数' + obj[i].NowProblemCount + '条',
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
                                    value: obj[i].y,
                                    name: obj[i].name + ':' + obj[i].Proportion + '%'
                                    //name: obj[i].name + obj[i].y + '条'
                                },
                                {
                                    value: 140 - obj[i].y,
                                    name: 'invisible',
                                    itemStyle: placeHolderStyle
                                }]
                            });
                        }
                        return arr;
                    })()
                };
                mQualityTwoChart.setOption(option);
            });

        }

        function getQualityOneUrl(page, size) {
            return "../HongKun/DataRequest.ashx?Command=getqualitycompanystaffing&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }
        function getQualityQuestionUrl() {
            return "../HongKun/DataRequest.ashx?Command=getqualityquestion&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }


    </script>
</body>
</html>
