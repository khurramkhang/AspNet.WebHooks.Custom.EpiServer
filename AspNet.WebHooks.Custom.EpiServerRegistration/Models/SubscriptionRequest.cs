using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.WebHooks.Custom.EpiServerRegistration.Models
{
    public class SubscriptionRequest
    {
        public string WebURI { get; set; }
        public string Secret { get; set; }
        public string Description { get; set; }
        public string Services { get; set; }
    }
}
