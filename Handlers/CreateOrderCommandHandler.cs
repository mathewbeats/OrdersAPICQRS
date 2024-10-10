using AAAaCQRSApi.IRepository;
using CQRSModels.Models;
using MediatR;

namespace AAAaCQRSApi.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly IOrderWriteRepository _orderWriteRepository;

        public CreateOrderCommandHandler(IOrderWriteRepository repository)
        {
            _orderWriteRepository = repository;
        }

        public async Task<Unit> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = new Order(command.OrderId, command.ProductName, command.Quantity);

            await _orderWriteRepository.AddAsync(order);

            return Unit.Value;  // Retornar Task<Unit> para cumplir con la interfaz de MediatR
        }
    }
}