using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a <see cref="ChartType.Bubble"/> data series in Blazor
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/bubble-charts">Blazor Example</see>
    /// </remarks>
    public class ApexBubbleSeriesDirect<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        /// <summary>
        /// This will determine where each bubble is drawn on the Y-axis.
        /// </summary>
        [Parameter] public Func<TItem, decimal> YValue { get; set; }
    
        /// <summary>
        /// This will determine the size of each bubble.
        /// </summary>
        [Parameter] public Func<TItem, DateTime> ZValue { get; set; }

        [Parameter] public Func<TItem, object> Extra { get; set; }

        /// <summary>
        /// Boolean to detrermine whether the size of bubbles changes based on the Z value.
        /// </summary>
        [Parameter] public bool EnableSize { get; set; } = false;

        /// <summary>
        /// Expression to determine the ordering of X-Values in the series
        /// </summary>
        [Parameter] public Func<BubblePoint<TItem>, object> OrderBy { get; set; }

        /// <summary>
        /// Expression to determine the inverse ordering of X-Values in the series
        /// </summary>
        [Parameter] public Func<BubblePoint<TItem>, object> OrderByDescending { get; set; }

        /// <summary>
        /// Function to conditionally modify individual data points in the series
        /// </summary>
        [Parameter] public Action<BubblePoint<TItem>> DataPointMutator { get; set; }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        /// <inheritdoc/>
        public ChartType GetChartType()
        {
            return ChartType.Bubble;
        }

        /// <inheritdoc/>
        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                return Enumerable.Empty<IDataPoint<TItem>>();
            }

            int i = items.Count()-1;
            var data = items.Select(d => new BubblePoint<TItem>
            {
                X = XValue.Invoke(d),
                Y = YValue.Invoke(d),
                Z = EnableSize ? ZValue.Invoke(d).Year + ZValue.Invoke(d).Month * (decimal)0.08 - ZValue.Invoke(items.First()).Year + 3 : 5 + (decimal)0.00_001 * i-- ,
                Items = new List<TItem>() {d},
                FillColor = GetPointColor(d),
                Extra = Extra.Invoke(d)
            });

            if (OrderBy != null)
            {
                if(!EnableSize)
                    data = data.OrderBy(OrderBy);
                if (EnableSize)
                    data = data.OrderByDescending(OrderBy);
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(OrderByDescending);
            }

            return UpdateDataPoints(data, DataPointMutator);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}
