using Bogus;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Domain.Validation
{
    public class DomainValidationTest
    {
        public Faker Faker { get; set; } = new Faker();

        // Não ser null
        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = Faker.Commerce.ProductName();
            Action action = () => DomainValidation.NotNull(value, "value");
 
            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NoNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NoNullThrowWhenNull()
        {
            string value = null;
            Action action = () => DomainValidation.NotNull(value, "FieldName");

            action.Should().Throw<EntityValidationException>().WithMessage("FieldName should not be null");
        }

        // Não ser null ou vazio
        // Tamanho mínimo
        // Tamanho Máximo

    }
}
