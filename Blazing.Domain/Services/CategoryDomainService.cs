using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;

namespace Blazing.Domain.Services
{
    #region Category domain service.
    public class CategoryDomainService(ICrudDomainRepository<Category> crudDomainRepository) : ICrudDomainService<Category>
    {

        private readonly ICrudDomainRepository<Category> _crudDomainRepository = crudDomainRepository 
            ?? throw new ArgumentNullException(nameof(crudDomainRepository));


        /// <summary>
        /// Adds a collection of categories to the repository.
        /// Throws a CategoryNotFoundExceptions if the input list is empty,
        /// and ExistingCategoryException if any categories already exist.
        /// </summary>
        /// <param name="categories">The categories to be added.</param>
        /// <returns>The added categories.</returns>
        public async Task<IEnumerable<Category?>> Add(IEnumerable<Category> categories)
        {
            if (categories == null || !categories.Any())
            {
                throw new CategoryNotFoundExceptions(categories ?? []);
            }

            var categoryNames = categories.Select(n => n.Name).ToList();

            try
            {

                await _crudDomainRepository.AddAsync(categories);
                return categories;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }
        }

        /// <summary>
        /// Updates categories based on their IDs.
        /// Throws IdentityCategoryInvalidException if no IDs are provided,
        /// and CategoryNotFoundExceptions if no categories match the given IDs.
        /// </summary>
        /// <param name="id">The IDs of the categories to update.</param>
        /// <param name="categories">The categories with updated information.</param>
        /// <returns>The updated categories.</returns>
        public async Task<IEnumerable<Category?>> Update(IEnumerable<Guid> id, IEnumerable<Category> categories)
        {
            if (id == null || !id.Any())
            {
                throw new IdentityCategoryInvalidException(id ?? []);
            }
            else if (categories == null || !categories.Any(c => id.Contains(c.Id)))
            {
                throw new CategoryNotFoundExceptions(categories ?? []);
            }

            try
            {
                await _crudDomainRepository.UpdateAsync(id, categories);

                if (categories == null)
                {
                    throw new CategoryInvalidExceptions(new Category());
                }

                return categories;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }
        }

        /// <summary>
        /// Deletes categories based on their IDs.
        /// Throws IdentityCategoryInvalidException if no IDs are provided,
        /// and CategoryNotFoundExceptions if categories remain after deletion.
        /// </summary>
        /// <param name="id">The IDs of the categories to delete.</param>
        /// <param name="categories">The categories to delete.</param>
        /// <returns>The deleted categories.</returns>
        public async Task<IEnumerable<Category?>> Delete(IEnumerable<Guid> id, IEnumerable<Category> categories)
        {
            if (id == null)
            {
                throw new IdentityCategoryInvalidException(id ?? []);
            }
            else if(!categories.Any(c => id.Contains(c.Id)))
            {
                throw new CategoryNotFoundExceptions(categories ?? []);
            }

            try
            {
                await _crudDomainRepository.DeleteByIdAsync(id, categories);

                if (!categories.Any())
                {
                    throw new CategoryNotFoundExceptions(categories);
                }

                return categories;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }
        }

        /// <summary>
        /// Retrieves categories by their IDs.
        /// Throws IdentityCategoryInvalidException if no IDs are provided,
        /// and CategoryNotFoundExceptions if no categories are found with the given IDs.
        /// </summary>
        /// <param name="id">The IDs of the categories to retrieve.</param>
        /// <param name="categories">The categories with the given IDs.</param>
        /// <returns>The retrieved categories.</returns>
        public async Task<IEnumerable<Category?>> GetById(IEnumerable<Guid> id, IEnumerable<Category> categories)
        {
            if (id == null || !id.Any())
            {
                throw new IdentityCategoryInvalidException(id ?? []);
            }
            else if (categories == null || !categories.Any(c => id.Contains(c.Id)))
            {
                throw new CategoryNotFoundExceptions(categories ?? []);
            }

            try
            {
                await _crudDomainRepository.GetByIdAsync(id, categories);
                return categories;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }
        }

        /// <summary>
        /// Retrieves all categories.
        /// Throws CategoryNotFoundExceptions if the input list is empty,
        /// and if no categories are found in the repository.
        /// </summary>
        /// <param name="categories">The categories to retrieve.</param>
        /// <returns>The retrieved categories.</returns>
        public async Task<IEnumerable<Category?>> GetAll(IEnumerable<Category> categories)
        {
            if (categories == null || !categories.Any())
            {
                throw new CategoryNotFoundExceptions(categories ?? []);
            }

            try
            {
                 await _crudDomainRepository.GetAllAsync(categories);

                if (categories == null || !categories.Any())
                {
                    throw new CategoryNotFoundExceptions(categories ?? []);
                }

                return categories;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }
        }

        /// <summary>
        /// Checks if a given boolean value exists in the repository.
        /// </summary>
        /// <param name="exists">The boolean value to check.</param>
        /// <returns>True if the value exists, false otherwise.</returns>
        public async Task<bool> ExistsAsync(bool exists)
        {

            try
            {

                if (exists)
                {
                    await _crudDomainRepository.ExistsAsync(exists);


                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (DomainException)
            {

                throw;
            }
        }
    }
    #endregion
}
