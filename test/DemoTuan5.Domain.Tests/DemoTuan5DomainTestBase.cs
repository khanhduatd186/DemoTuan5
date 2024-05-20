using Volo.Abp.Modularity;

namespace DemoTuan5;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DemoTuan5DomainTestBase<TStartupModule> : DemoTuan5TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
