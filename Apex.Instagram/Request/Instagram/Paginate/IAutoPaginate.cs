using System.Threading.Tasks;

namespace Apex.Instagram.Request.Instagram.Paginate
{
    public interface IAutoPaginate<T> where T : Response.JsonMap.Response, IPaginate
    {
        bool HasMore { get; }

        Task<T> Next();
    }
}