
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

            return Ok(data);
                

        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {

            var hotelToGet = _dataContext.Set<Hotel>()
                .FirstOrDefault(hotel =>  hotel.Id == Id);

            if (hotelToGet == null)
            {
                return NotFound();
            }

            
            return Ok(hotelToGet);
        }

        [HttpPost]
        public ActionResult Create([FromBody] HotelCreateDto createDto)
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

            _dataContext.Add(hotelToCreate);
            _dataContext.SaveChanges();

            var hotelToReturn = new HotelGetDto
            {
                Id = hotelToCreate.Id,
                Name = hotelToCreate.Name,
                Address = hotelToCreate.Address,
            };

            return Created("", hotelToReturn);
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            var hotelToDelete = _dataContext.Set<Hotel>()
                .FirstOrDefault(hotelToDelete => hotelToDelete.Id == Id);

            if(hotelToDelete == null)
            {
                return NotFound();
            }

            _dataContext.Set<Hotel>().Remove(hotelToDelete);
            _dataContext.SaveChanges();

            return Ok();
        }
    }
}
