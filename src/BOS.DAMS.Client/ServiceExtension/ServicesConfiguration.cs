using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
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

            services.AddHttpClient<IDAMSClient, DAMSClient>(client =>
            {
                client.BaseAddress = new Uri("https://apis.dev.bosframework.com/dams/odata/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            });
        }
    }
}
