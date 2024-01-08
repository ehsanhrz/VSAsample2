﻿using DbContext;
using Microsoft.EntityFrameworkCore;

namespace IntegrationalTests;


[SetUpFixture]
public class IntegrationalTestsBaseSetUp
{
    private static PostgreSqlContainerTest? Container { get; set; }
    protected static AppDbContext? DbContext { get; set; }

    [OneTimeSetUp]
    public async Task SetDbContextForTest()
    {
        
        Container = new PostgreSqlContainerTest();
        await Container.InitializeAsync();
        DbContext = new AppDbContext(Container.CreateNewContextOptions());
        DbContext.Database.Migrate();    
        
    }
    
    
    [OneTimeTearDown]
    public async Task Dispose()
    {
        await Container?.DisposeAsync()!;
        await DbContext?.DisposeAsync().AsTask()!;
    }    
}
