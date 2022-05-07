using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;

namespace Fptbook.Controllers
{
    
    public class CategoriesController : BaseController
    {
       
        private readonly FptDbContext _context;
        

        public CategoriesController(FptDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string keyword)
        {
            var query = _context.Categories.ToList();
            var categories = query.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                categories = categories.Where(x => x.Name.Contains(keyword));
            }
            var result = categories.Select(x => new CategoryViewModel()
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description
            }).ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryRequest request)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name == request.Name);
            if (category != null)
            {
                throw new Exception("category is exist");
            }
            //tao category moi , tao ten , mo ta, ngay
            category = new Category()
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now

            };
            // add
            _context.Categories.Add(category);
            // luu
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _context.Categories.Find(id);
            var updateRequest = new UpdateCategoryRequest()
            {
                Id = id,
                Name = category.Name,
                Description = category.Description,
            };
            return View(updateRequest);
        }
        [HttpPost]
        public IActionResult UpdateCategory(UpdateCategoryRequest request )
        {
            //kiem tra
            var category = _context.Categories.FirstOrDefault(x => x.Name == request.Name);
            if (category != null)
            {
                throw new Exception("category is exist");
            }
            //tim category
            category = _context.Categories.Find(request.Id);
            //update cai moi
            category.Name = request.Name;   
            category.Description = request.Description;
            //luu
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
       //HTTP delete
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
