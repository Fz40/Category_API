using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface ICategoryService
    {
        bool SaveChanges();
        Task<IEnumerable<Category>> GetAllCategoty();
        Task<Category> GetCategoryById(int id);
        void CreateCategory(Category cat);
        void UpdateCategory(Category cat);

        void DeleteCategory(Category cat);
        Task<IEnumerable<Category>> GetCategotyByCondition(ConditionModel condition);

    }
}
