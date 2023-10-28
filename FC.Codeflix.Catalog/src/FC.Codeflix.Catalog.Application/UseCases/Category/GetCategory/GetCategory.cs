using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategory : IGetCategory
    {
        private readonly ICategoryRespository _categoryRespository;

        public GetCategory(ICategoryRespository categoryRespository)
        {
            _categoryRespository = categoryRespository;
        }

        public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
        {
            var category = await _categoryRespository.Get(request.Id, cancellationToken);

            return CategoryModelOutput.FromCategory(category);
        }
    }
}
