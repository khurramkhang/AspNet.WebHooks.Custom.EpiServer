namespace AspNet.WebHooks.Custom.EpiServer.Test
{
    using AspNet.WebHooks.Custom.EpiServerStorage.Extensions;
    using AspNet.WebHooks.Custom.EpiServerStorage.WebHooks;
    using Microsoft.AspNet.WebHooks;
    using Microsoft.AspNet.WebHooks.Services;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    [TestFixture]
    public class EPiServerStorage_Tests
    {
        [Test]
        public void It_Is_EpiWebHookStore()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.InitializeCustomWebHooksEPiServerStorage();
            IWebHookStore actual = CustomServices.GetStore();

            Assert.IsInstanceOf<EpiWebHookStore>(actual);
        }
    }
}
