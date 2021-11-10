using Microsoft.AspNetCore.Http;
using System;

namespace api.Data.Utils
{
    public static class ExtensionMethods
    {
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri.ToString();
        }
    }
}
