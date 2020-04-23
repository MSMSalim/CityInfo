﻿using System;
using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityDtoWithoutPointsOfInterestDto>();
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}
