using System.Data;
using Data.Context;
using Domain.Entity;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly MyContext _context;
    protected DbSet<T> _dataset;
    
    public BaseRepository(MyContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }
    
    public async Task<T> InsentAsync(T item)
    {
        try
        {
            if (item.id == Guid.Empty)
            {
                item.id = Guid.NewGuid();
            }

            item.CreatAt = DateTime.UtcNow;
            _dataset.Add(item);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }

        return item;
    }

    public async Task<T> UpdateAsync(T item)
    {
        try
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.id.Equals(item.id));
            if (result == null)
                return null;
            item.UpdateAt = DateTime.UtcNow;
            item.CreatAt = result.CreatAt;
            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }

        return item;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.id.Equals(id)); 
            if (result == null)
                return false;
            
            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> ExistAsync (Guid id)  
    {
        return await _dataset.AnyAsync (p => p.id.Equals (id));
    }
    
    public async Task<T> SelectAsync(Guid id)
    {
        try
        {
            return await _dataset.SingleOrDefaultAsync (p => p.id.Equals (id)); 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<T>> SelectAsync()
    {
        try
        {
            return await _dataset.ToListAsync( );
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}