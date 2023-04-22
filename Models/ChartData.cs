using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ChartData
    {
        public string? Url { get; set; }
        public string? SearchPhrase { get; set; }
        public int? Impressions { get; set; }
        public int? Count  { get; set; }
        public double? Average  { get; set; }
    }
}
