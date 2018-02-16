using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;

namespace OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers.TokenDefinitions
{
    internal class SiteCollectionTermGroupIdToken : TokenDefinition
    {
        public SiteCollectionTermGroupIdToken(Web web)
            : base(web, "~sitecollectiontermgroupid", "{sitecollectiontermgroupid}")
        {
        }

        public override string GetReplaceValue()
        {
            if (string.IsNullOrEmpty(CacheValue))
            {
                // The token is requested. Check if the group exists and if not, create it
                this.Web.EnsureProperty(w => w.Url);
                using (ClientContext context = this.Web.Context.Clone(this.Web.Url))
                {
                    context.AddUserAgent();

                    var site = context.Site;
                    var session = TaxonomySession.GetTaxonomySession(context);
                    var termstore = session.GetDefaultSiteCollectionTermStore();
                    var termGroup = termstore.GetSiteCollectionGroup(site, true);
                    context.Load(termGroup);
                    context.ExecuteQueryRetry();

                    CacheValue = termGroup.Id.ToString();
                }
            }
            return CacheValue;
        }
    }
}