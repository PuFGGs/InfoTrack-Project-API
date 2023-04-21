using Microsoft.AspNetCore.Mvc;
using Models;
using WebScrapper.API.VO;
using WebScrapper.BLL.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScrapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        ISearchBLL _searchBLL;

        public SearchController(ISearchBLL searchBLL)
        {
            _searchBLL = searchBLL;
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchVO searchVO)
        {
            var result = await _searchBLL.Search(searchVO.Url, searchVO.SearchPhrase, searchVO.SearchEngineId);
            return Ok(new SearchResponseVO()
            {
                Url = result.Url,
                SearchPhrase = result.SearchPhrase,
                SearchEngineId = result.SearchEngineId,
                Date = result.Date.ToString(),
                Impressions = result.Impressions,
                Rank = result.Rank,
            });
        }


        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _searchBLL.GetHistory(15);

            if (result.Count() == 0)
                return NotFound();

            var response = result.Select(x => new SearchResponseVO
            {
                Url = x.Url,
                SearchPhrase = x.SearchPhrase,
                SearchEngineId = x.SearchEngineId,
                Date = x.Date.ToString(),
                Impressions = x.Impressions,
                Rank = x.Rank,
            });

            return Ok(response);
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetChart()
        {
            var data = await _searchBLL.GetChartData();

            if (data is null || data.Count() == 0)
                return NotFound();

            return Ok(data);
        }
    }
}
