﻿using Bogus;
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
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            var value = Faker.Commerce.ProductName();

            Action action = () => DomainValidation.NotNull(value, fieldName);
 
            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NoNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NoNullThrowWhenNull()
        {
            string? value = null;
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.NotNull(value, fieldName);

            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null");
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be empty or null");
        }


        [Fact(DisplayName = nameof(NotNullOrEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmpty()
        {
            var target = Faker.Commerce.ProductName();
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().NotThrow();
        }

        [Theory(DisplayName =nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanTheMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be at least {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanTheMin(int numberOfTests = 5)
        {
            yield return new object[] { "123456", 10 };
            var faker = new Faker();
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanTheMin), parameters: 10)]
        public void MinLengthOk(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesGreaterThanTheMin(int numberOfTests = 5)
        {
            yield return new object[] { "123456", 6 };
            var faker = new Faker();
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
        public void MaxLengthThrowWhenGreater(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests = 5)
        {
            yield return new object[] { "123456", 5 };
            var faker = new Faker();
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, maxLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
        public void MaxLengthOk(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesLessThanMax(int numberOfTests = 5)
        {
            yield return new object[] { "123456", 6 };
            var faker = new Faker();
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length + (new Random()).Next(0, 5);
                yield return new object[] { example, maxLength };
            }
        }
    }
}
