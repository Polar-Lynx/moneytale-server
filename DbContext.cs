/************************************************************************************
*   File:           DbContext.cs
*   Description:    Contains the ServerDbContext class, which provides the logic
*                   manages the MoneyTale database connection and the data models.
************************************************************************************/


/************************ IMPORTS **************************************************/
using Microsoft.EntityFrameworkCore;
using moneytale_server.Data_Models;


namespace moneytale_server
{
    /// <summary>
    /// Class <c>ServerDbContext</c> manages the MoneyTale database connection and the data models.
    /// </summary>
    public class ServerDbContext : DbContext
    {
        /************************ PROPERTIES ***********************************************/
        /// <summary>
        /// Gets or sets the User table.
        /// This property represents a collection of entities that can be queried and saved.
        /// </summary>
        public DbSet<UserDataModel> Users { get; set; }


        /************************ CONSTRUCTORS *********************************************/
        /// <summary>
        /// Initializes a new instance of the <c>ServerDbContext</c> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by this DbContext.</param>
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {
        }


        /************************ METHODS **************************************************/
        /// <summary>
        /// This method allows the configuration of the data entities.
        /// </summary>
        /// <param name="modelBuilder">The data model constructor.</param>
        /// <returns>Nothing.</returns>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDataModel>()
                .ToTable("Users");
        }
    }
}
