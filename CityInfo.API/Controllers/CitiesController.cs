using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            this._cityInfoRepository = cityInfoRepository;
            this._mapper = mapper;
        }

        public IActionResult GetCities()
        {
            //return Ok(CitiesDataStore.Current.Cities);

            var cityEntities = _cityInfoRepository.GetCities();

            /*
            var results = new List<CityDtoWithoutPointsOfInterestDto>();
            foreach(City cityEntity in cityEntities)
            {
                results.Add(new CityDtoWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }
            */

            return Ok(_mapper.Map<IEnumerable<CityDtoWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            /*

            var city =  CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (city == null)
            {
               return NotFound();
            }

             return Ok(city);
           */

            var cityEntity = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (cityEntity == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = new CityDto()
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                };

                foreach(var poi in cityEntity.PointsOfInterest)
                {
                    cityResult.PointsOfInterest.Add(
                            new PointOfInterestDto()
                            {
                                Id = poi.Id,
                                Name = poi.Name,
                                Description = poi.Description
                            });
                }

                return Ok(cityResult);
            }

            /*
            var cityWithoutPointOfInterestResult =
                new CityDtoWithoutPointsOfInterestDto()
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                };

            return Ok(cityWithoutPointOfInterestResult);
            */

            return Ok(_mapper.Map<IEnumerable<CityDtoWithoutPointsOfInterestDto>>(cityEntity));
        }
    }
}
