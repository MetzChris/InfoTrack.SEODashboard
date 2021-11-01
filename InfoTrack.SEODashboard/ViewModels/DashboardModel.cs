using InfoTrack.SEODashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.SEODashboard.ViewModels
{
    public class DashboardModel
    {
        public List<SEOData> seoData { get; set; }
        public List<RankData> rankData { get; set; }
    }
}
