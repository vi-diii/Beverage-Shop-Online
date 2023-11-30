using FinalWeb.Models;
using FinalWeb.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using X.PagedList;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FinalWeb.Areas.Admin.Models;

namespace FinalWeb.Areas.Admin.Controllers
{

    [Area ("Admin")]
    [Route("Admin")]
    [Route("Admin/HomeAdmin")]

    public class HomeAdminController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeAdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        BeverageRetailContext db = new BeverageRetailContext ();
        [AdminAthentication]
        [Route("ProductCategory")]
        public IActionResult productCategory(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstSanPham = db.Products.AsNoTracking().OrderBy(x => x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
            return View(lst);
        }
        [AdminAthentication]
        [Route("Searching")]
        public ActionResult Search(string searching, int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            IQueryable<Product> query = db.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(searching))
            {
                // Filter products based on the search criteria
                query = query.Where(p => p.ProductName.Contains(searching));
            }

            PagedList<Product> lst = new PagedList<Product>(query.OrderBy(x => x.ProductName), pageNumber, pageSize);
            ViewBag.searching = searching;
            return View(lst);
        }
        [AdminAthentication]
        [Route ("Create")]

        [HttpGet]
        public IActionResult Create() 
        {
            ViewBag.CategoryId = new SelectList(db.Categories.
                ToList(),"CategoryId", "CategoryName");
            return View();
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(Product sp, IFormFile imageFile)
        {

            try
            {
                if (imageFile != null)
                {
                    // Process the uploaded image
                    string uniqueFileName = GetUniqueFileName(imageFile.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "img", uniqueFileName);

                    using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fs);
                    }

                    // Save the image name to the model
                    sp.ProductImage = uniqueFileName;
                }
                else
                {
                        sp.ProductImage = "default.png";  
                }


                // Update the database
                db.Products.Add(sp);
                // Save the product to the database
                db.SaveChanges();

                // Set a success message in TempData
                TempData["SuccessMessage"] = "Product is created successfully!";
           
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                TempData["ErrorMessage"] = "An error occurred while creating the product." + ex.ToString();
            }

          
           return View(sp);

        }
        [AdminAthentication]
        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int ProductId)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.
                ToList(), "CategoryId", "CategoryName");
            var sp = db.Products.Find(ProductId);
            return View(sp);
        }
        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        [HttpPost]
       
        public async Task<IActionResult> Edit(Product sp, IFormFile  imageFile)
        {
            try
            {
                if (imageFile != null)
                {
                    // Process the uploaded image
                    string uniqueFileName = GetUniqueFileName(imageFile.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "img", uniqueFileName);

                    using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fs);
                    }

                    // Save the image name to the model
                    sp.ProductImage = uniqueFileName;
                }
                else
                {
                    if (sp.ProductImage != null)
                    {
                        sp.ProductImage = sp.ProductImage;
                    }
                    else { sp.ProductImage = null; }
                }


                // Update the database
                db.Entry(sp).State = EntityState.Modified;

                // Save the product to the database
                db.SaveChanges();

                // Set a success message in TempData
                TempData["SuccessMessage"] = "Product updated successfully!";
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                TempData["ErrorMessage"] = "An error occurred while updating the product." + ex.ToString();
            }

            return RedirectToAction("Edit", "HomeAdmin", new { ProductId = sp.ProductId });
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        
        [Route("Delete")]
        [HttpGet]
        public IActionResult Delete(int ProductId)
        {
            TempData["Message"] = "";
            var sp = db.OrderDetails.Where(x=>x.ProductId==ProductId);
            if (sp.Count() > 0)
            {
                TempData["Message"] = "This product can't be deleted";
                return RedirectToAction("productCategory","HomeAdmin");
            }
            //var anhSanPhams = db.TANhSps.Where(x => x.Equals(maSanPham)).ToList();
            //if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.Products.Find(ProductId));
            db.SaveChanges();
            TempData["Message"] = "The product has been deleted sucesfully";
            return RedirectToAction("productCategory", "HomeAdmin");
        }

    }

}
