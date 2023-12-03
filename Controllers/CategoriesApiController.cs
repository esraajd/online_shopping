using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.Models;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private ApplicationDbContext _context;

        public CategoriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("showparent")]
        public async Task<IActionResult> ShowParentCategories()
        {

            var categories = _context.Categories
               .Where(p => p.ParentCategoryId == null).Select(p=>new CategoryParentModelView
               {
                   ParentId=null,
                   CategoryId=p.CategoryId,
                   Name=p.Name,

               }).ToList();
            
            var json = System.Text.Json.JsonSerializer.Serialize(categories, new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            return Ok(json);
        }

        //[HttpGet]
        //[Route("showallcategories")]
        //public async Task<IActionResult> ShowAllCategories()
        //{
        //    var allCategories = await _context.Categories.ToListAsync();
        //    var rootCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
        //    var jsonObject = new ClosedJsonObject();
        //    jsonObject.categories = new List<ClosedJsonCategory>();
        //    foreach (var category in rootCategories)
        //    {
        //        jsonObject.categories.Add(BuildClosedCategoryObject(category, allCategories));
        //    }
        //    return Ok(jsonObject);
        //}

        //private ClosedJsonCategory BuildClosedCategoryObject(Category category, List<Category> allCategories)
        //{
        //    var children = allCategories.Where(c => c.ParentCategoryId == category.CategoryId).ToList();
        //    var childCategories = new List<ClosedJsonCategory>();
        //    foreach (var child in children)
        //    {
        //        childCategories.Add(BuildClosedCategoryObject(child, allCategories));
        //    }
        //    return new ClosedJsonCategory
        //    {
        //        id = category.CategoryId,
        //        name = category.Name,
        //        children = childCategories
        //    };
        //}
        //public class ClosedJsonObject
        //{
        //    public List<ClosedJsonCategory> categories { get; set; }
        //}
        //public class ClosedJsonCategory
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public List<ClosedJsonCategory> children { get; set; }
        //}

        //[HttpGet]
        //[Route("showallcategories")]
        //public async Task<IActionResult> ShowAllCategories()
        //{
        //    var allCategories = await _context.Categories.ToListAsync();
        //    var rootCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
        //    var jsonObject = new ClosedJsonObject();
        //    jsonObject.categories = new List<ClosedJsonCategory>();
        //    foreach (var category in rootCategories)
        //    {
        //        jsonObject.categories.Add(BuildClosedCategoryObject(category, allCategories));
        //    }

        //    // Remove empty child lists
        //    var newCategories = RemoveEmptyChildLists(jsonObject.categories);

        //    return Ok(newCategories);
        //}

        //private ClosedJsonCategory BuildClosedCategoryObject(Category category, List<Category> allCategories)
        //{
        //    var children = allCategories.Where(c => c.ParentCategoryId == category.CategoryId).ToList();
        //    var childCategories = new List<ClosedJsonCategory>();
        //    foreach (var child in children)
        //    {
        //        childCategories.Add(BuildClosedCategoryObject(child, allCategories));
        //    }
        //    return new ClosedJsonCategory
        //    {
        //        id = category.CategoryId,
        //        name = category.Name,
        //        children = childCategories
        //    };
        //}

        //private List<ClosedJsonCategory> RemoveEmptyChildLists(List<ClosedJsonCategory> categories)
        //{
        //    var newCategories = new List<ClosedJsonCategory>();
        //    foreach (var category in categories)
        //    {
        //        if (category.children.Count > 0)
        //        {
        //            newCategories.Add(category);
        //        }
        //    }
        //    return newCategories;
        //}
        public class ClosedJsonObject
        {
            public List<ClosedJsonCategory> categories { get; set; }
        }

        public class ClosedJsonCategory
        {
            public int id { get; set; }
            public string name { get; set; }
            public List<ClosedJsonCategory> children { get; set; }
        }

        [HttpGet]
        [Route("showallcategories")]
        public async Task<IActionResult> ShowAllCategories()
        {
            var allCategories = await _context.Categories.ToListAsync();
            var rootCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
            var jsonObject = new ClosedJsonObject();
            jsonObject.categories = new List<ClosedJsonCategory>();
            foreach (var category in rootCategories)
            {
                jsonObject.categories.Add(BuildClosedCategoryObject(category, allCategories));
            }

            // Remove empty child lists

            // Get 2 levels of category
            var twoLevelCategories = Get2LevelCategories(jsonObject.categories);

            return Ok(twoLevelCategories);
        }

        private ClosedJsonCategory BuildClosedCategoryObject(Category category, List<Category> allCategories)
        {
            var children = allCategories.Where(c => c.ParentCategoryId == category.CategoryId).ToList();
            var childCategories = new List<ClosedJsonCategory>();
            foreach (var child in children)
            {
                childCategories.Add(BuildClosedCategoryObject(child, allCategories));
            }
            return new ClosedJsonCategory
            {
                id = category.CategoryId,
                name = category.Name,
                children = childCategories
            };
        }

       

        private List<ClosedJsonCategory> Get2LevelCategories(List<ClosedJsonCategory> categories)
        {
            var newCategories = new List<ClosedJsonCategory>();
            foreach (var category in categories)
            {
                if (category.children.Count >= 2)
                {
                    newCategories.Add(category);
                }
            }
            return newCategories;
        }
        [HttpGet]
        [Route("searchproductname")]
        public async Task<ActionResult<IEnumerable<Product>>> ViewPro(string productname)
        {
            var product = from m in _context.Product
                          where m.Name.Contains(productname)
                          select m;

            return Ok(await product.ToListAsync());
        }
        [HttpGet]
        [Route("viewproduct")]
        public async Task<ActionResult<IEnumerable<Product>>> ViewProducts()
        {
            var products = await _context.Product
                 .Select(p => new ProductViewModel
                 {
                     ProductId = p.ProductId,
                     Name = p.Name,
                     Description = p.Description,
                     Image = p.Image,
                     Price = p.Price,
                     CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == p.CategoryId).Name
                 }).ToListAsync();

            if (products.Count == 0)
            {

                NotFound("Not Found");
            }
            return Ok(products);
        }
    }
}
