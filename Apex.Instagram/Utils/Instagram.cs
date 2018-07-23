using System.Text;

namespace Apex.Instagram.Utils
{
    internal class Instagram
    {
        public string GenerateMultipartBoundary()
        {
            const string chars  = "-_1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int    length = 30;
            var          result = new StringBuilder();
            var          max    = chars.Length - 1;
            for ( var i = 0; i < length; i++ )
            {
                result.Append(chars[Randomizer.Instance.Number(max)]);
            }

            return result.ToString();
        }

        #region Singleton     

        private static Instagram _instance;

        private static readonly object Lock = new object();

        private Instagram() { }

        public static Instagram Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Instagram());
                }
            }
        }

        #endregion
    }
}