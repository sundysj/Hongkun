<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamic_equipment.aspx.cs" Inherits="Service.HongKun.dynamic_equipment" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <meta name="viewport" content="width=width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0">
    <meta charset="utf-8">
    <title>设备动态</title>
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
                <div class="row_content content_body">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div id="equipment-one-content" style="height: 400px;"></div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div id="equipment-two-content" style="height: 400px;"></div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div id="equipment-three-content" style="height: 400px;"></div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div id="equipment-four-content" style="height: 400px;"></div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 ">
                <div class="row_content content_body row">
                    <span class="rowsli_one boders"></span>
                    <span class="rowsli_two boders"></span>
                    <span class="colsli_one boders"></span>
                    <span class="colsli_two boders"></span>
                    <div class="col-md-12 col-xs-12">
                        <strong style="line-height: 40px;" class="strong_tip">当前设备状态</strong>
                    </div>
                    <div class="col-md-12 col-xs-12">
                        <div id="equipment-five-content" class="row" style="height: 400px;"></div>
                    </div>
                </div>
            </div>
            <
        </div>
    </div>
    <form action="/" runat="server">
        <asp:HiddenField ID="OrganCode" runat="server" />
        <asp:HiddenField ID="OrganName" runat="server" />
        <asp:HiddenField ID="UserCode" runat="server" />
    </form>
    <script type="text/javascript">
        var mEquipmentOneChart;
        var mEquipmentTwoChart;
        var mEquipmentThreeChart;
        var mEquipmentFourChart;
        var mEquipmentFiveChart;

        $(function () {
            document.title = $("#OrganName").val() + '-' + '设备动态';
            mEquipmentOneChart = echarts.init(document.getElementById('equipment-one-content'));
            mEquipmentTwoChart = echarts.init(document.getElementById('equipment-two-content'));
            mEquipmentThreeChart = echarts.init(document.getElementById('equipment-three-content'));
            mEquipmentFourChart = echarts.init(document.getElementById('equipment-four-content'));
            mEquipmentFiveChart = echarts.init(document.getElementById('equipment-five-content'));
            initEquipmentCharts();
            initEquipmentStatusCharts();
        });

        function initEquipmentStatusCharts() {
            mEquipmentFiveChart.showLoading();
            var url = getEquipmentStatusUrl();
            $.ajax({
                url: url, success: function (json, status, xhr) {
                    json = eval('(' + json + ')');
                    var data = null;
                    if (!json.Result) {
                        alert(json.data);
                    } else {
                        data = json.data;
                    }
                    var strArr = [], dataArr = [];
                    var sbArr = [], zcArr = [], wxArr = [], wbArr = [], gzArr = [], zctjArr = [], yctjArr = [], zwztArr = [];

                    if (null != data && data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            strArr.push(data[i]["RankName"]);
                            sbArr.push(data[i]["设备数量"]);
                            zcArr.push(data[i]["正常数量"]);
                            wxArr.push(data[i]["维修数量"]);
                            wbArr.push(data[i]["维保数量"]);
                            gzArr.push(data[i]["故障数量"]);
                            zctjArr.push(data[i]["正常停机数量"]);
                            yctjArr.push(data[i]["异常停机数量"]);
                            zwztArr.push(data[i]["暂无状态数量"]);
                            dataArr.push(50);
                        }
                    } else {
                        for (var i = 0; i < 6; i++) {
                            strArr.push("无");
                        }
                        for (var u = 0; u < 8; u++) {
                            sbArr.push(0);
                            zcArr.push(0);
                            wxArr.push(0);
                            wbArr.push(0);
                            gzArr.push(0);
                            zctjArr.push(0);
                            yctjArr.push(0);
                            zwztArr.push(0);
                        }
                        dataArr.push(50, 50, 50, 50, 50, 50, 50, 50);
                    }
                    var placeHoledStyle = {
                        normal: {
                            barBorderColor: 'rgba(0,0,0,0)',
                            color: 'rgba(0,0,0,0)'
                        },
                        emphasis: {
                            barBorderColor: 'rgba(0,0,0,0)',
                            color: 'rgba(0,0,0,0)'
                        }
                    };

                    var dataStyle = {
                        normal: {
                            label: {
                                show: true,
                                position: 'insideLeft',
                                formatter: '0'
                            }
                        }
                    };
                    var x_datas = ['总数', '正常', '维修', '维保', '故障', '正常停机', '异常停机', '暂无状态'];

                    var option = {
                        color: ["#ff7f50", "#87cefa", "#da70d6", "#32cd32", "#6495ed", "#ff69b4", "#ba55d3", "#cd5c5c", "#ffa500", "#40e0d0", "#1e90ff", "#ff6347", "#7b68ee", "#00fa9a", "#ffd700", "#6699FF", "#ff6666", "#3cb371", "#b8860b", "#30e0e0"],
                        tooltip: {
                            trigger: 'item',
                            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                            },
                            formatter: function (params) {
                                var str = "";
                                if (params.seriesName == '总数') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + sbArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '正常') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + zcArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '维修') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + wxArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '维保') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + wbArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '故障') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + gzArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '正常停机') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + zctjArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '异常停机') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + yctjArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                                else if (params.seriesName == '暂无状态') {
                                    for (var i = 0; i < strArr.length; i++) {
                                        if (strArr[i] == params.name) {
                                            str = params.name + ' : ' + zwztArr[i];
                                            break;
                                        }
                                    }
                                    return str;
                                }
                            }
                        },
                        legend: {
                            y: 20,
                            itemGap: document.getElementById('equipment-five-content').offsetWidth / 24,
                            textStyle: {
                                color: '#ffffff'
                            },
                            data: x_datas
                        },
                        grid: {
                            y: 100,
                            y2: 20,
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
                                axisLabel: {
                                    textStyle: {
                                        color: '#ffffff'
                                    }
                                },
                                data: strArr
                            }
                        ],
                        series: [
                            {
                                name: '总数',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["设备数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '总数',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '正常',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["正常数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '正常',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '维修',
                                type: 'bar',
                                stack: '总量',
                                //itemStyle: dataStyle,
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["维修数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '维修',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '维保',
                                type: 'bar',
                                stack: '总量',
                                //itemStyle: dataStyle,
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["维保数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '维保',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '故障',
                                type: 'bar',
                                stack: '总量',
                                //itemStyle: dataStyle,
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["故障数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '故障',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '正常停机',
                                type: 'bar',
                                stack: '总量',
                                //itemStyle: dataStyle,
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["正常停机数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '正常停机',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '异常停机',
                                type: 'bar',
                                stack: '总量',
                                //itemStyle: dataStyle,
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["异常停机数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '异常停机',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            },
                            {
                                name: '暂无状态',
                                type: 'bar',
                                stack: '总量',
                                //itemStyle: dataStyle,
                                itemStyle: (function () {
                                    if (data != null && data.length > 0) {
                                        dataStyle = {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'insideLeft',
                                                    formatter: function (val) {
                                                        var str = "";
                                                        for (var i = 0; i < data.length; i++) {
                                                            if (data[i]["RankName"] == val.name) {
                                                                str = data[i]["暂无状态数量"];
                                                                break;
                                                            }
                                                        }
                                                        return str;
                                                    }
                                                }
                                            }
                                        };
                                    }
                                    return dataStyle;
                                })(),
                                data: dataArr
                            },
                            {
                                name: '暂无状态',
                                type: 'bar',
                                stack: '总量',
                                itemStyle: placeHoledStyle,
                                data: dataArr
                            }
                        ]
                    };
                    mEquipmentFiveChart.setOption(option);
                },
                error: function (xhr) {
                    alert("错误提示： " + xhr.status + " " + xhr.statusText);
                },
                complete: function (xhr, status) {
                    mEquipmentFiveChart.hideLoading();
                }
            }
            );
        }

        function initEquipmentCharts() {
            mEquipmentOneChart.showLoading();
            mEquipmentTwoChart.showLoading();
            mEquipmentThreeChart.showLoading();
            mEquipmentFourChart.showLoading();
            var url = getEquipmentUrl();

            $.ajax({
                dataType: "json", url: url, data: {}, success: function (json, status, xhr) {
                    var data = "";
                    if (!json.Result) {
                        alert(json.data);
                    } else {
                        data = json.data;
                    }
                    var eqSt = 99, eqXjbx = 99, eqJscl = 99, eqXjwc = 99, eqWbwc = 99;
                    var str = data.split(",");
                    if (str.length > 0) {
                        eqSt = str[0];
                        eqXjbx = str[1];
                        eqJscl = str[2];
                        eqXjwc = str[3];
                        eqWbwc = str[4];
                        eqGZL = str[5];
                    }
                    var option = {
                        tooltip: {
                            formatter: "{a} <br/>{b} : {c}%"
                        },
                        series: [
                            {
                                name: '',
                                type: 'gauge',
                                splitNumber: 10,
                                axisLine: {
                                    lineStyle:
                                        {
                                            color: [[0.2, '#ff4500'], [0.8, '#48b'], [1, '#228b22']],
                                            width: 8
                                        }
                                },
                                axisTick: {
                                    splitNumber: 10,
                                    length: 12,
                                    lineStyle: {
                                        color: 'auto'
                                    }
                                },
                                axisLabel: {
                                    textStyle: {
                                        color: 'auto'
                                    }
                                },
                                splitLine: {
                                    show: true,
                                    length: 30,
                                    lineStyle: {
                                        color: 'auto'
                                    }
                                },
                                pointer: {
                                    width: 5
                                },
                                title: {
                                    show: true,
                                    offsetCenter: [0, '-40%'],
                                    textStyle: {
                                        color: '#ffffff',
                                        fontSize: 12
                                    }
                                },
                                detail: {
                                    formatter: '{value}%',
                                    textStyle: {
                                        color: 'auto',
                                        fontWeight: 'bolder'
                                    }
                                },
                                data: [{ value: eqSt, name: '本月设备完好率' }]
                            }
                        ]
                    };
                    var option2 = {
                        tooltip: {
                            formatter: "{a} <br/>{c} {b}"
                        },
                        series: [
                            {
                                name: '业务指标',
                                type: 'gauge',
                                startAngle: 180,
                                endAngle: 0,
                                center: ['50%', '70%'],
                                radius: 180,
                                axisLine: {
                                    lineStyle: {
                                        width: 50,
                                        color: [[0.2, '#228b22'], [0.8, '#4488bb'], [1, '#ff4500']]
                                    }
                                },
                                axisTick: {
                                    splitNumber: 10,
                                    length: 12,
                                },
                                axisLabel: {
                                    formatter: function (v) {
                                        switch (v + '') {
                                            case '10': return '低';
                                            case '50': return '中';
                                            case '90': return '高';
                                            default: return '';
                                        }
                                    },
                                    textStyle: {
                                        color: '#fff',
                                        fontSize: 12,
                                        fontWeight: 'bolder'
                                    }
                                },
                                pointer: {
                                    width: 10,
                                    length: '90%',
                                    color: 'rgba(255, 255, 255, 0.8)'
                                },
                                title: {
                                    show: true,
                                    offsetCenter: [0, '-40%'],
                                    textStyle: {
                                        color: '#fff',
                                        fontSize: 12
                                    }
                                },
                                detail: {
                                    show: true,
                                    backgroundColor: 'rgba(0,0,0,0)',
                                    borderWidth: 0,
                                    borderColor: '#ccc',
                                    width: 100,
                                    height: 40,
                                    offsetCenter: [0, -40],
                                    formatter: '{value}%',
                                    textStyle: {
                                        fontSize: 15
                                    }
                                },
                                data: [{ value: eqXjwc, name: '本月巡检完成率' }]
                            }
                        ]
                    };
                    var option3 = {
                        title: {
                            text: '本月维保情况',
                            x: 'center',
                            textStyle: {
                                fontSize: 12
                            },
                            show: false
                        },
                        tooltip: {
                            formatter: "{a} <br/>{b} : {c}%"
                        },
                        series: [
                            {
                                name: '业务指标',
                                type: 'gauge',
                                startAngle: 180,
                                endAngle: 0,
                                center: ['50%', '70%'],
                                radius: 180,
                                axisLine: {
                                    lineStyle: {
                                        width: 50,
                                        color: [[0.2, '#228b22'], [0.8, '#4488bb'], [1, '#ff4500']]
                                    }
                                },
                                axisTick: {
                                    splitNumber: 10,
                                    length: 12,
                                },
                                axisLabel: {
                                    show: true,
                                    formatter: function (v) {
                                        switch (v + '') {
                                            case '10': return '低';
                                            case '50': return '中';
                                            case '90': return '高';
                                            default: return '';
                                        }
                                    },
                                    textStyle: {
                                        color: '#fff',
                                        fontSize: 12,
                                        fontWeight: 'bolder'
                                    }
                                },
                                pointer: {
                                    width: 10,
                                    length: '90%',
                                    color: 'rgba(255, 255, 255, 0.8)'
                                },
                                title: {
                                    show: true,
                                    offsetCenter: [0, '-40%'],
                                    textStyle: {
                                        color: '#fff',
                                        fontSize: 12
                                    }
                                },
                                detail: {
                                    show: true,
                                    backgroundColor: 'rgba(0,0,0,0)',
                                    borderWidth: 0,
                                    borderColor: '#ccc',
                                    width: 100,
                                    height: 40,
                                    offsetCenter: [0, -40],
                                    formatter: '{value}%',
                                    textStyle: {
                                        fontSize: 15
                                    }
                                },
                                data: [{ value: eqWbwc, name: '本月维保完成率' }]
                            }
                        ]
                    };
                    var option4 = {
                        tooltip: {
                            formatter: "{a} <br/>{b} : {c}%"
                        },
                        series: [
                            {
                                name: '',
                                type: 'gauge',
                                splitNumber: 10,
                                axisLine: {
                                    lineStyle: {
                                        color: [[0.2, '#ff4500'], [0.8, '#48b'], [1, '#228b22']],
                                        width: 8
                                    }
                                },
                                axisTick: {
                                    splitNumber: 10,
                                    length: 12,
                                    lineStyle: {
                                        color: 'auto'
                                    }
                                },
                                axisLabel: {
                                    textStyle: {
                                        color: 'auto',
                                    }
                                },
                                splitLine: {
                                    show: true,
                                    length: 30,
                                    lineStyle: {
                                        color: 'auto'
                                    }
                                },
                                pointer: {
                                    width: 5
                                },
                                title: {
                                    show: true,
                                    offsetCenter: [0, '-40%'],
                                    textStyle: {
                                        color: '#ffffff',
                                        fontSize: 12
                                    }
                                },
                                detail: {
                                    formatter: '{value}%',
                                    textStyle: {
                                        color: 'auto',
                                        fontWeight: 'bolder'
                                    }
                                },
                                data: [{ value: eqGZL, name: '本月设备故障率' }]
                            }
                        ]
                    };
                    mEquipmentOneChart.setOption(option);
                    mEquipmentTwoChart.setOption(option2);
                    mEquipmentThreeChart.setOption(option3);
                    mEquipmentFourChart.setOption(option4);
                },
                error: function (xhr) {
                    alert("错误提示： " + xhr.status + " " + xhr.statusText);
                },
                complete: function (xhr, status) {
                    mEquipmentOneChart.hideLoading();
                    mEquipmentTwoChart.hideLoading();
                    mEquipmentThreeChart.hideLoading();
                    mEquipmentFourChart.hideLoading();
                }
            }
            );
        }

        function getEquipmentUrl() {
            return "../HongKun/DataRequest.ashx?Command=getEquipmentDynamic&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }

        function getEquipmentStatusUrl() {
            return "../HongKun/DataRequest.ashx?Command=getEquipmentStatus&organcode=" + $("#OrganCode").val() + "&usercode=" + $("#UserCode").val();
        }

    </script>
</body>
</html>
