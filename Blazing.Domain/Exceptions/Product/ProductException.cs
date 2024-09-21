namespace Blazing.Domain.Exceptions.Product
{
    /// <summary>
    /// A static class that contains all exceptions related to products.
    /// This class serves as a centralized location for managing and referencing product-related exceptions.
    /// </summary>
    public static class ProductExceptions
    {
        #region Error exceptions.

        /// <summary>
        /// Exception that is thrown when a product already exists in the system.
        /// </summary>
        public class ProductAlreadyExistsException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ProductAlreadyExistsException"/> class with the specified product names.
            /// </summary>
            /// <param name="produtoNome">The names of the products that already exist.</param>
            public ProductAlreadyExistsException(IEnumerable<string?> produtoNome)
                : base($"O produto {string.Join(", ", produtoNome)} já existe.")
            {
            }

            public ProductAlreadyExistsException(IEnumerable<Entities.Product?> produtoNome)
               : base($"Nenhuma alteração foi detectada para os produtos: {string.Join(", ", produtoNome.Select(p => p.Name).ToList())}")
            {
            }
        }

        /// <summary>
        /// Exception that is thrown when a product's identifier (ID) is invalid.
        /// </summary>
        public class IdentityProductInvalidException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityProductInvalidException"/> class with the specified invalid ID.
            /// </summary>
            /// <param name="id">The invalid product ID.</param>
            public IdentityProductInvalidException(Guid id)
                : base($"Identificador inválido: {id}")
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityProductInvalidException"/> class with the specified list of invalid IDs.
            /// </summary>
            /// <param name="ids">The list of invalid product IDs.</param>
            public IdentityProductInvalidException(IEnumerable<Guid> id)
                : base($"Identificadores inválidos: {string.Join(", ", id)}")
            {
            }
            public IdentityProductInvalidException(IEnumerable<Guid> id , bool exists)
              : base($"Identificadores já existem: {string.Join(", ", id)}")
            {
            }
        }

        /// <summary>
        /// Exception that is thrown when a product is found to be invalid.
        /// </summary>
        public class ProductInvalidException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ProductInvalidException"/> class with the specified invalid product.
            /// </summary>
            /// <param name="produto">The product that is invalid.</param>
            public ProductInvalidException(Entities.Product produto)
                : base($"Produto {produto.Name} é inválido.")
            {
            }
            public ProductInvalidException(IEnumerable<string> name)
              : base($"Nome do produto {name} já existe.")
            {
            }
        }

        /// <summary>
        /// Exception that is thrown when a list of products is empty or not found.
        /// </summary>
        public class ProductNotFoundException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class with the specified product list.
            /// </summary>
            /// <param name="produtos">The list of products that was not found or is empty.</param>
            public ProductNotFoundException(IEnumerable<Entities.Product?> produtos)
                : base("A lista de produtos está vazia.")
            {
            }
        }
        #endregion
    }
}
