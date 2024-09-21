using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using System.Text;
using Blazing.Domain.Exceptions.Category;
using Blazing.Domain.Interface.Services;

namespace Blazing.Domain.Services
{
    #region Category domain service.
    public class CategoryDomainService: ICrudDomainService<Category>
    {
        /// <summary>
        /// Adds a collection of categories to the repository.
        /// </summary>
        /// <param name="categories">The categories to add.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>The added categories.</returns>
        /// <exception cref="CategoryExceptions.CategoryNotFoundException">Thrown when the input collection is null or empty.</exception>
        public async Task<IEnumerable<Category>> Add(IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            if (categories == null || !categories.Any())
            {
                throw  CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categories ?? []);
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
        /// Updates categories based on their id.
        /// Throws IdentityCategoryInvalidException if no id are provided,
        /// and CategoryNotFoundExceptions if no categories match the given id.
        /// </summary>
        /// <param name="id">The id of the categories to update.</param>
        /// <param name="updatedCategories">The categories with updated information.</param>
        /// <returns>The updated categories.</returns>
        public async Task<IEnumerable<Category>> Update(IEnumerable<Guid> id, IEnumerable<Category> originalCategories, IEnumerable<Category> updatedCategories, CancellationToken cancellationToken)
        {
            if (id == null || !id.Any() || id.Contains(Guid.Empty))
                throw DomainException.IdentityInvalidException.Identities(id ?? []);

            var categoriesDict = originalCategories.Where(c => id.Contains(c.Id)).ToDictionary(c => c.Id);
            var updatesDict = updatedCategories.Where(c => id.Contains(c.Id)).ToDictionary(c => c.Id);

            if (categoriesDict.Count == 0)
                throw  CategoryExceptions.CategoryNotFoundException.NotFoundCategories(originalCategories);

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
        /// Determines if two categories are equal by comparing their properties.
        /// </summary>
        /// <param name="category1">The first category to compare.</param>
        /// <param name="category2">The second category to compare.</param>
        /// <returns>True if the categories are equal, false otherwise.</returns>
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
        /// Deletes categories based on their id.
        /// Throws IdentityCategoryInvalidException if no id are provided,
        /// and CategoryNotFoundExceptions if categories remain after deletion.
        /// </summary>
        /// <param name="id">The id of the categories to delete.</param>
        /// <param name="categories">The categories to delete.</param>
        /// <returns>The deleted categories.</returns>
        public async Task<IEnumerable<Category>> Delete(IEnumerable<Guid> id, IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            if (id == null || !id.Any() || id.Contains(Guid.Empty))
            {
                throw DomainException.IdentityInvalidException.Identities(id ?? []);
            }
            else if(!categories.Any(c => id.Contains(c.Id)))
            {
                throw  CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categories ?? []);
            }

            try
            {
                if (!categories.Any())
                {
                    throw CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categories);
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
        /// Retrieves categories by their id.
        /// Throws IdentityCategoryInvalidException if no id are provided,
        /// and CategoryNotFoundExceptions if no categories are found with the given id.
        /// </summary>
        /// <param name="id">The id of the categories to retrieve.</param>
        /// <param name="categories">The categories with the given id.</param>
        /// <returns>The retrieved categories.</returns>
        public async Task<IEnumerable<Category>> GetById(IEnumerable<Guid> id, IEnumerable<Category> categories, CancellationToken cancellationToken)
        {
            if (id == null || !id.Any() || Guid.Empty == id.First())
            {
                throw  DomainException.IdentityInvalidException.Identities(id ?? []);
            }
            else if (categories == null || !categories.Any(c => id.Contains(c.Id)))
            {
                throw CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categories ?? []);
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
        /// <param name="cancellationToken"></param>
        /// <returns>The retrieved categories.</returns>
        public async Task<IEnumerable<Category>> GetAll(IEnumerable<Category> categories,
            CancellationToken cancellationToken)
        {
            if (categories == null || !categories.Any())
            {
                throw  CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categories ?? []);
            }

            try
            {
                if (categories == null || !categories.Any())
                {
                    throw CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categories ?? []);
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
        /// <param name="booleanI"></param>
        /// <param name="booleanII"></param>
        /// <param name="categories">The categories to check for existence.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the categories exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        /// <exception cref="DomainException.IdentityInvalidException.Identities">Thrown if the categories already exist.</exception>
        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<Category> categories,
            CancellationToken cancellationToken)
        {
            try
            {
                if (id)
                {
                    var categoriesId = categories.Select(c => c.Id).ToList();
                    throw DomainException.IdentityInvalidException.Identities(categoriesId);
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
