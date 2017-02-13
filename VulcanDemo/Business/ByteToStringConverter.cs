using EPiServer.ServiceLocation;
using System.Text;
using TcbInternetSolutions.Vulcan.AttachmentIndexer;

namespace VulcanDemo.Business
{
    [ServiceConfiguration(typeof(IVulcanBytesToStringConverter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class BytesToStringConverter : IVulcanBytesToStringConverter
    {
        public string ConvertToString(byte[] bytes, string mimeType)
        {
            var mType = mimeType.ToLowerInvariant();

            if (mType == "application/pdf")
            {
                using (var reader = new iTextSharp.text.pdf.PdfReader(bytes))
                {
                    StringBuilder s = new StringBuilder();
                    var parserStrategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                    var totalPages = reader.NumberOfPages;

                    for (int i = 1; i <= totalPages; i++)
                    {
                        s.AppendFormat(" {0}", iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i, parserStrategy));
                    }

                    return s.ToString().Trim();
                }
            }

            return null;
        }
    }
}