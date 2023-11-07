using Backend.Model;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IProductRepository
    {
        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);

        Product GetProductById(int Id);

        IEnumerable<Product> GetProducts();
        //ddsds
    }
}
