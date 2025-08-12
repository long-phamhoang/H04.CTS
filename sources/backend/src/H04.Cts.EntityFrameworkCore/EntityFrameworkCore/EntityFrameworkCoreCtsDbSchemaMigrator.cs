﻿using H04.Cts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace H04.Cts.EntityFrameworkCore;

public class EntityFrameworkCoreCtsDbSchemaMigrator
    : ICtsDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCtsDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the CtsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CtsDbContext>()
            .Database
            .MigrateAsync();
    }
}