using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.Models;

namespace TaskTracker.Core.Architecture.DAL
{
    public static class RepositoryExtensions
    {
        public static async Task<IEnumerable<GenericViewModel>> ToGenericViewModelAll<T>(this IRepository<T> repository) where T : class, IModel, IModelName
        {
            var targets = await repository.FindAllAsync(i => i.IsActive);
            var result = Mapper.Map<IModelName, GenericViewModel>(targets);
            return result;
        }

        public static async Task<IEnumerable<GenericViewModel>> ToGenericViewModelAllOrdered<T>(this IRepository<T> repository) where T : class, IModel, IModelName
        {
            var targets = (await repository.FindAllAsync(i => i.IsActive)).OrderBy(i => i.Name);
            var result = Mapper.Map<IModelName, GenericViewModel>(targets);
            return result;
        }
    }
}
