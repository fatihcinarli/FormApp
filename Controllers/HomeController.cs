using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FormApp.Controllers;

public class HomeController : Controller
{
   
    public IActionResult Index(string searchString, String category)
    {
        var products = Repository.GetProducts;
        if(!String.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name!.ToLower().Contains(searchString)).ToList();
        }
        if(!String.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        //ViewBag.Categories = new SelectList(Repository.GetCategories, "CategoryId", "CategoryName", category);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.GetCategories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.GetCategories, "CategoryId", "CategoryName");
        return View();
    
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model,IFormFile imageFile)
    {
        
        var extension = "";
        if(imageFile != null)
        {
            var allowedExtensions = new [] {".jpg",".jpeg",".png"};
            extension = Path.GetExtension(imageFile.FileName); // Doysa uzantısını ayırma
            if(!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("","Geçerli bir resim formatı seçiniz. (.jpg,.jpeg,.png)");
            }
        }

        if(ModelState.IsValid)
        {
            if(imageFile != null)
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
            }
        }
        ViewBag.Categories = new SelectList(Repository.GetCategories, "CategoryId", "CategoryName");
        return View(model);
        
    }

    public IActionResult Edit(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var entity = Repository.GetProducts.FirstOrDefault(p => p.ProductId == id);
        if(entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.GetCategories, "CategoryId", "CategoryName");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if(id != model.ProductId)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            if(imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName); // Doysa uzantısını ayırma
            var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
            }

            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
         ViewBag.Categories = new SelectList(Repository.GetCategories, "CategoryId", "CategoryName");
            return View(model);
    }
    public IActionResult Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var entity = Repository.GetProducts.FirstOrDefault(p => p.ProductId == id);
        if(entity == null)
        {
            return NotFound();
        }
        //Repository.DeleteProduct(entity);
        //return RedirectToAction("Index");
        return View("DeleteConfirm", entity);
    }

    [HttpPost]
     public IActionResult Delete(int? id, int ProductId)
    {
        if(id != ProductId)
        {
            return NotFound();
        }


        var entity = Repository.GetProducts.FirstOrDefault(p => p.ProductId == ProductId);

        if(entity == null)
        {
            return NotFound();
        }

        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");
    }

    public IActionResult EditProducts(List<Product> Products)
    {
        foreach (var products in Products)
        {
            Repository.EditIsActive(products);
        }
        return RedirectToAction("Index");
    }


}
