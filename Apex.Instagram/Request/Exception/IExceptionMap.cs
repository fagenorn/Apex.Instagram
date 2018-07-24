namespace Apex.Instagram.Request.Exception
{
    internal interface IExceptionMap
    {
        bool TryGet(string message, out RequestException result);
    }
}