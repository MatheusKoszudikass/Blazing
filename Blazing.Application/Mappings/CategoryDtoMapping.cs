using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Application.Mappings
{
    public class CategoryDtoMapping
    {
        public virtual IEnumerable<CategoryDto> ReturnCategoryDto(IEnumerable<Category>? categories, CancellationToken cancellationToken)
        {
            var categoriesList = new List<CategoryDto>();
            foreach (var category in categories)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var categoryDto = new CategoryDto
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
