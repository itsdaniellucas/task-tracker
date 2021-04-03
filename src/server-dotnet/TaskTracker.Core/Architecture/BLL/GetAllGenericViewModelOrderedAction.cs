using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Core.Architecture.Models;

namespace TaskTracker.Core.Architecture.BLL
{
    public class GetAllGenericViewModelOrderedAction<T> : IServiceAction<IServiceResult<IEnumerable<GenericViewModel>>> where T : class, IModel, IModelName
    {
        private readonly IRepository<T> _repository;

        public GetAllGenericViewModelOrderedAction(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual IServiceResult<IEnumerable<GenericViewModel>> Execute()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IServiceResult<IEnumerable<GenericViewModel>>> ExecuteAsync()
        {
            var result = await _repository.ToGenericViewModelAllOrdered();
            return ServiceResult<IEnumerable<GenericViewModel>>.CreateSuccess(result);
        }
    }
}
