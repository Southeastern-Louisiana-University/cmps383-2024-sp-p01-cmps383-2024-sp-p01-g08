
using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Entities;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController: ControllerBase
    {
        private readonly DataContext _dataContext;
        public HotelsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dataContext
                .Set<Hotel>()
                .Select(hotel => new HotelGetDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Address = hotel.Address,
                }).ToList();

            return Ok();
                

        }

        [HttpGet("{id}")]
        public ActionResult GetHotelById(int Id)
        {

            var h = _dataContext.Set<Hotel>()
                .FirstOrDefault(h =>  h.Id == Id);

            if (h == null)
            {
                return NotFound();

            }

            
            return Ok();
        }

        [HttpPost]
        public ActionResult CreateHotel([FromBody] HotelCreateDto createDto)
        {
            var hotelToCreate = new Hotel
            {
                Name = createDto.Name,
                Address = createDto.Address,
            };

            if(hotelToCreate.Name == null)
            {

                return BadRequest();
            }

            if(hotelToCreate.Name.Length > 120)
            {
                return BadRequest();
            }

            if (hotelToCreate.Address == null)
            {
                return BadRequest();
            }

   
            _dataContext.Set<Hotel>().Add(hotelToCreate);
            _dataContext.SaveChanges();

            var hotelToReturn = new HotelGetDto
            {
                Id = hotelToCreate.Id,
                Name = hotelToCreate.Name,
                Address = hotelToCreate.Address,
            };

            return Created("", hotelToReturn);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHotel(int Id)
        {
            var h = _dataContext.Set<Hotel>()
                .FirstOrDefault(h => h.Id == Id);

            if(h == null)
            {
                return NotFound();
            }

            _dataContext.Set<Hotel>().Remove(h);
            _dataContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateHotel([FromBody] HotelUpdateDto updateDto, int id)
        {
            
            var hotelToUpdate = _dataContext.Set<Hotel>()
                .FirstOrDefault(h => h.Id == id);

            if (hotelToUpdate == null)
            {
                return NotFound();
            }

            hotelToUpdate.Name = updateDto.Name;
            hotelToUpdate.Address = updateDto.Address;

            _dataContext.SaveChanges();

            var hotelToReturn = new HotelGetDto
            {
                Id = hotelToUpdate.Id,
                Name = hotelToUpdate.Name,
                Address = hotelToUpdate.Address,
            };

            return Ok();

        }
    }
}
