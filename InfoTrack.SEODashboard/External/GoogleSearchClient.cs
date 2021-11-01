using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Search.Web.Request;
using GoogleApi;
using GoogleApi.Entities.Search;

namespace InfoTrack.SEODashboard.External
{
    public class GoogleSearchClient : ISearchClient
    {
        private string _key { get; set; }
        private string _searchEngineId { get; set; }
        public GoogleSearchClient(string key, string searchEngineId)
        {
            _key = key;
            _searchEngineId = searchEngineId;
        }
        public List<SEOData> GetSearchResults(string keyword)
        {
            var seoData = new List<SEOData>();
            var request = new WebSearchRequest()
            {
                Key = _key,
                SearchEngineId = _searchEngineId,
                Query = keyword
            };

            var response = GoogleSearch.WebSearch.Query(request);

            int rank = 0;
            foreach (var item in response.Items)
            {
                rank++;
                seoData.Add(new SEOData()
                {
                    Rank = rank,
                    Url = item.DisplayLink.Replace("www.", ""),
                    SearchKeywords = keyword
                });
            }

            return seoData;
        }
    }
}
