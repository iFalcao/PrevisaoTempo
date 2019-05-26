using APIPrevisaoTempo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace APIPrevisaoTempo.WebApi.Data.Repositories
{
    public interface ICityRepository
    {
        City Insert(City city);
        List<City> SelectAll();
        IQueryable<City> Where(Expression<Func<City, bool>> predicate);
    }
}
