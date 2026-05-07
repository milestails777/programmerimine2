using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.BlazorWasm
{
    public interface IApiClient
    {
        Task<OperationResult<PagedResult<Project>>> List(int page, int pageSize);
        Task<OperationResult> Save(Project list);
        Task<OperationResult> Delete(int id);
    }
}