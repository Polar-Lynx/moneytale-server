/************************************************************************************
*   File:           ICategoryRepository.cs
*   Description:    Contains the ICategoryRepository interface class, which defines
*                   the CRUD operations for the Category entity.
************************************************************************************/


/************************ IMPORTS **************************************************/
using moneytale_server.Data_Models;


namespace moneytale_server.Repositories
{
    /// <summary>
    /// Represents a repository for managing category data.
    /// This interface defines the methods for CRUD operations on categories.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Asynchronously retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the category if found; otherwise, null.</returns>
        Task<CategoryDataModel?> GetCategoryByIdAsync(int categoryId);

        /// <summary>
        /// Asynchronously retrieves all categories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of categories.</returns>
        Task<IEnumerable<CategoryDataModel>> GetAllCategoriesAsync();

        /// <summary>
        /// Asynchronously adds a new category to the repository.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddCategoryAsync(CategoryDataModel category);

        /// <summary>
        /// Asynchronously updates an existing category in the repository.
        /// </summary>
        /// <param name="category">The category with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCategoryAsync(CategoryDataModel category);

        /// <summary>
        /// Asynchronously deletes a category from the repository by its unique identifier.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCategoryAsync(int categoryId);
    }
}
