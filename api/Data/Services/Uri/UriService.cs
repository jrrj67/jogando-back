using JogandoBack.API.Data.Models.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace JogandoBack.API.Data.Services.Uri
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public System.Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new System.Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new System.Uri(modifiedUri);
        }
    }
}
