using Volo.Abp;
using Volo.Abp.MongoDB;

namespace DemoTuan5.MongoDB;

public static class DemoTuan5MongoDbContextExtensions
{
    public static void ConfigureDemoTuan5(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
