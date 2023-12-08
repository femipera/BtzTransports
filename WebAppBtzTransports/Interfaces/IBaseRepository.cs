using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Interfaces;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int? id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
