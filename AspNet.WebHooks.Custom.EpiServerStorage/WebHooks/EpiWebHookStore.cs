using Microsoft.AspNet.WebHooks.Config;
using AspNet.WebHooks.Custom.EpiServerStorage.Storage;
using Microsoft.AspNet.WebHooks.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

namespace AspNet.WebHooks.Custom.EpiServerStorage.WebHooks
{
    public class EpiWebHookStore : IWebHookStore
    {
        private readonly IStorageManager manager;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpiWebHookStore"/> class with the given 
        /// <paramref name="managerObject"/>,
        /// and <paramref name="logger"/>.
        /// </summary>
        public EpiWebHookStore(IStorageManager managerObject, ILogger loggerObject)
        {
            if (managerObject == null)
            {
                throw new ArgumentNullException("manager");
            }
            
            if (loggerObject == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.manager = managerObject;
            this.logger = loggerObject;
        }

        public Task DeleteAllWebHooksAsync(string user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            manager.DeleteWebHook(NormalizeKey(user));
            return Task.FromResult(true);
        }

        public Task<StoreResult> DeleteWebHookAsync(string user, string id)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            manager.DeleteWebHook(NormalizeKey(user));
            return Task.FromResult(StoreResult.Success);
        }

        public Task<ICollection<WebHook>> GetAllWebHooksAsync(string user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var epiHooks = manager.GetWebHooks(NormalizeKey(user));
            ICollection<WebHook> result = epiHooks.Select(c => ConvertIntoWebHooks(c)).Where(w => w != null).ToArray();
            return Task.FromResult(result);
        }

        public Task<StoreResult> InsertWebHookAsync(string user, WebHook webHook)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (webHook == null)
            {
                throw new ArgumentNullException("WebHook");
            }

            manager.CreateWebHook(
                new EpiWebHook()
                {
                    User = NormalizeKey(user),
                    WebHook = webHook
                }
            );
            return Task.FromResult(StoreResult.Success);
        }

        public Task<WebHook> LookupWebHookAsync(string user, string id)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var epiHook = manager.GetWebHooks(NormalizeKey(user), NormalizeKey(id));
            return Task.FromResult(this.ConvertIntoWebHooks(epiHook));
        }

        public Task<ICollection<WebHook>> QueryWebHooksAsync(string user, IEnumerable<string> actions)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (actions == null)
            {
                throw new ArgumentNullException("actions");
            }

            var epiHooks = manager.GetWebHooks(NormalizeKey(user));
            ICollection<WebHook> webHooks = epiHooks.Select(c => ConvertIntoWebHooks(c)).Where(w => w != null).ToArray();
            ICollection<WebHook> matches = new List<WebHook>();
            foreach (WebHook webHook in webHooks)
            {
                if (webHook.IsPaused)
                {
                    continue;
                }

                foreach (string action in actions)
                {
                    if (webHook.MatchesAction(action))
                    {
                        matches.Add(webHook);
                        break;
                    }
                }
            }

            return Task.FromResult(matches);
        }

        public Task<StoreResult> UpdateWebHookAsync(string user, WebHook webHook)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (webHook == null)
            {
                throw new ArgumentNullException("WebHook");
            }

            manager.CreateWebHook(
                new EpiWebHook()
                {
                    User = NormalizeKey(user),
                    WebHook = webHook
                }
            );
            return Task.FromResult(StoreResult.Success);
        }

        protected WebHook ConvertIntoWebHooks(EpiWebHook epiHook)
        {
            return epiHook.WebHook;
        }

        private static string NormalizeKey(string value)
        {
            return value.ToLowerInvariant();
        }
    }
}
