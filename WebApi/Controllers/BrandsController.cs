using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Brand;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandsController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            var result = _brandService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("add")]
        public IActionResult Add(AddBrandDto addBrandDto)
        {
            var result = _brandService.Add(_mapper.Map<Brand>(addBrandDto));
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
