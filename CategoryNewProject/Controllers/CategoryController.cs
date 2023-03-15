using CategoryNewProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
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
        public async Task<ActionResult> CategoryList(int PageNumber = 1)
        {
            var row = await db.Categories.ToListAsync();
            ViewBag.TotalPage = Math.Ceiling(row.Count() / 4.0);
            //int totalPagePerView = 4;
            var data = db.Categories.OrderBy(x => x.CategoryId).Skip((PageNumber - 1) * 4).Take(4);
            //var count=data.Count();
            Session["Number"] = PageNumber;
            return View(data);
            // return View();
        }
        public async Task<ActionResult> ProductList(int id,int ProductPageNumber=1)
        {
            Session["CategoryIdforProductCreatio"] = id;
            var data = await db.Products.Where(c => c.CategoryId == id).ToListAsync();
            ViewBag.ProductTotalPage = Math.Ceiling(data.Count() / 3.0);
            var row = db.Products.Where(x=>x.CategoryId==id).OrderBy(x => x.ProductId).Skip((ProductPageNumber - 1) * 3).Take(3);
            Session["Number"] = ProductPageNumber;
            return View(row);
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
            return RedirectToAction("ProductList", new {id= Session["id"] });
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
        public async Task<ActionResult> EditProduct(int id,int number)
        {
            TempData["ProductPageNumber"] = number;
            var data = await db.Products.Where(c => c.ProductId == id).FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> EditProduct(Product product)
        {
            var id = product.CategoryId;
            db.Products.AddOrUpdate(product);


            //db.Entry(product).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("ProductList", new { id,ProductPageNumber = TempData["ProductPageNumber"] });
        }
        public async Task<ActionResult> DeleteProduct(int id,int cid)
        {
            var row = await db.Products.Where(model => model.ProductId == id).FirstOrDefaultAsync();
            db.Products.Remove(row);
            await db.SaveChangesAsync();
            return RedirectToAction("ProductList", new {id= cid, ProductPageNumber= Session["Number"] });
        }
        public async Task<ActionResult> EditCategory(int id,int number)
        {
            TempData["Number"] = number;
            var row = await db.Categories.Where(model => model.CategoryId == id).FirstOrDefaultAsync();
            return View(row);
        }
        [HttpPost]
        public async Task<ActionResult> EditCategory(Category category)
        {

            db.Entry(category).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList", new {PageNumber= TempData["Number"] });

        }
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var row = await db.Categories.Where(model => model.CategoryId == id).FirstOrDefaultAsync();
            db.Categories.Remove(row);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList", new {PageNumber= Session["Number"] });
        }

        public ActionResult Activate(int id,int number)
        {
            var result = db.Database.ExecuteSqlCommand("EXEC active  @CategoryId", new SqlParameter("@CategoryId", id));
            return RedirectToAction("CategoryList", new { PageNumber= number });
        }

        public ActionResult Deactivate(int id,int number)
        {
            var result = db.Database.ExecuteSqlCommand("EXEC deactive  @CategoryId", new SqlParameter("@CategoryId", id));
            return RedirectToAction("CategoryList", new { PageNumber=number });
        }




    }
}