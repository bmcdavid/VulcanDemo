using EPiServer.Core;

namespace VulcanDemo.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
