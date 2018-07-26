namespace Apex.Instagram.Request.Exception.Model
{
    internal interface IMap<in T>
    {
        bool TryGet(T message, out RequestException result);
    }
}