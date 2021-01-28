using SimpleORMDemo.Models;
using System.Collections.Generic;

namespace SimpleORMDemo.DataAccess
{
    public interface ICategoryORM
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
    }
}