using JogandoBack.API.Data.Models.Filters;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Services.Uri;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace JogandoBack.API.Data.Utils
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedResponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords,
            IUriService uriService, string route, Dictionary<string, string> filtersList)
        {
            var response = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);

            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);

            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;

            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;

            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);

            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);

            response.TotalPages = roundedTotalPages;

            response.TotalRecords = totalRecords;

            foreach (var filter in filtersList)
            {
                if (filter.Value == null)
                {
                    continue;
                }

                if (response.FirstPage != null)
                {
                    response.FirstPage = new Uri(QueryHelpers.AddQueryString(response.FirstPage.ToString(), filter.Key, filter.Value));
                }

                if (response.LastPage != null)
                {
                    response.LastPage = new Uri(QueryHelpers.AddQueryString(response.LastPage.ToString(), filter.Key, filter.Value));
                }

                if (response.NextPage != null)
                {
                    response.NextPage = new Uri(QueryHelpers.AddQueryString(response.NextPage.ToString(), filter.Key, filter.Value));
                }

                if (response.PreviousPage != null)
                {
                    response.PreviousPage = new Uri(QueryHelpers.AddQueryString(response.PreviousPage.ToString(), filter.Key, filter.Value));
                }
            }

            return response;
        }
    }
}
