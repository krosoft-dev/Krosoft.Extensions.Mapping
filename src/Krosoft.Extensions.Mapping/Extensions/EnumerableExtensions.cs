using AutoMapper;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Models;

namespace Krosoft.Extensions.Mapping.Extensions;

/// <summary>
/// Méthodes d'extensions pour les classes implémentant l'interface <see cref="IEnumerable{T}" />.
/// </summary>
public static class EnumerableExtensions
{
    public static IEnumerable<TDestination> MapTo<TSource, TDestination>(this IEnumerable<TSource> items,
                                                                         IMapper mapper) =>
        mapper.Map<IEnumerable<TDestination>>(items);

    public static PaginationResult<TDestination> ToPagination<TSource, TDestination>(this IEnumerable<TSource> items,
                                                                                     ISearchPaginationRequest paginationRequest,
                                                                                     IMapper mapper,
                                                                                     IDictionary<string, Func<TDestination, object?>>? customSorts = null)
    {
        var list = items.ToList();

        var pagined = list
                      .MapTo<TSource, TDestination>(mapper)
                      .SortBy(paginationRequest, customSorts)
                      .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                      .Take(paginationRequest.PageSize);

        return new PaginationResult<TDestination>(pagined, list.Count, paginationRequest.PageNumber, paginationRequest.PageSize);
    }
}
 