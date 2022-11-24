using System.IO;

namespace FantasyApi.Data.AzureStorage.Dtos
{
    public class BlobDto
    {
        public string Uri { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}
