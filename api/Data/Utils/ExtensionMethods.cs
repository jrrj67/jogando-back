using JogandoBack.API.Data.Models.Filters.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JogandoBack.API.Data.Utils
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
        
        public static Dictionary<string, string> ToDictionary(this object source)
        {
            return source.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(source, null));
        }
    }
}
