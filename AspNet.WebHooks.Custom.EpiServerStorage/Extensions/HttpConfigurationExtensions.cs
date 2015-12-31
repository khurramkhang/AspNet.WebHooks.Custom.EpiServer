using AspNet.WebHooks.Custom.EpiServerStorage.Storage;
using AspNet.WebHooks.Custom.EpiServerStorage.WebHooks;
using Microsoft.AspNet.WebHooks;
using Microsoft.AspNet.WebHooks.Config;
using Microsoft.AspNet.WebHooks.Diagnostics;
using Microsoft.AspNet.WebHooks.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNet.WebHooks.Custom.EpiServerStorage.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HttpConfiguration"/>.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class HttpConfigurationExtensions
    {
        private const string ApplicationName = "Microsoft.AspNet.WebHooks";
        private const string Purpose = "WebHookPersistence";
        private const string DataProtectionKeysFolderName = "DataProtection-Keys";

        /// <summary>
        /// Configures an EPiServer DDS Storage implementation of <see cref="IWebHookStore"/>
        /// which provides a persistent store for registered WebHooks used by the custom WebHooks module.
        /// </summary>
        /// <param name="config">The current <see cref="HttpConfiguration"/>config.</param>
        public static void InitializeCustomWebHooksEPiServerStorage(this HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            WebHooksConfig.Initialize(config);
            ILogger logger = config.DependencyResolver.GetLogger();            
            IStorageManager storageManager = new StorageManager(logger);
            IWebHookStore store = new EpiWebHookStore(storageManager, logger);
            CustomServices.SetStore(store);
        }
    }
}
