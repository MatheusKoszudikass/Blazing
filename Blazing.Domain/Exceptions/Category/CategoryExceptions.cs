using Blazing.Domain.Entities;

namespace Blazing.Domain.Exceptions.Produtos
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

            // <summary>
            /// Initializes a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a specified message.
            /// </summary>
            /// <param name="message">The error message that explains the reason for the exception.</param>
            private CategoryAlreadyExistsException(string message
            ) : base(message) { }

            /// <summary>
            /// Creates a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a message that indicates that the specified category IDs already exist.
            /// </summary>
            /// <param name="id">The IDs of the categories that already exist.</param>
            /// <returns>A new instance of the <see cref="CategoryAlreadyExistsException"/> class.</returns>
            public static CategoryAlreadyExistsException FromExistingId(IEnumerable<Guid> id)
            {
                return new CategoryAlreadyExistsException(
                    $"Identificador já existe: {string.Join(", ", id)}");
            }

            /// <summary>
            /// Creates a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a message that indicates that the specified category names already exist.
            /// </summary>
            /// <param name="categoryName">The names of the categories that already exist.</param>
            /// <returns>A new instance of the <see cref="CategoryAlreadyExistsException"/> class.</returns>
            public static CategoryAlreadyExistsException FromExistingName(IEnumerable<string?> categoryName)
            {
                return new CategoryAlreadyExistsException(
                    $"Categoria ja existe: {string.Join(", ", categoryName.ToString())}");
            }

            /// <summary>
            /// Creates a new instance of the <see cref="CategoryAlreadyExistsException"/> class with a message that indicates that no changes were detected for the specified categories.
            /// </summary>
            /// <param name="categories">The categories for which no changes were detected.</param>
            public static CategoryAlreadyExistsException FromExistingUsers(IEnumerable<Category?> categories)
            {
                return new CategoryAlreadyExistsException(
                    $"Nenhuma alteração foi detectada para as categorias: {string.Join(", ", categories.ToString())}");
            }
        }

        /// <summary>
        /// Represents an exception that occurs when an invalid category identifier is encountered.
        /// </summary>
        public class IdentityCategoryInvalidException : DomainException
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityCategoryInvalidException"/> class with the specified invalid ID.
            /// </summary>
            /// <param name="id">The invalid category ID.</param>
            public IdentityCategoryInvalidException(Guid id)
                : base($"Identificador inválido: {id}")
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityCategoryInvalidException"/> class with the specified invalid IDs.
            /// </summary>
            /// <param name="id">The invalid category IDs.</param>
            public IdentityCategoryInvalidException(IEnumerable<Guid> id)
                : base($"Identificadores inválidos: {string.Join(", ", id)}")
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityCategoryInvalidException"/> class with the specified invalid IDs and whether the IDs exist or not.
            /// </summary>
            /// <param name="id">The invalid category IDs.</param>
            /// <param name="exists">Whether the IDs exist or not.</param>
            public IdentityCategoryInvalidException(IEnumerable<Guid> id, bool exists)
                : base($"Identificadores já existem: {string.Join(", ", id)}")
            {
            }
        }

        /// <summary>
        /// Represents an exception that occurs when a category is invalid.
        /// </summary>
        public class CategoryInvalidExceptions : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryInvalidExceptions"/> class with the specified category.
            /// </summary>
            /// <param name="category">The invalid category.</param>
            public CategoryInvalidExceptions(Category category)
                : base($"Categoria {category} não encontrada.")
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryInvalidExceptions"/> class with the specified categories.
            /// </summary>
            /// <param name="categories">The invalid categories.</param>
            public CategoryInvalidExceptions(IEnumerable<Category> categories)
                : base($"Nenhuma alteração foi detectada para as categorias: {string.Join(", ", categories.Select(p => p.Name).ToList())}")
            {
            }
        }

        /// <summary>
        /// Represents an exception that occurs when a category is not found.
        /// </summary>
        public class CategoryNotFoundException(IEnumerable<Category> categories)
            : DomainException($"A lista de categoria está vazia. {categories}");
    }
    #endregion
}