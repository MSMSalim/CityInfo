using System;
using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Context;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _ctx;

        public CityInfoRepository(CityInfoContext ctx)
        {
            this._ctx = ctx;
        }

        public IEnumerable<City> GetCities()
        {
            return _ctx.Cities
                .OrderBy(c => c.Name)
                .ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _ctx.Cities
                     .Where(c => c.Id == cityId)
                     .Include(c => c.PointsOfInterest)
                     .FirstOrDefault();

            }

            return _ctx.Cities
                .Where(c => c.Id == cityId)
                .FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointOfInterestForCity(int cityId)
        {
            return _ctx.PointOfInterests
                       .Where(p => p.CityId == cityId)
                       .ToList();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _ctx.PointOfInterests
                       .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                       .FirstOrDefault();
        }

    }
}
