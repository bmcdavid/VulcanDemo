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
        IEnumerable<IContent> Items { get; }

        long TotalHits { get; }

        string Version { get; }
    }
    
    [ServiceConfiguration(typeof(ISearchResults))]
    public class CustomSearchResults : ISearchResults
    {
        public CustomSearchResults(IEnumerable<IContent> items, long totalHits, string version)
        {
            Items = items;
            TotalHits = totalHits;
            Version = version;
        }

        public IEnumerable<IContent> Items { get; }

        public long TotalHits { get; }

        public string Version { get; }
    }
}