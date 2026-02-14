using System.Collections.Generic;

namespace DemoArquitectura.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Guid id);
    }
}