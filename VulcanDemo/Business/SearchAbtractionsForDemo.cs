using EPiServer.Core;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Web;

namespace VulcanDemo.Business
{
    public interface ISearchService
    {
        bool IsActive { get; }

        ISearchResults Search(string searchText, IEnumerable<ContentReference> searchRoots, HttpContextBase context, string languageBranch, int currentPage, int maxResults);
    }

    public interface ISearchResults
    {
        IEnumerable<IContent> Items { get; set; }

        long TotalHits { get; set; }

        string Version { get; set; }
    }
        
    [ServiceConfiguration(typeof(ISearchResults), Lifecycle = ServiceInstanceScope.Transient)]
    public class CustomSearchResults : ISearchResults
    {
        public CustomSearchResults(IEnumerable<IContent> items, long totalHits, string version)
        {
            Items = items;
            TotalHits = totalHits;
            Version = version;
        }

        public IEnumerable<IContent> Items { get; set; }

        public long TotalHits { get; set; }

        public string Version { get; set; }
    }
}