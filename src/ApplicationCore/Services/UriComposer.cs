using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;

namespace ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly CatalogSettings _settings;

        public UriComposer(IOptions<CatalogSettings> settings)
        {
            _settings = settings.Value;
        }

        public string ComposePictureUri(string uriTemplate)
        {
            return uriTemplate.Replace("http://catalogbaseurltobereplaced", _settings.CatalogBaseUrl);
        }
    }
}
