using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Features;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController : ControllerBase
    {
        private readonly DataContext dataContext;

        public HotelsController(DataContext dataContext)
        {
            this.dataContext = dataContext;

        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelDto>> ListAllHotels()
        {
            var hotels = dataContext.Set<Hotel>()
                .Select(x => new HotelDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .ToList();

            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public ActionResult<HotelDto> GetHotel(int id)
        {
            var hotel = dataContext.Set<Hotel>().FirstOrDefault(x => x.Id == id);

            if (hotel == null )
            {
                return NotFound();
            }
            

            var hotelDto = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address
            };

            return Ok(hotelDto);
        }

        [HttpPost]
        public IActionResult Create(HotelDto hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(hotel.Address) || hotel.Address.Trim() == "string")
            {
                ModelState.AddModelError("Address", "Address is a required field");
                return BadRequest(ModelState);
            }

            if (hotel.Name.Length > 120)
            {
                ModelState.AddModelError("Name", "The hotel name has too many characters");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(hotel.Name) || hotel.Name.Trim() == "string")
            {
                ModelState.AddModelError("Name", "Name is a required field");
                return BadRequest(ModelState);
            }

            var newHotel = new Hotel
            {
                Name = hotel.Name,
                Address = hotel.Address,
            };

            dataContext.Add(newHotel);
            dataContext.SaveChanges();

            var createdHotelDto = new HotelDto
            {
                Id = newHotel.Id,
                Name = newHotel.Name,
                Address = newHotel.Address
            };

            return CreatedAtAction(nameof(GetHotel),
                new { id = newHotel.Id },
                createdHotelDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateHotelById(int id, HotelDto updatedHotel)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(updatedHotel.Name))
            {
                ModelState.AddModelError("Name", "Name is a required");
                return BadRequest(ModelState);
            }

            if (updatedHotel.Name.Length > 120)
            {
                ModelState.AddModelError("Name", "Name cannot be more than 120 characters");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(updatedHotel.Address))
            {
                ModelState.AddModelError("Address", "Address is a required");
                return BadRequest(ModelState);
            }

            var existingHotel = dataContext
                .Set<Hotel>()
                .FirstOrDefault(x => x.Id == id);
            
            if (existingHotel == null)
            {
                return NotFound();
            }

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;

            dataContext.SaveChanges();

            var updatedHotelDto = new HotelDto
            {
                Id = existingHotel.Id,
                Name = existingHotel.Name,
                Address = existingHotel.Address
            };

            return Ok(updatedHotelDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHotel(int id)
        {
            var hotel = dataContext
                .Set<Hotel>()
                .FirstOrDefault(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            dataContext.Set<Hotel>().Remove(hotel);
            dataContext.SaveChanges();

            return Ok(new HotelDto
            {
                Name = hotel.Name,
                Address = hotel.Address,
                Id = hotel.Id
            });
        }
    }
}

