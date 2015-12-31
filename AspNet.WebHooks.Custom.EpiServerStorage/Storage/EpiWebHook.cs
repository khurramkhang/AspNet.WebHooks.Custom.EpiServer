using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using Microsoft.AspNet.WebHooks;

namespace AspNet.WebHooks.Custom.EpiServerStorage.Storage
{
    [EPiServerDataStore(AutomaticallyCreateStore =true, AutomaticallyRemapStore =true, StoreName = "EpiWebHook")]
    public class EpiWebHook : IDynamicData
    {
        /// <summary>
        /// dynamic data store Id
        /// </summary>
        public Identity Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Web hook
        /// </summary>
        [EPiServerIgnoreDataMember]
        public WebHook WebHook
        {
            get
            {
                WebHook hook = new WebHook()
                {
                    Id = this.WebHookId,
                    Description = this.Description,
                    IsPaused = this.IsPaused,
                    WebHookUri = this.WebHookUri,
                    Secret = this.Secret,
                };

                if (!string.IsNullOrWhiteSpace(this.Filters))
                {
                    foreach (string filter in this.Filters.Split(','))
                    {
                        hook.Filters.Add(filter.Trim());
                    }
                }

                return hook;
            }
            set {
                if (value != null)
                {
                    this.Description = value.Description;
                    this.Filters = string.Join(",", value.Filters.ToList());
                    this.IsPaused = value.IsPaused;
                    this.Secret = value.Secret;
                    this.WebHookId = value.Id;
                    this.WebHookUri = value.WebHookUri;
                }
            }
        }

        public string Description { get; set; }

        public string Filters { get; set; }
        
        public string WebHookId { get; set; }
        //
        // Summary:
        //     Gets or sets a value indicating whether the WebHook is paused.
        public bool IsPaused { get; set; }

        //
        // Summary:
        //     Gets or sets the secret used to sign the body of the WebHook request.
       public string Secret { get; set; }
        //
        // Summary:
        //     Gets or sets the URI of the WebHook.
        public string WebHookUri { get; set; }

        /// <summary>
        /// Default constuctor
        /// </summary>
        public EpiWebHook()
        {
            Initialize();
        }

        /// <summary>
        /// Intialization of objects
        /// </summary>
        protected void Initialize()
        {
            // Generate a new ID
            Id = Identity.NewIdentity(Guid.NewGuid());
        }
    }
}
