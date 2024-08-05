using Blazing.Domain.Entities;

namespace Blazing.Domain.Exceptions.Produtos
{
    #region Exception Handling Category

    /// <summary>
    /// Represents an exception that is thrown when an attempt is made to add a category
    /// that already exists in the system.
    /// Initializes a new instance of the <see cref="ExistingCategoryException"/> class
    /// with a specified collection of category names that already exist.
    /// </remarks>
    /// <param name="categoriaNome">A collection of category names that are already present in the system.</param>
    public class ExistingCategoryException(IEnumerable<string?> categoriaNome) 
        : DomainException($"A categoria {string.Join(", ", categoriaNome)} já existe.")
    {
    }

    public class IdentityCategoryInvalidException : DomainException
    {
        public IdentityCategoryInvalidException(Guid id)
            : base($"Identificador inválido: {id}")
        {
        }

        public IdentityCategoryInvalidException(IEnumerable<Guid> ids)
            : base($"Identificadores inválidos: {string.Join(", ", ids)}")
        {
        }
    }

    public class CategoryInvalidExceptions(Category categories)
    : DomainException($"Categoria {categories} invalido.")
    {
    }

    public class CategoryNotFoundExceptions(IEnumerable<Category> categories)
     : DomainException($"A lista {categories} está vazia.")
    {
    }


    #endregion
}