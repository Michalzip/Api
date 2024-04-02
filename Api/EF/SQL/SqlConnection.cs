using System;
using Microsoft.EntityFrameworkCore;

namespace Api.EF.SQL
{
    public static class SqlConnection
    {
        public static IServiceCollection AddSql<T>(this IServiceCollection services)
            where T : DbContext
        {
            var options = services.GetOptions<SqlOptions>("sql");

            services.AddDbContext<T>(o => o.UseSqlServer(options.ConnectionString));

            return services;
        }
    }
}
