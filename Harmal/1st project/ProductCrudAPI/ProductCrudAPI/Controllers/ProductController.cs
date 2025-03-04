﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCrudAPI.Models;

namespace ProductCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> Post(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Product>>> Put(Product productData)
        {
            if (productData == null || productData.Id == 0)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(productData.Id);
            if (product == null)
            {
                return NotFound();
            }
              
            product.Name = productData.Name;
            product.Description = productData.Description;
            product.Price = productData.Price;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}