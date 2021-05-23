using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS_labs.Data
{
    public class ChartData
    {
        public static readonly ChartEntry[] PieEntries = new[]{
            new ChartEntry(10)
            {
                Color = SKColor.Parse("#FFFFCC")
            },
            new ChartEntry(20)
            {
                Color = SKColor.Parse("#006633")
            },
            new ChartEntry(25)
            {
                Color = SKColor.Parse("#003366")
            },
            new ChartEntry(5)
            {
                Color = SKColor.Parse("#660000")
            },
            new ChartEntry(40)
            {
                Color = SKColor.Parse("#66B2FF")
            }
        };
        public static readonly IEnumerable<float> First = Enumerable.Range(0, 80).Select(x => { var y = x / 20f - 4; return (float)0; });
        public static readonly IEnumerable<float> Second = Enumerable.Range(81, 160).Select(x => { var y = x / 20f - 4; return (float)Math.Log(y); });
        public static readonly IEnumerable<float> Data = First.Concat(Second);
        public static readonly IEnumerable<ChartEntry> LineEntries = Data.Select(x => new ChartEntry(x) { Color = x == 0 ? SKColor.Parse("#FFFFFF") : SKColor.Parse("#000000") });
    }
}
