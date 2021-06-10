using LifeBackup.Core.Communication.Interfaces;
using LifeBackup.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeBackup.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddSingleton<IBucketRepository, BucketRepository>();
            service.AddSingleton<IFilesRepository, FilesRepository>();
        }
    }
}
