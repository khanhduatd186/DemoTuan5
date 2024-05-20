using DemoTuan5.Samples;
using Xunit;

namespace DemoTuan5.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<DemoTuan5MongoDbTestModule>
{

}
