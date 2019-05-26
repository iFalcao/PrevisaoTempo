using APIPrevisaoTempo.WebApi.Data;
using APIPrevisaoTempo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace APIPrevisaoTempo.WebApi.Data.Repositories
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

        public IQueryable<City> Where(Expression<Func<City, bool>> predicate)
        {
            return _context.Cities.Where(predicate);
        }

        public List<City> SelectAll()
        {
            return _context.Cities.ToList();
        }
    }
}
