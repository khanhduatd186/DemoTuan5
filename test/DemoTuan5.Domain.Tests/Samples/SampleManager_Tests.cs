﻿using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace DemoTuan5.Samples;

public abstract class SampleManager_Tests<TStartupModule> : DemoTuan5DomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    //private readonly SampleManager _sampleManager;

    protected SampleManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

    [Fact]
    public Task Method1Async()
    {
        return Task.CompletedTask;
    }
}
