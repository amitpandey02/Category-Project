using CategoryNewProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CategoryNewProject.Controllers
{

    public class CategoryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Category;
        public async Task<ActionResult> CategoryList()
        {
            var row = await db.Categories.ToListAsync();

            return View(row);
        }
        public async Task<ActionResult> ProductList(int id)
        {

            Session["CategoryIdforProductCreatio"] = id;
            var data = await db.Products.Where(c => c.CategoryId == id).ToListAsync();
            return View(data);
        }
        public ActionResult AddProduct(int id)
        {

            Session["id"] = id;
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> AddProduct(Product product, FormCollection fs)
        {
            product.CategoryId = Convert.ToInt32(fs["Id"]);
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        public async Task<ActionResult> EditProduct(int id)
        {
           var data = await db.Products.Where( c=> c.ProductId== id).FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> EditProduct(Product product )
        {
            var id = product.CategoryId;
            db.Products.AddOrUpdate(product);
     
            //db.Entry(product).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("ProductList", new {id});
        }
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var row = await db.Products.Where(model => model.ProductId == id).FirstOrDefaultAsync();
            db.Products.Remove(row);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        public async Task<ActionResult> EditCategory(int id)
        {
            var row = await db.Categories.Where(model => model.CategoryId == id).FirstOrDefaultAsync();
            return View(row);
        }
        [HttpPost]
        public async Task<ActionResult> EditCategory(Category category)
        {

            db.Entry(category).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");

        }
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var row = await db.Categories.Where(model => model.CategoryId == id).FirstOrDefaultAsync();
            db.Categories.Remove(row);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        


    }
}