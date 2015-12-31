using EPiServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Framework.Initialization;
using Mediachase.Commerce.Catalog.Events;
using Mediachase.Commerce.Engine.Events;
using Microsoft.AspNet.WebHooks;
using System.Web.Mvc;

namespace WebHooks.Custom.EpiServerServices.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class CatalogEventListener : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            CatalogEventBroadcaster.Instance.LocalEntryUpdating += InstanceOnLocalEntryUpdating;
            var broadcaster = context.Locate.Advanced.GetInstance<CatalogKeyEventBroadcaster>();
            broadcaster.InventoryUpdated += Broadcaster_InventoryUpdated;
        }

        private void Broadcaster_InventoryUpdated(object sender, InventoryUpdateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InstanceOnLocalEntryUpdating(object sender, EntryEventArgs e)
        {
            SendWebhookContentChangedAsync("", "").Wait();
        }

        public void Uninitialize(InitializationEngine context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send Notification to register clients
        /// </summary>
        /// <param name="product">product code</param>
        /// <param name="productName"> New Product Name</param>
        /// <returns></returns>
        private static async Task SendWebhookContentChangedAsync(string product, string productName)
        {
            string eventName = "contentschanged";
            var notifications = new List<NotificationDictionary> { new NotificationDictionary(eventName, new { id = product, name = productName }) };

            // Send a notification to registered WebHooks with matching filters
            IWebHookManager manager = DependencyResolver.Current.GetManager();
            //ToDo: get all subscribers
            var x = await manager.NotifyAsync("admin", notifications);            
        }
    }
}
