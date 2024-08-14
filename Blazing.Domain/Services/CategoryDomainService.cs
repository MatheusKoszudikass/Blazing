using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;

namespace Blazing.Domain.Services
{
    #region Category domain service.
    public class CategoryDomainService: ICrudDomainService<Category>
    {
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

            try
            {
                await Task.CompletedTask;
                return  categories;
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
        public async Task<IEnumerable<Category?>> Update(IEnumerable<Guid> id, IEnumerable<Category> categories, IEnumerable<Category> categoriesUpdate)
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

                foreach (var category in categories)
                {
                    var updateCategoryDto = categoriesUpdate.Where(x => x.Id == category.Id).FirstOrDefault();
                    if (updateCategoryDto != null)
                    {
                        category.Name = updateCategoryDto.Name;
                    }
                }

                await Task.CompletedTask;
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
                if (!categories.Any())
                {
                    throw new CategoryNotFoundExceptions(categories);
                }
                await Task.CompletedTask;
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
        public async Task<IEnumerable<Category?>> GetById(IEnumerable<Guid> id, IEnumerable<Category> categories, CancellationToken cancellationToken)
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
                await Task.CompletedTask;
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
        public async Task<IEnumerable<Category?>> GetAll(IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            if (categories == null || !categories.Any())
            {
                throw new CategoryNotFoundExceptions(categories ?? []);
            }

            try
            {
                if (categories == null || !categories.Any())
                {
                    throw new CategoryNotFoundExceptions(categories ?? []);
                }

                await Task.CompletedTask;
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
        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<Category> categories)
        {

            try
            {

                if (id)
                {

                    await Task.CompletedTask;
                    return id;
                }
                else
                {
                    await Task.CompletedTask;
                    return id;
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
