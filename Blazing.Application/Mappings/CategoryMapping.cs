using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Mappings
{
    public class CategoryMapping
    {
        public virtual IEnumerable<Category> ReturnCategory(IEnumerable<CategoryDto> categories)
        {
            var categoriesList = new List<Category>();
            foreach (var category in categories)
            {
                var categoryDto = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    DataCreated = category.DataCreated,
                    DataDeleted = category.DataDeleted,
                    DataUpdated = category.DataUpdated
                };

                categoriesList.Add(categoryDto);
            }

            return categoriesList;
        }
    }
}
