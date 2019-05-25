using APIPrevisaoTempo.WebApi.Data;
using APIPrevisaoTempo.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace APIPrevisaoTempo.WebApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        public readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            this._context = context;
        }

        public City Insert(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return city;
        }

        public List<City> SelectAll()
        {
            return _context.Cities.ToList();
        }
    }
}
