﻿using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity User.
    /// <summary>
    /// Entity responsible for the user.
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "O primeiro nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O primeiro nome não pode ter mais de 50 caracteres.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O sobrenome não pode ter mais de 50 caracteres.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome de usuário não pode ter mais de 30 caracteres.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser um endereço de e-mail válido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode ter mais de 100 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O hash da senha é obrigatório.")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A data da última atualização é obrigatória.")]
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Addresses associated with the user.
        /// </summary>
        public IEnumerable<Address>? Addresses { get; set; } = [];

        /// <summary>
        /// Shopping carts associated with the user.
        /// </summary>
        public IEnumerable<ShoppingCart>? ShoppingCarts { get; set; } = [];
    }
    #endregion
}
