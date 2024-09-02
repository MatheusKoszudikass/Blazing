using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;
using System.Text;

namespace Blazing.Domain.Services
{
    #region Category domain service.
    public class CategoryDomainService(): ICrudDomainService<Category>
    {

        /// <summary>
        /// Adds a collection of categories to the repository.
        /// Throws a CategoryNotFoundExceptions if the input list is empty,
        /// and ExistingCategoryException if any categories already exist.
        /// </summary>
        /// <param name="categories">The categories to be added.</param>
        /// <returns>The added categories.</returns>
        public async Task<IEnumerable<Category?>> Add(IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            if (categories == null || !categories.Any())
            {
                throw new CategoryExceptions.CategoryNotFoundException(categories ?? []);
            }

            try
            {
                foreach (var item in categories)
                {
                    item.DataCreated = DateTime.Now;
                }

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
        public async Task<IEnumerable<Category?>> Update(IEnumerable<Guid> ids, IEnumerable<Category> originalCategories, IEnumerable<Category> updatedCategories, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any() || ids.Contains(Guid.Empty))
                throw new CategoryExceptions.IdentityCategoryInvalidException(ids ?? []);

            var categoriesDict = originalCategories.Where(c => ids.Contains(c.Id)).ToDictionary(c => c.Id);
            var updatesDict = updatedCategories.Where(c => ids.Contains(c.Id)).ToDictionary(c => c.Id);

            if (categoriesDict.Count == 0)
                throw new CategoryExceptions.CategoryNotFoundException(originalCategories);

            try
            {
                var modifiedCategories = updatesDict
                .Where(update => categoriesDict.TryGetValue(update.Key, out var original) && !AreProductsEqual(original, update.Value))
                .Select(update =>
                {
                    var updatedCategory = update.Value;
                    updatedCategory.DataCreated = categoriesDict[update.Key].DataCreated;
                    updatedCategory.DataUpdated = DateTime.Now;
                    return updatedCategory;
                })
                .ToList();

                if (modifiedCategories.Count == 0)
                    throw  CategoryExceptions.CategoryAlreadyExistsException.FromExistingUsers(updatedCategories);

                return await Task.FromResult(modifiedCategories);
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Compares two products to determine if they are equal based on their properties.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns><c>true</c> if the products have the same values for all relevant properties; otherwise, <c>false</c>.</returns>
        private static bool AreProductsEqual(Category category1, Category category2)
        {
            if (category1 == null && category2 == null)
                return false;


            return category1.Id == category2.Id &&
                  NormalizeString(category1.Name) == NormalizeString(category2.Name) &&
                   category1.DataCreated == category2.DataCreated;
        }

        /// <summary>
        /// Normalizes a string by trimming leading and trailing whitespace, normalizing it to the specified form,
        /// and converting it to lowercase.
        /// </summary>
        /// <param name="input">The string to normalize. Can be null.</param>
        /// <returns>The normalized string. If the input is null, returns an empty string.</returns>
        private static string NormalizeString(string? input)
        {
            if (input == null)
                return string.Empty;
            else
                return input.Trim().Normalize(NormalizationForm.FormC).ToLowerInvariant();
        }

        /// <summary>
        /// Deletes categories based on their IDs.
        /// Throws IdentityCategoryInvalidException if no IDs are provided,
        /// and CategoryNotFoundExceptions if categories remain after deletion.
        /// </summary>
        /// <param name="id">The IDs of the categories to delete.</param>
        /// <param name="categories">The categories to delete.</param>
        /// <returns>The deleted categories.</returns>
        public async Task<IEnumerable<Category?>> Delete(IEnumerable<Guid> id, IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                throw new CategoryExceptions.IdentityCategoryInvalidException(id ?? []);
            }
            else if(!categories.Any(c => id.Contains(c.Id)))
            {
                throw new CategoryExceptions.CategoryNotFoundException(categories ?? []);
            }

            try
            {
                if (!categories.Any())
                {
                    throw new CategoryExceptions.CategoryNotFoundException(categories);
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
                throw new CategoryExceptions.IdentityCategoryInvalidException(id ?? []);
            }
            else if (categories == null || !categories.Any(c => id.Contains(c.Id)))
            {
                throw new CategoryExceptions.CategoryNotFoundException(categories ?? []);
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
                throw new CategoryExceptions.CategoryNotFoundException(categories ?? []);
            }

            try
            {
                if (categories == null || !categories.Any())
                {
                    throw new CategoryExceptions.CategoryNotFoundException(categories ?? []);
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
        /// Checks if the specified categories exist in the repository.
        /// Throws a CategoryAlreadyExistsException if the categories already exist.
        /// </summary>
        /// <param name="id">A flag indicating whether the categories exist by ID.</param>
        /// <param name="existsName">A flag indicating whether the categories exist by name.</param>
        /// <param name="categories">The categories to check for existence.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the categories exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        /// <exception cref="CategoryAlreadyExistsException">Thrown if the categories already exist.</exception>
        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            try
            {
                if (id)
                {
                    var categoriesId = categories.Select(c => c.Id).ToList();
                    throw  CategoryExceptions.CategoryAlreadyExistsException.FromExistingId(categoriesId);
                }
                else if (existsName)
                {
                    var categoriesName = categories.Select(c => c.Name).ToList();

                    throw  CategoryExceptions.CategoryAlreadyExistsException.FromExistingName(categoriesName);
                }

                await Task.CompletedTask;
                return id;
            }
            catch (DomainException)
            {

                throw;
            }
        }
    }
    #endregion
}
