using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.SEODashboard.External
{
    public interface ISearchClient
    {
        public List<SEOData> GetSearchResults(string keywords);
    }
}
