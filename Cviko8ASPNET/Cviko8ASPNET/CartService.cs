using Cviko8ASPNET.Models;

namespace Cviko8ASPNET
{
    public class CartService
    {
        //Mělo by se to dělat přes session, bylo ukazane na přednašce
        private List<Product> products = new List<Product>();

        public void Add(Product product) { products.Add(product); }
        public List<Product> GetProducts() { return products; }
        public int Count { get { return products.Count; } }
    }
}
