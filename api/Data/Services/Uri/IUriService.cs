using JogandoBack.API.Data.Models.Filters;

namespace JogandoBack.API.Data.Services.Uri
{
    public interface IUriService
    {
        public System.Uri GetPageUri(PaginationFilter filter, string route);
    }
}
