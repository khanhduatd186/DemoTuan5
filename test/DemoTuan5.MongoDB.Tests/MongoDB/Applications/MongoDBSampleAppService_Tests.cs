using DemoTuan5.MongoDB;
using DemoTuan5.Samples;
using Xunit;

namespace DemoTuan5.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<DemoTuan5MongoDbTestModule>
{

}
