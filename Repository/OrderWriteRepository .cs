using AAAaCQRSApi.Data;
using AAAaCQRSApi.IRepository;
using CQRSModels.Models;

namespace AAAaCQRSApi.Repository;

public class OrderWriteRepository  : IOrderWriteRepository
{
    private readonly ApplicationDbContext _context;


    public OrderWriteRepository(ApplicationDbContext context)
    {
        this._context = context;
    }


    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {

        _context.Orders.Update(order);

        await _context.SaveChangesAsync();

    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}