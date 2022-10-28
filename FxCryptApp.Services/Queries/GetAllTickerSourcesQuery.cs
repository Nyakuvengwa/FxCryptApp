using FxCryptApp.Common.Models;
using MediatR;
namespace FxCryptApp.Services.Queries;

public class GetAllTickerSourcesQuery : IRequest<List<TickerSourceResponse>>
{
}
