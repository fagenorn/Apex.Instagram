using System.Collections.Generic;
using System.Threading.Tasks;

using Apex.Instagram.Service.Model;

namespace Apex.Instagram.Service.Database
{
    internal interface IDatabase<T> where T : IModel
    {
        Task SaveAsync(T data);

        Task<T> LoadAsync(int id);

        Task DeleteAsync(int id);

        Task<List<T>> LoadAllAsync();
    }
}