/************************************************************************************
*   File:           IUserRepository.cs
*   Description:    Contains the IUserRepository interface class, which defines the
*                   CRUD operations for the User entity.
************************************************************************************/


/************************ IMPORTS **************************************************/
using moneytale_server.Data_Models;


namespace moneytale_server.Repositories
{
    /// <summary>
    /// Represents a repository for managing user data.
    /// This interface defines the methods for CRUD operations on users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Asynchronously retrieves a user by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the user if found; otherwise, null.</returns>
        Task<UserDataModel?> GetUserByIdAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the user if found; otherwise, null.</returns>
        Task<UserDataModel?> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Asynchronously retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the user if found; otherwise, null.</returns>
        Task<UserDataModel?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Asynchronously retrieves all users.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of users.</returns>
        Task<IEnumerable<UserDataModel>> GetAllUsersAsync();

        /// <summary>
        /// Asynchronously adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddUserAsync(UserDataModel user);

        /// <summary>
        /// Asynchronously updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateUserAsync(UserDataModel user);

        /// <summary>
        /// Asynchronously deletes a user from the repository by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteUserAsync(int userId);
    }
}
