/************************************************************************************
*   File:           CategoryDataModel.cs
*   Description:    Contains the CategoryDataModel entity framework class, which
*                   defines a registered user of the MoneyTale application.
************************************************************************************/


/************************ IMPORTS **************************************************/
using System.ComponentModel.DataAnnotations;


namespace moneytale_server.Data_Models
{
    /// <summary>
    /// Represents a category for financial transactions.
    /// Categories can be either system-defined (default) or user-defined.
    /// They help classify financial transactions into distinct groups, such as
    /// "Food," "Rent," or "Entertainment."
    /// </summary>
    public class CategoryDataModel
    {
        /************************ PRIMARY **************************************************/
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// This property serves as the primary key in the database.
        /// </summary>
        [Key]
        public int CategoryID { get; set; }


        /************************ FOREIGN **************************************************/
        /// <summary>
        /// Gets or sets the unique identifier of the user who owns the category.
        /// This property acts as a foreign key linking to the <see cref="UserDataModel"/> table.
        /// A null value indicates that the category is a system-defined default category.
        /// Categories can be user-specific, allowing each user to define their own categories.
        /// </summary>
        public int? UserID { get; set; }


        /************************ FIELDS ***************************************************/
        /// <summary>
        /// Gets or sets the name of the category.
        /// This property is required, has a maximum length of 50 characters,
        /// and is used to describe the category (e.g., "Food" or "Rent").
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Category name must not exceed 50 characters.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the category is a system-defined default.
        /// System-defined categories are available to all users.
        /// </summary>
        [Required]
        public required bool IsDefault { get; set; }
    }
}
