using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialData.Shared.Models
{
    public class Record
    {
        public int Id { get; set; }
        public required int DataTypeId { get; set; }
        //public required DataType DataType { get; set; }
        public required int FrequencyId { get; set; }
        //public required Frequency Frequency { get; set; }
        public required int PresentationTypeId { get; set; }
        //public required PresentationType PresentationType { get; set; }
        public DateTime Date { get; set; }

        [JsonPropertyName("dateString")]
        public string DateString
        {
            get
            {
                return Date.ToString(
                    FrequencyId switch
                    {
                        // Yearly
                        1 => "yyyy",
                        // Quarterly
                        2 => $"yyyy 'Q'{(Date.Month - 1) / 3 + 1}",
                        // Monthly
                        3 => "MMM \\\'yy",
                        _ => "dd/MM/yyyy"
                    }
                    );
            }
        }
        public decimal Value { get; set; }

    }

    public class LineSeries : Series<Record>
    {
        public string Name { get; set; } = "";
    }

    public class Series<T>
    {
        public List<T> Points { get; set; } = [];
        public (double, double) Margin(Func<T, decimal> f)
        {
            if (Points.Count == 0)
                return (0, 0);
            var list = Points.Select(f);
            var min = (double)list.Min();
            var max = (double)list.Max();
            var margin = (max - min) * 0.1;
            return (min - margin, max + margin);
        }
    }

    public class ScatterPoint
    {
        public ScatterSeries? Series { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }

        //public decimal Z
        //{
        //    get
        //    {
        //        if (Series != null)
        //            return (Date.Year - Series.MinYear) + 1;
        //        return 1;
        //    }
        //}

        public DateTime Date;
        public string DateString { get; set; } = "default";
    }

    public class ScatterSeries : Series<ScatterPoint>
    {
        public string NameX { get; set; } = "";
        public string NameY { get; set; } = "";
        public int MinYear;

        public ScatterSeries() { }
        public ScatterSeries(LineSeries a, LineSeries b)
        {

            List<ScatterPoint> points = [];
            foreach (var ra in a.Points)
            {
                var rb = b.Points.Find(x => x.Date.Equals(ra.Date));
                if (rb == null) continue;
                var point = new ScatterPoint
                {
                    X = ra.Value,
                    Y = rb.Value,
                    DateString = ra.FrequencyId > rb.FrequencyId ? ra.DateString : rb.DateString,
                    Date = ra.FrequencyId < rb.FrequencyId ? ra.Date : rb.Date,
                    Series = this
                };
                points.Add(point);
            }
            Points = points;
            NameX = a.Name;
            NameY = b.Name;
            MinYear = points.Select(x => x.Date.Year).Min();
        }
    }
}
