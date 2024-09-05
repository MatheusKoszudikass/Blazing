using Blazing.Domain.Entities;

namespace Blazing.Domain.Exceptions.Category
{
    #region Exception Handling Category
    /// <summary>
    /// A static class that contains all exceptions related to category.
    /// This class serves as a centralized location for managing and referencing category-related exceptions.
    /// </summary>
    public static class CategoryExceptions
    {
        /// <summary>
        /// Represents an exception that occurs when a category already exists.
        /// </summary>
        public class CategoryAlreadyExistsException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a specified message.
            /// </summary>
            /// <param name="message">The error message that explains the reason for the exception.</param>
            private CategoryAlreadyExistsException(string message
            ) : base(message) { }

            /// <summary>
            /// Creates a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a message that indicates that the specified category names already exist.
            /// </summary>
            /// <param name="categoryName">The names of the categories that already exist.</param>
            /// <returns>A new instance of the <see cref="CategoryAlreadyExistsException"/> class.</returns>
            public static CategoryAlreadyExistsException FromExistingName(IEnumerable<string?> categoryName)
            {
                return new CategoryAlreadyExistsException(
                    $"Categoria ja existe: {string.Join(", ", categoryName)}");
            }

            /// <summary>
            /// Creates a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a message that indicates that no changes were detected for the specified categories.
            /// </summary>
            /// <param name="categories">The categories for which no changes were detected.</param>
            public static CategoryAlreadyExistsException FromExistingUsers(IEnumerable<Entities.Category?> categories)
            {
                return new CategoryAlreadyExistsException(
                    $"Nenhuma alteração foi detectada para as categorias: {string.Join(", ", categories.ToString())}");
            }
        }

        /// <summary>
        /// Represents an exception that occurs when a category is invalid.
        /// </summary>
        public class CategoryInvalidExceptions(string message) : DomainException(message)
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryInvalidExceptions"/> class with the specified categories.
            /// </summary>
            /// <param name="categories">The invalid categories.</param>
            public static CategoryInvalidExceptions NotDetectedEditCategories(IEnumerable<Entities.Category> categories)
            {
                return new CategoryInvalidExceptions(
                    $"Nenhuma alteração foi detectada para as categorias: {string.Join(", ", categories.Select(p => p.Name).ToList())}");
            }
        }

        /// <summary>
        /// Represents an exception that occurs when a category is not found.
        /// </summary>
        public class CategoryNotFoundException(string message) : DomainException(message)
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryNotFoundException"/> class with the specified category.
            /// </summary>
            /// <param name="category">The category that was not found.</param>
            public static CategoryNotFoundException NotFoundCategory(Entities.Category category)
            {
                return new CategoryNotFoundException(
                    $"Categoria não foi encontrada: {category.Name}");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryNotFoundException"/> class with the specified categories.
            /// </summary>
            /// <param name="categories">The categories that were not found.</param>
            public static CategoryNotFoundException NotFoundCategories(IEnumerable<Entities.Category?> categories)
            {
                return new CategoryNotFoundException(
                    $"Nenhuma categoria foi encontrada: {string.Join(", ", categories.Select(p => p.Name).ToList())}");
            }
        }
    }
    #endregion
}