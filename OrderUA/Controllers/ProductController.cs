using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using OrderUA.Data;
using OrderUA.Models;
using OrderUA.Models.ViewModels;

namespace OrderUA.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
			_webHostEnvironment = webHostEnvironment;
		}

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product.Include(u => u.Category).Include(u => u.ProductType);

            return View(objList);
        }

        //GET - UPSERT
        public IActionResult Upsert(int? id)
        {
			ProductVM productVM = new ProductVM()
			{
				Product = new Product(),
				CategorySelectList = _db.Category.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				}),
				ProductTypeSelectList = _db.ProductType.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				})
			};

			if (id == null)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _db.Product.Find(id);
				if (productVM.Product == null)
				{
					return NotFound();
				}
				return View(productVM);
			}
        }

		//POST - UPSERT
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM productVM)
		{
			var files = HttpContext.Request.Form.Files;
			string webRootPath = _webHostEnvironment.WebRootPath;

			if (productVM.Product.Id == 0)
			{
				string upload = webRootPath + WC.ImagePath;
				string fileName = Guid.NewGuid().ToString();
				string extension = Path.GetExtension(files[0].FileName);

				using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
				{
					files[0].CopyTo(fileStream);
				}

				productVM.Product.Image = fileName + extension;
				_db.Product.Add(productVM.Product);
			}
			else
			{
				var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);
				if (files.Count > 0)
				{
					string upload = webRootPath + WC.ImagePath;
					string fileName = Guid.NewGuid().ToString();
					string extension = Path.GetExtension(files[0].FileName);
					var oldFile = Path.Combine(upload, objFromDb.Image);
					if (System.IO.File.Exists(oldFile))
					{
						System.IO.File.Delete(oldFile);
					}
					using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}
					productVM.Product.Image = fileName + extension;
				}
				else
				{
					productVM.Product.Image = objFromDb.Image;
				}
				_db.Product.Update(productVM.Product);
			}
			_db.SaveChanges();
			LogInformation("Created/edited product: " + productVM.Product.Name);
			return RedirectToAction("Index");
		}

		//GET - DELETE
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Product product = _db.Product.Include(u => u.Category).Include(u => u.ProductType).FirstOrDefault(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		//POST - DELETE
		[HttpPost]
		[ValidateAntiForgeryToken,ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			var obj = _db.Product.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            _db.Product.Remove(obj);
			_db.SaveChanges();
			LogInformation("Deleted product: " + obj.Name);
			return RedirectToAction("Index");
		}

		private void LogInformation(string log)
		{
			string filePath = "ServerLogs.txt";
			using (StreamWriter writer = new StreamWriter(filePath, true))
			{
				writer.WriteLine(log);
			}
		}
	}
}
