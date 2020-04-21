using FluentAssertions;
using Ninject;
using Xunit;

namespace TestableCodeDemos.Module5.Easy
{
    public class SingletonTests
    {
        [Fact]
        public void TestTransientScopeReturnsDifferentInstance()
        {
            var container = new StandardKernel();

            container.Bind<ISecurity>()
                .To<Security>();

            var security1 = container.Get<ISecurity>();

            var security2 = container.Get<ISecurity>();

            //Assert.That(security1,
            //    Is.Not.SameAs(security2));

            security1.Should().NotBe(security2);
        }

        [Fact]
        public void TestSingletonReturnsSameInstance()
        {
            var container = new StandardKernel();

            container.Bind<ISecurity>()
                .To<Security>()
                .InSingletonScope();

            var security1 = container.Get<ISecurity>();

            var security2 = container.Get<ISecurity>();

            //Assert.That(security1,
            //    Is.SameAs(security2));

            security1.Should().Be(security2);
        }
    }
}
