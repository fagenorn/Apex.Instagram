using System.Threading.Tasks;

namespace Apex.Instagram.API.Request.Instagram.Paginate
{
    /// <summary>Automatically paginate through a certain feed.</summary>
    /// <typeparam name="T">Response type</typeparam>
    public interface IAutoPaginate<T> where T : Response.JsonMap.Response, IPaginate
    {
        /// <summary>Gets a value indicating whether this instance has more results.</summary>
        /// <value>
        ///     <c>true</c> if this instance has more; otherwise, <c>false</c>.
        /// </value>
        bool HasMore { get; }

        /// <summary>Next page of this instance.</summary>
        Task<T> NextAsync();
    }
}