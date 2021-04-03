using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Core.Architecture.BLL
{
    public interface IServiceAction
    {
        void Execute();
        Task ExecuteAsync();
    }

    public interface IServiceActionInput<TInput>
    {
        void Execute(TInput parameter);
        Task ExecuteAsync(TInput parameter);
    }

    public interface IServiceAction<TOutput>
    {
        TOutput Execute();
        Task<TOutput> ExecuteAsync();
    }

    public interface IServiceAction<TOutput, TInput>
    {
        TOutput Execute(TInput parameter);
        Task<TOutput> ExecuteAsync(TInput parameter);
    }
}
