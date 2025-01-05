/************************************************************************************
*   File:           CategoryRepository.cs
*   Description:    Contains the CategoryRepository class, which implements the CRUD
*                   operations for the Category entity.
************************************************************************************/


/************************ IMPORTS **************************************************/
using Microsoft.EntityFrameworkCore;
using moneytale_server.Data_Models;


namespace moneytale_server.Repositories
{
    /// <summary>
    /// Represents a repository for managing category data in the database.
    /// This class implements the <see cref="ICategoryRepository"/> interface
    /// and provides methods for CRUD operations on categories.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CategoryRepository"/> class
    /// via primary constructor.
    /// </remarks>
    /// <param name="context">The database context used to access category data.</param>
    public class CategoryRepository(ServerDbContext context) : ICategoryRepository
    {
        /************************ PROPERTIES ***********************************************/
        private readonly ServerDbContext _context = context;


        /************************ METHODS **************************************************/
        /// <summary>
        /// Asynchronously retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the category if found; otherwise, null.</returns>
        public async Task<CategoryDataModel?> GetCategoryByIdAsync(int categoryId)
            => await _context.Categories.FindAsync(categoryId);

        /// <summary>
        /// Asynchronously retrieves all categories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of categories.</returns>
        public async Task<IEnumerable<CategoryDataModel>> GetAllCategoriesAsync()
            => await _context.Categories.ToListAsync();

        /// <summary>
        /// Asynchronously retrieves all categories belonging to the
        /// specified user, as well as default categories.
        /// </summary>
        /// <param name="userId">The unique identifier of a user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of categories.</returns>
        public async Task<IEnumerable<CategoryDataModel>> GetUserCategoriesAsync(int userId)
            => await _context.Categories.Where(c => c.UserID == userId || c.IsDefault).ToListAsync();

        /// <summary>
        /// Asynchronously adds a new category to the repository.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddCategoryAsync(CategoryDataModel category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing category in the repository.
        /// </summary>
        /// <param name="category">The category with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateCategoryAsync(CategoryDataModel category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes a category from the repository by its unique identifier.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
