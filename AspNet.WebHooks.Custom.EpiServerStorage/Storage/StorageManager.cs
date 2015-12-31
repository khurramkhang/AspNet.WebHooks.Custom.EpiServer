using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Data.Dynamic;
using Microsoft.AspNet.WebHooks.Diagnostics;

namespace AspNet.WebHooks.Custom.EpiServerStorage.Storage
{
    public class StorageManager : IStorageManager
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageManager"/> class with the given <paramref name="logger"/>.
        /// </summary>
        public StorageManager(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            _logger = logger;
        }

        /// <summary>
        /// Create a new web hook
        /// </summary>
        /// <param name="webHook">EPiServer Web hook</param>
        public void CreateWebHook(EpiWebHook webHook)
        {
            try
            {
                var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(EpiWebHook));
                var id = store.Save(webHook);
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw;
            }
        }

        /// <summary>
        /// Update existing web hook
        /// </summary>
        /// <param name="webHook">EPiServer Web hook</param>
        public void UpdateWebHook(EpiWebHook webHook)
        {
            try
            {
                var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(EpiWebHook));
                store.Save(webHook);
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw;
            }
        }

        /// <summary>
        /// delete existing web hook
        /// </summary>
        /// <param name="webHook">EPiServer Web hook</param>
        public void DeleteWebHook(EpiWebHook webHook)
        {
            try
            {
                var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(EpiWebHook));
                var item = store.Items<EpiWebHook>().Where(x => x.WebHookId == webHook.WebHookId).FirstOrDefault();

                if (item != null)
                {
                    store.Delete(item.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw;
            }
        }

        /// <summary>
        /// delete existing web hooks of user
        /// </summary>
        /// <param name="user">user name</param>
        public void DeleteWebHook(string user)
        {
            try
            {
                foreach (var hook in this.GetWebHooks(user))
                {
                    this.DeleteWebHook(hook);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw;
            }
        }

        /// <summary>
        /// delete existing web hook of user by id
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="id">hook id</param>
        public void DeleteWebHook(string user, string id)
        {
            try
            {
                this.DeleteWebHook(this.GetWebHooks(user, id));
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw;
            }
        }

        /// <summary>
        /// get all web hooks
        /// </summary>
        public IEnumerable<EpiWebHook> GetWebHooks()
        {
            try
            {
                var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(EpiWebHook));
                return store.LoadAll<EpiWebHook>();
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw new InvalidOperationException("Error in DDS", ex);
            }
        }

        /// <summary>
        /// get all web hooks of user
        /// </summary>
        /// <param name="user">user name</param>
        public IEnumerable<EpiWebHook> GetWebHooks(string user)
        {
            try
            {
                var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(EpiWebHook));
                return store.Items<EpiWebHook>().Where(x => x.User == user);
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw new InvalidOperationException("Error in DDS", ex);
            }
        }

        /// <summary>
        /// Get all web hooks of user of id
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="id">web hook id</param>
        /// <returns></returns>
        public EpiWebHook GetWebHooks(string user, string id)
        {
            try
            {
                var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(EpiWebHook));
                var items = store.Items<EpiWebHook>().Where(x => x.User == user && x.WebHookId.ToString().Equals(id));
                if (items != null && items.Count() > 0)
                {
                    return items.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(System.Web.Http.Tracing.TraceLevel.Error, "Error in DDS", ex);
                throw new InvalidOperationException("Error in DDS", ex);
            }
        }
    }
}
