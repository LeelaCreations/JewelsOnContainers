using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _configuration;
        public CatalogController(CatalogContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items([FromQuery]int pageIndex = 0, [FromQuery]int pageSize= 6)
        {
           var itemsCount= await _context.CatalogItems.LongCountAsync();
           var items= await _context.CatalogItems.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            items = ChangePictureUrl(items);
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]/type/{catalogType}/brand/{catalogBrand}")]
        public async Task<IActionResult> Items(int?catalogType,int?catalogBrand, [FromQuery]int pageIndex = 0, [FromQuery]int pageSize = 6)
        {
            var root =(IQueryable<CatalogItem>)_context.CatalogItems;
            if (catalogType.HasValue)
            {
                root = root.Where(c => c.CatalogTypeId == catalogType);
            }
            if (catalogBrand.HasValue)
            {
                root = root.Where(c => c.CatalogBrandId == catalogBrand);
            }
            var itemsCount = await root.LongCountAsync();
            var items = await root.OrderBy(c => c.Name)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            items = ChangePictureUrl(items);
            return Ok(items);
        }
        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(c => c.PictureUrl = c.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _configuration["ExternalCatalogBaseUrl"]));
            return items;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var items =await _context.CatalogTypes.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogBrands()
        {
            var items =await _context.CatalogBrands.ToListAsync();
            return Ok(items);
        }
    }
}