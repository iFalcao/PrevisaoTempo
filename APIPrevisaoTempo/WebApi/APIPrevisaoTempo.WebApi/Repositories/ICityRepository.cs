using APIPrevisaoTempo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPrevisaoTempo.WebApi.Repositories
{
    public interface ICityRepository
    {
        City Insert(City city);
        List<City> SelectAll();
    }
}
