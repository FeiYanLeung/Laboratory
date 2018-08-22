using Laboratory.Resources;
using System;
using System.ComponentModel;

namespace Laboratory.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string resourceId;
        public LocalizedDisplayNameAttribute(string resourceId)
        {
            this.resourceId = resourceId;
        }

        public override string DisplayName => Resource.ResourceManager.GetString(resourceId, Resource.Culture);
    }
}