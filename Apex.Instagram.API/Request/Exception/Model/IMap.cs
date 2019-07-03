namespace Apex.Instagram.API.Request.Exception.Model
{
    internal interface IMap<in T>
    {
        bool TryGet(T message, out RequestException result);
    }
}