using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void InsertProduct(Product Product);
        void UpdateProduct(Product Product);
        void DeleteProduct(int id);
    }
}
