using Volo.Abp.Modularity;

namespace DemoTuan5;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class DemoTuan5ApplicationTestBase<TStartupModule> : DemoTuan5TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
