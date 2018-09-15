using System.IO;
using System.Threading.Tasks;

namespace Apex.Instagram.Storage
{
    public interface IStorage
    {
        /// <summary>
        ///     Save an object stream associated with an <see cref="Account" />.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="subId">The settings id</param>
        /// <param name="data">The data stream that has to be saved</param>
        Task SaveAsync(int id, int subId, Stream data);

        /// <summary>
        ///     Load an object stream based on the gived id's. If no object stream is found, simply return
        ///     <see langword="null"></see>.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="subId">The settings id</param>
        /// <returns>The data stream of the found object</returns>
        Stream Load(int id, int subId);
    }
}