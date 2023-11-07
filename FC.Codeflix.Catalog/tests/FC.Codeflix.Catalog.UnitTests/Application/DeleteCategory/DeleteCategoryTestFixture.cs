using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.common;
using Moq;
using FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Application.DeleteCategory
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture> { }
    public class DeleteCategoryTestFixture : BaseFixture
    {
        public Mock<ICategoryRespository> GetRepositoryMock()
        {
            return new Mock<ICategoryRespository>(); ;
        }

        public Mock<IUnitOfWork> GetUnitOfWorkMock() 
        { 
            return new Mock<IUnitOfWork>(); 
        }
 

        public string GetValidCategoryName()
        {
            var categoryName = "";
            while (categoryName.Length < 3)
                categoryName = Faker.Commerce.Categories(1)[0];
            if (categoryName.Length > 255)
                categoryName = categoryName[..255];
            return categoryName;
        }

        public string GetValidCategoryDescription()
        {
            var categoryDescription = Faker.Commerce.ProductDescription();
            if (categoryDescription.Length > 10000)
                categoryDescription = categoryDescription[..10000];
            return categoryDescription;

        }

        public Category GetValidCategory()
            => new(GetValidCategoryName(),
                GetValidCategoryDescription());
    }
}
