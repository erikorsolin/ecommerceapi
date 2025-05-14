using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Models;
using EcommerceApi.Data;

// POST/PUT: recebe um DTO → converte em Model para salvar no banco.
// GET: busca um Model no banco → converte em DTO para retornar.

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var productsDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                if (product.Category != null)
                {
                    var productDTO = new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Stock = product.Stock,
                        Category = new CategoryDTO
                        {
                            Id = product.Category.Id,
                            Name = product.Category.Name
                        }
                    };
                    productsDTOs.Add(productDTO);
                }
                else
                {
                    var productDTO = new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Stock = product.Stock
                    };
                    productsDTOs.Add(productDTO);
                }
            }
            return Ok(productsDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            
            ProductDTO productDTO;

            if (product.Category != null)
            {
                productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Category = new CategoryDTO
                    {
                        Id = product.Category.Id,
                        Name = product.Category.Name
                    }
                };
            }
            else
            {
                productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock
                };
            }    
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (productDTO.Price <= 0) return BadRequest("Price must be positive.");
            if (productDTO.Stock < 0) return BadRequest("Stock cannot be negative.");

            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                CategoryId = productDTO.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (productDTO.Price <= 0) return BadRequest("Price must be positive.");
            if (productDTO.Stock < 0) return BadRequest("Stock cannot be negative.");

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.CategoryId = productDTO.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}