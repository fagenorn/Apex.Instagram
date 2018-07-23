namespace Apex.Instagram.Request.Model
{
    internal class PostString : IPostValue
    {
        private readonly string _value;

        public PostString(string value) { _value = value; }

        public PostString(bool value) : this(value ? "true" : "false") { }

        public PostString(int value) : this(value.ToString()) { }

        public string Serialize() { return $"\"{_value}\""; }

        public override string ToString() { return _value; }
    }
}