using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Search
    {
        public int Id { get; set; }
        public string? Url { get; set; }

        public string? SearchPhrase { get; set; }

        public int? SearchEngineId { get; set; }

        public string? Rank { get; set; }

        public int? Impressions { get; set; }
        public DateTime? Date { get; set; } = DateTime.UtcNow;
    }
}
