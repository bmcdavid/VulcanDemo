using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Web;
using TcbInternetSolutions.Vulcan.Core;
using TcbInternetSolutions.Vulcan.Core.Extensions;
using TcbInternetSolutions.Vulcan.Core.Implementation;

namespace VulcanDemo.Business
{
    [ServiceConfiguration(typeof(ISearchService))]
    public class VulcanSearchService : ISearchService
    {
        IVulcanHandler _VulcanHandler;
        IContentLoader _ContentLoader;

        public bool IsActive => true;

        public VulcanSearchService(IVulcanHandler vulcanHandler, IContentLoader contentLoader)
        {
            _VulcanHandler = vulcanHandler;
            _ContentLoader = contentLoader;
        }

        public ISearchResults Search(string searchText, IEnumerable<ContentReference> searchRoots, HttpContextBase context, string languageBranch, int currentPage, int maxResults)
        {
            var client = _VulcanHandler.GetClient();
            var siteHits = client.GetSearchHits(searchText, currentPage, maxResults, searchRoots: searchRoots);            

            return new CustomSearchResults(ConvertToSearchResponseItem(siteHits.Items), siteHits.TotalHits, "Vulcan");
        }

        private IEnumerable<IContent> ConvertToSearchResponseItem(List<VulcanSearchHit> items)
        {
            foreach(var item in items)
            {
                yield return _ContentLoader.Get<IContent>(new ContentReference(item.Id.ToString()));
            }
        }
    }
}