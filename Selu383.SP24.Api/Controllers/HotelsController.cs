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

            return Ok(200);

        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int id)
        {
            var data = _dataContext
                .Set<Hotel>()
                .Select(hotel => new HotelGetDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Address = hotel.Address,
                }).ToList();
            return Ok(200);
        }

        [HttpPost]
        public ActionResult Create([FromBody] HotelCreateDto createDto)
        {
            var hotelToCreate = new Hotel
            {
                Name = createDto.Name,
                Address = createDto.Address,
            };

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
    }
}
