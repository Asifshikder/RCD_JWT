using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCD.DATA.Entity;
using RCD.DATA.Models;
using RCD.SERVICE.Interface;

namespace RCD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("ProductList")]
        public IActionResult Index()
        {
            List<ProductVM> lstProduct = new List<ProductVM>();
            var lst = productService.GetProducts().ToList();
            foreach (var item in lst)
            {
                ProductVM product = new ProductVM();
                product.ID = item.Id;
                product.Title = item.Title;
                product.Description = item.Description;
                product.Price = item.Price;
                product.Avatar = item.Avatar;
                product.Balance = item.Balance;
                product.ImageUrl = item.ImageUrl;
                lstProduct.Add(product);
            }
            return Ok(lstProduct);
        }



        [HttpPost("Create")]
        public IActionResult Create(ProductCreateVM item)
        {
            try
            {
                Product product = new Product();
                product.Title = item.Title;
                product.Description = item.Description;
                product.Price = item.Price;
                product.Avatar = item.Avatar;
                product.Balance = item.Balance;
                product.ImageUrl = item.ImageUrl;
                productService.InsertProduct(product);
                return Ok(new ResponseManager { Message = "Success", IsSuccess = true });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseManager { Message = "Something went wrong", IsSuccess = false });
            }
        }

        [HttpPost("Update")]
        public IActionResult Update(ProductVM item)
        {
            try
            {
                Product product = productService.GetProduct(item.ID);
                product.Title = item.Title;
                product.Description = item.Description;
                product.Price = item.Price;
                product.Avatar = item.Avatar;
                product.Balance = item.Balance;
                product.ImageUrl = item.ImageUrl;
                productService.InsertProduct(product);
                return Ok(new ResponseManager { Message = "Success", IsSuccess = true });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseManager { Message = "Something went wrong", IsSuccess = false });
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete(ProductVM product)
        {
            try
            {
                var prod = productService.GetProduct(product.ID);
                productService.DeleteProduct(prod.Id);
                return Ok(new ResponseManager { Message = "Success", IsSuccess = true });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseManager { Message = "Something went wrong", IsSuccess = false });
            }
        }
        [HttpGet("Details")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var prod = productService.GetProduct(id);
                productService.DeleteProduct(prod.Id);
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseManager { Message = "Something went wrong", IsSuccess = false });
            }
        }
    }
}


