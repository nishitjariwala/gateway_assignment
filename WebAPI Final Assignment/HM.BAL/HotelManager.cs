using HM.BAL.Interface;
using HM.DAL.Interface;
using HM.DAL.Repository;
using HM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.BAL
{
    public class HotelManager : IHotelManager
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelManager(HotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public Hotel AddHotel(Hotel hotel)
        {
           return _hotelRepository.AddHotel(hotel);
        }

        public bool Book(Booking booking)
        {
            return _hotelRepository.Book(booking);
        }


        public bool DeleteBook(int id)
        {
            return _hotelRepository.DeleteBook(id);
        }


        public List<Hotel> GetHotels()
        {
            return _hotelRepository.GetHotels();
        }



        public List<Room> GetRooms(string city, decimal? pincode, decimal? price, string category)
        {
            return _hotelRepository.GetRooms(city, pincode, price, category);
        }

        public bool IsAvailable(int id,DateTime date)
        {
            return _hotelRepository.IsAvailable(id, date);
        }

        public bool UpdateBookDate(int id, Booking booking)
        {
            return _hotelRepository.UpdateBookDate(id, booking);
        }

        public bool UpdateStatus(int id,Booking model)
        {
            return _hotelRepository.UpdateStatus(id, model);
        }
    }
}

