﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Goalsetter.DataAccess
{
    public static class ApplicationBuilderMigrationExtension
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppContext>();
                
                context.Database.Migrate();
            }
            return builder;
        }
    }
}