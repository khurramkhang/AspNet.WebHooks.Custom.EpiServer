using Microsoft.AspNet.WebHooks.Config;
using System.ComponentModel;
using System.Web.Http;

namespace AspNet.WebHooks.Custom.EpiServerRegistration.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        ///  Initializes support for registering and managing custom WebHooks through 
        ///  a set of ASP.NET Web APIs.
        /// </summary>
        /// <param name="config">The current <see cref="HttpConfiguration"/>config.</param>
        public static void InitializeEpiserverCustomWebHooksApis(this HttpConfiguration config)
        {
            WebHooksConfig.Initialize(config);
        }
    }
}
