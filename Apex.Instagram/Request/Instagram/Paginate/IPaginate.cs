namespace Apex.Instagram.Request.Instagram.Paginate
{
    /// <summary>
    ///     Indicated that a response type can be paginated.
    /// </summary>
    public interface IPaginate
    {
        /// <summary>Gets the next maximum identifier.</summary>
        /// <value>The next maximum identifier.</value>
        string NextMaxId { get; }
    }
}