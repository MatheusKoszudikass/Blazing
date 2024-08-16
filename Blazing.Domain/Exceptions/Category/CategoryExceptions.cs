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

        public class CategoryAlreadyExistsException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CategoryAlreadyExistsException"/> class with the specified category names.
            /// </summary>
            /// <param name="categoriaNome">The names of the category that already exist.</param>
            public CategoryAlreadyExistsException(IEnumerable<string?> categoryName)
                : base($"A categoria {categoryName} já existe.")
            {

            }
            public CategoryAlreadyExistsException(IEnumerable<Category> categoryName)
               : base($"Nenhuma alteração foi detectada para as categorias: {string.Join(", ", categoryName.Select(p => p.Name).ToList())}")
            {

            }
        }

        /// <summary>
        /// Exception that is thrown when a category's identifier (ID) is invalid.
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
            /// Initializes a new instance of the <see cref="IdentityCategoryInvalidException"/> class with the specified list of invalid IDs.
            /// </summary>
            /// <param name="id">The list of invalid category IDs.</param>
            public IdentityCategoryInvalidException(IEnumerable<Guid> id)
                : base($"Identificadores inválidos: {string.Join(", ", id)}")
            {
            }

            public IdentityCategoryInvalidException(IEnumerable<Guid> id, bool exists)
                : base($"Identificadores já existem: {string.Join(", ", id)}")
            {
            }
        }

        /// <summary>
        /// Exception that is thrown when a category is found to be invalid.
        /// </summary>
        public class CategoryInvalidExceptions : DomainException
        {
            public CategoryInvalidExceptions(Category category)
                : base($"Categoria {category} não encontrada.")
            {
            }

            public CategoryInvalidExceptions(IEnumerable<Category> category)
                : base($"Nenhuma alteração foi detectada para as categorias: {string.Join(", ", category.Select(p => p.Name).ToList())}")
            {
            }
        }

        /// <summary>
        /// Exception that is thrown when a list of categories is empty or not found.
        /// </summary>
        public class CategoryNotFoundException : DomainException
        {
            public CategoryNotFoundException(IEnumerable<Category> categories)
             : base($"A lista de categoria está vazia. {categories}")
            {
            }

        }
    }
    #endregion
}