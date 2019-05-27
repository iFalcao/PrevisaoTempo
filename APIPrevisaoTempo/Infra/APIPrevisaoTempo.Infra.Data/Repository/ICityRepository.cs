using APIPrevisaoTempo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace APIPrevisaoTempo.Infra.Data.Repository
{
    public interface ICityRepository
    {
        City Insert(City city);
        List<City> SelectAll();
        IQueryable<City> Where(Expression<Func<City, bool>> predicate);
    }
}
