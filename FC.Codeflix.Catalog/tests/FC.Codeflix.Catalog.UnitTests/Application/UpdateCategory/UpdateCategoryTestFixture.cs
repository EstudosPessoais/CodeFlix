﻿using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> {};

    public class UpdateCategoryTestFixture : BaseFixture
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

        public bool GetRandomBoolean()
        {
            return (new Random()).NextDouble() < 0.5;
        }

        public CreateCategoryInput GetInput()
            => new(
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    GetRandomBoolean()
                   );

        public Category GetExampleCategory()
            => new Category(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

        public UpdateCategoryInput GetValidInput(Guid? id = null)
        {
            return new UpdateCategoryInput(
                   id ?? Guid.NewGuid(),
                   GetValidCategoryName(),
                   GetValidCategoryDescription(),
                   GetRandomBoolean()
               );
        }

        public UpdateCategoryInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetValidInput();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }

        public UpdateCategoryInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetValidInput();
            var tooLongNameForCategory = Faker.Commerce.ProductName();
            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"${tooLongNameForCategory} {Faker.Commerce.ProductName()}";

            invalidInputTooLongName.Name = tooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public UpdateCategoryInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetValidInput();
            var tooLongDescriptionForCategory = Faker.Commerce.ProductDescription();
            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"${tooLongDescriptionForCategory} {Faker.Commerce.ProductDescription()}";

            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }

    }
}
