using Bogus;

namespace FC.Codeflix.Catalog.UnitTests.common
{
    public abstract class BaseFixture
    {
        public Faker Faker { get; private set; }

        protected BaseFixture() => Faker = new Faker("pt_BR");
    }
}
