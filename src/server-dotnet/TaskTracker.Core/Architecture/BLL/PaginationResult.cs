using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Architecture.DAL;

namespace TaskTracker.Core.Architecture.BLL
{
    public class PaginationResult<T>
    {
        public T Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public static PaginationResult<T> Create(T items, int pages, int count)
        {
            return new PaginationResult<T>()
            {
                Items = items,
                TotalPages = pages,
                TotalItems = count,
            };
        }

        public static PaginationResult<IEnumerable<T>> FromQuery(IContext context, IOrderedQueryable<T> itemQuery, PaginationParameter pagination)
        {
            var itemCount = itemQuery.Count();
            var queryPaginated = context.Paginate(itemQuery, pagination);
            var result = queryPaginated.ToList();
            var pageCount = (int)Math.Ceiling((double)itemCount / (double)pagination.ItemsPerPage);

            var paginatedResult = PaginationResult<IEnumerable<T>>.Create(result, pageCount, itemCount);

            return paginatedResult;
        }

        public static async Task<PaginationResult<IEnumerable<T>>> FromQueryAsync(IContext context, IOrderedQueryable<T> itemQuery, PaginationParameter pagination)
        {
            var itemCount = await itemQuery.CountAsync();
            int pageCount = 1;
            List<T> result = new List<T>();

            if (pagination.Page == -1)
                result = await itemQuery.ToListAsync();
            else
            {
                result = await context.Paginate(itemQuery, pagination).ToListAsync();
                pageCount = (int)Math.Ceiling((double)itemCount / (double)pagination.ItemsPerPage);
            }

            var paginatedResult = PaginationResult<IEnumerable<T>>.Create(result, pageCount, itemCount);

            return paginatedResult;
        }
    }

    public class PaginationResult<OldType, NewType>
    {
        public static PaginationResult<IEnumerable<NewType>> FromQuery(IContext context, IOrderedQueryable<OldType> itemQuery, PaginationParameter pagination, Func<IEnumerable<OldType>, IEnumerable<NewType>> transform)
        {
            var itemCount = itemQuery.Count();
            var queryPaginated = context.Paginate(itemQuery, pagination);
            var result = transform(queryPaginated.ToList());
            var pageCount = (int)Math.Ceiling((double)itemCount / (double)pagination.ItemsPerPage);

            var paginatedResult = PaginationResult<IEnumerable<NewType>>.Create(result, pageCount, itemCount);

            return paginatedResult;
        }

        public static async Task<PaginationResult<IEnumerable<NewType>>> FromQueryAsync(IContext context, IOrderedQueryable<OldType> itemQuery, PaginationParameter pagination, Func<IEnumerable<OldType>, IEnumerable<NewType>> transform)
        {
            var itemCount = await itemQuery.CountAsync();
            var queryPaginated = context.Paginate(itemQuery, pagination);
            var result = transform(await queryPaginated.ToListAsync());
            var pageCount = (int)Math.Ceiling((double)itemCount / (double)pagination.ItemsPerPage);

            var paginatedResult = PaginationResult<IEnumerable<NewType>>.Create(result, pageCount, itemCount);

            return paginatedResult;
        }
    }
}
