using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Interfaces;
using WebLogic.Models;

namespace WebLogic.Services
{
    public class LocationService : IService
    {
        public LocationService()
        {

        }

        public string GetName()
        {
            return "LocationService";
        }
        
        public IQueryable<ZipCode> GetZipCodesInRange(float latitude, float longitude, float radiusInMiles = 5)
        {
            float tolerance = 0.08f * 5;        // 0.08 lat/lon units approximately equals 5 miles
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            return entities.ZipCodes.Where(z => ((z.Lat > latitude && (z.Lat - latitude) <= tolerance) || (z.Lat <= latitude && (latitude - z.Lat) <= tolerance)) && 
                                                ((z.Long > longitude && (z.Long - longitude) <= tolerance) || (z.Long <= longitude && (longitude - z.Long) <= tolerance))
                                           )
                                    .AsQueryable();
        }

    }
}
