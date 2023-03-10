using Cookbook.Communication.Request;
using Cookbook.Communication.Response;

namespace Cookbook.Application.UseCases.Dashboard;

public interface IDashboardUseCase
{
    Task<DashboardResponse> ExecuteAsync(DashboardRequest request);
}
