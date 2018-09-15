using MessagePack;

namespace Apex.Instagram.Login
{
    [MessagePackObject]
    public class LoginInfo
    {
        [Key(0)]
        public bool IsLoggedIn { get; internal set; }
    }
}