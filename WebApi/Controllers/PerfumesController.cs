using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Brand;
using Entities.Dtos.Perfume;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfumesController : ControllerBase
    {
        IPerfumeService _perfumeService;
        private readonly IMapper _mapper;

        public PerfumesController(IPerfumeService perfumeService, IMapper mapper)
        {
            _perfumeService = perfumeService;
            _mapper = mapper;
        }


        [HttpGet("getall")]
        [EnableQuery]
        public IActionResult GetAll()
        {
            var result = _perfumeService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getperfumedetails")]
        [EnableQuery]
        public IActionResult GetPerfumeDetails()
        {
            var result = _perfumeService.GetPerfumeDetails();
            if (result.Success)
            {
                return Ok(result.Data.AsQueryable());
            }
            return BadRequest(result);

        }

        [HttpGet("getallasqueryable")]
        [EnableQuery]
        public IActionResult GetAllAsQueryable()
        {
            var result = _perfumeService.GetAllAsQueryable();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _perfumeService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);

        }

        [HttpPost("add")]
        public IActionResult Add(AddPerfumeDto addPerfumeDto)
        {
            var result = _perfumeService.Add(_mapper.Map<Perfume>(addPerfumeDto));
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
