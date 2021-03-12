using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HM.BAL;
using HM.BAL.Interface;
using HM.Models;

namespace HotelManagement.Controllers
{
    [RoutePrefix("api")]
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;
        
        public HotelController(HotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }



        // GET: api/Hotels
        //GET all hotels sort by alphabetic order.Response: List of hotels
       [Route("hotels")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.GetHotels());
            return response;
            
        }



        //GET all rooms of hotels with optional parameter by hotel city, pin code, Price, Category. (Default sort by price of room low to high)
        [Route("room")]
        [HttpGet]
        public HttpResponseMessage GetRooms(string city = null, string category = null, decimal? pincode = null, decimal? price=null)
        {
            //decimal pin = pincode.HasValue ? pincode.Value : 0;
            //decimal Price = price.HasValue ? price.Value : 0;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.GetRooms(city,pincode,price,category));
            return response;
        }



        //GET availability of room on some particular date(parameter), Response: should be return True False only
        [Route("isAvailable")]
        [HttpGet]
        public HttpResponseMessage isAvailable(int id,string dateString)
        {
            DateTime date= DateTime.Parse(dateString);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.IsAvailable(id,date));
            return response;
        }






       //POST Booked the room of hotel for particular date with(optional status)
       // POST: api/book
       [Route("Book")]
        [HttpPost]
        public HttpResponseMessage Book([FromBody]Booking model)
        {
            if (model.bookingStatus == null) model.bookingStatus = "Optional";

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.Book(model));
            return response;
        }








        //POST: api/hotel
        [Route("hotels")]
        [HttpPost]
        //POST 5-10 hotels with details of hotel and 3-5 rooms in each hotel with different price and different category.
        public HttpResponseMessage Post([FromBody]Hotel hotel)
        {
            hotel.CreatedDate = DateTime.Today;
            hotel.UpdatedDate = DateTime.Today;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.AddHotel(hotel));
            return response;
        }





        //PUT update booking date for any room by Id
        // PUT: api/BookDate/5
        [Route("UpdateBookDate")]
        [HttpPut]
        public HttpResponseMessage UpdateBookDate([FromBody]Booking model)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.UpdateBookDate(model.roomID, model));
            return response;
        }






        //PUT update booking status by booking Id(optional status to Definitive or Cancelled)
        //PUT: api/BookStatus
        [Route("UpdateStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateStatus([FromBody] Booking model)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.UpdateStatus(model.Id, model));
            return response;
        }





        //DELETE delete booking by booking Id(change status Deleted – soft delete)
        // DELETE: api/Booking/5
        [Route("Booking")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,_hotelManager.DeleteBook(id));
            return response;
        }
    }
}
