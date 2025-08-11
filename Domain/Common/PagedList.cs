namespace Domain.Common
{
    using System;
    using System.Collections.Generic;

    public class PagedList<T>
    {
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public IEnumerable<T> Items { get; init; } = Array.Empty<T>();

        public PagedList() { }

        public PagedList(IEnumerable<T> items, int count, int? pageNumber, int? pageSize)
        {
            TotalCount = count;
            PageSize = pageSize ?? 1;
            CurrentPage = pageNumber ?? 1;
            TotalPages = pageSize.HasValue && pageSize.Value > 0
                ? (int)Math.Ceiling(count / (double)pageSize.Value)
                : 0;
            Items = items ?? Array.Empty<T>();
        }
    }
}

