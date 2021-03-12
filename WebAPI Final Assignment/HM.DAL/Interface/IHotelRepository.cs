using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM.Models;
namespace HM.DAL.Interface
{
    public interface IHotelRepository
    {
        List<Hotel> GetHotels();
        List<Room> GetRooms(string city, decimal? pincode, decimal? price, string category);
        bool IsAvailable(int id,DateTime date);
        Hotel AddHotel(Hotel hotel);
        bool Book(Booking booking);
        bool UpdateBookDate(int id, Booking booking);
        bool UpdateStatus(int id, Booking booking);
        bool DeleteBook(int id);
    }
}
