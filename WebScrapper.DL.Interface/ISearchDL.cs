using Models;

namespace WebScrapper.DL.Interface
{
    public interface ISearchDL
    {
        Task Search(Search search);
        Task<List<Search>> GetHistory(int count);
    }
}