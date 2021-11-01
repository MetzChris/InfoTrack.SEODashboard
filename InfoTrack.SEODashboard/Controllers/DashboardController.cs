using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Search.Common;
using GoogleApi.Entities.Search.Common.Enums;
using GoogleApi.Entities.Search.Web.Request;
using GoogleApi.Exceptions;
using Language = GoogleApi.Entities.Search.Common.Enums.Language;
using GoogleApi;
using InfoTrack.SEODashboard.ViewModels;
using InfoTrack.SEODashboard.Models;
using InfoTrack.SEODashboard.External;

namespace InfoTrack.SEODashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ISearchClient _searchClient;

        public DashboardController(ILogger<DashboardController> logger, ISearchClient searchClient)
        {
            _logger = logger;
            _searchClient = searchClient;
        }

        [HttpGet]
        public DashboardModel Get(string keyword)
        {
            
            var seoData = new List<SEOData>();
            var model = new DashboardModel();
            try
            {
                seoData = _searchClient.GetSearchResults(keyword);

                model.seoData = seoData;
                model.rankData = seoData.GroupBy(item => item.Url).Select(x => new RankData { URL = x.Key, RankRating = x.ToList().Sum(y => y.Rank), NumberOfAppearances = x.ToList().Count() }).OrderByDescending(x => x.RankRating).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return model;
        }
    }
}
