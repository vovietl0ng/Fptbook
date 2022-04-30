using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Fptbook.Controllers
{
    
    public class CategoriesController : Controller
    {
       
        private readonly FptDbContext _context;
        

        public CategoriesController(FptDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryRequest request)
        {
            //tao category moi , tao ten , mo ta, ngay
            var category = new Category()
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
            //tim category
            var category = _context.Categories.Find(request.Id);
            //update cai moi
            category.Name = request.Name;   
            category.Description = request.Description;
            //luu
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
