﻿using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.DeleteCategory
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public void DeleteCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var categoryExample = _fixture.GetValidCategory();

            repositoryMock.Setup(x => x.Get(
                categoryExample.Id, 
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(categoryExample);
            var input = new DeleteCategoryInput(categoryExample.Id);
            var useCase = new DeleteCategory(
                    repositoryMock.Object,
                    unitOfWorkMock.Object
                );

            useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(
                categoryExample.Id,
                It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.Verify(x => x.Delete(
                categoryExample.Id,
                It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}