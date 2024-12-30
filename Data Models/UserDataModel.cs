/************************************************************************************
*   File:           UserDataModel.cs
*   Description:    Contains the UserDataModel entity framework class, which defines a registered
*                   user of the MoneyTale application.
************************************************************************************/


/************************ IMPORTS **************************************************/
using System.ComponentModel.DataAnnotations;


namespace moneytale_server.Data_Models
{
    public class UserDataModel
    {
        /************************ PROPERTIES ***********************************************/
        /// <summary>
        /// Gets or sets the unique identifier for the system user.
        /// This property serves as the primary key in the database.
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier given to each new system user that is used in conjunction with their password to access the system.
        /// This property is required, has a maximum length of 10 characters, and serves as a unique key in the database.
        /// </summary>
        [Required]
        [StringLength(10, ErrorMessage = "Username must have a maximum length of 10 characters.")]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the hashed password used by a system user in conjunction with their username to access the system.
        /// This property is required and has a maximum length of 255 characters.
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "Password must have a maximum length of 255 characters.")]
        public required string HashedSecretKey { get; set; }

        /// <summary>
        /// Gets or sets the unique email address belonging to a system user and is used as an identifier.
        /// This property is required, has a maximum length of 100 characters, and serves as a unique key in the database.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Email must have a maximum length of 100 characters.")]
        public required string EmailAddress { get; set; }

        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Gets or sets the role of the system user (e.g., Admin, User).
        /// This property is required and has a maximum length of 20 characters.
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "User role must have a maximum length of 20 characters.")]
        public required string UserRole { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user was initially created.
        /// This property is required.
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user was last updated.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user last logged into the system.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of failed login attempts for the system user.
        /// This property is required and has a maximum count of 5.
        /// </summary>
        [Required]
        [Range(0, 5, ErrorMessage = "FailedLoginAttempts must be between 0 and 5.")]
        public int FailedLoginAttempts { get; set; }
    }
}
