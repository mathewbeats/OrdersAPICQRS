using AAAaCQRSApi.Data;
using AAAaCQRSApi.IRepository;
using CQRSModels.Models;
using Microsoft.EntityFrameworkCore;

namespace AAAaCQRSApi.Repository;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly ApplicationDbContext _context;


    public OrderReadRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = await _context.Orders.OrderBy(c => c.OrderId).ThenBy(c => c.ProductName).ToListAsync();

        return orders;
    }
}