using Newtonsoft.Json;
using PagedList;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace ProductManagement.Controllers
{
    //To Give Access To Authenticated User Only

    [Authorize]
    public class ProductController : Controller
    {
        //Lists For Dropdown Menu in ADD OR EDIT Products 

        List<string> categories = new List<string> { "Electronics", "TVs", "Mobile", "Laptops" };
        List<int> Quantity = new List<int> { 1,2,3,4,5,6,7,8,9,10};

        //Database Connection
        GatewayEntities db = new GatewayEntities();

        //To Display The Products List IT Takes The Perameters like Page No. for Pagging, Sort Order for Sorting and Search_data for Searching
        // GET: Product
        public ActionResult Index(int? Page_No, string sortOrder, string CurrentSort, string Search_Data)
        {
            
            IEnumerable<Product> Products_List;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;
            
            // Connecting To the API For Getting The Data
            HttpResponseMessage responce = GlobalVariables.WebApiClient.GetAsync("Product").Result;
            Products_List = responce.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            //For Filter The Data with Search_data
            Products_List = Products_List.Where(product => product.Name.ToLower().Contains(Search_Data.ToLower()) || product.Category.ToLower().Contains(Search_Data.ToLower()));

            //For Sorting The Data
            switch (sortOrder)
            {
                case "Name": 
                    Products_List = Products_List.OrderBy(a => a.Name);
                    break;
                case "Quantity":
                    Products_List = Products_List.OrderBy(a => a.Quantity);
                    break;
                case "Category":
                    Products_List = Products_List.OrderBy(a => a.Category);
                    break;
                case "Price":
                    Products_List = Products_List.OrderBy(a => a.Price);
                    break;
            }

            //For Pass The Pagging
            int Size_Of_Page = 3;
            int No_Of_Page = Page_No ?? 1;
            return View(Products_List.ToPagedList(No_Of_Page, Size_Of_Page));
       
        }

        //To Display The Details Of the Products
        public ActionResult Detail(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product/"+id).Result;
            return View(response.Content.ReadAsAsync<Product>().Result);
        }

        //GET Method For ADD Or Edit the Products
        public ActionResult AddOrEdit(int id = 0)
        {
            //Passing the List of Category and Quantity For Dropdown
            ViewBag.categories = categories;
            ViewBag.quantity = Quantity;

            //Checking If Adding a New Product of Editting Existing Product
            // If id = 0 then Adding New Product
            //If id != 0 then Editing in Existing product
            if (id == 0)
            {
                Session["id"] = "0";
                return View();
            }
            else
            {
                Session["id"] = id;
                // Getting the Product From API for Edit And Passing it to View 
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product/"+ id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Product>().Result);
            }
        }

        //Post Method For Add Or Edit The Products
        [HttpPost]
        public ActionResult AddOrEdit(Product product)
        {
            Product_Item p = new Product_Item();

            //Checking If Adding a New Product of Editting Existing Product
            // If id = 0 then Adding New Product
            //If id != 0 then Editing in Existing product

            if (!(Session["id"].ToString()!="0"))
            {
                // For Adding the New Product
                if (ModelState.IsValid)
                {

                    //Uploading The Image File
                    string imagePath = "~/Images/";
                    string smallImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_small_image_" + Path.GetFileName(product.Small_Image.FileName);
                    string largeImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_large_image_" + Path.GetFileName(product.Lage_Image.FileName);
                    product.Small_Image_path = Path.Combine(imagePath, smallImageName);
                    product.Lage_Image_path = Path.Combine(imagePath, largeImageName);
                    product.Small_Image.SaveAs(Server.MapPath(product.Small_Image_path));
                    product.Lage_Image.SaveAs(Server.MapPath(product.Lage_Image_path));
                    p.Lage_Image_path = product.Lage_Image_path;
                    p.Long_Desc = product.Long_Desc;
                    p.Short_Desc = product.Short_Desc;
                    p.Category = product.Category;
                    p.Name = product.Name;
                    p.Quantity = product.Quantity;
                    p.Small_Image_path = product.Small_Image_path;
                    p.Price = product.Price;

                    //Sending Product Data to the API to Store it into the Database
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Product", p).Result;

                    //For Alert Box
                    if (response.IsSuccessStatusCode)
                    {

                        TempData["SuccessMessage"] = "Product Added Successfully";

                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Something Went Wrong";
                    }

                    return RedirectToAction("Index");
                }
                
            }
            // For Editing The Existing Product
            else
            {

                string smallImageName;
                string largeImageName;
                string imagePath = "~/Images/";

                //If User Changed Small Image then 
                if (product.Small_Image != null)
                {
                    smallImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_small_image_" + Path.GetFileName(product.Small_Image.FileName);
                    product.Small_Image_path = Path.Combine(imagePath, smallImageName);
                    product.Small_Image.SaveAs(Server.MapPath(product.Small_Image_path));

                }
                else
                {
                    product.Small_Image_path = Session["Small_Image_path"].ToString();
                    p.Small_Image_path = Session["Small_Image_path"].ToString();
                }
                //If User Changed Large Image then 

                if (product.Lage_Image != null)
                {
                    largeImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_large_image_" + Path.GetFileName(product.Lage_Image.FileName);
                    product.Lage_Image_path = Path.Combine(imagePath, largeImageName);
                    product.Lage_Image.SaveAs(Server.MapPath(product.Lage_Image_path));


                }
                else
                {
                    product.Lage_Image_path = Session["Large_Image_path"].ToString();
                    p.Lage_Image_path = Session["Large_Image_path"].ToString();
                }

                p.Long_Desc = product.Long_Desc;
                p.Short_Desc = product.Short_Desc;
                p.Category = product.Category;
                p.Name = product.Name;
                p.Quantity = product.Quantity;
                p.Price = product.Price;

                //Sending Details To The API With Product ID
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Product/"+product.Product_Id,p).Result;
                
                //For Alert Box
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Product Updated Successfully";

                }
                else
                {
                    TempData["SuccessMessage"] = "Something Went Wrong";
                }

                return RedirectToAction("Index");
            }
            return View();
            
 
        }

        //For Delete The Product
        public ActionResult Delete(int id)
        {
            //Sending Product Id to the API for Delete The Product 
            HttpResponseMessage responce = GlobalVariables.WebApiClient.DeleteAsync("Product/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Product Deleted Successfully";

            return RedirectToAction("Index");

        }

        

    }
}