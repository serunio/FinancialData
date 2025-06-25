let tooltipDates = [];
let currentPoint = null;
let currentSeries = null;


window.setupCustomTooltip = function(datesArray) {
    tooltipDates = datesArray;
}

window.customTooltipScatter = function({ series, seriesIndex, dataPointIndex, w }) {
    const x = w.config.series[seriesIndex].data[dataPointIndex].x;
    const y = w.config.series[seriesIndex].data[dataPointIndex].y;
    const label = tooltipDates[dataPointIndex] || "brak";
    return `<b>${label}</b>X: ${x}<br>Y: ${y}`;
}

window.customTooltip = function({ series, seriesIndex, dataPointIndex, w }) {
    currentPoint = dataPointIndex;
    currentSeries = seriesIndex;
    const y = series[seriesIndex][dataPointIndex];
    const seriesName = w.config.series[seriesIndex].name;
    return `
        <div style="display: flex; align-items: center; gap: 6px;">
            <div><b>${seriesName}:</b><br/>${y}</div>
        </div>
    `;
}

window.customFormatter = function(val) {
    console.log(`[DEBUG] ${val}`);
    return `${tooltipDates[currentSeries][currentPoint]}`;
}

//window.tooltipConfigList = [];
//window.newTooltipConfig = function(num) {
//    tooltipConfigList[num] = new TooltipConfig();
//}
//
//class TooltipConfig {
//    constructor() {
//        tooltipDates = [];
//        currentPoint = null;
//        currentSeries = null;
//    }
//
//    setupCustomTooltip(datesArray) {
//        tooltipDates = datesArray;
//    }
//
//    customTooltipScatter({ series, seriesIndex, dataPointIndex, w }) {
//        const x = w.config.series[seriesIndex].data[dataPointIndex].x;
//        const y = w.config.series[seriesIndex].data[dataPointIndex].y;
//        const label = tooltipDates[dataPointIndex] || "brak";
//        return `<b>${label}</b>X: ${x}<br>Y: ${y}`;
//    }
//
//    customTooltip({ series, seriesIndex, dataPointIndex, w }) {
//        currentPoint = dataPointIndex;
//        currentSeries = seriesIndex;
//        const y = series[seriesIndex][dataPointIndex];
//        const seriesName = w.config.series[seriesIndex].name;
//        return `
//        <div style="display: flex; align-items: center; gap: 6px;">
//            <div><b>${seriesName}:</b><br/>${y}</div>
//        </div>
//    `;
//    }
//
//    customFormatter(val) {
//        console.log(`[DEBUG] ${val}`);
//        return `${tooltipDates[currentSeries][currentPoint]}`;
//    }
//}

