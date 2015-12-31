using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.WebHooks.Custom.EpiServerStorage.Storage
{
    public interface IStorageManager
    {
        /// <summary>
        /// Create a new web hook
        /// </summary>
        /// <param name="webHook">EPiServer Web hook</param>
        void CreateWebHook(EpiWebHook webHook);

        /// <summary>
        /// Update existing web hook
        /// </summary>
        /// <param name="webHook">EPiServer Web hook</param>
        void UpdateWebHook(EpiWebHook webHook);

        /// <summary>
        /// delete existing web hook
        /// </summary>
        /// <param name="webHook">EPiServer Web hook</param>
        void DeleteWebHook(EpiWebHook webHook);

        /// <summary>
        /// delete existing web hooks of user
        /// </summary>
        /// <param name="user">user name</param>
        void DeleteWebHook(string user);

        /// <summary>
        /// delete existing web hook of user by id
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="id">hook id</param>
        void DeleteWebHook(string user, string id);

        /// <summary>
        /// get all web hooks
        /// </summary>
        IEnumerable<EpiWebHook> GetWebHooks();

        /// <summary>
        /// get all web hooks of user
        /// </summary>
        /// <param name="user">user name</param>
        IEnumerable<EpiWebHook> GetWebHooks(string user);

        /// <summary>
        /// Get all web hooks of user of id
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="id">web hook id</param>
        /// <returns></returns>
        EpiWebHook GetWebHooks(string user, string id);
    }
}
