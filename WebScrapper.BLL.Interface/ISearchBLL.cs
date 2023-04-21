using Models;

namespace WebScrapper.BLL.Interface
{
    public interface ISearchBLL
    {
        Task<Search> Search(string url, string searchPhrase, int searchEngineId);
        Task<List<Search>> GetHistory(int count);
        Task<List<ChartData>> GetChartData();
    }
}