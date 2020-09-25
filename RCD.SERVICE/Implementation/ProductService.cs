using RCD.DATA.Entity;
using RCD.REPO;
using RCD.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Implementation
{
   public class ProductService : IProductService
    {
        private readonly IRepository<Product> ProductRepository;
        public ProductService(IRepository<Product> ProductRepository)
        {
            this.ProductRepository = ProductRepository;
        }

        public void DeleteProduct(int id)
        {
            Product Product = ProductRepository.Get(id);
            ProductRepository.Remove(Product);

        }

        public Product GetProduct(int id)
        {
            return ProductRepository.Get(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return ProductRepository.GetAll();
        }

        public void InsertProduct(Product Product)
        {
            ProductRepository.Insert(Product);
        }

        public void UpdateProduct(Product Product)
        {
            ProductRepository.Update(Product);
        }
    }
}

