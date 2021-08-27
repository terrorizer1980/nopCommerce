using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Stores;

namespace Nop.Web.Framework.Globalization
{
    /// <summary>
    /// Determines the culture information for a request
    /// </summary>
    public class NopRequestCultureProvider : RequestCultureProvider
    {
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var localizationSettings = httpContext.RequestServices.GetService<LocalizationSettings>();

            if(!localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                return null;

            //localized URLs are enabled, so try to get language from the requested page URL
            var (isLocalized, language) = await httpContext.Request.Path.Value.IsLocalizedUrlAsync(httpContext.Request.PathBase, true);

            if(!isLocalized)
                return null;

            return new ProviderCultureResult(language.LanguageCulture);
        }
    }
}
