using SimpleORMDemo.Models;
using System.Collections.Generic;

namespace SimpleORMDemo.DataAccess
{
    public interface IManufacturerORM
    {
        List<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerById(int id);
    }
}