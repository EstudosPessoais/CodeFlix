using FC.Codeflix.Catalog.Domain.Repository;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategory : IRequestHandler<GetCategoryInput, GetCategoryOutput>
    {
        private readonly ICategoryRespository _categoryRespository;

        public GetCategory(ICategoryRespository categoryRespository)
        {
            _categoryRespository = categoryRespository;
        }

        public async Task<GetCategoryOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
        {
            var category = await _categoryRespository.Get(request.Id, cancellationToken);

            return GetCategoryOutput.FromCategory(category);
        }
    }
}
