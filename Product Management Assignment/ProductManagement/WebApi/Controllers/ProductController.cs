using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private GatewayEntities db = new GatewayEntities();

        // GET: api/Product
        public IQueryable<Products_List> GetProducts_List()
        {
            return db.Products_List;
        }

        
        // GET: api/Product/5
        [ResponseType(typeof(Products_List))]
        
        public IHttpActionResult GetProducts_List(int id)
        {
             
            Products_List products_List = db.Products_List.Find(id);
            if (products_List == null) 
            {
                return NotFound();
            }
            return Ok(products_List);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducts_List(int id, Products_List products_List)
        {
            
            if (id != products_List.Product_Id)
            {
                return BadRequest();
            }

            db.Entry(products_List).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Products_ListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Product
        [ResponseType(typeof(Products_List))]
        public IHttpActionResult PostProducts_List(Products_List products_List)
        {
            
            db.Products_List.Add(products_List);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = products_List.Product_Id }, products_List);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(Products_List))]
        public IHttpActionResult DeleteProducts_List(int id)
        {
            Products_List products_List = db.Products_List.Find(id);
            if (products_List == null)
            {
                return NotFound();
            }

            db.Products_List.Remove(products_List);
            db.SaveChanges();

            return Ok(products_List);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Products_ListExists(int id)
        {
            return db.Products_List.Count(e => e.Product_Id == id) > 0;
        }
    }
}