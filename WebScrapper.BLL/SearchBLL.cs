using Models;
using System.Text.RegularExpressions;
using WebScrapper.BLL.Interface;
using WebScrapper.DL.Interface;

namespace WebScrapper.BLL
{
    public class SearchBLL : ISearchBLL
    {
        ISearchDL _seachDL;
        public readonly List<SearchEngine> _searchEngines = new()
        {
            new() { Id = 0, Label = "Google Chrome", Url = "https://www.google.co.uk/search?num=100&q=", Regex = new Regex("<div class=\"MjjYud\">(.+?)</div>") },
            new() { Id = 1, Label = "Bing", Url = "https://www.bing.com/search?&count=100&q=", Regex = new Regex("<li class=\"b_algo(.+?)</li>") },
        };

        public SearchBLL(ISearchDL seachDL)
        {
            _seachDL = seachDL;
        }

        public Task<List<Search>> GetHistory(int count) => _seachDL.GetHistory(count);

        public async Task<Search> Search(string url, string searchPhrase, int searchEngineId)
        {
            if (string.IsNullOrWhiteSpace(url)
                || string.IsNullOrWhiteSpace(searchPhrase)
                || (searchEngineId < 0 || searchEngineId > _searchEngines.Count()))
                throw new ArgumentException();

            var searchEngine = _searchEngines[searchEngineId];

            var searchUrl = $"{searchEngine.Url}{string.Join('+', searchPhrase.Split(' '))}";

            var results = await GetResults(url, searchUrl, searchEngine.Regex);

            var searchObject = new Search()
            {
                Url = url,
                SearchPhrase = searchPhrase,
                SearchEngineId = searchEngineId,
                Rank = string.Join(", ", results),
                Impressions = results.Count()
            };

            await _seachDL.Search(searchObject);

            return searchObject;
        }

        async Task<List<int>> GetResults(string url, string searchUrl, Regex regex)
        {
            var result = new List<int>();
            List<Match> matches;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36 OPR/97.0.0.0");
                var response = await client.GetAsync(searchUrl);

                response.EnsureSuccessStatusCode();
                var htmlContent = await response.Content.ReadAsStringAsync();

                matches = regex.Matches(htmlContent).ToList();
            }

            for (int i = 0; i < matches.Count - 1; i++)
            {
                var item = matches[i].Groups[1].Value;

                if (item.Contains(url))
                    result.Add(i);
            }

            return result;
        }

        public async Task<List<ChartData>> GetChartData()
        {
            var result = await _seachDL.GetHistory(100);
            var response = new List<ChartData>();

            if (result.Count() == 0)
                return null;


            var group = result.GroupBy(x => new
            {
                x.Url,
                x.SearchPhrase
            }).Select(x => x.ToList());


            foreach (var item in group)
            {
                var firstItem = item.First();
                var data = new ChartData()
                {
                    Url = firstItem.Url,
                    SearchPhrase = firstItem.SearchPhrase,
                    Impressions = 0,
                    Count = item.Count()
                };

                item.ForEach(x => data.Impressions += x.Impressions);

                data.Avarage = data.Impressions / data.Count;

                response.Add(data);
            }

            return response;
        }
    }
}