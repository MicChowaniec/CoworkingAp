using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoworkingAp.Models;
using CoworkingAp.Data;

namespace CoworkingAp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeskBookingController : ControllerBase
    {
        private readonly ApiContext _context;


        public DeskBookingController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost] //Create
        public JsonResult CreateEditRoom(Room room )
        {
            if(room.Id==0)
            {
                _context.Rooms.Add(room);

                for (int i = 1; i <= room.NumberOfDesks; i++)
                {
                    
                    _context.Desks.Add(new Desk {Number=i, roomId=room.Id });
                }
            }
            else
            {
                var roomInDb = _context.Rooms.Find(room.Id);

                    if (roomInDb == null)
                {
                    return new JsonResult(NotFound());
                }

            }
            _context.SaveChanges();

            return new JsonResult(Ok(room));
        }
        [HttpGet] //Read
        public JsonResult GetRoom(int id)
        {
            var result = _context.Rooms.Find(id);
                if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }
        [HttpDelete] //Delete
        public JsonResult DeleteRoom(int id)
        {   
            var result = _context.Rooms.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            
            var assignedDesks = _context.Desks.Where(e => e.roomId == id ) ;

            if (assignedDesks != null)
                return new JsonResult(Content("Sorry, desks occupied"));

            _context.Rooms.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());

        }
        [HttpGet()] //Read all desks.
        public JsonResult GetAllDesks()
        {
            var result = _context.Desks.ToList();

            return new JsonResult(Ok(result));

        }
        [HttpPut()] //Update desk resrvations.
        public JsonResult PutReservation(int id, DateTime reservation)
        {
            var result = _context.Desks.Find(id);
            result.reservation = reservation;
            _context.Desks.Update(result);
           _context.SaveChanges();
            return new JsonResult(Ok(result));

        }
        [HttpPut()]
        public JsonResult CancelReservation(int id)
        {
            var result = _context.Desks.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            DateTime d2 = DateTime.Now.AddHours(+24);
            if (DateTime.Compare(result.reservation, d2) >= 24)
            {
                result.reservation = new DateTime(9999,12,31,0,0,0);
                _context.Desks.Update(result);
                _context.SaveChanges();
                return new JsonResult(Ok(result));
            }
            else  return new JsonResult(Content("You cannot cancel reservation"));

        }

        [HttpDelete()]
        public JsonResult DeleteDesk(int id)
        {
            var result = _context.Desks.Find(id);
            if (result == null)
                return new JsonResult(NotFound());


            if (result != null)
            {
                DateTime d2 = DateTime.Now.AddHours(+24);
                if (DateTime.Compare(result.reservation, d2) >= 24)
                {
                    _context.Desks.Remove(result);
                    _context.SaveChanges();
                    return new JsonResult(Ok(result));
                }
                
            }
            return new JsonResult(Content("You cannot delete desk"));
        }


    }
}
