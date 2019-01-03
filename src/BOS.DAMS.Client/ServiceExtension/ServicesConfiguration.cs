using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.DAMS.Client.ServiceExtension
{
    public static class ServicesConfiguration
    {
        public static void AddBOSDAMSClient(this IServiceCollection services, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new NullReferenceException("BOS API Key must not be null or empty.");
            }

            services.AddHttpClient<IDAMSClient, DAMSClient>();
        }
    }
}
