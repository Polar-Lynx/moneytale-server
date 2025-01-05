/************************************************************************************
*   File:           UserRepository.cs
*   Description:    Contains the UserRepository class, which implements the CRUD
*                   operations for the User entity.
************************************************************************************/


/************************ IMPORTS **************************************************/
using Microsoft.EntityFrameworkCore;
using moneytale_server.Data_Models;


namespace moneytale_server.Repositories
{
    /// <summary>
    /// Represents a repository for managing user data in the database.
    /// This class implements the <see cref="IUserRepository"/> interface
    /// and provides methods for CRUD operations on users.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserRepository"/> class
    /// via primary constructor.
    /// </remarks>
    /// <param name="context">The database context used to access user data.</param>
    public class UserRepository(ServerDbContext context) : IUserRepository
    {
        /************************ PROPERTIES ***********************************************/
        private readonly ServerDbContext _context = context;


        /************************ METHODS **************************************************/
        /// <summary>
        /// Asynchronously retrieves a user by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the user if found; otherwise, null.</returns>
        public async Task<UserDataModel?> GetUserByIdAsync(int userId)
            => await _context.Users.FindAsync(userId);

        /// <summary>
        /// Asynchronously retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the user if found; otherwise, null.</returns>
        public async Task<UserDataModel?> GetUserByUsernameAsync(string username)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        /// <summary>
        /// Asynchronously retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the user if found; otherwise, null.</returns>
        public async Task<UserDataModel?> GetUserByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);

        /// <summary>
        /// Asynchronously retrieves all users from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of users.</returns>
        public async Task<IEnumerable<UserDataModel>> GetAllUsersAsync()
            => await _context.Users.ToListAsync();

        /// <summary>
        /// Asynchronously adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddUserAsync(UserDataModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateUserAsync(UserDataModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes a user from the repository by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
