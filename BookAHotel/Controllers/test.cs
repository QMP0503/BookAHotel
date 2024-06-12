//using BookAHotel.Data;
//using BookAHotel.IRepository;
//using BookAHotel.Models;
//using BookAHotel.Repository;
//using BookAHotel.ViewModel;
//using log4net;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BookAHotel.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookAHotelController : ControllerBase
//    {
//        private readonly  ;
//        private readonly ILog _logger;
//        public BookAHotelController(IServiceRepository serviceRepository, ILog log)
//        {
//            _serviceRepository = serviceRepository;
//            _logger = log;
//        }
//        Search

//        //Edit Room Booking (swagger can only have one [http code])
//        [HttpPost]
//        public JsonResult CreateEdit(string action, string ClientName, string RoomName, string checkInDate, string checkOutDate)
//        {
//            if (action.ToLower() == "edit")
//            {
//                var result = _serviceRepository.EditBooking(ClientName, RoomName, checkInDate, checkOutDate);
//                _logger.Info($"{result.Result}");
//                return new JsonResult(result);

//            }
//            else if (action.ToLower() == "create")
//            {
//                var result = _serviceRepository.CreateBooking(ClientName, RoomName, checkInDate, checkOutDate);
//                _logger.Info($"{result.Result.Value}");
//                return new JsonResult(result);
//            }
//            else
//            {
//                return new JsonResult("Enter valid action" + NotFound());
//            }

//        }

//        Get
//        [HttpGet] //check if i want to diplay client booking
//        public async Task<JsonResult> Get(string Name, string type)
//        {
//            var result = await _serviceRepository.GetAsync(Name, type);
//            _logger.Info($"{result.StatusCode}");
//            return new JsonResult(result);
//        }

//        [HttpDelete]
//        public async Task<JsonResult> Delete(string ClientName, string RoomName)
//        {
//            var result = await _serviceRepository.DeleteAsync(ClientName, RoomName);
//            _logger.Info($"{result}");
//            return new JsonResult(result);
//        }

//        Make api to edit roomtype price


//    }
//}
