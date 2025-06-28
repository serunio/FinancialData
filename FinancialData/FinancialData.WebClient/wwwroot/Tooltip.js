

window.customTooltipScatter = function({ series, seriesIndex, dataPointIndex, w }) {
    const x =    w.config.series[seriesIndex].data[dataPointIndex].x;
    const y =    w.config.series[seriesIndex].data[dataPointIndex].y;
    const date = w.config.series[seriesIndex].data[dataPointIndex].date;
    console.log(w);
    return `
    <div style="
        border: 1px solid #ccc;
        border-radius: 4px;
        overflow: hidden;
        font-family: sans-serif;
    ">
        <div style="
            background: #008FFB;
            color: white;
            font-weight: bold;
            padding: 6px 8px;
            font-size: 13px;
        ">
            ${date}
        </div>
        <div style="
            background: white;
            color: #333;
            padding: 6px 8px;
            font-size: 12px;
        ">
            X: ${x}<br>
            Y: ${y}
        </div>
    </div>`;
}

