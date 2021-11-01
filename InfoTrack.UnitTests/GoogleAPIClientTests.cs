using GoogleApi.Exceptions;
using InfoTrack.SEODashboard.External;
using NUnit.Framework;
using System;
using System.Linq;

namespace InfoTrack.UnitTests
{
    public class Tests
    {
        private ISearchClient _searchClient;
        private string _key;
        private string _searchEngineId;
        [SetUp]
        public void Setup()
        {
            _key = "AIzaSyCcnP_a1RQNLIvrbl4u_ctRMLt88uw5wc4";
            _searchEngineId = "dd5f890a99a3b1930";
        }

        [Test]
        public void TestGetSearchResults()
        {
            _searchClient = new GoogleSearchClient(_key, _searchEngineId);
            var results = _searchClient.GetSearchResults("efiling integration");
            Assert.IsTrue(results.Count() > 0);
        }

        [Test]
        public void TestKeywordError()
        {
            _searchClient = new GoogleSearchClient(_key, _searchEngineId);
            try
            {
                var results = _searchClient.GetSearchResults(null);
            }
            catch (GoogleApiException ex)
            {
                Assert.IsTrue(ex.Message == "Query is required");
            }
        }

        [Test]
        public void TestSearchEngineIDError()
        {
            _searchClient = new GoogleSearchClient(_key, null);
            try
            {
                var results = _searchClient.GetSearchResults(_key);
            }
            catch (GoogleApiException ex)
            {
                Assert.IsTrue(ex.Message == "SearchEngineId is required");
            }
        }
    }
}