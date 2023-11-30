using FinalWeb.Models;
using FinalWeb.SalesDataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Collections.Generic;
using System.Diagnostics;
using X.PagedList;
using static System.Formats.Asn1.AsnWriter;

namespace FinalWeb.Controllers
{
    public class HomeController : Controller
    {
        BeverageRetailContext db = new BeverageRetailContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null|| page<0?1 : page.Value;
            
            var lstSanPham = db.Products.AsNoTracking().OrderBy(x=> x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
          

            return View(lst);
        }
        public class IndexViewModel
        {
            public Product Products { get; set; }
            public List<RecommendedProduct> RecommendedProducts { get; set; }
        }
        public List<RecommendedProduct> GetRecommendedProducts()
        {

            try
            {
                var user = uint.Parse(HttpContext.Session.GetString("CustomerID"));


                BeverageRetailContext context = new BeverageRetailContext();
                MLContext mlContext = new MLContext();
                ITransformer trainedModel = null;
                DataViewSchema modelSchema;
                string pathTrainedmodel = "D:\\NguyenTuongVy_K204162009_K20416C\\FinalWeb\\FinalWeb\\TrainedData\\trained.zip";
                trainedModel = mlContext.Model.Load(pathTrainedmodel, out modelSchema);

                var predictionengine = mlContext.Model.CreatePredictionEngine<DataEntry, DataPrediction>(trainedModel);

                var products = context.Products.ToList(); // Assuming you have a Products DbSet in your DbContext
                var recommendedProducts = new List<RecommendedProduct>();
                
                foreach (var product in products)
                {
                    var result = predictionengine.Predict(new DataEntry { CustomerID = user, ProductID = (uint)product.ProductId });
                    if (result.Score > 0.4)
                    {
                        recommendedProducts.Add(new RecommendedProduct { ProductID = product.ProductId, ProductName = product.ProductName, Score  = (float)Math.Min(1, Math.Round(result.Score, 1)), ProductImage = product.ProductImage });
                    }
                }

                return recommendedProducts;
            }
            catch
            {
                return null;
            }
        }
        public IActionResult ProductToType (int TypeID, int ?page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstSanPham = db.Products.AsNoTracking().Where(x => x.CategoryId == TypeID).OrderBy(x => x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
            ViewBag.TypeID = TypeID;
            return View(lst);

          
        }
        public IActionResult Details(int ProductID)
        {
            ViewBag.ShowFullScreenButton = false;
            var product = db.Products.SingleOrDefault(x => x.ProductId == ProductID);
            // var anhSanPham = db.TAnhSps.Where(x=> x.MaSp == maSp).ToList();
            // ViewBag.anhSanPham = anhSanPham;
            var recommendedProducts = GetRecommendedProducts();
            // Create a ViewModel that combines the main product list and recommended products
            var viewModel = new IndexViewModel
            {
                Products = product,
                RecommendedProducts = recommendedProducts
            };
            if (product == null)
            {
                return NotFound(); // or handle the case where the product is not found
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // GET: TimKiem
        public ActionResult Search(string searching, int? page)
        {
            int pageSize = 8;
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
    }
}