using AutoMapper;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Tools;

namespace Krosoft.Extensions.Mapping.Extensions;

public static class MapperExtensions
{
    public static void MapIfExist<TKey, TSource, TDestination>(this IMapper mapper,
                                                               IDictionary<TKey, TSource> sourcesParKey,
                                                               TKey key,
                                                               TDestination? destination)
        where TSource : class
        where TDestination : class
    {
        Guard.IsNotNull(nameof(sourcesParKey), sourcesParKey);
        Guard.IsNotNull(nameof(destination), destination);

        var source = sourcesParKey.GetValueOrDefault(key);
        if (source != default)
        {
            mapper.Map(source, destination);
        }
    }

    public static void MapIfExist<TSource, TDestination>(this IMapper mapper,
                                                         TSource? source,
                                                         TDestination? destination)
        where TSource : class
        where TDestination : class

    {
        Guard.IsNotNull(nameof(destination), destination);

        if (source != default)
        {
            mapper.Map(source, destination);
        }
    }

    public static void MapIfExist<TSource, TDestination>(this IMapper mapper,
                                                         TSource? source,
                                                         TDestination? destination,
                                                         Action action)
        where TSource : class
        where TDestination : class
    {
        Guard.IsNotNull(nameof(destination), destination);

        if (source == default)
        {
            action();
        }

        mapper.Map(source, destination);
    }

    public static TDestination? MapIfExist<TDestination>(this IMapper mapper,
                                                         object? source)
    {
        if (source != default)
        {
            return mapper.Map<TDestination>(source);
        }

        return default;
    }

    public static TDestination MapIfExist<TDestination>(this IMapper mapper,
                                                        object? source,
                                                        Action action)
    {
        if (source == default)
        {
            action();
        }

        return mapper.Map<TDestination>(source);
    }
}