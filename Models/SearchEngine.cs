using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models
{
    public class SearchEngine
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public Regex Regex { get; set; }
    }
}
