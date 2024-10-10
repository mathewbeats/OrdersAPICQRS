using AAAaCQRSApi.IRepository;
using CQRSModels.Dtos;
using CQRSModels.Models;
using MediatR;

namespace AAAaCQRSApi.Handlers;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrdersDto>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }

    public async Task<OrdersDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId);

        if (order is null)
        {
            throw new ArgumentException("Id not found", nameof(request.OrderId));
        }

        return new OrdersDto
        {
            OrderId = order.OrderId,
            ProductName = order.ProductName,
            Quantity = order.Quantity
        };
    }
}