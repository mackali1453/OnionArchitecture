using System.Net.Http.Headers;

namespace Github.NetCoreWebApp.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Definition { get; set; }
        public List<Product> Products { get; set; }
    }
}
