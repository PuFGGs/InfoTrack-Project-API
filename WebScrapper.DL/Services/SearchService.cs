using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.DL.Interface;

namespace WebScrapper.DL.Services
{
    public class SearchService : ISearchDL
    {
        public WebScrapperContext _context;

        public SearchService(WebScrapperContext context)
        {
            _context = context;
        }


        public async Task<List<Search>> GetHistory(int count)
            => _context.Searches.OrderByDescending(x => x.Date)
                .Take(count)
                .ToList();
        public async Task Search(Search search)
        {

            await _context.Searches.AddAsync(search);
            await _context.SaveChangesAsync();
        }
    }
}
