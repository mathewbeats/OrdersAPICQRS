using AAAaCQRSApi.IRepository;
using CQRSModels.Models;
using MediatR;

namespace AAAaCQRSApi.Handlers;

public class DeleteOrderQueryHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderWriteRepository _repository;

    private readonly IOrderReadRepository _readRepository;

    public DeleteOrderQueryHandler(IOrderReadRepository readRepository, IOrderWriteRepository repository)
    {
        this._repository = repository;
        this._readRepository = readRepository;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orders = await _readRepository.GetByIdAsync(request.OrderId);

        if (orders is null)
        {
            throw new ArgumentException("order not found", nameof(request.OrderId));
        }

        await _repository.DeleteAsync(orders);

        return Unit.Value;
    }
}