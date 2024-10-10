using AAAaCQRSApi.IRepository;
using CQRSModels.Dtos;
using CQRSModels.Models;
using MediatR;

namespace AAAaCQRSApi.Handlers;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrdersDto>>
{
    private readonly IOrderReadRepository _repository;

    public GetAllOrdersQueryHandler(IOrderReadRepository repository)
    {
        this._repository = repository;
    }

    public async Task<IEnumerable<OrdersDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync();

        if (orders is null)
        {
            throw new ArgumentException("Orders not found");
        }

        return orders.Select(order => new OrdersDto
        {
            OrderId = order.OrderId,
            ProductName = order.ProductName,
            Quantity = order.Quantity
        }).ToList();
    }
}