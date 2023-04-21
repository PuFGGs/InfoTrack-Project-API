namespace WebScrapper.API.VO
{
    public class SearchResponseVO
    {
        public string? Url { get; set; }

        public string? SearchPhrase { get; set; }

        public int? SearchEngineId { get; set; }

        public string? Rank { get; set; }

        public int? Impressions { get; set; }

        public string? Date { get; set; }
    }
}
